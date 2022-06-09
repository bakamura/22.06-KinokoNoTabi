using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour {

    public static UserInterface Instance { get; private set; }

    [Header("UI")]

    [SerializeField] private CanvasGroup _uiCanvas;
    [SerializeField] private Image[] _healthImages;

    [Header("Pause Menu")]

    [SerializeField] private CanvasGroup _pauseCanvas;
    [SerializeField] private KeyCode _pauseMenuKey;

    [Header("Settings Menu")]

    [SerializeField] private CanvasGroup _settingsCanvas;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        ActivateCanvas(_uiCanvas, true);
        ActivateCanvas(_pauseCanvas, false);
    }

    private void Update() {
        if (Input.GetKeyDown(_pauseMenuKey)) {
            if (_uiCanvas.interactable) {
                Time.timeScale = 0;
                ActivateCanvas(_uiCanvas, false);
                ActivateCanvas(_pauseCanvas, true);
            }
            else {
                Time.timeScale = 1;
                ActivateCanvas(_uiCanvas, true);
                ActivateCanvas(_pauseCanvas, false);
                ActivateCanvas(_settingsCanvas, false);
            }
        }
    }

    public void SetHealthUI(int healthPoints) {
        for (int i = 0; i < _healthImages.Length; i++) _healthImages[i].enabled = i < healthPoints;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        ActivateCanvas(_uiCanvas, true);
        ActivateCanvas(_pauseCanvas, false);
        ActivateCanvas(_settingsCanvas, false);
    }

    public void OpenSettingsMenu() {
        ActivateCanvas(_settingsCanvas, !_settingsCanvas.interactable);
    }

    public void QuitLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public static void ActivateCanvas(CanvasGroup canvas, bool isActive) {
        canvas.alpha = isActive ? 1 : 0;
        canvas.interactable = isActive;
        canvas.blocksRaycasts = isActive;
    }

}