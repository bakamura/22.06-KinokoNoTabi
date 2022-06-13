using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvokadoBoss : MonoBehaviour {

    private EnemyData _dataScript;

    [Header("Info")]

    [SerializeField] private Vector3 _jumpPoints;
    [Tooltip("Jump duration for each phase of the boss")]
    [SerializeField] private float[] _jumpDuration = new float[3];
    [Tooltip("Kb assumes player is to the right")]
    [SerializeField] private Vector3 _jumpKb;

    [SerializeField] private float _seedSpeed;
    [Tooltip("Kb assumes player is to the right")]
    [SerializeField] private Vector3 _seedKb;

    [SerializeField] private float _actionDelay;

    [Header("State Control")]

    private static GameObject[] _instances;
    private int _state = 0;
    private int _action = 0;
    [SerializeField] private float _hpAmountToSplit;

    private void Awake() {
        _dataScript = GetComponent<EnemyData>();

        if (_instances[0] == null) _instances[0] = gameObject;
        else _instances[1] = gameObject;
    }

    private void FixedUpdate() {
        switch (_state) {
            case 0:
                State1();
                if (_dataScript.healthPoints <= _hpAmountToSplit) _state = 1;
                break;
            case 1:
                State2();
                // Check if one of them dies9
                break;
            case 2:
                State3();
                break;
        }
    }

    private void State1() {
        switch(_action) {

        }
    }
    private void State2() {
        switch (_action) {

        }
    }
    private void State3() {
        switch (_action) {

        }
    }

}
