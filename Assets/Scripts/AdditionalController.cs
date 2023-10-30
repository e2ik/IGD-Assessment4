using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalController : MonoBehaviour {

    [SerializeField] private GameObject projectileObj;
    private GameObject shotProjectile;
    [SerializeField] private int maxProjectiles = 5;
    [SerializeField] private int currentProjectiles;
    private Tweener tweener;

    void Start() {
        currentProjectiles = maxProjectiles;
        tweener = GetComponent<Tweener>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (currentProjectiles > 0) ShootProjectile();
        }
    }

    void ShootProjectile() {
        if (shotProjectile == null) {
            shotProjectile = Instantiate(projectileObj, transform.position, Quaternion.identity);
            currentProjectiles--;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        string otherName = other.gameObject.name.ToLower();

        if (otherName.Contains("pellet")) {
            if (currentProjectiles < maxProjectiles) currentProjectiles++;
        }
    }

}
