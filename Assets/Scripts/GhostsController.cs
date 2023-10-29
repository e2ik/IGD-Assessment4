using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostsController : MonoBehaviour {

    private Animator animator;
    private Tweener tweener;
    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField] private float slowSpeed = 2.0f;
    [SerializeField] private float fastSpeed = 3.5f;
    private float moveSpeed;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask ghostWallLayer;
    private LayerMask originalGhostWallLayer;
    private LayerMask originalWallLayer;
    public string currentInput;
    public string lastInput;
    private bool canRight = false;
    private bool canLeft = false;
    private bool canUp = false;
    private bool canDown = false;
    private Vector3 impactPos;
    [SerializeField] private LevelManager lvlMgr;
    private GhostStates states;
    public enum Mode { runAway, chase, roundTheWorld, random, home }
    public Mode currentMode;
    [SerializeField] private Mode thisGhostMode;
    [SerializeField] Vector3 targetPos;
    private int roundCounter = 0;
    private Vector3 homePosition;
    private bool isHome = false;

    void Start() {
        moveSpeed = fastSpeed;
        originalGhostWallLayer = ghostWallLayer;
        originalWallLayer = wallLayer;
        tweener = GetComponent<Tweener>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        currentInput = "Idle";
        states = GetComponent<GhostStates>();
        homePosition = new Vector3(-2f,-4f, 0f);
    }

    void Update() {
        if (!lvlMgr.PauseGame) { UpdateGame(); }
    }

    void UpdateGame() {

        GhostStates.State currentState = states.currentState;
        if (currentState == GhostStates.State.scared || currentState == GhostStates.State.recover) {
            moveSpeed = slowSpeed;
            currentMode = Mode.runAway;
        } else if (currentState == GhostStates.State.normal) {
            currentMode = thisGhostMode;
            moveSpeed = fastSpeed;
        }

        if (currentState == GhostStates.State.eaten && !isHome) {
            currentMode = Mode.home;
            moveSpeed = 10f;
            wallLayer &= ~LayerMask.GetMask("Wall");
        } else { wallLayer = originalWallLayer; }

        switch (currentMode) {
            case Mode.runAway:
                RunAway(); break;
            case Mode.chase:
                Chase(); break;
            case Mode.roundTheWorld:
                RoundRound(); break;
            case Mode.random:
                MoveRandom(); break;
            case Mode.home:
                GoHome(); break;
        }

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
        if (other.gameObject.name.Contains("ghostHome")) {
            isHome = true;
            ghostWallLayer &= ~LayerMask.GetMask("GhostWall");

            // GhostStates.State currentState = states.currentState;
            // if (currentState == GhostStates.State.eaten) {
            //     StartCoroutine(nameof(returnToNormal));
            // }
        }
    }

    IEnumerator returnToNormal() {
        yield return new WaitForSeconds(1f);
        states.currentState = GhostStates.State.normal;
        tweener.KillAllTweens();
        if (transform.position != homePosition) { transform.position = homePosition; }
        startPosition = transform.position;
        transform.Translate(Vector3.zero);
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name.Contains("ghostHome")) {
            ghostWallLayer = originalGhostWallLayer;
            isHome = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.Contains("ghostHome")) {
            isHome = true;
        }
    }

    void Chase() {
        GetTarget();
        Vector3 direction = (targetPos - transform.position).normalized;
        bool moved = false;

        if (transform.position == targetPos) {
            MoveRandom();
            moved = true;
        }

        if (!moved && direction.x > 0 && lastInput != "Left") {  // Moving towards Right
            if (canRight) {
                currentInput = "Right";
                moved = true;
            } else if (direction.y > 0 && lastInput != "Down") {
                if (canUp) {
                    currentInput = "Up";
                    moved = true;
                } else if (canDown && lastInput != "Up") {
                    currentInput = "Down";
                    moved = true;
                }
            }
        }

        if (!moved && direction.x < 0 && lastInput != "Right") { // Moving towards Left
            if (canLeft) {
                currentInput = "Left";
                moved = true;
            } else if (direction.y > 0 && lastInput != "Down") {
                if (canUp) {
                    currentInput = "Up";
                    moved = true;
                } else if (canDown && lastInput != "Up") {
                    currentInput = "Down";
                    moved = true;
                }
            }
        }

        if (!moved && direction.y > 0 && lastInput != "Down") { // Moving towards Up
            if (canUp) {
                currentInput = "Up";
                moved = true;
            } else if (direction.x > 0 && lastInput != "Left") {
                if (canRight) {
                    currentInput = "Right";
                    moved = true;
                } else if (canLeft && lastInput != "Right") {
                    currentInput = "Left";
                    moved = true;
                }
            }
        }

        if (!moved && direction.y < 0 && lastInput != "Up") { // Moving towards Down
            if (canDown) {
                currentInput = "Down";
                moved = true;
            } else if (direction.x > 0 && lastInput != "Left") {
                if (canRight) {
                    currentInput = "Right";
                    moved = true;
                } else if (canLeft && lastInput != "Right") {
                    currentInput = "Left";
                    moved = true;
                }
            }
        }

        if (!moved) { MoveRandom(); }
    }

    void RunAway() {
        GetTarget();
        Vector3 direction = (targetPos - transform.position).normalized;

        if (transform.position == targetPos) {
            MoveRandom();
        }
        
        bool moved = false;

        if (direction.x > 0 && lastInput != "Right") {  // Moving towards Right
            if (canLeft) {
                currentInput = "Left";
                moved = true;
            } else if (direction.y > 0 && lastInput != "Up") {
                if (canDown) {
                    currentInput = "Down";
                    moved = true;
                } else if (canUp && lastInput != "Down") {
                    currentInput = "Up";
                    moved = true;
                }
            }
        }

        if (!moved && direction.x < 0 && lastInput != "Left") { // Moving towards Left
            if (canRight) {
                currentInput = "Right";
                moved = true;
            } else if (direction.y > 0 && lastInput != "Up") {
                if (canDown) {
                    currentInput = "Down";
                    moved = true;
                } else if (canUp && lastInput != "Down") {
                    currentInput = "Up";
                    moved = true;
                }
            }
        }

        if (!moved && direction.y > 0 && lastInput != "Up") { // Moving towards Up
            if (canDown) {
                currentInput = "Down";
                moved = true;
            } else if (direction.x > 0 && lastInput != "Right") {
                if (canLeft) {
                    currentInput = "Left";
                    moved = true;
                } else if (canRight && lastInput != "Left") {
                    currentInput = "Right";
                    moved = true;
                }
            }
        }

        if (!moved && direction.y < 0 && lastInput != "Down") { // Moving towards Down
            if (canUp) {
                currentInput = "Up";
                moved = true;
            } else if (direction.x > 0 && lastInput != "Right") {
                if (canLeft) {
                    currentInput = "Left";
                    moved = true;
                } else if (canRight && lastInput != "Left") {
                    currentInput = "Right";
                    moved = true;
                }
            }
        }

        if (!moved) { MoveRandom(); }
    }

    void MoveRandom() {

        List<string> availableDirections = new List<string>();

        if (lastInput != "Right" && canLeft) {
            availableDirections.Add("Left");
        }
        if (lastInput != "Left" && canRight) {
            availableDirections.Add("Right");
        }
        if (lastInput != "Up" && canDown) {
            availableDirections.Add("Down");
        }
        if (lastInput != "Down" && canUp) {
            availableDirections.Add("Up");
        }

        if (availableDirections.Count > 0) {
            int randomIndex = Random.Range(0, availableDirections.Count);
            currentInput = availableDirections[randomIndex];
        }

    }

    void RoundRound() {
        Chase();
        if (roundCounter > 7) { roundCounter = -1; }
        if (transform.position == targetPos) { roundCounter++; }
    }

    void GoHome() {    
        Chase();
        ghostWallLayer &= ~LayerMask.GetMask("GhostWall");
    }

    void GetTarget() {
        if (currentMode == Mode.runAway || currentMode == Mode.chase) {
            GameObject targetObj = GameObject.Find("Zombie");
            targetPos = targetObj.transform.position;
        } else if (currentMode == Mode.roundTheWorld) {
            switch (roundCounter) {
                case 0: targetPos = new Vector3(11f, -17f, 0f); break;
                case 1: targetPos = new Vector3(-3f, -17f, 0f); break;
                case 2: targetPos = new Vector3(-14f, -17f, 0f); break;
                case 3: targetPos = new Vector3(-14f, 2f, 0f); break;
                case 4: targetPos = new Vector3(-14f, 9f, 0f); break;
                case 5: targetPos = new Vector3(0f, 9f, 0f); break;
                case 6: targetPos = new Vector3(11f, 9f, 0f); break;
                case 7: targetPos = new Vector3(11f, -10f, 0f); break;
            }
        } else if (currentMode == Mode.home) { targetPos = homePosition; }
    }

}
