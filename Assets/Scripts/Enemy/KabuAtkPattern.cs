using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KabuAtkPattern : MonoBehaviour {

    private EnemyData _dataScript;

    private void Awake() {
        _dataScript = GetComponent<EnemyData>();
    }

    private void Start() {

    }

    private void Update() {

    }

    private void FixedUpdate() {
        if (_dataScript.playerDetected > 0 && !_dataScript.takingKb) { // Error?
            _dataScript.rbEnemy.velocity = (PlayerData.Instance.transform.position - transform.position).normalized; //TEST
        }
    }

}
