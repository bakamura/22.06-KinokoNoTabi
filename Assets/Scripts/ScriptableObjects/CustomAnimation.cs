using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomAnimation")]
public class CustomAnimation : ScriptableObject {

    public Sprite[] sprites;
    [Tooltip("Number of frames/second")]
    public float speed = 1;
    public bool repeat = false;
    public int repeatFrom = 0;

}