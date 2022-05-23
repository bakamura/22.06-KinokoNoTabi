using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelSelector : MonoBehaviour {

    [Header("Inputs")]

    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;
    private Vector2 _movementDirection;

    [Header("Info")]

    [SerializeField] private float _movementSpeed;
    private float _currentSpeed;
    [SerializeField] private float _damping;
    private Rigidbody2D _rb;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _movementDirection = new Vector2((Input.GetKey(leftKey) ? -1 : 0) + (Input.GetKey(rightKey) ? 1 : 0), (Input.GetKey(downKey) ? -1 : 0) + (Input.GetKey(upKey) ? 1 : 0)).normalized;
    }

    private void FixedUpdate() {
        if (_currentSpeed != _movementDirection.x) {
            _currentSpeed += Mathf.Sign(_movementDirection.x - _currentSpeed) / _damping;
            if (_movementDirection.x == 0 && Mathf.Abs(_currentSpeed) < 0.05f) _currentSpeed = 0;
        }
        _rb.velocity = _movementDirection * _movementSpeed * _currentSpeed;
    }
}
