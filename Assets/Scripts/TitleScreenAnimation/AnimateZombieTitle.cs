using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimateZombieTitle : MonoBehaviour {
    private Canvas animCanvas;
    private Animator animator;
    private bool isMoving;

    void Start() {
        animCanvas = GetComponent<Canvas>();

        foreach (Transform child in animCanvas.transform) {
            animator = child.GetComponent<Animator>();
            if (animator.gameObject.name.Equals("TitleZombie")) { animator.SetBool("movingLeft", true); }
            else if (animator.gameObject.name.Contains("Hunter")) { animator.SetBool("isNormal", true) ; animator.SetBool("movingLeft", true); }
        }
        // animator = GetComponent<Animator>();
        // animator.SetBool("movingLeft", isMoving);
    }

}
