public static class GameManager {

    // Evitar colliders, desligar objetos distantes
    public static int currentSave = -1;

    // World
    public static bool[] world1Unlocked = { true, false, false, false, false };
    public static bool[] world2Unlocked = new bool[5];
    public static bool[] world3Unlocked = new bool[5];
    public static bool[] world4Unlocked = new bool[5];
    public static bool[] world5Unlocked = new bool[5];

    public static bool[] world1Cleared = new bool[5];
    public static bool[] world2Cleared = new bool[5];
    public static bool[] world3Cleared = new bool[5];
    public static bool[] world4Cleared = new bool[5];
    public static bool[] world5Cleared = new bool[5];

    public static int[] levelPosition = new int[2]; // On load, set map position

    // Player
    public static bool doubleJumpUpgrade = false;
    public static bool healthPointsUpgrade = false;
    //public static bool poisonBlowUpgrade = false;

    // Settings
    public static float MasterVol = 1f;
    public static float MusicVol = 0.5f;
    public static float SfxVol = 0.5f;

}
