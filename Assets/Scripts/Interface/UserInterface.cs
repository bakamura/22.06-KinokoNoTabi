using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
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
        SceneManager.LoadScene(1);
    }

    public static void ActivateCanvas(CanvasGroup canvas, bool isActive) {
        canvas.alpha = isActive ? 1 : 0;
        canvas.interactable = isActive;
        canvas.blocksRaycasts = isActive;
    }
}

[System.Serializable]
public class SliderData {
    public string name;
    public Slider slider;
    public TextMeshProUGUI valueText;
}