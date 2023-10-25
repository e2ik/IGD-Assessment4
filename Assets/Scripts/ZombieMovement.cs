using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieMovement : MonoBehaviour {
    private Animator animator;
    private AudioSource audioSource;
    private Tweener tweener;
    private bool isMoving = false;

    private enum MovementDirection { Right, Down, Left, Up }
    private MovementDirection currentDirection = MovementDirection.Right;
    private Vector3 startPosition;
    [SerializeField]private float moveSpeed = 4.0f;

    void Start() {
        tweener = GetComponent<Tweener>();
        isMoving = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        startPosition = transform.position;
    }

    void Update() {
        moveZombie();
    }

    public void TurnOffAnimParameters() {
        AnimatorControllerParameter[] parameters = animator.parameters;
        foreach (AnimatorControllerParameter parameter in parameters) {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    animator.SetBool(parameter.name, false);
                }
        }
    }
    
    void moveZombie() {

        if (isMoving) {
            Vector3 endPosition = startPosition;
            TurnOffAnimParameters();

            switch (currentDirection) {
                case MovementDirection.Right:
                    animator.SetBool("movingRight", true);
                    endPosition += Vector3.right * 5.0f;
                    break;
                case MovementDirection.Down:
                    animator.SetBool("movingDown", true);
                    startPosition = endPosition;
                    endPosition += Vector3.down * 4.0f;
                    break;
                case MovementDirection.Left:
                    animator.SetBool("movingLeft", true);
                    endPosition += Vector3.left * 5.0f;
                    break;
                case MovementDirection.Up:
                    animator.SetBool("movingUp", true);
                    endPosition += Vector3.up * 4.0f;
                    break;
            }

            float distanceToMove = Vector3.Distance(transform.position, endPosition);

            // this is important for uniform movement speed
            // tweener only interpolates, 
            float animDuration = distanceToMove / moveSpeed;

            if (tweener != null) {
                tweener.AddTween(transform, transform.position, endPosition, animDuration);
            }

            if (distanceToMove <= 0.01) {
                startPosition = endPosition;
                currentDirection = (MovementDirection)(((int)currentDirection + 1) % 4);

                if (currentDirection == MovementDirection.Right) {
                    startPosition = transform.position;
                }
            }
        }
    }

    private IEnumerator PlayAudio() {
        yield return new WaitForSeconds(0.0f);
        if (audioSource != null) {
            audioSource.Play();
        }
    }

    public void StartMovement() {
        isMoving = true;
        StartCoroutine(PlayAudio());
        animator.SetBool("movingRight", true);
    }
}
