using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public string currentInput;
    public string lastInput;
    private bool canRight = false;
    private bool canLeft = false;
    private bool canUp = false;
    private bool canDown = false;
    private Vector3 impactPos;
    public int score;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private ParticleSystem wallps;
    [SerializeField] private ParticleSystem deadSplat;
    [SerializeField] private GameObject leftPort;
    [SerializeField] private GameObject rightPort;
    [SerializeField] private LevelManager lvlMgr;
    private float doubleTapTheshold = 0.2f;
    private float lastTapTime;
    private bool isDoubleTap = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        tweener = GetComponent<Tweener>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        currentInput = "Idle";
        ps.Stop();
    }

    void setInput() {
        currentInput = "Left";
        MoveLeft();
    }

    void Update() {
        if (!lvlMgr.PauseGame) { UpdateGame(); }
    }

    void UpdateControl() {

        if (Input.GetKeyDown(KeyCode.W)) {
            float currentTime = Time.time;
            if (currentTime - lastTapTime < doubleTapTheshold) {
                isDoubleTap = true;
                lastTapTime = 0f;
            } else {
                lastTapTime = currentTime;
            }
        }

        if (isDoubleTap) {
            StartCoroutine(nameof(SpeedUp));
        }
        isDoubleTap = false;
    }

    IEnumerator SpeedUp() {
        float storeSpeed = moveSpeed;
        moveSpeed = 7f;
        yield return new WaitForSeconds(0.6f);
        moveSpeed = storeSpeed;
    }

    void UpdateGame() {

        if (SceneManager.GetActiveScene().buildIndex == 2) {
            UpdateControl();
        }

        if (lvlMgr.numOfPellets == 0) { lvlMgr.PauseGame = true; StartCoroutine(nameof(GameOver)); }

        if (currentInput.Contains("Impact")) {
            ps.Stop();
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

    void OnCollisionEnter2D(Collision2D other) {
        // Debug.Log(other.gameObject.name);
        string otherName = other.gameObject.name.ToLower();
        if (otherName.Contains("wall")) {
            Vector3 collisionPoint = other.contacts[0].point;
            wallps.transform.position = collisionPoint;
            wallps.Play();
            audioSource.clip = audioClips[2];
            audioSource.loop = false;
            audioSource.Play();
            currentInput = "BounceBack";
            Invoke(nameof(SetIdleText), 0.2f);
        }

        if (otherName.Contains("power")) {
            Destroy(other.gameObject);
            score += LevelManager.powerpellet;
            lvlMgr.numOfPellets--;
            lvlMgr.countdown = 10.0f;
            lvlMgr.powerPelletEaten = true;
        } else if (otherName.Contains("pellet")) {
            Destroy (other.gameObject);
            score += LevelManager.pellet;
            lvlMgr.numOfPellets--;
        }

        if (otherName.Contains("cherry")) {
            Destroy(other.gameObject);
            audioSource.clip = audioClips[1];
            audioSource.Play();
            score += LevelManager.cherry;
            CherryController.isCherryEaten = true;
            Invoke(nameof(setCherryTime), 1.0f);
        }

        if (otherName.Contains("hunter")) {
   
            if (otherName.Contains("red")) {
                if (lvlMgr.RedGhost.currentState == GhostStates.State.scared || lvlMgr.RedGhost.currentState == GhostStates.State.recover) {
                    lvlMgr.RedGhost.currentState = GhostStates.State.eaten;
                    lvlMgr.hasEaten++;
                    score += LevelManager.ghost;
                } else if (lvlMgr.RedGhost.currentState == GhostStates.State.normal) {
                    lvlMgr.isRespawning = true;
                    ps.Stop();
                    deadSplat.Play();
                    lvlMgr.PauseGame = true;
                    currentInput = "Idle";
                    lastInput = "Idle";
                    tweener.KillAllTweens();
                    StartCoroutine(nameof(respawn));
                }
            }

            if (otherName.Contains("blue")) {
                if (lvlMgr.BlueGhost.currentState == GhostStates.State.scared || lvlMgr.BlueGhost.currentState == GhostStates.State.recover) {
                    lvlMgr.BlueGhost.currentState = GhostStates.State.eaten;
                    lvlMgr.hasEaten++;
                    score += LevelManager.ghost;
                } else if (lvlMgr.BlueGhost.currentState == GhostStates.State.normal) {
                    lvlMgr.isRespawning = true;
                    ps.Stop();
                    deadSplat.Play();
                    lvlMgr.PauseGame = true;
                    currentInput = "Idle";
                    lastInput = "Idle";
                    tweener.KillAllTweens();
                    StartCoroutine(nameof(respawn));
                }
            }

            if (otherName.Contains("pink")) {
                if (lvlMgr.PinkGhost.currentState == GhostStates.State.scared || lvlMgr.PinkGhost.currentState == GhostStates.State.recover) {
                    lvlMgr.PinkGhost.currentState = GhostStates.State.eaten;
                    lvlMgr.hasEaten++;
                    score += LevelManager.ghost;
                } else if (lvlMgr.PinkGhost.currentState == GhostStates.State.normal) {
                    lvlMgr.isRespawning = true;
                    ps.Stop();
                    deadSplat.Play();
                    lvlMgr.PauseGame = true;
                    currentInput = "Idle";
                    lastInput = "Idle";
                    tweener.KillAllTweens();
                    StartCoroutine(nameof(respawn));
                }
            }

            if (otherName.Contains("orange")) {
                if (lvlMgr.OrangeGhost.currentState == GhostStates.State.scared || lvlMgr.OrangeGhost.currentState == GhostStates.State.recover) {
                    lvlMgr.OrangeGhost.currentState = GhostStates.State.eaten;
                    lvlMgr.hasEaten++;
                    score += LevelManager.ghost;
                } else if (lvlMgr.OrangeGhost.currentState == GhostStates.State.normal) {
                    lvlMgr.isRespawning = true;
                    ps.Stop();
                    deadSplat.Play();
                    lvlMgr.PauseGame = true;
                    currentInput = "Idle";
                    lastInput = "Idle";
                    tweener.KillAllTweens();
                    StartCoroutine(nameof(respawn));
                }
            }
        }
    }

    IEnumerator respawn() {
        TurnOffAnimParameters();
        animator.SetBool("isDead", true);
        if (audioSource.isPlaying) {
            audioSource.Stop();
            audioSource.clip = audioClips[3];
            audioSource.Play();
        }
        yield return new WaitForSeconds(1f);
        lvlMgr.livesCount--;
        if (lvlMgr.livesCount == 1) {
            Destroy(GameObject.Find("LifeTwo"));
        }
        if (lvlMgr.livesCount == 0) {
            Destroy(GameObject.Find("LifeOne"));
        }
        if (lvlMgr.livesCount == -1) {
            StartCoroutine(nameof(GameOver));
            lvlMgr.PauseGame = true;
            lvlMgr.isRespawning = false;
            yield break;
        }
        animator.SetBool("isDead", false);
        animator.SetBool("isIdle", true);
        transform.position = new Vector3(-14f, 9f, 0f);
        transform.Translate(Vector3.zero);
        Vector3 currentPosition = transform.position;
        float roundedX = Mathf.Round(currentPosition.x);
        float roundedY = Mathf.Round(currentPosition.y);
        Vector3 roundedPosition = new Vector3(roundedX, roundedY, currentPosition.z);
        transform.position = roundedPosition;
        startPosition = transform.position;
        yield return new WaitForSeconds(1f);
        lvlMgr.PauseGame = false;
        tweener.KillAllTweens();
        lvlMgr.isRespawning = false;   
    }

    IEnumerator GameOver() {
        lvlMgr.currentScore = score.ToString();
        ps.Stop();
        lvlMgr.isGameOver = true;
        animator.SetBool("isIdle", true);
        lvlMgr.gameOverText.SetActive(true);
        yield return new WaitForSeconds(3f);
        lvlMgr.returnButton.onClick.Invoke();

    }

    void setCherryTime() {
        CherryController.isCherryEaten = false;
    }

    bool CheckCollision(Vector2 direction) {
        Vector2 startPos = transform.position;
        float rayDistance = 0.6f;
        RaycastHit2D hit = Physics2D.BoxCast(startPos, new Vector2(0.5f, 0.5f), 0f, direction, rayDistance, wallLayer);
        // Debug.DrawRay(startPos, direction * rayDistance, Color.red);

        if (hit.collider != null) { return true; }
        return false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "LeftTeleporterSet") {
            if (currentInput == "Left") { currentInput = "Up"; }
        } else if (other.gameObject.name ==  "RightTeleporterSet") {
            if (currentInput == "Right") { currentInput = "Up"; }
        }

        if (other.gameObject.name == "WallLeftTeleportStop") {
            rightPort.SetActive(false);
            currentInput = "Idle";
            lastInput = "Idle";
            tweener.KillAllTweens();
            CancelInvoke();
            Invoke(nameof(tpLeft), 0f);
        } else if (other.gameObject.name == "WallRightTeleportStop") {
            leftPort.SetActive(false);
            currentInput = "Idle";
            lastInput = "Idle";
            tweener.KillAllTweens();
            CancelInvoke();
            Invoke(nameof(tpRight), 0f);
        }
    }

    void tpLeft() {
        transform.Translate(Vector3.right*27f);
        startPosition = transform.position;
        tweener.KillAllTweens();
        currentInput = "Left";
        Invoke(nameof(activatePort), 0.5f);
    }

    void tpRight() {
        transform.Translate(Vector3.right*-27f);
        startPosition = transform.position;
        tweener.KillAllTweens();
        currentInput = "Right";
        Invoke(nameof(activatePort), 0.5f);
    }

    void activatePort() {
        rightPort.SetActive(true);
        leftPort.SetActive(true);
    }


    bool CheckSound(Vector2 direction) {
        Vector2 startPos = transform.position;
        float rayDistance = 0.6f;
        RaycastHit2D hit = Physics2D.BoxCast(startPos, new Vector2(0.5f, 0.5f), 0f, direction, rayDistance, edibleLayer);
        // Debug.DrawRay(startPos, direction * rayDistance, Color.blue);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

}
