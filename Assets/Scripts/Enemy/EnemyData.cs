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
    [HideInInspector] public UnityAction onDetection;
    [HideInInspector] public UnityAction onPatrol;

    [Header("Stats")]

    [SerializeField] private float _maxHealthPoints;
    [HideInInspector] public float healthPoints;

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
        healthPoints = _maxHealthPoints;
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
            if(onActivate != null) onActivate.Invoke();
            healthPoints = _maxHealthPoints;
            // Set position
        }
        else if(onDeactivate != null) onDeactivate.Invoke();
        return true;
    }

    public void TakeDamage(float damage, Vector3 knockBack) {
        healthPoints -= damage;
        if (healthPoints <= 0) {
            // Include delay for death animation
            Activate(false);
        }
        else if (_kbDuration != 0){
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
