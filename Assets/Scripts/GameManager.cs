using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public AudioSource openMusic;
    private int sceneSelect;
    private int startSceneIndex = 0;
    private int levelOneIndex = 1;
    private int levelTwoIndex = 2;
    [SerializeField] private GameObject levelOneObject;
    [SerializeField] private GameObject levelTwoObject;

    void Awake() {
        DontDestroyOnLoad(this);
    }

    void Start() {
        levelOneObject = GameObject.FindGameObjectWithTag("Level1");
        levelTwoObject = GameObject.FindGameObjectWithTag("Level2");
    }

    void Update() {
        if (SceneManager.GetSceneByBuildIndex(startSceneIndex).isLoaded) {
            if (!openMusic.isPlaying) { openMusic.Play(); }
        } else {
            if (openMusic.isPlaying) { openMusic.Stop(); }
        }
    }

    public void LoadFirstLevel() {

            if (!SceneManager.GetSceneByBuildIndex(levelOneIndex).isLoaded) {
                SceneManager.LoadScene(levelOneIndex);
            }

    }

    public void QuitGame() { UnityEditor.EditorApplication.isPlaying = false; }

    // public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

    //     if (scene.buildIndex == 1 || scene.buildIndex == 2 ) {
    //         GameObject quitButtonObject = GameObject.FindGameObjectWithTag("QuitButton");
    //         mainCam = Camera.main;


    //         if (quitButtonObject != null) {
    //             quitButton = quitButtonObject.GetComponent<Button>();

    //             if (quitButton != null) { quitButton.onClick.AddListener(QuitGame); }
    //         }
    //     }
    // }
}
