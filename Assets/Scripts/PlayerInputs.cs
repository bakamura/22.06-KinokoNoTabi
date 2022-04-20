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
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;
    public KeyCode shotAttackKey;
    public KeyCode streamAttackKey;
    // TO DO: Change key codes for different platforms / left handed people (possibly an Enum ?)

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Update() {
        if (canInput) {
            horizontalAxis = (Input.GetKey(leftKey) ? -1 : 0) + (Input.GetKey(rightKey) ? 1 : 0);
            // Think on how to store input for a few milliseconds to provide smoother gameplay
            if (Input.GetKeyDown(jumpKey)) jumpKeyPressed = true;
            if (Input.GetKeyDown(shotAttackKey)) shotAttackKeyPressed = true;
            if (Input.GetKeyDown(streamAttackKey)) streamAttackKeyPressed = true;
        }
    }
}
