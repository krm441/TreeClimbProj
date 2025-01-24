using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    static readonly List<string> messages = new List<string>()
    { 
        "Use the wasd keys or the arrows keys to move.",
        "Press spacebar to jump.",
        "Press b to climb on a ladder. Use W ,S or the up and down keys to transverse the ladder.",
        "Press space on a ladder to jump off it."
    };

    private int index = 0;

    public int Index { get => index; set => index = value; }
    public ShowHelpText help;
    private void OnTriggerEnter(Collider other)
    {
        /*When the play a*/
        if (other.gameObject.name == "PlayerTemplate(Clone)")
        {
           // Debug.Log(messages[index]);
           help.UpdateText(messages[index]);
        }
    }
}
