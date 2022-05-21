using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {

    // World
    public bool[] world1Unlocked;
    public bool[] world2Unlocked;
    public bool[] world3Unlocked;
    public bool[] world4Unlocked;
    public bool[] world5Unlocked;

    public bool[] world1Cleared;
    public bool[] world2Cleared;
    public bool[] world3Cleared;
    public bool[] world4Cleared;
    public bool[] world5Cleared;

    // Player
    public int[] levelPosition = new int[2]; // [0] level, [1] world
    public bool doubleJumpUpgrade;
    public bool healthPointsUpgrade;
    public bool poisonBlowUpgrade;

    // Settings
    public float MasterVol = 1f;
    public float MusicVol = 0.5f;
    public float SfxVol = 0.5f;

    public SaveData(PlayerData playerData) {
        // World 
        world1Unlocked = GameManager.world1Unlocked;
        world2Unlocked = GameManager.world2Unlocked;
        world3Unlocked = GameManager.world3Unlocked;
        world4Unlocked = GameManager.world4Unlocked;
        world5Unlocked = GameManager.world5Unlocked;

        world1Cleared = GameManager.world1Cleared;
        world2Cleared = GameManager.world2Cleared;
        world3Cleared = GameManager.world3Cleared;
        world4Cleared = GameManager.world4Cleared;
        world5Cleared = GameManager.world5Cleared;

        // Player
        doubleJumpUpgrade = GameManager.doubleJumpUpgrade;
        healthPointsUpgrade = GameManager.healthPointsUpgrade;
        poisonBlowUpgrade = GameManager.poisonBlowUpgrade;

        // Settings
        MasterVol = GameManager.MasterVol;
        MusicVol = GameManager.MusicVol;
        SfxVol = GameManager.SfxVol;
    }
}
