using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene2Manager : MonoBehaviour
{
    public static Scene2Manager instance;

    public Text collectableText; 
    public GameObject popupPanel;
    public Text popupText;
    public Button backButton; 

    private int collectableCount = 0;
    private int totalCollectables = 10; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Hide pop-up/totalCollectables Ui & counter
        popupPanel.SetActive(false); 
        UpdateCollectableText();
    }

    public void Collect()
    {
        collectableCount++;
        UpdateCollectableText();

        if (collectableCount >= totalCollectables)
        {
            ShowLevelComplete();
        }
    }

    void UpdateCollectableText()
    {
        collectableText.text = "Collected: " + collectableCount + " / " + totalCollectables;
    }

    void ShowLevelComplete()
    {
        popupPanel.SetActive(true);
        popupText.text = "Level 1 Completed!";
        backButton.onClick.AddListener(ReturnToScene1);

        PlayerPrefs.SetInt("Level1Completed", 1); 
        PlayerPrefs.Save();
    }


    void ReturnToScene1()
    {
        SceneManager.LoadScene(0); // Reload Scene 1
        SceneManager.UnloadSceneAsync(1); // Unload Scene 2
    }
    
}
