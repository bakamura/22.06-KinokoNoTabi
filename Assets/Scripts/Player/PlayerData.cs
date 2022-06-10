using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance { get; private set; }

    [Header("Components")]

    [HideInInspector] public static Transform transformPlayer;
    [HideInInspector] public static Rigidbody2D rbPlayer;
    [HideInInspector] public static SpriteRenderer srPlayer;
    [HideInInspector] public static AnimationHandler ahPlayer;

    [Header("Combat Stats")]

    [SerializeField] private int _maxHealthPoints;
    [SerializeField] private int _healthUpgradeBonusPoints;
    private static int _healthPoints;
    [SerializeField] private float _kbDuration;

    [SerializeField] private float _delayToRestart;

    private void Awake() {
        Instance = this;

        transformPlayer = GetComponent<Transform>();
        rbPlayer = GetComponent<Rigidbody2D>();
        srPlayer = GetComponent<SpriteRenderer>();
        ahPlayer = GetComponent<AnimationHandler>();
    }

    private void Start() {
        _healthPoints = _maxHealthPoints + (GameManager.healthPointsUpgrade ? _healthUpgradeBonusPoints : 0);
        UserInterface.Instance.SetHealthUI(_healthPoints);
    }

    public void TakeDamage(int damage, Vector3 knockBack) {
        _healthPoints -= damage;
        UserInterface.Instance.SetHealthUI(_healthPoints);
        if (_healthPoints < 0) {
            ahPlayer.ChangeAnimation("PlayerDeath");
            Invoke(nameof(RestartScene), _delayToRestart);
        }
        else {
            PlayerMovement.Instance.canMove = false;
            rbPlayer.velocity = knockBack;
            Invoke(nameof(StopKnockback), _kbDuration);
        }
    }

    public void StopKnockback() {
        rbPlayer.velocity = Vector2.zero;
        PlayerMovement.Instance.canMove = false;
    }

    private void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
