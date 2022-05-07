using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance { get; private set; }

    [Header("Components")]

    [HideInInspector] public static Transform transformPlayer;
    [HideInInspector] public static Rigidbody2D rbPlayer;
    [HideInInspector] public static SpriteRenderer srPlayer;
    //[HideInInspector] public static Animator animatorPlayer;

    [Header("Combat Stats")]

    [SerializeField] private float _maxHealthPoints;
    private static float _healthPoints;

    [Header("Upgrades")]

    // Later, make them all [HideInInspector]
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

    private void Start() {
        _healthPoints = _maxHealthPoints;
    }

    public void TakeDamage(float damage, Vector3 knockBack) {
        _healthPoints -= damage;
        if (_healthPoints < 0) {
            // Player death anim/restart
        }
        else {
            // Remember to stop other velocity scripts
            rbPlayer.velocity = knockBack;
        }
    }

    // Evitar colliders, desligar objetos distantes
}
