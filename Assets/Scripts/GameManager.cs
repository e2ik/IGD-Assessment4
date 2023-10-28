using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

    private Button returnButton;
    public AudioSource openMusic;
    private int startSceneIndex = 0;
    private int levelOneIndex = 1;
    private int levelTwoIndex = 2;
    private bool isPlaying = false;
    public Text timerText;
    public TextMeshProUGUI highScoreTMP;
    public TextMeshProUGUI bestTimeTMP;


    void Start() {
        openMusic.Play();
        isPlaying = true;
        if (PlayerPrefs.HasKey("HighScore") && PlayerPrefs.HasKey("BestTime")) {
            highScoreTMP.text = PlayerPrefs.GetString("HighScore");
            bestTimeTMP.text = PlayerPrefs.GetString("BestTime");
        }   
    }

    public void LoadFirstLevel() {
        if (isPlaying) openMusic.Stop();
        if (!SceneManager.GetSceneByBuildIndex(levelOneIndex).isLoaded) {
            SceneManager.LoadScene(levelOneIndex);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        DontDestroyOnLoad(this);
    }

    public void LoadSecondLevel() {
        if (isPlaying) openMusic.Stop();
        if (!SceneManager.GetSceneByBuildIndex(levelTwoIndex).isLoaded) {
            SceneManager.LoadScene(levelTwoIndex);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        DontDestroyOnLoad(this);
    }

    public void LoadStartScene() {
        Destroy(this.gameObject);
        if (!isPlaying) { openMusic.Play(); isPlaying = true; }
        if (!SceneManager.GetSceneByBuildIndex(startSceneIndex).isLoaded) {
            SceneManager.LoadScene(startSceneIndex);
        }
    }

    public void QuitGame() { UnityEditor.EditorApplication.isPlaying = false; }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

        if (scene.buildIndex == 1 || scene.buildIndex == 2) {
            GameObject returnButtonObject = GameObject.FindGameObjectWithTag("returnButton");

            if (returnButtonObject != null) {
               returnButton = returnButtonObject.GetComponent<Button>();

                if (returnButton != null) { returnButton.onClick.AddListener(LoadStartScene); }
            }
        }
    }
}
