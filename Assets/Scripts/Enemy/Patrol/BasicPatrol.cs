using UnityEngine;

public class BasicPatrol : MonoBehaviour {

    private EnemyData _dataScript;

    [Header("Stats")]

    [SerializeField] private float _patrolSpeed;
    [SerializeField] private float _movementDistance;
    [SerializeField] private float _wallDetectDistance;
    [SerializeField] private float _jumpStrenght;
    private float _startPos;

    private void Awake() {
        _dataScript = GetComponent<EnemyData>();
    }

    private void Start() {
        _startPos = transform.position.x;
    }

    private void FixedUpdate() {
        if (_dataScript.playerDetected <= 0 && !_dataScript.takingKb) {
            if ((_patrolSpeed > 0 && transform.position.x > _startPos) || (_patrolSpeed < 0 && transform.position.x < _startPos - _movementDistance)) {
                _dataScript.srEnemy.flipX = !_dataScript.srEnemy.flipX;
                _patrolSpeed *= -1;
            }
            _dataScript.rbEnemy.velocity = new Vector2(_patrolSpeed, _dataScript.rbEnemy.velocity.y);
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.right, transform.lossyScale.x * _wallDetectDistance * Mathf.Sign(_patrolSpeed));
            for (int i = 0; i < hit.Length; i++) {
                if (hit[i].transform.tag == "Ground") {
                    _dataScript.rbEnemy.velocity = new Vector2(_dataScript.rbEnemy.velocity.x, _jumpStrenght);
                    break;
                }
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        if (!Application.isPlaying) Gizmos.DrawWireCube(transform.position - new Vector3(_movementDistance / 2, transform.lossyScale.y / 4, 0), new Vector2(transform.lossyScale.x + _movementDistance, 0.25f));
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.lossyScale.x * _wallDetectDistance * Mathf.Sign(_patrolSpeed));
    }
#endif

}
