using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance { get; private set; }

    [Header("Components")]

    [System.NonSerialized] public static Rigidbody2D rbPlayer;
    [System.NonSerialized] public static Animator animatorPlayer;
    [System.NonSerialized] public static SpriteRenderer srPlayer;
    [System.NonSerialized] public static Transform transformPlayer;

    [Header("Combat Stats")]

    public float maxHealthPoints;
    [System.NonSerialized] public static float healthPoints;

    [Header("Upgrades")]

    // Later, make them all [System.nonSerializable]
    public bool doubleJumpUpgrade = false;
    public bool healthPointsUpgrade = false;
    public bool poisonBlowUpgrade = false;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        srPlayer = GetComponent<SpriteRenderer>();
        transformPlayer = GetComponent<Transform>();
    }

    // IENumerator pesa
    // InvokeRepeating pra evitar codigo desnecessário no update
    // Evitar colliders, desligar objetos distantes

}
