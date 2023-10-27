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
    [SerializeField] private bool isMoving = false;
    [SerializeField] private LayerMask wallLayer;

    void Start() {
        tweener = GetComponent<Tweener>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        currentDirection = MovementDirection.Idle;
    }

    void Update() {
        // ! Need a Way to Store the key press

        
        if (CheckCollision(Vector2.up)) { Debug.Log("Wall at up"); }
        if (CheckCollision(Vector2.down)) { Debug.Log("Wall at down"); }
        if (CheckCollision(Vector2.left)) { Debug.Log("Wall at left"); }
        if (CheckCollision(Vector2.right)) { Debug.Log("Wall at right"); }


        if (Input.GetKeyDown(KeyCode.W)) {
            lastDirection = MovementDirection.Up;
            if (!isMoving && !CheckCollision(Vector2.up)) { MoveUp(); }            
        }
        if (Input.GetKeyDown(KeyCode.S)) {            
            lastDirection = MovementDirection.Down;
            if (!isMoving && !CheckCollision(Vector2.down)) { MoveDown(); }
            
        }
        if (Input.GetKeyDown(KeyCode.A)) {            
            lastDirection = MovementDirection.Left;
            if (!isMoving && !CheckCollision(Vector2.left)) { MoveLeft(); }
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            lastDirection = MovementDirection.Right;
            if (!isMoving && !CheckCollision(Vector2.right)) { MoveRight(); }
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
        isMoving = true;
        float distanceToMove = Vector3.Distance(transform.position, endPosition);
        float animDuration = distanceToMove / moveSpeed;
        if (tweener != null) {
            tweener.AddTween(transform, transform.position, endPosition, animDuration);
        }
        startPosition = endPosition;
        yield return new WaitForSeconds(animDuration);
        TurnOffAnimParameters();
        isMoving = false;    

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
    }

    bool CheckCollision(Vector2 direction)
    {
        Vector2 startPos = transform.position;
        float rayDistance = 1.0f; // Adjust this distance as needed
        RaycastHit2D hit = Physics2D.BoxCast(startPos, new Vector2(0.9f, 0.9f), 0f, direction, rayDistance, wallLayer);
        Debug.DrawRay(startPos, direction * rayDistance, Color.red);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

}
