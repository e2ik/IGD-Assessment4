using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour {
    public enum Mode { runAway, chase, roundTheWorld, random }
    public Mode currentMode;
    [SerializeField] private GhostsController controller;
    [SerializeField] Vector3 targetPos;

    void Start() {
    }

    void Update() {
        
        switch (currentMode) {
            case Mode.runAway:
                RunAway(); break;
            case Mode.chase:
                break;
            case Mode.roundTheWorld:
                break;
            case Mode.random:
                break;
        }
    }

    void RunAway() {
        GetTarget();
        controller.currentInput = "Left";

    }

    void GetTarget() {
        if (currentMode == Mode.runAway) {
            GameObject targetObj = GameObject.Find("Zombie");
            targetPos = targetObj.transform.position;

        }
    }
}