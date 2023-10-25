using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public AudioSource openMusic;
    private int sceneSelect;
    [SerializeField] private Scene startScene;
    [SerializeField] private Scene levelOne;
    [SerializeField] private Scene levelTwo;

    void Awake() {
        startScene = SceneManager.GetSceneByBuildIndex(0);
        levelOne = SceneManager.GetSceneByBuildIndex(1);
        levelTwo = SceneManager.GetSceneByBuildIndex(2);
    }

    public void LoadScene(int scene) {
        SceneManager.LoadScene(scene);
    }

}
