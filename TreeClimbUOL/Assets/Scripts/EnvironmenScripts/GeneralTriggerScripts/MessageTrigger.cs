using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    //For closing the message after a period of time
    static private Coroutine closeMessage = null;
    static readonly List<string> messages = new List<string>()
    {
        "Use the a and d keys or the left and right arrows keys to move.",
        "Press s or down on a platform to fall through it.",
        "Press spacebar to jump. You can move while jumping",
        "The cars and the mistletoe will harm you on contact. Avoid them!",
        "Throw snowballs using n. Use them to defeat the snowman!",
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
            //If we all already message closing routine happening cancel that one
            if (closeMessage != null)
            {
                StopCoroutine(closeMessage);
                closeMessage = null;
            }
            //Start the message closing coroutine
            closeMessage = StartCoroutine(ShowMessage());
        }
    }

    //Shows the message for a limit amount of the time
    IEnumerator ShowMessage()
    {
        UIUpdater.UpdateHelpMessage(messages[index]);
        yield return new WaitForSeconds(3.0f);
        UIUpdater.UpdateHelpMessage("");

    }
}
