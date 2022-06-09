using UnityEngine;

public class SelectorPlayerMove : MonoBehaviour {

    [Header("Inputs")]

    [SerializeField] private KeyCode _leftKey;
    [SerializeField] private KeyCode _rightKey;
    [SerializeField] private KeyCode _upKey;
    [SerializeField] private KeyCode _downKey;

    [Header("Info")]

    private Rigidbody2D _rb;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _damping;
    private Vector2 _movementDirection;
    private Vector2 _currentSpeed;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _movementDirection = new Vector2((Input.GetKey(_leftKey) ? -1 : 0) + (Input.GetKey(_rightKey) ? 1 : 0), (Input.GetKey(_downKey) ? -1 : 0) + (Input.GetKey(_upKey) ? 1 : 0)).normalized;
    }

    private void FixedUpdate() {
        if (_currentSpeed.x != _movementDirection.x || _currentSpeed.y != _movementDirection.y) {
            _currentSpeed.x += Mathf.Sign(_movementDirection.x - _currentSpeed.x) / _damping;
            _currentSpeed.y += Mathf.Sign(_movementDirection.y - _currentSpeed.y) / _damping;
            _currentSpeed = new Vector2((_movementDirection.x == 0 && Mathf.Abs(_currentSpeed.x) < 0.05f) ? 0 : _currentSpeed.x, (_movementDirection.y == 0 && Mathf.Abs(_currentSpeed.y) < 0.05f) ? 0 : _currentSpeed.y);
        }
        _rb.velocity = SelectorPlayerData.Instance.delayToLoadLevel > 0 ? _movementSpeed * _currentSpeed : Vector2.zero;
    }

}
