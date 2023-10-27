using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour {

    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    private Tweener tweener;
    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask edibleLayer;
    [SerializeField] private string currentInput;
    [SerializeField] private string lastInput;
    private bool canRight = false;
    private bool canLeft = false;
    private bool canUp = false;
    private bool canDown = false;
    private Vector3 impactPos;
    public int score;
    [SerializeField] ParticleSystem ps;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        tweener = GetComponent<Tweener>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        currentInput = "Idle";
        ps.Stop();
    }

    void Update() {
        // ! Need a Way to Store the key press

        if (currentInput.Contains("Impact")) {
            ps.Stop();
        }

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

        if (!canRight && canLeft && !canUp && !canDown) { if (currentInput == "Right") { currentInput="ImpactRight"; impactPos = startPosition; }}
        if (!canLeft && canRight && !canUp && !canDown) { if (currentInput == "Left") { currentInput="ImpactLeft"; impactPos = startPosition; }}
        if (!canUp && canDown && !canLeft && !canRight) { if (currentInput == "Up") { currentInput="ImpactUp"; impactPos = startPosition; }}
        if (!canDown && canUp && !canLeft && !canRight) { if (currentInput == "Down") { currentInput="ImpactDown"; impactPos = startPosition; }}


        if (currentInput == "ImpactRight") {
            animator.SetBool("movingRight", true);
            endPosition = startPosition;
            endPosition += Vector3.right * 0.5f;
            float distanceToMove = Vector3.Distance(transform.position, endPosition);
            float animDuration = distanceToMove / moveSpeed;
            tweener.AddTween(transform, transform.position, endPosition, animDuration);
            startPosition = impactPos;
            TurnOffAnimParameters();
        }

        if (currentInput == "ImpactLeft") {
            animator.SetBool("movingLeft", true);
            endPosition = startPosition;
            endPosition += Vector3.left * 0.5f;
            float distanceToMove = Vector3.Distance(transform.position, endPosition);
            float animDuration = distanceToMove / moveSpeed;
            tweener.AddTween(transform, transform.position, endPosition, animDuration);
            startPosition = impactPos;
            TurnOffAnimParameters();
        }

       if (currentInput == "ImpactUp") {
            animator.SetBool("movingUp", true);
            endPosition = startPosition;
            endPosition += Vector3.up * 0.5f;
            float distanceToMove = Vector3.Distance(transform.position, endPosition);
            float animDuration = distanceToMove / moveSpeed;
            tweener.AddTween(transform, transform.position, endPosition, animDuration);
            startPosition = impactPos;
            TurnOffAnimParameters();
        }

       if (currentInput == "ImpactDown") {
            animator.SetBool("movingDown", true);
            endPosition = startPosition;
            endPosition += Vector3.down * 0.5f;
            float distanceToMove = Vector3.Distance(transform.position, endPosition);
            float animDuration = distanceToMove / moveSpeed;
            tweener.AddTween(transform, transform.position, endPosition, animDuration);
            startPosition = impactPos;
            TurnOffAnimParameters();
        }

        if (currentInput == "BounceBack") {
            endPosition = impactPos;
            float distanceToMove = Vector3.Distance(transform.position, endPosition);
            float animDuration = distanceToMove / moveSpeed;
            tweener.AddTween(transform, transform.position, endPosition, animDuration);
            startPosition = impactPos;
            TurnOffAnimParameters();

        }


        if (Input.GetKeyDown(KeyCode.W)) {
            if (!currentInput.Contains("Impact") && currentInput != "BounceBack" || currentInput == "Idle") {
                currentInput = "Up";
                ps.Play();
                MoveUp();
            }
        }
        if (Input.GetKeyDown(KeyCode.S)) {  
            if (!currentInput.Contains("Impact") && currentInput != "BounceBack" || currentInput == "Idle") {
                currentInput = "Down";
                ps.Play();
                MoveDown();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.A)) {    
            if (!currentInput.Contains("Impact") && currentInput != "BounceBack" || currentInput == "Idle") {
                currentInput = "Left";
                ps.Play();
                MoveLeft();
            }
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            if (!currentInput.Contains("Impact") && currentInput != "BounceBack" || currentInput == "Idle") {
                currentInput = "Right";
                ps.Play();
                MoveRight();
            }
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
        if (!isMoving && !CheckCollision(Vector2.up)) {
            lastInput = "Up";
            animator.SetBool("movingUp", true);
            endPosition = startPosition;
            endPosition += Vector3.up * 1.0f;
            StartCoroutine(nameof(Move));

            if (CheckSound(Vector2.up)) {
                audioSource.clip = audioClips[1];
                audioSource.Play();
            } else {
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
        }
    }

    void MoveDown() {
        if (!isMoving && !CheckCollision(Vector2.down)) {
            lastInput = "Down";
            animator.SetBool("movingDown", true);
            endPosition = startPosition;
            endPosition += Vector3.down * 1.0f;
            StartCoroutine(nameof(Move));

            if (CheckSound(Vector2.down)) {
                audioSource.clip = audioClips[1];
                audioSource.Play();
            } else {
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
        }
    }

    void MoveLeft() {
        if (!isMoving && !CheckCollision(Vector2.left)) {
            lastInput = "Left";
            animator.SetBool("movingLeft", true);
            endPosition = startPosition;
            endPosition += Vector3.left * 1.0f;
            StartCoroutine(nameof(Move));

            if (CheckSound(Vector2.left)) {
                audioSource.clip = audioClips[1];
                audioSource.Play();
            } else {
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
        }
    }

    void MoveRight() {
        if (!isMoving && !CheckCollision(Vector2.right)) {
            lastInput = "Right";
            animator.SetBool("movingRight", true);
            endPosition = startPosition;
            endPosition += Vector3.right * 1.0f;
            StartCoroutine(nameof(Move));

            if (CheckSound(Vector2.right)) {
                audioSource.clip = audioClips[1];
                audioSource.Play();
            } else {
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
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

    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log(other.gameObject.name);
        string otherName = other.gameObject.name.ToLower();
        if (otherName.Contains("wall")) {
            Debug.Log("Wall Hit!");
            audioSource.clip = audioClips[2];
            audioSource.loop = false;
            audioSource.Play();
            currentInput = "BounceBack";
            Invoke(nameof(SetIdleText), 0.2f);
        }

        if (otherName.Contains("power")) {
            Destroy(other.gameObject);
            score += LevelManager.powerpellet;
        } else if (otherName.Contains("pellet")){
            Destroy (other.gameObject);
            score += LevelManager.pellet;
        }

        if (otherName.Contains("cherry")) {
            Destroy(other.gameObject);
            score += LevelManager.cherry;
        }
    }

    bool CheckCollision(Vector2 direction)
    {
        Vector2 startPos = transform.position;
        float rayDistance = 0.6f;
        RaycastHit2D hit = Physics2D.BoxCast(startPos, new Vector2(0.5f, 0.5f), 0f, direction, rayDistance, wallLayer);
        Debug.DrawRay(startPos, direction * rayDistance, Color.red);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    bool CheckSound(Vector2 direction) {
        Vector2 startPos = transform.position;
        float rayDistance = 0.6f;
        RaycastHit2D hit = Physics2D.BoxCast(startPos, new Vector2(0.5f, 0.5f), 0f, direction, rayDistance, edibleLayer);
        Debug.DrawRay(startPos, direction * rayDistance, Color.blue);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

}
