using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDetection : MonoBehaviour {

    private EnemyData _dataScript;

    [Header("Stats")]

    [SerializeField] private float _lineDistance;
    [SerializeField] private Vector2 _lineDirection;

    private void Awake() {
        _dataScript = GetComponent<EnemyData>();
    }

    private void FixedUpdate() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _lineDirection, _lineDistance);
        if (hit.transform.tag == "Player") _dataScript.playerDetected = _dataScript.playerDetectedDuration;
    }

}
