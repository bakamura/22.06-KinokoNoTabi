using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorInterface : MonoBehaviour {

    [Header("Pause Menu")]

    [SerializeField] private CanvasGroup _pauseCanvas;
    [SerializeField] private KeyCode _pauseMenuKey;

    [Header("Settings Menu")]

    [SerializeField] private CanvasGroup _settingsCanvas;

    private void Start() {
        UserInterface.ActivateCanvas(_pauseCanvas, false);
        UserInterface.ActivateCanvas(_settingsCanvas, false);
    }

    private void Update() {
        if (Input.GetKeyDown(_pauseMenuKey)) {
            if (_pauseCanvas.interactable) {
                Time.timeScale = 1;
                UserInterface.ActivateCanvas(_pauseCanvas, false);
                UserInterface.ActivateCanvas(_settingsCanvas, false);
            }
            else if (SelectorPlayerData.Instance.delayToLoadLevel > 0){
                Time.timeScale = 0;
                UserInterface.ActivateCanvas(_pauseCanvas, true);
            }
        }
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        UserInterface.ActivateCanvas(_pauseCanvas, false);
        UserInterface.ActivateCanvas(_settingsCanvas, false);
    }

    public void OpenSettingsMenu() {
        UserInterface.ActivateCanvas(_settingsCanvas, !_settingsCanvas.interactable);
    }

    public void QuitLevelSelector() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
