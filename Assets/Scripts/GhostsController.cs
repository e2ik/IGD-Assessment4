using System.Collections;
using UnityEngine;

public class GhostsController : MonoBehaviour {

    private Animator animator;
    private Tweener tweener;
    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask ghostWallLayer;
    [SerializeField] private string currentInput;
    [SerializeField] private string lastInput;
    private bool canRight = false;
    private bool canLeft = false;
    private bool canUp = false;
    private bool canDown = false;
    private Vector3 impactPos;
    [SerializeField] private LevelManager lvlMgr;

    void Start() {
        tweener = GetComponent<Tweener>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        currentInput = "Idle";
    }

    void Update() {
        if (!lvlMgr.PauseGame) { UpdateGame(); }
    }

    void UpdateGame() {

        if (currentInput == "Idle") {
            animator.SetBool("isIdle", true);
        } else { animator.SetBool("isIdle", false); }

        if (CheckCollision(Vector2.up)) { canUp = false; } else { canUp = true; }
        if (CheckCollision(Vector2.down)) { canDown = false; } else { canDown = true; }
        if (CheckCollision(Vector2.left)) { canLeft = false; } else { canLeft = true; }
        if (CheckCollision(Vector2.right)) { canRight = false; } else { canRight = true; }

        if (canDown && currentInput == "Down") { MoveDown(); }
        if (canUp && currentInput == "Up") { MoveUp(); }
        if (canLeft && currentInput == "Left") { MoveLeft(); }
        if (canRight && currentInput == "Right") { MoveRight(); }


        if (lastInput == "Right" && canRight) { MoveRight(); }
        if (lastInput == "Left" && canLeft) { MoveLeft(); }
        if (lastInput == "Down" && canDown) { MoveDown(); }
        if (lastInput == "Up" && canUp) { MoveUp(); }

        if (Input.GetKeyDown(KeyCode.W)) {
            currentInput = "Up";
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.S)) {  
            currentInput = "Down";
            MoveDown();
            
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            currentInput = "Left";
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            currentInput = "Right";
            MoveRight();
        }
    }

    public void TurnOffAnimParameters() {
        AnimatorControllerParameter[] parameters = animator.parameters;
        foreach (AnimatorControllerParameter parameter in parameters) {
            if (parameter.type == AnimatorControllerParameterType.Bool) {
                animator.SetBool(parameter.name, false);
            }
        }
    }

    void MoveUp() {
        if (!isMoving && !CheckCollision(Vector2.up)) {
            lastInput = "Up";
            animator.SetBool("movingUp", true);
            endPosition = startPosition;
            endPosition += Vector3.up * 1.0f;
            StartCoroutine(nameof(Move));
        }
    }

    void MoveDown() {
        if (!isMoving && !CheckCollision(Vector2.down)) {
            lastInput = "Down";
            animator.SetBool("movingDown", true);
            endPosition = startPosition;
            endPosition += Vector3.down * 1.0f;
            StartCoroutine(nameof(Move));
        }
    }

    void MoveLeft() {
        if (!isMoving && !CheckCollision(Vector2.left)) {
            lastInput = "Left";
            animator.SetBool("movingLeft", true);
            endPosition = startPosition;
            endPosition += Vector3.left * 1.0f;
            StartCoroutine(nameof(Move));
        }
    }

    void MoveRight() {
        if (!isMoving && !CheckCollision(Vector2.right)) {
            lastInput = "Right";
            animator.SetBool("movingRight", true);
            endPosition = startPosition;
            endPosition += Vector3.right * 1.0f;
            StartCoroutine(nameof(Move));
        }
        
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

    void SetIdleText() {
        currentInput = "Idle";
    }

    bool CheckCollision(Vector2 direction) {
        Vector2 startPos = transform.position;
        float rayDistance = 0.6f;
        LayerMask combinedLayers = wallLayer | ghostWallLayer;
        RaycastHit2D hit = Physics2D.BoxCast(startPos, new Vector2(0.5f, 0.5f), 0f, direction, rayDistance, combinedLayers);
        Debug.DrawRay(startPos, direction * rayDistance, Color.red);

        if (hit.collider != null) { return true; }
        return false;
    }

    void OnTriggerStay2D(Collider2D other) {
        Debug.Log("Inside Something!");
    }

}
