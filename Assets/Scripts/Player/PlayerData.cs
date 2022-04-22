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

    [SerializeField] private float _maxHealthPoints;
    private static float _healthPoints;

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
    // IENumerator pesa
    // InvokeRepeating pra evitar codigo desnecess�rio no update
    // Evitar colliders, desligar objetos distantes

}