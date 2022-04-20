using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement Instance { get; private set; }

    [Header("Run Stats")]

    [SerializeField] private float _movementSpeed;
    [Tooltip("How long it takes to achieve maximum speed, or to stop movement. 50 = 1 second")]
    [SerializeField] [Range(1, 500)] private float _damping; // Smoothing
    private float _currentSpeed = 0;

    [Header("Jump Stats")]

    [SerializeField] private float _jumpStrenght;
    [Tooltip("Size of a box that stays at the middle-bottom of the player, checking if it's colliding with ground")]
    [SerializeField] private Vector2 _groundCheckArea;
    [SerializeField] private LayerMask _groundLayer;
    private bool _isGrounded;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Update() {
        // Jumping
        _isGrounded = Physics2D.BoxCast(transform.position - new Vector3(0, transform.lossyScale.y / 2, 0), _groundCheckArea, 0, Vector2.down, _groundCheckArea.y / 2, _groundLayer);

        if (PlayerInputs.jumpKeyPressed) {
            if (_isGrounded) {
                PlayerData.rbPlayer.velocity = new Vector2(PlayerData.rbPlayer.velocity.x, _jumpStrenght);

                // Setting Animations
                PlayerData.animatorPlayer.SetBool("Jumping", true);
            }
        }
        // NOTE: if player hits platform before falling, might not work properly
        if (PlayerData.rbPlayer.velocity.y <= 0 && PlayerData.animatorPlayer.GetBool("Jumping") && _isGrounded) PlayerData.animatorPlayer.SetBool("Jumping", false);

        // Outside so that it doesn't unproperly store a jump input.
        PlayerInputs.jumpKeyPressed = false;
    }

    private void FixedUpdate() {
        // Horizontal Movement
        if (_currentSpeed != PlayerInputs.horizontalAxis) {
            _currentSpeed += Mathf.Sign(PlayerInputs.horizontalAxis - _currentSpeed) / _damping;
            if (PlayerInputs.horizontalAxis == 0 && Mathf.Abs(_currentSpeed) < 0.05f) _currentSpeed = 0;
        }
        PlayerData.rbPlayer.velocity = new Vector2(_currentSpeed * _movementSpeed, PlayerData.rbPlayer.velocity.y);

        // Settings Animations
        if (PlayerData.rbPlayer.velocity.x != 0) PlayerData.srPlayer.flipX = PlayerData.rbPlayer.velocity.x < 0;
        if (PlayerData.rbPlayer.velocity.x == 0 && PlayerInputs.horizontalAxis == 0) PlayerData.animatorPlayer.SetInteger("Running", 0);
        else if (Mathf.Sign(PlayerData.rbPlayer.velocity.x) == (-1 * PlayerInputs.horizontalAxis)) PlayerData.animatorPlayer.SetInteger("Running", 2);
        else PlayerData.animatorPlayer.SetInteger("Running", 1);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position - new Vector3(0, transform.lossyScale.y / 2, 0), _groundCheckArea);
    }
}
