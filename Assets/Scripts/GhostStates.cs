using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStates : MonoBehaviour {

    [SerializeField] private Animator animator;
    public enum State { normal, scared, recover, eaten }
    public State currentState;

    void Start() {
        animator = GetComponent<Animator>();
        currentState = State.normal;
        animator.SetBool("movingLeft", true);
    }

    void Update() {
        ScaredState();
        NormalState();
        RecoverState();
        EatenState();
    }

    void ScaredState() {
        if (currentState == State.scared) {
            animator.SetBool("isNormal", false);
            animator.SetBool("isScared", true);
            animator.SetBool("isRecover", false);
            animator.SetBool("isEaten", false);
        }
    }

    void NormalState() {
        if (currentState == State.normal) {
            animator.SetBool("isNormal", true);
            animator.SetBool("isScared", false);
            animator.SetBool("isRecover", false);
            animator.SetBool("isEaten", false);
        }
    }

    void RecoverState() {
        if (currentState == State.recover) {
            animator.SetBool("isNormal", false);
            animator.SetBool("isScared", false);
            animator.SetBool("isRecover", true);
            animator.SetBool("isEaten", false);
        }
    }

    void EatenState() {
        if (currentState == State.eaten) {
            animator.SetBool("isNormal", false);
            animator.SetBool("isScared", false);
            animator.SetBool("isRecover", false);
            animator.SetBool("isEaten", true);
        }
    }

}
