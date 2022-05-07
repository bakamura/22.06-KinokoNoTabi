using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour {

    [Header("Instance Reusing")]

    private bool _isActive = false;

    [Header("Components")]

    [HideInInspector] public Rigidbody2D rbEnemy;
    [HideInInspector] public SpriteRenderer srEnemy;
    [HideInInspector] public Animator animatorEnemy; // REMAKE

    [Header("Stats")]

    [SerializeField] private float _maxHealthPoints;
    private float _healthPoints;

    private void Awake() {
        rbEnemy = GetComponent<Rigidbody2D>();
        animatorEnemy = GetComponent<Animator>();
        srEnemy = GetComponent<SpriteRenderer>();
    }

    // Instance reusing
    public bool Activate(bool isActivating) {
        if (_isActive && isActivating) return false;
        _isActive = isActivating;
        rbEnemy.simulated = isActivating;
        srEnemy.enabled = isActivating;
        if (isActivating) {
            _healthPoints = _maxHealthPoints;
            // Set position
        }
        return true;
    }

    public void TakeDamage(float damage, Vector3 knockBack) {
        _healthPoints -= damage;
        if (_healthPoints < 0) {
            // Include delay for death animation
            Activate(false);
        }
        else {
            // Remember to stop other velocity scripts
            rbEnemy.velocity = knockBack;

            // Make knockback stop after X seconds
        }
    }

}
