using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [Header("Main")]

    [SerializeField] private CanvasGroup _mainCanvas;

    [Header("Saves")]

    [SerializeField] private CanvasGroup _savesCanvas;
    [SerializeField] private Image[] _worldImages1;
    [SerializeField] private Image[] _worldImages2;
    [SerializeField] private Image[] _worldImages3;

    [Header("Settings")]

    [SerializeField] private CanvasGroup _settingsCanvas;

    [Header("Quit")]

    [SerializeField] private CanvasGroup _quitCanvas;

    private void Start() {
        SaveData save = SaveSystem.LoadProgress(0, false);
        if (save != null) {
            _worldImages1[0].color = save.world1Unlocked[0] ? Color.gray : Color.black;
            _worldImages1[1].color = save.world2Unlocked[0] ? Color.gray : Color.black;
            _worldImages1[2].color = save.world3Unlocked[0] ? Color.gray : Color.black;
            _worldImages1[3].color = save.world4Unlocked[0] ? Color.gray : Color.black;
            _worldImages1[4].color = save.world5Unlocked[0] ? Color.gray : Color.black;
        }
        save = SaveSystem.LoadProgress(1, false);
        if (save != null) {
            _worldImages2[0].color = save.world1Unlocked[0] ? Color.gray : Color.black;
            _worldImages2[1].color = save.world2Unlocked[0] ? Color.gray : Color.black;
            _worldImages2[2].color = save.world3Unlocked[0] ? Color.gray : Color.black;
            _worldImages2[3].color = save.world4Unlocked[0] ? Color.gray : Color.black;
            _worldImages2[4].color = save.world5Unlocked[0] ? Color.gray : Color.black;
        }
        save = SaveSystem.LoadProgress(2, false);
        if (save != null) {
            _worldImages3[0].color = save.world1Unlocked[0] ? Color.gray : Color.black;
            _worldImages3[1].color = save.world2Unlocked[0] ? Color.gray : Color.black;
            _worldImages3[2].color = save.world3Unlocked[0] ? Color.gray : Color.black;
            _worldImages3[3].color = save.world4Unlocked[0] ? Color.gray : Color.black;
            _worldImages3[4].color = save.world5Unlocked[0] ? Color.gray : Color.black;
        }

        OpenMenu(_mainCanvas);
    }

    public void OpenMenu(CanvasGroup menuToOpen) {
        UserInterface.ActivateCanvas(_mainCanvas, menuToOpen == _mainCanvas);
        UserInterface.ActivateCanvas(_savesCanvas, menuToOpen == _savesCanvas);
        UserInterface.ActivateCanvas(_settingsCanvas, menuToOpen == _settingsCanvas);
        UserInterface.ActivateCanvas(_quitCanvas, menuToOpen == _quitCanvas);
    }

    public void EnterSaveBtn(int saveID) {
        SaveSystem.LoadProgress(saveID);
        SceneManager.LoadScene(1);
    }

    public void QuitBtn() {
        Application.Quit();
    }
}
