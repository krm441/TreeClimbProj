using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Collider triggerCollider;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Level1Completed", 0) == 1)
        {
            DisableTrigger();
        }

        HideScene2UI(); // Hide Scene2 UI when in Scene1
    }

    private void HideScene2UI()
    {
        GameObject scene2UI = GameObject.FindWithTag("Scene2UI");
        if (scene2UI != null)
        {
            scene2UI.SetActive(false);
            Debug.Log("Scene2 UI hidden in Scene1");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && triggerCollider.enabled)
        {
            SceneManager.LoadScene(1); // Load Scene2
            StartCoroutine(ActivateScene2Camera());
        }
    }

    private IEnumerator ActivateScene2Camera()
    {
        yield return new WaitForSeconds(1f);

        GameObject scene2Camera = GameObject.FindWithTag("Camera2");
        if (scene2Camera != null)
        {
            scene2Camera.GetComponent<Scene2Camera>().ActivateCamera();
            Debug.Log("Scene 2 Camera Activated!");
        }
        else
        {
            Debug.LogWarning("Scene 2 Camera not found!");
        }
    }

    public void DisableTrigger()
    {
        if (triggerCollider != null)
        {
            triggerCollider.enabled = false;
            Debug.Log("Trigger disabled after Level 1 completion.");
        }
    }

    public void StartNewGame()
    {
        PlayerPrefs.DeleteKey("Level1Completed");
        PlayerPrefs.Save();

        triggerCollider.enabled = true; 
        Debug.Log("New game started. Trigger re-enabled.");

        SceneManager.LoadScene(0); // Restart game at Scene1
    }

    public static string CurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }
}
