using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHelpText : MonoBehaviour
{
    public static string textValue;
    public Text textElement;
    // Start is called before the first frame update
    void Start()
    {
        textElement.text = textValue;
    }

    // Update is called once per frame
    public void UpdateText(string str)
    {
        Debug.Log(textValue);
        textValue = str;
        Debug.Log(textValue);
    }

    public void Update()
    {
        textElement.text = textValue;
    }
}
