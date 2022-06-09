using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorPlayerData : MonoBehaviour {

    public static SelectorPlayerData Instance { get; private set; }

    [Header("Inputs")]

    [SerializeField] private KeyCode _enterLevelKey;

    [Header("Info")]

    [HideInInspector] public int sceneToLoad = 0;
    public float delayToLoadLevel;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        LevelEnterPoint[] enterPoints = (LevelEnterPoint[])FindObjectsOfType(typeof(LevelEnterPoint));
        for (int i = 0; i < enterPoints.Length; i++) {
            if (enterPoints[i].levelNumber[1] == GameManager.levelPosition[1] && enterPoints[i].levelNumber[0] == GameManager.levelPosition[0]) {
                transform.position = enterPoints[i].transform.position;
                Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                break;
            }
            //if (i == enterPoints.Length - 1) print("Invalid Level, Placed Player On Start");
        }
    }

    private void Update() {
        if (Input.GetKeyDown(_enterLevelKey) && sceneToLoad > 1) {
            Invoke(nameof(GoToLevel), delayToLoadLevel);
            delayToLoadLevel = -1;
        }
        if (delayToLoadLevel < 0) {
            // Create cool transition
            // Possibly by having a large sprite with a hole in the middle, shrinking to the player's pos on level enter
        }
    }

    private void GoToLevel() {
        SceneManager.LoadScene(sceneToLoad);
    }

}
