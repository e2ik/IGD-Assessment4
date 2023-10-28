using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour {

    public string timerText;
    public int timerSecond;
    public int timerMinute;
    private float startTime;
    public GameObject ghostTimerPanel;
    public float countdown = 10.0f;
    public TextMeshProUGUI timerTextTMP;
    public TextMeshProUGUI scoreTextTMP;
    public TextMeshProUGUI ghostTimerTMP;
    public static readonly int pellet = 10;
    public static readonly int powerpellet = 50;
    public static readonly int cherry = 100;
    public PacStudentController psc;
    public LayerMask edibleLayer;
    public int numOfPellets = 0;
    public GhostStates RedGhost;
    public GhostStates BlueGhost;
    public GhostStates PinkGhost;
    public GhostStates OrangeGhost;
    [SerializeField] private GhostStates[] allGhosts;
    public bool powerPelletEaten = false;

    void Start() {
        
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allGameObjects) {
            if (go.name.ToLower().Contains("pellet")) { numOfPellets++; }
        }
        startTime = Time.time;
        GameObject timer = GameObject.FindWithTag("GameTimer");
        if ( timer != null ) { timerTextTMP = timer.GetComponent<TextMeshProUGUI>(); }
        GameObject scoreObj = GameObject.FindWithTag("GameScore");
        if (scoreObj != null) { scoreTextTMP = scoreObj.GetComponent<TextMeshProUGUI>(); }
        GameObject ghostTimerObj = GameObject.FindWithTag("GhostTimer");
        if (ghostTimerObj != null) { ghostTimerTMP = ghostTimerObj.GetComponent<TextMeshProUGUI>(); }
    }

    void Update() {
        setTime();
        setScore();
        if (powerPelletEaten) { ghostTimerPanel.SetActive(true); StartScared(); } else {ghostTimerPanel.SetActive(false);}
        
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
                gs.currentState = GhostStates.State.normal;
                powerPelletEaten = false;
                ghostTimerPanel.SetActive(false);
            }

            else if (countdown <= 3) {
                gs.currentState = GhostStates.State.recover;
            }

            else if (countdown <= 10) {
                gs.currentState = GhostStates.State.scared;
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
