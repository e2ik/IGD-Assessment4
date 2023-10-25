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

    void Awake() {
        DontDestroyOnLoad(this);
    }

    void Update() {
        if (SceneManager.GetSceneByBuildIndex(startSceneIndex).isLoaded) {
            if (!openMusic.isPlaying) { openMusic.Play(); }
        } else {
            if (openMusic.isPlaying) { openMusic.Stop(); }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!SceneManager.GetSceneByBuildIndex(levelOneIndex).isLoaded) {
                SceneManager.LoadScene(levelOneIndex);
            }
        }
    }
}
