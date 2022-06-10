using UnityEngine;

[CreateAssetMenu(menuName = "AnimationI")]
public class AnimationI : ScriptableObject {

    public Sprite[] sprites;
    [Tooltip("Number of frames/second")]
    public float speed = 1;
    public bool repeat = false;
    public int repeatFrom = 0;

}