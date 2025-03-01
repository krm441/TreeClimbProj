using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Load Scene 2
            SceneManager.LoadScene(1);
            // SceneManager.LoadScene(1, LoadSceneMode.Additive);
            
            // Wait for Scene 2 to load and then activate its camera
            StartCoroutine(ActivateScene2Camera());

            // Debugging
            Debug.Log("Switched to: " + SceneManager.GetActiveScene().name);
        }
    }

    private IEnumerator ActivateScene2Camera()
    {
        yield return new WaitForSeconds(1f); // Small delay to ensure Scene 2 loads

        // Find Scene 2's camera and activate it
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

}
