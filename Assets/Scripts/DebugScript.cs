using UnityEngine;

public class DebugScript : MonoBehaviour {

    [SerializeField] private bool _save;
    [SerializeField] private bool _erase;

    public int currentSave = -1;

    // World
    public bool[] world1Unlocked = new bool[5];
    public bool[] world2Unlocked = new bool[5];
    public bool[] world3Unlocked = new bool[5];
    public bool[] world4Unlocked = new bool[5];
    public bool[] world5Unlocked = new bool[5];

    public bool[] world1Cleared = new bool[5];
    public bool[] world2Cleared = new bool[5];
    public bool[] world3Cleared = new bool[5];
    public bool[] world4Cleared = new bool[5];
    public bool[] world5Cleared = new bool[5];

    // Player
    public int[] levelPosition = new int[2]; // On load, set map position
    public bool doubleJumpUpgrade;
    public bool healthPointsUpgrade;
    public bool poisonBlowUpgrade;

    // Settings
    public float MasterVol; // On load, set slider values
    public float MusicVol;
    public float SfxVol;

    private void Start() {
        SaveSystem.LoadProgress(currentSave); // Debug
    }

    private void Update() {
        if (_save) {
            _save = false;
            // World
            GameManager.world1Unlocked = world1Unlocked;
            GameManager.world2Unlocked = world2Unlocked;
            GameManager.world3Unlocked = world3Unlocked;
            GameManager.world4Unlocked = world4Unlocked;
            GameManager.world5Unlocked = world5Unlocked;

            GameManager.world1Cleared = world1Cleared;
            GameManager.world2Cleared = world2Cleared;
            GameManager.world3Cleared = world3Cleared;
            GameManager.world4Cleared = world4Cleared;
            GameManager.world5Cleared = world5Cleared;

            // Player
            GameManager.levelPosition = levelPosition; // On load, set map position
            GameManager.doubleJumpUpgrade = doubleJumpUpgrade;
            GameManager.healthPointsUpgrade = healthPointsUpgrade;
            //GameManager.poisonBlowUpgrade = poisonBlowUpgrade;

            // Settings
            GameManager.MasterVol = MasterVol; // On load, set slider values
            GameManager.MusicVol = MusicVol;
            GameManager.SfxVol = SfxVol;

            SaveSystem.SaveProgress(currentSave);
        }
        if (_erase) {
            _erase = false;
            SaveSystem.EraseProgress(currentSave);
        }
    }

}
