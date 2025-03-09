using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    public GameObject scene1Controls;
    public GameObject scene2Controls;

    void Start()
    {
        if (SceneLoader.CurrentScene() == "Scene1")
        {
            scene1Controls.SetActive(true);
            scene2Controls.SetActive(false);
        }
        else if (SceneLoader.CurrentScene() == "Scene2")
        {
            scene1Controls.SetActive(false);
            scene2Controls.SetActive(true);
        }
    }
}
