using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
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
    [SerializeField] private SliderData _masterVol;
    [SerializeField] private SliderData _musicVol;
    [SerializeField] private SliderData _sfxVol;

    [Header("Audio")]
    [SerializeField] private AudioMixer _mixer;
    private float _currentSliderValue;

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

    public void SetVolume(int volToChange) {
        SliderData slider = GetSliderVol(volToChange);
        _mixer.SetFloat(slider.name, Mathf.Log10(_currentSliderValue) * 20); // Slider lowest must be 0.001 !!!
        GetManagerVol(volToChange) = Mathf.Round(slider.slider.value * 1000f) / 1000f;
        slider.valueText.text = (slider.slider.value * 100f).ToString("F0");
    }

    private ref float GetManagerVol(int volVarID) {
        switch (volVarID) {
            case 0: return ref GameManager.MasterVol;
            case 1: return ref GameManager.MusicVol;
            case 2: return ref GameManager.SfxVol;
            default:
                Debug.Log("Error Fetching Volume Var From Manager, used MasterVol Instead");
                return ref GameManager.MasterVol;
        }
    }

    private SliderData GetSliderVol(int volVarID) {
        switch (volVarID) {
            case 0: return _masterVol;
            case 1: return _musicVol;
            case 2: return _sfxVol;
            default:
                Debug.Log("Error Fetching Volume Slider Value, used MasterVolSlider Instead");
                return _masterVol;
        }
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