using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public static MainMenu Instance { get; private set; }

    [Header("Main")]

    [SerializeField] private CanvasGroup _mainCanvas;

    [Header("Saves")]

    [SerializeField] private CanvasGroup _savesCanvas;

    [Header("Settings")]

    [SerializeField] private CanvasGroup _settingsCanvas;

    [Header("Quit")]

    [SerializeField] private CanvasGroup _quitCanvas;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        OpenMenu(_mainCanvas);
    }

    private void Update() {
        
    }

    private void FixedUpdate() {
        
    }

    public void OpenMenu(CanvasGroup menuToOpen) {
        UserInterface.ActivateCanvas(_mainCanvas, menuToOpen == _mainCanvas);
        UserInterface.ActivateCanvas(_savesCanvas, menuToOpen == _savesCanvas);
        UserInterface.ActivateCanvas(_settingsCanvas, menuToOpen == _settingsCanvas);
        UserInterface.ActivateCanvas(_quitCanvas, menuToOpen == _quitCanvas);
    }

    public void QuitBtn() {
        Application.Quit();
    }
}
