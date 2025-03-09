using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject textObject;
    public AudioClip collectSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    void OnMouseDown()
    {
        Debug.Log("Collectable Clicked: " + gameObject.name);

        // Play sound
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        // Update collectable count
        Scene2Manager.instance.Collect();

        // Destroy text and collectable
        if (textObject != null)
        {
            Destroy(textObject);
        }
        
        Destroy(gameObject);
    }
}
