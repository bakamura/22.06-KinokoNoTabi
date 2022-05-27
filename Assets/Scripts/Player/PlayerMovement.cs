using System.Collections;
using System.Collections.Generic;
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
    private bool _isGrounded;
    private bool _hasDoubleJumped = false;
    [SerializeField] private float _doubleJumpMovementSpeedMultiplier;

    private float jumpGroundCheckDelay = 0.2f;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Update() { // Maybe change for an invokeRepeating?
        // Jumping
        if (jumpGroundCheckDelay < 0) _isGrounded = Physics2D.OverlapBox(transform.position + _groundCheckOffset, _groundCheckArea, 0, _groundLayer);
        else jumpGroundCheckDelay -= Time.deltaTime;
        if (_isGrounded) {
            _hasDoubleJumped = false;
            _movementSpeedMultiplier = 1;
        }
        if (PlayerInputs.jumpKeyPressed > 0) {
            if (_isGrounded) {
                PlayerInputs.jumpKeyPressed = 0;
                PlayerData.rbPlayer.velocity = new Vector2(PlayerData.rbPlayer.velocity.x, _jumpStrenght);
                PlayerAnimations.Instance.ChangeAnimation("PlayerJump"); // Animations
                jumpGroundCheckDelay = 0.2f;
                _isGrounded = false;
            }
            else if (!_hasDoubleJumped && GameManager.doubleJumpUpgrade) {
                PlayerInputs.jumpKeyPressed = 0;
                PlayerData.rbPlayer.velocity = new Vector2(PlayerData.rbPlayer.velocity.x, _jumpStrenght);
                _movementSpeedMultiplier = _doubleJumpMovementSpeedMultiplier;
                _hasDoubleJumped = true;
                PlayerAnimations.Instance.ChangeAnimation("PlayerDoubleJump"); // Animations
            }
        }

        // NOTE: if player hits platform before falling, might not work properly
        if (PlayerData.rbPlayer.velocity.y <= -0.05f && _isGrounded) {
            if ((PlayerAnimations.Instance.GetCurrentAnimationName() == "PlayerJump" || PlayerAnimations.Instance.GetCurrentAnimationName() == "PlayerDoubleJump") && _isGrounded) PlayerAnimations.Instance.ChangeAnimation("PlayerIdle"); // Anim
        }
        if (PlayerData.rbPlayer.velocity.y >= -0.1f) PlayerData.srPlayer.flipY = false;
    }

    private void FixedUpdate() {
        if (canMove) {
            // Horizontal Movement
            if (_currentSpeed != PlayerInputs.horizontalAxis) {
                _currentSpeed += Mathf.Sign(PlayerInputs.horizontalAxis - _currentSpeed) / _damping;
                if (PlayerInputs.horizontalAxis == 0 && Mathf.Abs(_currentSpeed) < 0.05f) _currentSpeed = 0;
            }
            PlayerData.rbPlayer.velocity = new Vector2(_currentSpeed * _movementSpeed * _movementSpeedMultiplier, PlayerData.rbPlayer.velocity.y);

            // Animations
            if (PlayerData.rbPlayer.velocity.x != 0) PlayerData.srPlayer.flipX = PlayerData.rbPlayer.velocity.x < 0;
            if (_isGrounded) {
                if (PlayerData.rbPlayer.velocity.x == 0 && PlayerInputs.horizontalAxis == 0) PlayerAnimations.Instance.ChangeAnimation("PlayerIdle");
                else if (Mathf.Sign(PlayerData.rbPlayer.velocity.x) == (-1 * PlayerInputs.horizontalAxis)) PlayerAnimations.Instance.ChangeAnimation("PlayerBreak");
                else PlayerAnimations.Instance.ChangeAnimation("PlayerMove");
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + _groundCheckOffset, _groundCheckArea);
    }
#endif
}
