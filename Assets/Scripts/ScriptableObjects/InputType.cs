using UnityEngine;

[CreateAssetMenu(menuName = "InputPreset")]
public class InputType : ScriptableObject {

    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;
    public KeyCode shotAttackKey;
    public KeyCode streamAttackKey;

}

// Remove?
public enum InputPreset {

    ArrowMovement,
    WasdMovement,
    Personalized

};
