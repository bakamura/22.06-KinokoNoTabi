using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvokadoBoss : MonoBehaviour {

    private EnemyData _dataScript;

    [Header("Info")]

    [SerializeField] private Vector3[] _jumpPoints;
    private int _currentJumpPoint;
    [Tooltip("Jump duration for each phase of the boss")]
    [SerializeField] private float[] _jumpDuration = new float[3];
    private Vector3 _jumpStartPos;
    private float _currentJumpPos;
    [Tooltip("Kb assumes player is to the right")]
    [SerializeField] private Vector3 _jumpKb;

    [SerializeField] private float _seedSpeed;
    [Tooltip("Kb assumes player is to the right")]
    [SerializeField] private Vector3 _seedKb;

    [SerializeField] private float[] _actionDelay = new float[3];

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
                if (_dataScript.healthPoints <= _hpAmountToSplit) {
                    _state = 1;
                    _action = 0;
                }
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

    private void GoNextAction() {
        _action++;
    }

    // Initial

    private void State1() {
        switch(_action) {
            case 0: // Sets jump info
                _jumpStartPos = transform.position;
                _currentJumpPos = 0;
                _action++;
                break;
            case 1: // Jumps
                _currentJumpPos += Time.deltaTime / _jumpDuration[_state];
                if (_currentJumpPos >= 1) {
                    transform.position = _jumpPoints[_currentJumpPoint];
                    _action++;
                    Invoke(nameof(GoNextAction), _actionDelay[_state]);
                }
                else transform.position = Vector3.Lerp(_jumpStartPos, _jumpPoints[_currentJumpPoint], _currentJumpPos);
                break;
            case 2: break; // Here just for visualization
            case 3: // Sets jump info
                _jumpStartPos = transform.position;
                _currentJumpPos = 0;
                _action++;
                break;
            case 4: // Jumps
                _currentJumpPos += Time.deltaTime / _jumpDuration[_state];
                if (_currentJumpPos >= 1) {
                    transform.position = _jumpPoints[_currentJumpPoint];
                    _action++;
                    Invoke(nameof(GoNextAction), _actionDelay[_state]);
                }
                else transform.position = Vector3.Lerp(_jumpStartPos, _jumpPoints[_currentJumpPoint], _currentJumpPos);
                break;
            case 5: break; // Here just for visualization
            case 6: // Shots

                break;

        }
    }

    // Divided

    private void State2() {
        switch (_action) {
            case 0:

                break;
            case 1:

                break;
        }
    }

    // Alone

    private void State3() {
        switch (_action) {
            case 0:

                break;
            case 1:

                break;
        }
    }

}
