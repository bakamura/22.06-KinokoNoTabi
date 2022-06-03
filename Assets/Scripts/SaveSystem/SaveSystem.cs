using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SaveProgress(int saveFile) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Progress" + saveFile.ToString() + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData();

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static SaveData LoadProgress(int saveFile, bool loadToManager = true) {
        string path = Application.persistentDataPath + "/Progress" + saveFile.ToString() +".data";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            if(!loadToManager) return data;

            GameManager.currentSave = saveFile;
            // World
            GameManager.world1Unlocked = data.world1Unlocked;
            GameManager.world2Unlocked = data.world2Unlocked;
            GameManager.world3Unlocked = data.world3Unlocked;
            GameManager.world4Unlocked = data.world4Unlocked;
            GameManager.world5Unlocked = data.world5Unlocked;

            GameManager.world1Cleared = data.world1Cleared;
            GameManager.world2Cleared = data.world2Cleared;
            GameManager.world3Cleared = data.world3Cleared;
            GameManager.world4Cleared = data.world4Cleared;
            GameManager.world5Cleared = data.world5Cleared;

            GameManager.levelPosition = data.levelPosition; // On load, set map position

            // Player
            GameManager.doubleJumpUpgrade = data.doubleJumpUpgrade;
            GameManager.healthPointsUpgrade = data.healthPointsUpgrade;
            GameManager.poisonBlowUpgrade = data.poisonBlowUpgrade;

            // Settings
            GameManager.MasterVol = data.MasterVol; // On load, set slider values
            GameManager.MusicVol = data.MusicVol;
            GameManager.SfxVol = data.SfxVol;

            stream.Close();
        }
        else Debug.Log("Save file could'nt be found in" + path);
        return null;
    }

    public static void EraseProgress(int saveFile) {
        string path = Application.persistentDataPath + "/Progress" + saveFile.ToString() + ".data";
        if (File.Exists(path)) File.Delete(path);
        else Debug.Log("Couldn't find anything in" + path);
    }
}
