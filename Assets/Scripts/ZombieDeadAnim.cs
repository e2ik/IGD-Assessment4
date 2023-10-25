using UnityEngine;

public class ZombieDeadAnim : MonoBehaviour
{

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isDead", true);
    }
}
