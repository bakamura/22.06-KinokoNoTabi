using UnityEngine;

public class LineDetection : MonoBehaviour {

    private EnemyData _dataScript;

    [Header("Stats")]

    [SerializeField] private float _lineDistance;
    [Tooltip("Assuming enemy is looking to the right")]
    [SerializeField] private Vector2 _lineDirection;

    private void Awake() {
        _dataScript = GetComponent<EnemyData>();
    }

    private void FixedUpdate() {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, _dataScript.srEnemy.flipX ? -_lineDirection : _lineDirection, _lineDistance);
        for (int i = 0; i < hits.Length; i++) if (hits[i].transform.tag == "Player") {
                _dataScript.playerDetected = _dataScript.playerDetectedDuration;
                break;
            }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if (Application.isPlaying) Gizmos.DrawLine(transform.position, transform.position + new Vector3(((_dataScript.srEnemy.flipX ? -_lineDirection : _lineDirection) * _lineDistance).x, 
                                                                                                       ((_dataScript.srEnemy.flipX ? -_lineDirection : _lineDirection) * _lineDistance).y));
    }
#endif

}
