using UnityEngine;

public class KabuAtkPattern : MonoBehaviour {

    private EnemyData _dataScript;

    private void Awake() {
        _dataScript = GetComponent<EnemyData>();
    }

    private void FixedUpdate() {
        if (_dataScript.playerDetected > 0 && !_dataScript.takingKb) {
            _dataScript.rbEnemy.velocity = new Vector2((PlayerData.Instance.transform.position - transform.position).x, _dataScript.rbEnemy.velocity.y); //TEST
            _dataScript.srEnemy.flipX = (PlayerData.Instance.transform.position - transform.position).x < 0;
        }
    }

}