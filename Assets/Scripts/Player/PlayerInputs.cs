using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour {

    public static PlayerInputs Instance { get; private set; }

    [Header("Outputs")]

    [HideInInspector] public static float horizontalAxis = 0;
    [HideInInspector] public static bool jumpKeyPressed = false;
    [HideInInspector] public static bool shotAttackKeyPressed = false;
    [HideInInspector] public static bool streamAttackKeyPressed = false;

    [Header("Inputs")]

    [System.NonSerialized] public static bool canInput = true;
    private KeyCode _leftKey;
    private KeyCode _rightKey;
    private KeyCode _jumpKey;
    private KeyCode _shotAttackKey;
    private KeyCode _streamAttackKey;
    public InputType[] inputTypes;
    public InputPreset selectedPreset;
    // TO DO: Make compatible with mobile / joystick (maybe another script will be necessary)

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        InputType selectedType = GetInputType(selectedPreset);
        _leftKey = selectedType.leftKey;
        _rightKey = selectedType.rightKey;
        _jumpKey = selectedType.jumpKey;
        _shotAttackKey = selectedType.shotAttackKey;
        _streamAttackKey = selectedType.streamAttackKey;
    }

    private void Update() {
        if (canInput) {
            horizontalAxis = (Input.GetKey(_leftKey) ? -1 : 0) + (Input.GetKey(_rightKey) ? 1 : 0);
            // Think on how to store input for a few milliseconds to provide smoother gameplay
            if (Input.GetKeyDown(_jumpKey)) jumpKeyPressed = true;
            if (Input.GetKeyDown(_shotAttackKey)) shotAttackKeyPressed = true;
            if (Input.GetKeyDown(_streamAttackKey)) streamAttackKeyPressed = true;
        }
    }

    private InputType GetInputType(InputPreset preset) {
        switch (preset) {
            case InputPreset.ArrowMovement:  return inputTypes[0];
            case InputPreset.WasdMovement: return inputTypes[1];
            case InputPreset.JoyStick: return inputTypes[2];
            default:
                Debug.LogError("Non existent input preset received, activated PC arrow movement instead.");
                return inputTypes[0];
        }
    }
}
