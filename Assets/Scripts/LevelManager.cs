using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour {

    public string timerText;
    private float startTime;
    public TextMeshProUGUI timerTextTMP;
    public TextMeshProUGUI scoreTextTMP;
    public static readonly int pellet = 10;
    public static readonly int powerpellet = 50;
    public PacStudentController psc;

    void Start() {
        startTime = Time.time;
        GameObject timer = GameObject.FindWithTag("GameTimer");
        if ( timer != null ) { timerTextTMP = timer.GetComponent<TextMeshProUGUI>(); }
        GameObject scoreObj = GameObject.FindWithTag("GameScore");
        if (scoreObj != null) { scoreTextTMP = scoreObj.GetComponent<TextMeshProUGUI>(); }
    }

    void Update() {

        setTime();
        setScore();
        
    }

    void setTime() {
        float elapsedTime = Time.time - startTime;
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);
        int milliseconds = (int)((elapsedTime * 100) % 100);
        timerText = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        timerTextTMP.text = timerText;
    }

    void setScore() {
        scoreTextTMP.text = psc.score.ToString();
    }
}
