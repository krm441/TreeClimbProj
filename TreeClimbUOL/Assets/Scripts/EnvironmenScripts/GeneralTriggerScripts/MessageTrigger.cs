using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    static readonly List<string> messages = new List<string>()
    { 
        "Use the wasd keys or the arrows keys to move.",   
        "Press s or down on a platform to fall through it.",
        "Press spacebar to jump. You can move while jump",
        "The cars and the mistletoe will harm you on contact. Avoid them!",
        "Press space on a ladder to jump off it.",
        "Throw snowballs using v. Use them to defeat the snowman!",
        "Throughout the level there are decorations hidden across the level. You can collect them by clicking on them."
    };

    //The index of the selected message
    public int index = 0;

    public int Index { get => index; set => index = value; }
    public OverlayUIScript UIUpdater;
    private void OnTriggerEnter(Collider other)
    {
        /*When the play a*/
        if (other.gameObject.name == "Player")
        {
            UIUpdater.UpdateHelpMessage(messages[index]);
        }
    }
}
