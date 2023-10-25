using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour {

    private Animator animator;
    private AudioSource audioSource;
    private Tweener tweener;
    private enum MovementDirection { Idle, Right, Down, Left, Up }
    [SerializeField] private MovementDirection currentDirection;
    [SerializeField] private MovementDirection lastDirection;
    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField] private float moveSpeed = 4.0f;

    void Start() {
        tweener = GetComponent<Tweener>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        currentDirection = MovementDirection.Idle;
    }

    void Update() {
        // ! Need a Way to Store the key press

        if (Input.GetKeyDown(KeyCode.W)) {
            lastDirection = MovementDirection.Up;
            MoveUp();          
        }
        if (Input.GetKeyDown(KeyCode.S)) {            
            lastDirection = MovementDirection.Down;
            MoveDown();
            
        }
        if (Input.GetKeyDown(KeyCode.A)) {            
            lastDirection = MovementDirection.Left;
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            lastDirection = MovementDirection.Right;
            MoveRight();
        }
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

    void MoveUp() {
        currentDirection = MovementDirection.Up;
        animator.SetBool("movingUp", true);
        endPosition = startPosition;
        endPosition += Vector3.up * 1.0f;
        StartCoroutine(nameof(Move));  
    }

    void MoveDown() {
        currentDirection = MovementDirection.Down;
        animator.SetBool("movingDown", true);
        endPosition = startPosition;
        endPosition += Vector3.down * 1.0f;
        StartCoroutine(nameof(Move)); 
    }

    void MoveLeft() {
        currentDirection = MovementDirection.Left;
        animator.SetBool("movingLeft", true);
        endPosition = startPosition;
        endPosition += Vector3.left * 1.0f;
        StartCoroutine(nameof(Move)); 
    }

    void MoveRight() {
        currentDirection = MovementDirection.Right;
        animator.SetBool("movingRight", true);
        endPosition = startPosition;
        endPosition += Vector3.right * 1.0f;
        StartCoroutine(nameof(Move)); 
    }

    IEnumerator Move() {
        
        float distanceToMove = Vector3.Distance(transform.position, endPosition);
        float animDuration = distanceToMove / moveSpeed;
        if (tweener != null) {
            tweener.AddTween(transform, transform.position, endPosition, animDuration);
        }
        startPosition = endPosition;
        yield return new WaitForSeconds(animDuration);
        TurnOffAnimParameters();      

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
    }

}
