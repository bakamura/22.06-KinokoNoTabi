using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Divide into 2?
public class PlayerLevelSelector : MonoBehaviour {

    public static PlayerLevelSelector Instance { get; private set; }

    [Header("Inputs")]

    [SerializeField] private KeyCode _leftKey;
    [SerializeField] private KeyCode _rightKey;
    [SerializeField] private KeyCode _upKey;
    [SerializeField] private KeyCode _downKey;
    [SerializeField] private KeyCode _enterLevelKey;

    [Header("Info")]

    private Rigidbody2D _rb;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _damping;
    private Vector2 _movementDirection;
    private Vector2 _currentSpeed;

    [HideInInspector] public int sceneToLoad = 0;
    public float delayToLoadLevel;

    private void Start() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        _rb = GetComponent<Rigidbody2D>();

        LevelEnterPoint[] enterPoints = (LevelEnterPoint[])FindObjectsOfType(typeof(LevelEnterPoint));
        for (int i = 0; i < enterPoints.Length; i++) {
            if (enterPoints[i].levelNumber[1] == GameManager.levelPosition[1] && enterPoints[i].levelNumber[0] == GameManager.levelPosition[0]) {
                transform.position = enterPoints[i].transform.position;
                Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                break;
            }
            if(i == enterPoints.Length - 1) print("Invalid Level, Placed Player On Start"); //
        }

    }

    private void Update() {
        _movementDirection = new Vector2((Input.GetKey(_leftKey) ? -1 : 0) + (Input.GetKey(_rightKey) ? 1 : 0), (Input.GetKey(_downKey) ? -1 : 0) + (Input.GetKey(_upKey) ? 1 : 0)).normalized;
        if (Input.GetKeyDown(_enterLevelKey) && sceneToLoad > 1) {
            // Create cool transition
            Invoke(nameof(GoToLevel), delayToLoadLevel);
            delayToLoadLevel = -1;
        }
    }

    private void FixedUpdate() {
        if (_currentSpeed.x != _movementDirection.x || _currentSpeed.y != _movementDirection.y) {
            _currentSpeed.x += Mathf.Sign(_movementDirection.x - _currentSpeed.x) / _damping;
            _currentSpeed.y += Mathf.Sign(_movementDirection.y - _currentSpeed.y) / _damping;
            _currentSpeed = new Vector2((_movementDirection.x == 0 && Mathf.Abs(_currentSpeed.x) < 0.1f) ? 0 : _currentSpeed.x,
                                        (_movementDirection.y == 0 && Mathf.Abs(_currentSpeed.y) < 0.1f) ? 0 : _currentSpeed.y);
        }
        _rb.velocity = _movementSpeed * _currentSpeed;
    }

    private void GoToLevel() {
        SceneManager.LoadScene(sceneToLoad);
    }
}
