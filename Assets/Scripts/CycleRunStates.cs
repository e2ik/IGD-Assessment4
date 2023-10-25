using UnityEngine;
using System.Collections;

public class CycleRunStates : MonoBehaviour
{
    private Animator animator;
    private int state1 = 0;
    private int state2 = 0;

    private float cycleTime = 2f;

    private void Start() {
        animator = GetComponent<Animator>();
        StartCoroutine(CycleDirection());
        
        foreach (AnimatorControllerParameter p in animator.parameters) {
            if (p.name == "isNormal") {
                StartCoroutine(CycleStates());
            }
        }
    }

    private IEnumerator CycleDirection() {
        while (true) {
            state1 = (state1 + 1) % 4;                     

            switch (state1) {
                case 0: animator.SetBool("movingRight", false); animator.SetBool("movingUp", true); break;
                case 1: animator.SetBool("movingUp", false); animator.SetBool("movingLeft", true); break;
                case 2: animator.SetBool("movingLeft", false); animator.SetBool("movingDown", true); break;
                case 3: animator.SetBool("movingDown", false); animator.SetBool("movingRight", true); break;
            }
            yield return new WaitForSeconds(cycleTime);
        }
    }

    private IEnumerator CycleStates() {
        while (true) {
            state2 = (state2 + 1) % 4;

            switch (state2) {
                case 0: animator.SetBool("isRecover", false); animator.SetBool("isEaten", true); break;
                case 1: animator.SetBool("isEaten", false); animator.SetBool("isNormal", true); break;
                case 2: animator.SetBool("isNormal", false); animator.SetBool("isScared", true); break;
                case 3: animator.SetBool("isScared", false); animator.SetBool("isRecover", true); break;
            }
            yield return new WaitForSeconds(cycleTime*4);
        }
    }

}
