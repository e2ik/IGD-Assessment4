using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalController : MonoBehaviour {

    [SerializeField] private GameObject projectileObj;
    private GameObject shotProjectile;
    [SerializeField] private int maxProjectiles = 5;
    [SerializeField] private int currentProjectiles;
    [SerializeField] private RawImage camFog;
    [SerializeField] LevelManager lvlMgr;
    [SerializeField] private int dashCount;
    [SerializeField] private GameObject dashOne;
    [SerializeField] private GameObject dashTwo;
    [SerializeField] private TextMeshProUGUI ammoCount;


    void Start() {
        currentProjectiles = maxProjectiles;
        
    }

    void Update() {
        if (!lvlMgr.PauseGame) { UpdateGame();}
        ammoCount.text = currentProjectiles.ToString();
        PacStudentController ps = GetComponent<PacStudentController>();
        dashCount = ps.currentDashCharge;
        if (dashCount == 2) {
            dashOne.SetActive(true);
            dashTwo.SetActive(true);
        } else if (dashCount == 1) {
            dashOne.SetActive(true);
            dashTwo.SetActive(false);
        } else {
            dashOne.SetActive(false);
            dashTwo.SetActive(false);
        }
    }

    void UpdateGame() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            PacStudentController ps = GetComponent<PacStudentController>();
            if (currentProjectiles > 0 && ps.currentInput != "Idle") ShootProjectile();
        }
        if (camFog != null) {
            Transform fogTransform = camFog.transform;
            switch (currentProjectiles) {
                case 0: fogTransform.localScale = new Vector3(0.85f,0.85f,1f); break;
                case 1: fogTransform.localScale = new Vector3(0.88f,0.88f,1f); break;
                case 2: fogTransform.localScale = new Vector3(0.9f,0.9f,1f); break;
                case 3: fogTransform.localScale = new Vector3(0.93f,0.93f,1f); break;
                case 4: fogTransform.localScale = new Vector3(1f,1f,1f); break;
                case 5: fogTransform.localScale = new Vector3(1.2f,1.2f,1f); break;
            }
        }
    }

    void ShootProjectile() {
        if (shotProjectile == null) {
            shotProjectile = Instantiate(projectileObj, transform.position, Quaternion.identity);
            StartCoroutine(DespawnProjectile(shotProjectile));
            currentProjectiles--;
        }
    }

    IEnumerator DespawnProjectile(GameObject projectile) {
        yield return new WaitForSeconds(1.5f);
        Destroy(projectile);
    }

    void OnCollisionEnter2D(Collision2D other) {
        string otherName = other.gameObject.name.ToLower();

        if (otherName.Contains("pellet")) {
            if (currentProjectiles < maxProjectiles) currentProjectiles++;
        }
    }

}
