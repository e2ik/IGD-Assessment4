using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHealth : MonoBehaviour {

    public int ghostHealth = 100;
    public int currentHealth;
    [SerializeField] private GhostStates gs;

    void Start() {
        currentHealth = ghostHealth;  
        gs = GetComponent<GhostStates>();      
    }

    void Update() {
        if (currentHealth == 0) {
            StartCoroutine(nameof(Deaded));
        }

    }

    IEnumerator Deaded() {
        gs.currentState = GhostStates.State.eaten;
        yield return new WaitForSeconds(3f);
        gs.currentState = GhostStates.State.normal;
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.name.Contains("ghostHome")) {
            currentHealth = ghostHealth;
        }
    }

}
