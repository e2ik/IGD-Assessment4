using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour {

    public int livesCount;
    public string timerText;
    public int timerSecond;
    public int timerMinute;
    private float startTime;
    public GameObject ghostTimerPanel;
    public GameObject gameOverText;
    public float countdown = 10.0f;
    public float deadtime = 5.0f;
    public TextMeshProUGUI timerTextTMP;
    public TextMeshProUGUI scoreTextTMP;
    public TextMeshProUGUI ghostTimerTMP;
    public TextMeshProUGUI levelStartTMP;
    public static readonly int pellet = 10;
    public static readonly int powerpellet = 50;
    public static readonly int cherry = 100;
    public static readonly int ghost = 300;
    public PacStudentController psc;
    public LayerMask edibleLayer;
    public int numOfPellets = 0;
    public GhostStates RedGhost;
    public GhostStates BlueGhost;
    public GhostStates PinkGhost;
    public GhostStates OrangeGhost;
    [SerializeField] private GhostStates[] allGhosts;
    public bool powerPelletEaten = false;
    public int hasEaten = 0;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    private bool hasScared = false;
    private bool isNormalPlaying = false;
    private bool isScaredPlaying = false;
    private bool isDeadPlaying = false;
    private bool isStartPlaying = false;
    public bool PauseGame = true;
    public bool isGameOver = false;
    public bool isRespawning = false;
    public Button returnButton;
    public string highScore;
    public string bestTime;
    public string currentScore;
    public string currentTime;
    public GhostStates eatenGhost;
    private List<GhostStates> eatenList = new List<GhostStates>();

    void Start() {
        
        audioSource = GetComponent<AudioSource>();
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allGameObjects) {
            if (go.name.ToLower().Contains("pellet")) { numOfPellets++; }
        }
        GameObject timer = GameObject.FindWithTag("GameTimer");
        if ( timer != null ) { timerTextTMP = timer.GetComponent<TextMeshProUGUI>(); }
        GameObject scoreObj = GameObject.FindWithTag("GameScore");
        if (scoreObj != null) { scoreTextTMP = scoreObj.GetComponent<TextMeshProUGUI>(); }
        GameObject ghostTimerObj = GameObject.FindWithTag("GhostTimer");
        if (ghostTimerObj != null) { ghostTimerTMP = ghostTimerObj.GetComponent<TextMeshProUGUI>(); }
        GameObject levelStartObj = GameObject.FindWithTag("LevelStart");
        if (levelStartObj != null) { levelStartTMP = levelStartObj.GetComponent<TextMeshProUGUI>(); }
    }

    IEnumerator LevelStart() {
        ghostTimerPanel.SetActive(false);
        audioSource.clip = audioClips[3];
        audioSource.loop = false;
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
        levelStartTMP.text = "3";
        yield return new WaitForSeconds(1f);
        levelStartTMP.text = "2";
        yield return new WaitForSeconds(1f);
        levelStartTMP.text = "1";
        yield return new WaitForSeconds(1f);
        levelStartTMP.text = "GO";
        GameObject lifeIconThree = GameObject.Find("LifeThree");
        Destroy(lifeIconThree);
        livesCount--;
        yield return new WaitForSeconds(1f);
        PauseGame = false;
        GameObject levelStartObj = GameObject.FindWithTag("LevelStart");
        if (levelStartObj != null) { levelStartObj.SetActive(false); }
        startTime = Time.time;
        
    }

    void Update() {
        if (isRespawning) { setTime(); }
        if (isGameOver) { SaveScore(); }
        if (PauseGame) {
            if (!isStartPlaying) {
                StartCoroutine(nameof(LevelStart));
                isStartPlaying = true;
            }
        } else if (!PauseGame) { RunGame(); }
    }

    void RunGame() {
        setTime();
        setScore();
        checkEaten();
        if (powerPelletEaten) { ghostTimerPanel.SetActive(true); StartScared(); } else {ghostTimerPanel.SetActive(false);}
        if (hasEaten > 0) {
            StartCoroutine(EatenTimer(eatenGhost));
        }
        checkGhosts();
        selectClip();  
    }

    void checkEaten() {
        foreach (GhostStates gs in allGhosts) {
            if (gs.currentState == GhostStates.State.eaten) {
                eatenGhost = gs;
            }
        }
    }

    void SaveScore() {
        currentTime = timerTextTMP.text;
        if (PlayerPrefs.HasKey("HighScore") && PlayerPrefs.HasKey("bestTime")) {
            highScore = PlayerPrefs.GetString("HighScore");
            bestTime = PlayerPrefs.GetString("BestTime");
            string newHighScore = CalculateHigherScore(currentScore, highScore);
            if (newHighScore == currentScore) { PlayerPrefs.SetString("HighScore", currentScore); PlayerPrefs.SetString("BestTime", currentTime); }
        } else {
            PlayerPrefs.SetString("HighScore", currentScore);
            PlayerPrefs.SetString("BestTime", currentTime);
        }
    }

    string CalculateHigherScore(string current, string saved) {
        int currentInt = int.Parse(current);
        int savedInt = int.Parse(saved);
        if (currentInt > savedInt) { return current; }
        else { return saved; }
        
    }

    void checkGhosts() {
        foreach (GhostStates gs in allGhosts) {
            if (gs.currentState == GhostStates.State.scared || gs.currentState == GhostStates.State.recover ) {
                hasScared = true;
                break;
            } else { hasScared = false; }
        }
    }

    void selectClip() {
        if (PauseGame) {
            audioSource.clip = audioClips[3];
            audioSource.loop = false;
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
            if (audioSource.isPlaying && (isNormalPlaying || isScaredPlaying || isDeadPlaying)) { audioSource.Stop(); }
            isNormalPlaying = false;
            isScaredPlaying = false;
            isDeadPlaying = false;

            return;
        }

        if (hasEaten > 0) {
            if (!isDeadPlaying) {
                audioSource.clip = audioClips[2];
                audioSource.loop = true;
                audioSource.Play();

                isNormalPlaying = false;
                isScaredPlaying = false;
                isDeadPlaying = true;
            }
        } else if (hasScared) {
            if (!isScaredPlaying) {
                audioSource.clip = audioClips[1];
                audioSource.loop = true;
                audioSource.Play();

                isNormalPlaying = false;
                isScaredPlaying = true;
                isDeadPlaying = false;
            }
        } else {
            if (!isNormalPlaying) {
                audioSource.clip = audioClips[0];
                audioSource.loop = true;
                audioSource.Play();

                isNormalPlaying = true;
                isScaredPlaying = false;
                isDeadPlaying = false;
            }
        }
    }

    IEnumerator EatenTimer(GhostStates ghost) {
        yield return new WaitForSeconds(deadtime);
        if (ghost.currentState == GhostStates.State.eaten) {
            ghost.currentState = GhostStates.State.normal;
            hasEaten--;
        }
    }

    void StartScared() {
        int countdownInt = Mathf.FloorToInt(countdown); 
        string countdownText = countdownInt.ToString();
        ghostTimerTMP.text = countdownText;

        if (countdown > 0) {
            countdown -= Time.deltaTime;
            countdown = Mathf.Max(0, countdown);
        }
        foreach (GhostStates gs in allGhosts) {
            if (countdown == 0) {
                if (gs.currentState != GhostStates.State.eaten) {
                    gs.currentState = GhostStates.State.normal;
                }
                powerPelletEaten = false;
                ghostTimerPanel.SetActive(false);
            }

            else if (countdown <= 3) {
                if (gs.currentState != GhostStates.State.eaten && gs.currentState != GhostStates.State.recover) {
                    gs.currentState = GhostStates.State.recover;
                }
            }

            else if (countdown <= 10) {
                if (gs.currentState != GhostStates.State.eaten && gs.currentState != GhostStates.State.scared) {
                    gs.currentState = GhostStates.State.scared;
                }
            }
        }

    }

    void setTime() {
        float elapsedTime = Time.time - startTime;
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);
        timerSecond = seconds;
        timerMinute = minutes;
        int milliseconds = (int)((elapsedTime * 100) % 100);
        timerText = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        timerTextTMP.text = timerText;
    }

    void setScore() {
        scoreTextTMP.text = psc.score.ToString();
    }


}
