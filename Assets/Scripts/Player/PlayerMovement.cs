using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement Instance { get; private set; }

    [Header("Run Stats")]

    [SerializeField] private float _movementSpeed;
    private float _movementSpeedMultiplier = 1;
    [Tooltip("How long it takes to achieve maximum speed, or to stop movement. By unity default, 50 = 1 second")]
    [SerializeField] [Range(1, 500)] private float _damping;
    private float _currentSpeed = 0;

    [Header("Jump Stats")]

    [SerializeField] private float _jumpStrenght;
    [Tooltip("Size of a box that stays at the middle-bottom of the player, checking if it's colliding with ground")]
    [SerializeField] private Vector2 _groundCheckArea;
    [SerializeField] private LayerMask _groundLayer;
    private bool _isGrounded;
    private bool _hasDoubleJumped = false;
    [SerializeField] private float _doubleJumpMovementSpeedMultiplier;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Update() { // Maybe change for an invokeRepeating?
        // Jumping
        _isGrounded = Physics2D.OverlapBox(transform.position - new Vector3(0, transform.lossyScale.y / 2, 0), _groundCheckArea, 0, _groundLayer);
        if (_isGrounded) {
            _hasDoubleJumped = false;
            _movementSpeedMultiplier = 1;
        }
        if (PlayerInputs.jumpKeyPressed > 0) {
            if (_isGrounded) {
                PlayerInputs.jumpKeyPressed = 0;
                PlayerData.rbPlayer.velocity = new Vector2(PlayerData.rbPlayer.velocity.x, _jumpStrenght);

                // Setting Animations REMAKE
                PlayerData.animatorPlayer.SetBool("Jumping", true);
            }
            else if (!_hasDoubleJumped && PlayerData.Instance.doubleJumpUpgrade) {
                PlayerInputs.jumpKeyPressed = 0;
                PlayerData.rbPlayer.velocity = new Vector2(PlayerData.rbPlayer.velocity.x, _jumpStrenght);
                _movementSpeedMultiplier = _doubleJumpMovementSpeedMultiplier;
                _hasDoubleJumped = true;

                // Setting Animations REMAKE
                PlayerData.animatorPlayer.SetBool("DoubleJumping", true);
            }
        }

        // NOTE: if player hits platform before falling, might not work properly
        if (PlayerData.rbPlayer.velocity.y <= -0.1f) {
            if (PlayerData.animatorPlayer.GetBool("Jumping") && _isGrounded) PlayerData.animatorPlayer.SetBool("Jumping", false); // REMAKE
            else if (_hasDoubleJumped) PlayerData.srPlayer.flipY = true;
        }
        if (PlayerData.rbPlayer.velocity.y >= -0.1f) PlayerData.srPlayer.flipY = false;
    }

    private void FixedUpdate() {
        // Horizontal Movement
        if (_currentSpeed != PlayerInputs.horizontalAxis) {
            _currentSpeed += Mathf.Sign(PlayerInputs.horizontalAxis - _currentSpeed) / _damping;
            if (PlayerInputs.horizontalAxis == 0 && Mathf.Abs(_currentSpeed) < 0.05f) _currentSpeed = 0;
        }
        PlayerData.rbPlayer.velocity = new Vector2(_currentSpeed * _movementSpeed * _movementSpeedMultiplier, PlayerData.rbPlayer.velocity.y);

        // Settings Animations REMAKE
        if (PlayerData.rbPlayer.velocity.x != 0) PlayerData.srPlayer.flipX = PlayerData.rbPlayer.velocity.x < 0;
        if (PlayerData.rbPlayer.velocity.x == 0 && PlayerInputs.horizontalAxis == 0) PlayerData.animatorPlayer.SetInteger("Running", 0);
        else if (Mathf.Sign(PlayerData.rbPlayer.velocity.x) == (-1 * PlayerInputs.horizontalAxis)) PlayerData.animatorPlayer.SetInteger("Running", 2);
        else PlayerData.animatorPlayer.SetInteger("Running", 1);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position - new Vector3(0, transform.lossyScale.y / 2, 0), _groundCheckArea);
    }
#endif
}
