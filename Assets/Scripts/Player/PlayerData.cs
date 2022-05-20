using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance { get; private set; }

    [Header("Components")]

    [HideInInspector] public static Transform transformPlayer;
    [HideInInspector] public static Rigidbody2D rbPlayer;
    [HideInInspector] public static SpriteRenderer srPlayer;

    [Header("Combat Stats")]

    [SerializeField] private float _maxHealthPoints;
    private static float _healthPoints;

    [SerializeField] private float _delayToRestart;

    [Header("Upgrades")]

    // Later, make them all [HideInInspector]
    public bool doubleJumpUpgrade;
    public bool healthPointsUpgrade;
    public bool poisonBlowUpgrade;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        rbPlayer = GetComponent<Rigidbody2D>();
        srPlayer = GetComponent<SpriteRenderer>();
        transformPlayer = GetComponent<Transform>();
    }

    private void Start() {
        SaveData data = SaveSystem.LoadProgress(GameManager.currentSave);

        doubleJumpUpgrade = data.doubleJumpUpgrade;
        healthPointsUpgrade = data.healthPointsUpgrade;
        poisonBlowUpgrade = data.poisonBlowUpgrade;
    
        _healthPoints = _maxHealthPoints;
    }

    public void TakeDamage(float damage, Vector3 knockBack) {
        _healthPoints -= damage;
        if (_healthPoints < 0) {
            PlayerAnimations.Instance.ChangeAnimation("PlayerDeath");
            Invoke(nameof(RestartScene), _delayToRestart);
        }
        else {
            // Remember to stop other velocity scripts
            rbPlayer.velocity = knockBack;
        }
    }

    private void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Evitar colliders, desligar objetos distantes
}
