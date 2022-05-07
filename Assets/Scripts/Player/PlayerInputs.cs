using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour {

    public static PlayerInputs Instance { get; private set; }

    [Header("Outputs")]

    [HideInInspector] public static float horizontalAxis = 0;
    [HideInInspector] public static float jumpKeyPressed = 0;
    [HideInInspector] public static float shotAttackKeyPressed = 0;
    [HideInInspector] public static float streamAttackKeyPressed = 0;

    [Header("Inputs")]

    [SerializeField] private float _inputRememberTime = 0.2f;
    [HideInInspector] public static bool canInput = true;
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

            if (Input.GetKeyDown(_jumpKey)) jumpKeyPressed = _inputRememberTime;
            if (Input.GetKeyDown(_shotAttackKey)) shotAttackKeyPressed = _inputRememberTime;
            if (Input.GetKeyDown(_streamAttackKey)) streamAttackKeyPressed = _inputRememberTime;
        }

        jumpKeyPressed -= Time.deltaTime;
        shotAttackKeyPressed -= Time.deltaTime;
        streamAttackKeyPressed -= Time.deltaTime;
    }

    private InputType GetInputType(InputPreset preset) {
        switch (preset) {
            case InputPreset.ArrowMovement:  return inputTypes[0];
            case InputPreset.WasdMovement: return inputTypes[1];
            case InputPreset.Personalized: return inputTypes[2];
            default:
                Debug.LogError("Non existent input preset received, activated PC arrow movement instead.");
                return inputTypes[0];
        }
    }
}
