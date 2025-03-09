using UnityEngine;
using UnityEngine.SceneManagement;

public class SantaSleighController : MonoBehaviour
{
    private Animator animator;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0.4f; 
        animator.Play("SantaSleighAnim"); // Play animation only when the scene is active
    }
}
