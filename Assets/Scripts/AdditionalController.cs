using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalController : MonoBehaviour {

    [SerializeField] private GameObject projectileObj;
    [SerializeField] private int maxProjectiles = 5;
    [SerializeField] private int currentProjectiles;

    void Start() {
        currentProjectiles = maxProjectiles;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (maxProjectiles > 0) ShootProjectile();
        }
    }

    void ShootProjectile() {
        currentProjectiles--;
    }

    void OnCollisionEnter2D(Collision2D other) {
        string otherName = other.gameObject.name.ToLower();

        if (otherName.Contains("pellet")) {
            if (currentProjectiles < maxProjectiles) currentProjectiles++;
        }
    }

}
