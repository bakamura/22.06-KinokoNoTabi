using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {

    // World
    public bool[] world1Unlocked;
    public bool[] world1Cleared;

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
