using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelSelector : MonoBehaviour {

    public static PlayerLevelSelector Instance { get; private set; }

    [Header("Inputs")]

    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;
    private Vector2 _movementDirection;

    [Header("Info")]

    [SerializeField] private float _movementSpeed;
    private Vector2 _currentSpeed;
    [SerializeField] private float _damping;
    private Rigidbody2D _rb;

    private void Start() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _movementDirection = new Vector2((Input.GetKey(leftKey) ? -1 : 0) + (Input.GetKey(rightKey) ? 1 : 0), (Input.GetKey(downKey) ? -1 : 0) + (Input.GetKey(upKey) ? 1 : 0)).normalized;
    }

    private void FixedUpdate() {
        if (_currentSpeed.x != _movementDirection.x || _currentSpeed.y != _movementDirection.y) {
            _currentSpeed.x += Mathf.Sign(_movementDirection.x - _currentSpeed.x) / _damping;
            _currentSpeed.y += Mathf.Sign(_movementDirection.y - _currentSpeed.y) / _damping;
            _currentSpeed = new Vector2((_movementDirection.x == 0 && Mathf.Abs(_currentSpeed.x) < 0.05f) ? 0 : _currentSpeed.x, 
                                        (_movementDirection.y == 0 && Mathf.Abs(_currentSpeed.y) < 0.05f) ? 0 : _currentSpeed.y);
        }
        _rb.velocity = _movementSpeed * _currentSpeed;
    }
}
