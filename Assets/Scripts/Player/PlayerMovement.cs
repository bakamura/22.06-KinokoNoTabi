using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement Instance { get; private set; }

    public bool canMove = true;

    [Header("Run Stats")]

    [SerializeField] private float _movementSpeed;
    private float _movementSpeedMultiplier = 1;
    [Tooltip("How long it takes to achieve maximum speed, or to stop movement. By unity default, 50 = 1 second")]
    [SerializeField] [Range(1, 500)] private float _damping;
    private float _currentSpeed = 0;

    [Header("Jump Stats")]

    [SerializeField] private float _jumpStrenght;
    [SerializeField] private Vector3 _groundCheckOffset;
    [Tooltip("Size of a box that stays at the middle-bottom of the player, checking if it's colliding with ground")]
    [SerializeField] private Vector2 _groundCheckArea;
    [SerializeField] private LayerMask _groundLayer;
    [HideInInspector] public bool isGrounded;
    private bool _hasDoubleJumped = false;
    [SerializeField] private float _doubleJumpMovementSpeedMultiplier;

    private float jumpGroundCheckDelay = 0.2f;

    [Header("Animation")]

    const string PlayerJump = "PlayerJump";
    const string PlayerDoubleJump = "PlayerDoubleJump";

    private void Awake() {
        Instance = this;
    }

    private void Update() { // Maybe change for an invokeRepeating?
        // Jumping
        if (jumpGroundCheckDelay < 0) isGrounded = Physics2D.OverlapBox(transform.position + _groundCheckOffset, _groundCheckArea, 0, _groundLayer);
        else jumpGroundCheckDelay -= Time.deltaTime;
        if (canMove) {
            if (isGrounded) {
                _hasDoubleJumped = false;
                _movementSpeedMultiplier = 1;
            }
            if (PlayerInputs.jumpKeyPressed > 0) {
                if (isGrounded) {
                    PlayerInputs.jumpKeyPressed = 0;
                    PlayerData.rbPlayer.velocity = new Vector2(PlayerData.rbPlayer.velocity.x, _jumpStrenght);
                    PlayerData.animPlayer.FromAnyTo(PlayerJump); // Animations
                    jumpGroundCheckDelay = 0.2f;
                    isGrounded = false;
                }
                else if (!_hasDoubleJumped && GameManager.doubleJumpUpgrade) {
                    PlayerInputs.jumpKeyPressed = 0;
                    PlayerData.rbPlayer.velocity = new Vector2(PlayerData.rbPlayer.velocity.x, _jumpStrenght);
                    _movementSpeedMultiplier = _doubleJumpMovementSpeedMultiplier;
                    _hasDoubleJumped = true;
                    PlayerData.animPlayer.FromAnyTo(PlayerDoubleJump); // Animations
                }
            }
        }
    }

    private void FixedUpdate() {
        if (canMove) {
            // Horizontal Movement
            if (_currentSpeed != PlayerInputs.horizontalAxis) {
                _currentSpeed += Mathf.Sign(PlayerInputs.horizontalAxis - _currentSpeed) / _damping;
                if (PlayerInputs.horizontalAxis == 0 && Mathf.Abs(_currentSpeed) < 0.05f) _currentSpeed = 0;
            }
            PlayerData.rbPlayer.velocity = new Vector2(_currentSpeed * _movementSpeed * _movementSpeedMultiplier, PlayerData.rbPlayer.velocity.y);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + _groundCheckOffset, _groundCheckArea);
    }
#endif

}
