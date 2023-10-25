using UnityEngine;

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
            level = GameObject.Find("Level01");
            numRows = 15;
            numCols = 15;
        }

        if (level != null) {
            levelGenerator = level.GetComponent<LevelGenerator>();

            if (levelGenerator != null) {
                numRows = levelGenerator.numRows;
                numCols = levelGenerator.numCols;
            }
        }
        Vector3 lvlPos = level.transform.position;
        float newX = (numCols + (numCols-1)) /2;
        float newY = (numRows + (numRows-2)) /2;
        setCamPos = new Vector3(lvlPos.x + newX, lvlPos.y - newY, lvlPos.z - 4);
        cam.transform.position = setCamPos;
        cam.orthographicSize = (float)(numRows + 1) + padding/10;
    }
}