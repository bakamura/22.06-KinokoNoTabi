using UnityEngine;
using UnityEngine.Events;

public class EnemyData : MonoBehaviour {

    [Header("Instance Reusing")]

    private bool _isActive = false;

    [Header("Components")]

    [HideInInspector] public Rigidbody2D rbEnemy;
    [HideInInspector] public SpriteRenderer srEnemy;
    [HideInInspector] public UnityAction onActivate;
    [HideInInspector] public UnityAction onDeactivate;

    [Header("Stats")]

    [SerializeField] private float _maxHealthPoints;
    private float _healthPoints;

    [SerializeField] private float _kbDuration;
    [HideInInspector] public bool takingKb = false;

    public float playerDetectedDuration;
    [HideInInspector] public float playerDetected = 0;

    private void Awake() {
        rbEnemy = GetComponent<Rigidbody2D>();
        srEnemy = GetComponent<SpriteRenderer>();
    }

    //Debug
    private void Start() {
        _healthPoints = _maxHealthPoints;
    }

    private void FixedUpdate() {
        playerDetected -= Time.fixedDeltaTime;
    }

    // Instance reusing
    public bool Activate(bool isActivating) {
        if (_isActive && isActivating) return false;
        _isActive = isActivating;
        rbEnemy.simulated = isActivating;
        srEnemy.enabled = isActivating;
        if (isActivating) {
            onActivate.Invoke();
            _healthPoints = _maxHealthPoints;
            // Set position
        }
        else onDeactivate.Invoke();
        return true;
    }

    public void TakeDamage(float damage, Vector3 knockBack) {
        _healthPoints -= damage;
        if (_healthPoints <= 0) {
            // Include delay for death animation
            Activate(false);
        }
        else {
            // Remember to stop other velocity scripts
            rbEnemy.velocity = knockBack;
            takingKb = true;
            Invoke(nameof(StopKB), _kbDuration);
        }
    }

    private void StopKB() {
        takingKb = false;
    }

}
