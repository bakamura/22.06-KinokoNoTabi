using System;
using System.Collections;
using UnityEngine;

public class AvokadoBoss : MonoBehaviour {

    private EnemyData _dataScript;

    [Header("Info")]

    [SerializeField] private Vector3[] _jumpPoints;
    private int _currentJumpPoint;
    [Tooltip("Jump duration for each phase of the boss Start/Split/Alone")]
    [SerializeField] private float[] _jumpDuration = new float[3];
    private Vector3 _jumpStartPos;
    private float _currentJumpPos;
    [Tooltip("Kb assumes player is to the right")]
    [SerializeField] private Vector3 _jumpKb;
    [SerializeField] private float _delayToJump;

    [SerializeField] private GameObject _seedPrefab;
    private AvokadoSeed _seedInstance;
    public float _seedSpeed;
    [Tooltip("Kb assumes player is to the right")]
    public Vector3 _seedKb;
    [SerializeField] private float _delayToShoot;

    [SerializeField] private float[] _actionDelay = new float[3];

    [Header("State Control")]

    [SerializeField] private float _hpAmountToSplit;
    private enum StateN {
        Initial,
        Split,
        Alone
    }
    private StateN _state = StateN.Initial;
    private enum ActionN {
        Jump0,
        Jump1,
        Jump2,
        Shoot
    }
    private ActionN _lastMajorAction = ActionN.Shoot;
    private Action _stateUpdate;
    private static GameObject[] _instances = new GameObject[2];

    private void Awake() {
        _dataScript = GetComponent<EnemyData>();

        if (_instances[0] == null) _instances[0] = gameObject;
        else _instances[1] = gameObject;
    }

    private void Start() {
        _seedInstance = Instantiate(_seedPrefab, transform).GetComponent<AvokadoSeed>();

        GoToIdle();
    }

    private void Update() {
        _stateUpdate?.Invoke();
    }

    private void GoToIdle() {
        StartCoroutine(Idle());
    }

    private void GoToNextAction() {
        if (_dataScript.healthPoints < _hpAmountToSplit) {
            //StartCoroutine();
            return;
        }

        switch (_state) {
            case StateN.Initial:
                switch (_lastMajorAction) {
                    case ActionN.Jump0:
                        StartCoroutine(Jump());
                        break;
                    case ActionN.Jump1:
                        StartCoroutine(Shoot());
                        break;
                    case ActionN.Shoot:
                        StartCoroutine(Jump());
                        break;
                }
                break;
            case StateN.Split:
                switch (_lastMajorAction) {
                    case ActionN.Jump0:
                        StartCoroutine(Jump());
                        break;
                    case ActionN.Jump1:
                        StartCoroutine(Shoot());
                        break;
                    case ActionN.Shoot:
                        StartCoroutine(Jump());
                        break;
                }
                break;
            case StateN.Alone:
                switch (_lastMajorAction) {
                    case ActionN.Jump0:
                        StartCoroutine(Jump());
                        break;
                    case ActionN.Jump1:
                        StartCoroutine(Shoot());
                        break;
                    case ActionN.Shoot:
                        StartCoroutine(Jump());
                        break;
                }
                break;
        }
    }

    private IEnumerator Idle() {
        yield return new WaitForSeconds(_actionDelay[(int)_state]);

        GoToNextAction();
    }

    private IEnumerator Jump() {
        _currentJumpPos = 0;
        _currentJumpPoint = UnityEngine.Random.Range(0, _jumpPoints.Length);
        _jumpStartPos = transform.position;

        yield return new WaitForSeconds(_delayToJump);

        _stateUpdate = JumpUpdate;
    }

    private void JumpUpdate() {
        _currentJumpPos += Time.deltaTime / _jumpDuration[(int) _state];
        if (_currentJumpPos < 1) transform.position = Vector3.Lerp(_jumpStartPos, _jumpPoints[_currentJumpPoint], _currentJumpPos);
        else {
            transform.position = _jumpPoints[_currentJumpPoint];
            _stateUpdate = null;
            if (_lastMajorAction == ActionN.Jump0) _lastMajorAction = ActionN.Jump1;
            else _lastMajorAction = ActionN.Jump0;
            GoToIdle();
        }
    }

    private IEnumerator Shoot() {
        yield return new WaitForSeconds(_delayToShoot);

        _seedInstance.Activate(true);

        yield return new WaitForSeconds(0.1f);

        GoToIdle();
    }

}
