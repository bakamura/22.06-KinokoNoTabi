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
    public bool doubleJumpUpgrade;
    public bool healthPointsUpgrade;
    public bool poisonBlowUpgrade;

    public SaveData(PlayerData playerData) {


        doubleJumpUpgrade = playerData.doubleJumpUpgrade;
        healthPointsUpgrade = playerData.healthPointsUpgrade;
        poisonBlowUpgrade = playerData.poisonBlowUpgrade;
    }
}
