using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostHealth : MonoBehaviour {

    public int ghostHealth = 100;
    public int currentHealth;
    [SerializeField] private GhostStates gs;
    [SerializeField] private Image healthBar;

    void Start() {
        currentHealth = ghostHealth;  
        gs = GetComponent<GhostStates>();      
    }

    void Update() {
        if (currentHealth == 0) {
            StartCoroutine(nameof(Deaded));
        }

        if (healthBar != null) {
            float health = Mathf.Clamp(currentHealth, 0, ghostHealth) / (float)ghostHealth;
            healthBar.fillAmount = health;
            if (health == 1.0f) {
                healthBar.color = Color.green;
            } else if (health >= 0.6f) {
                healthBar.color = Color.yellow;
            } else if (health >= 0.3f) {
                healthBar.color = new Color(1.0f, 0.5f, 0.0f);
            } else {
                healthBar.color = Color.red;
            }
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

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name.Contains("Zombie") && gs.currentState == GhostStates.State.scared || gs.currentState == GhostStates.State.recover) {
            currentHealth = -1;
        }
        
    }

}
