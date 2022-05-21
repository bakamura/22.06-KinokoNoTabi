using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {

    // Evitar colliders, desligar objetos distantes
    // MAKE RECEIVE VALUES ON START
    public static int currentSave = -1;

    // World
    public static bool[] world1Unlocked;
    public static bool[] world2Unlocked;
    public static bool[] world3Unlocked;
    public static bool[] world4Unlocked;
    public static bool[] world5Unlocked;

    public static bool[] world1Cleared;
    public static bool[] world2Cleared;
    public static bool[] world3Cleared;
    public static bool[] world4Cleared;
    public static bool[] world5Cleared;

    public static int[] levelPosition = new int[2]; // On load, set map position

    // Player
    public static bool doubleJumpUpgrade;
    public static bool healthPointsUpgrade;
    public static bool poisonBlowUpgrade;

    // Settings
    public static float MasterVol; // On load, set slider values
    public static float MusicVol;
    public static float SfxVol;
}
