using UnityEngine;

public class CherryController : MonoBehaviour {
    public GameObject cherryObject;
    [SerializeField] private GameObject currentCherry;
    [SerializeField] private LevelManager levelMgr;
    private bool hasSpawned = false;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private float lerpDuration = 5.0f;
    [SerializeField] private Vector3 cameraMiddlePos;

    private float lerpStartTime;
    private bool isMoving = false;

    void Update() {
        int combinedTime = levelMgr.timerSecond + (levelMgr.timerMinute * 60);

        if (currentCherry == null) {
            hasSpawned = false;
        }

        if (levelMgr.timerSecond % 10 == 0 && combinedTime > 0 && !hasSpawned) {
            spawnPos = CalculateSpawn();
            currentCherry = Instantiate(cherryObject, spawnPos, Quaternion.identity);
            hasSpawned = true;
            lerpStartTime = Time.time;
            isMoving = true;
            cameraMiddlePos = CalculateMiddleOfScreen();
            targetPos = cameraMiddlePos - (spawnPos - cameraMiddlePos);
        }

        if (currentCherry != null) {
            if (isMoving) {
                float journeyTime = Time.time - lerpStartTime;
                float journeyFraction = journeyTime / lerpDuration;

                if (journeyFraction < 1.0f) {
                    currentCherry.transform.position = Vector3.Lerp(spawnPos, targetPos, journeyFraction);
                } else {
                    currentCherry.transform.position = targetPos;
                    Destroy(currentCherry);
                    hasSpawned = false;
                    isMoving = false;
                }
            }
        }
    }

    private Vector3 CalculateSpawn() {
        float cameraOrthoSize = Camera.main.orthographicSize;
        float cameraAspect = Camera.main.aspect;
        int randomSide = Random.Range(0, 4);
        float randomX = 0f;
        float randomY = 0f;
        cameraMiddlePos = CalculateMiddleOfScreen();

        switch (randomSide) {
            case 0: // Left
                randomX = -cameraAspect * cameraOrthoSize - 2f;
                randomY = Random.Range(-cameraOrthoSize, cameraOrthoSize);
                break;
            case 1: // Right
                randomX = cameraAspect * cameraOrthoSize + 2f;
                randomY = Random.Range(-cameraOrthoSize, cameraOrthoSize);
                break;
            case 2: // Top
                randomX = Random.Range(-cameraAspect * cameraOrthoSize, cameraAspect * cameraOrthoSize);
                randomY = cameraOrthoSize + 2f;
                break;
            case 3: // Bottom
                randomX = Random.Range(-cameraAspect * cameraOrthoSize, cameraAspect * cameraOrthoSize);
                randomY = -cameraOrthoSize - 2f;
                break;
        }
        spawnPos = new Vector3(randomX, randomY, 0f);
        // adjustments
        spawnPos += new Vector3(-1f, -4f, 0f);
        return spawnPos;
    }

    Vector3 CalculateMiddleOfScreen() {
        Camera mainCamera = Camera.main;

        if (mainCamera != null) {
            float middleX = mainCamera.transform.position.x;
            float middleY = mainCamera.transform.position.y;
            return new Vector3(middleX, middleY, 0f);
        }
        return Vector3.zero;
    }
}