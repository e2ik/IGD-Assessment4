using UnityEngine;
using UnityEngine.SceneManagement;

public class CenterCamera : MonoBehaviour
{
    private Vector3 setCamPos;
    private GameObject level;
    private LevelGenerator levelGenerator;
    private int numRows;
    private int numCols;
    private Camera cam;
    [SerializeField] private float padding;

    void Start()
    {
        cam = Camera.main;
        level = GameObject.Find("GenerateLevel");
        if (level == null) {
            if (SceneManager.GetActiveScene().buildIndex == 1) {
                level = GameObject.Find("Level01");
                numRows = 15;
                numCols = 15;
            } else if (SceneManager.GetActiveScene().buildIndex == 2) {
                level = GameObject.Find("Level02");             
            }
        }

        if (level != null) {
            levelGenerator = level.GetComponent<LevelGenerator>();

            if (levelGenerator != null) {
                numRows = levelGenerator.numRows;
                numCols = levelGenerator.numCols;
            }
        }

        if (level.name == "Level01") {
            Vector3 lvlPos = level.transform.position;
            float newX = (numCols + (numCols-1)) /2;
            float newY = (numRows + (numRows-2)) /2;
            setCamPos = new Vector3(lvlPos.x + newX, lvlPos.y - newY, lvlPos.z - 4);
            cam.transform.position = setCamPos;
            cam.orthographicSize = (float)(numRows + 1) + padding/10;
        } else if (level.name == "Level02") {
            GameObject zombie = GameObject.Find("Zombie");
            setCamPos = new Vector3(zombie.transform.position.x, zombie.transform.position.y, -4f);
            cam.transform.position = setCamPos;
            cam.orthographicSize = 5f;
        }
    }

    void Update() {
        if (level.name == "Level02") {
            GameObject zombie = GameObject.Find("Zombie");
            setCamPos = new Vector3(zombie.transform.position.x, zombie.transform.position.y, -4f);
            cam.transform.position = setCamPos;
        }
    }
}