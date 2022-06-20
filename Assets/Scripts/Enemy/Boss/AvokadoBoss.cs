using System;
using System.Collections;
using UnityEngine;

public class AvokadoBoss : MonoBehaviour {

    private EnemyData _dataScript;

    [Header("Jump")]

    [Tooltip("First Element is Dynamic")]
    [SerializeField] private Vector3[] _jumpPoints;
    [SerializeField] private float _jumpHeight;
    private int _currentJumpPoint = 1;
    [Tooltip("Jump duration for each phase of the boss Start/Split/Alone")]
    [SerializeField] private float[] _jumpDuration = new float[3];
    [SerializeField] private Vector2 _jumpAtkArea;
    [SerializeField] private Vector3 _jumpAreaOffset;
    private Vector3 _jumpStartPos;
    private float _currentJumpPos;
    [Tooltip("Kb assumes player is to the right")]
    [SerializeField] private Vector3 _jumpKb;
    [SerializeField] private float _delayToJump;

    [Header("Seed")]

    [SerializeField] private GameObject _seedPrefab;
    private AvokadoSeed _seedInstance;
    public float _seedSpeed;
    [Tooltip("Kb assumes player is to the right")]
    public Vector3 _seedKb;
    [SerializeField] private float _delayToShoot;

    [Header("Clone")]

    [SerializeField] private GameObject _clonePrefab;

    [Header("Misc")]

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
    private ActionN _lastMajorAction = ActionN.Jump2;
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
        if (_dataScript.healthPoints < _hpAmountToSplit && _lastMajorAction == ActionN.Jump0) {
            _state = StateN.Split;
            _lastMajorAction = ActionN.Jump2;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), _seedInstance.GetComponent<Collider2D>(), false);
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
                        StartCoroutine(CatchSeed());
                        break;
                    case ActionN.Jump2:
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
        print("AvoIdle");
        yield return new WaitForSeconds(_actionDelay[(int)_state]);

        GoToNextAction();
    }

    private IEnumerator Jump() {
        _currentJumpPos = 0;
        while ((transform.position - _jumpPoints[_currentJumpPoint]).magnitude < 0.1f) _currentJumpPoint = UnityEngine.Random.Range(1, _jumpPoints.Length);
        _jumpStartPos = transform.position;
        _dataScript.srEnemy.flipX = (_jumpPoints[_currentJumpPoint].x - transform.position.x) < 0;

        yield return new WaitForSeconds(_delayToJump);

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), PlayerData.Instance.GetComponent<Collider2D>());
        _stateUpdate = JumpUpdate;
    }

    private void JumpUpdate() {
        _currentJumpPos += Time.deltaTime / _jumpDuration[(int)_state];
        if (_currentJumpPos < 1) transform.position = Vector3.Lerp(_jumpStartPos, _jumpPoints[_currentJumpPoint], _currentJumpPos) + Vector3.up * _jumpHeight * 4 * (-Mathf.Pow(_currentJumpPos, 2) + _currentJumpPos);
        else {
            transform.position = _jumpPoints[_currentJumpPoint];

            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), PlayerData.Instance.GetComponent<Collider2D>(), false);
            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position - _jumpAreaOffset, _jumpAtkArea, 0);
            foreach (Collider2D hit in hits) if (hit.GetComponent<PlayerData>() != null) PlayerData.Instance.TakeDamage(1, PlayerData.srPlayer.flipX ? -_jumpKb : _jumpKb);

            _stateUpdate = null;
            switch (_lastMajorAction) {
                case ActionN.Jump0:
                    _lastMajorAction = ActionN.Jump1;
                    break;
                case ActionN.Shoot:
                    _seedInstance.Activate(false); //
                    _lastMajorAction = ActionN.Jump2;
                    break;
                case ActionN.Jump2:
                    _lastMajorAction = ActionN.Jump0;
                    break;
            }

            GoToIdle();
        }
    }

    private IEnumerator Shoot() {
        _dataScript.srEnemy.flipX = (PlayerData.transformPlayer.position.x - transform.position.x) < 0;

        yield return new WaitForSeconds(_delayToShoot);

        _seedInstance.Activate(true);
        _seedInstance.rb.velocity = new Vector2(_dataScript.srEnemy.flipX ? -_seedSpeed : _seedSpeed, 0);

        yield return new WaitForSeconds(0.1f);

        _lastMajorAction = ActionN.Shoot;
    }

    public void SetSeedPos(Vector3 pos) {
        GoToIdle();
        _jumpPoints[0] = pos;
    }

    private IEnumerator CatchSeed() {
        _currentJumpPos = 0;
        _currentJumpPoint = 0;
        _jumpStartPos = transform.position;
        _dataScript.srEnemy.flipX = (_jumpPoints[0].x - transform.position.x) < 0;

        yield return new WaitForSeconds(_delayToJump);

        _stateUpdate = JumpUpdate;
    }

    private IEnumerator ShootSeedSpawnClone() {
        _currentJumpPos = 0;
        while ((transform.position - _jumpPoints[_currentJumpPoint]).magnitude < 0.1f) _currentJumpPoint = UnityEngine.Random.Range(1, _jumpPoints.Length);
        _jumpStartPos = transform.position;
        _dataScript.srEnemy.flipX = (_jumpPoints[_currentJumpPoint].x - transform.position.x) < 0;

        yield return new WaitForSeconds(_delayToShoot);

        _stateUpdate = SeedArcUpdate;

    }

    private void SeedArcUpdate() {
        _currentJumpPos += Time.deltaTime / _jumpDuration[(int)_state];
        if (_currentJumpPos < 1) transform.position = Vector3.Lerp(_jumpStartPos, _jumpPoints[_currentJumpPoint], _currentJumpPos) + Vector3.up * _jumpHeight * 4 * (-Mathf.Pow(_currentJumpPos, 2) + _currentJumpPos);
        else {
            Instantiate(_clonePrefab, transform.position, Quaternion.identity)
        }
    }
}
