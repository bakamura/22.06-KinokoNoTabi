using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [Header("Follow")]

    [SerializeField] private Transform _follow;
    [SerializeField] private Vector3 _followOffset;
    [SerializeField] private float _followSpeed;

    [Header("Boundaries")]

    [SerializeField] private bool _useBounds = false;
    [SerializeField] private Vector2 _boundMin;
    [SerializeField] private Vector2 _boundMax;
    private Vector3 _boundSize; // Make these in inspector if no better solution was found
    private Vector3 _boundPivot;

#if UNITY_EDITOR
    private void Start() {
    }
#endif

    private void Update() {
        Vector3 targetPos = _follow.position + _followOffset;
        targetPos = _useBounds ? new Vector3(Mathf.Clamp(targetPos.x, _boundMin.x, _boundMax.x), Mathf.Clamp(targetPos.y, _boundMin.y, _boundMax.y), -10) : new Vector3(targetPos.x, targetPos.y, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, Mathf.Clamp01(_followSpeed * Time.deltaTime)); // Naturally Damps
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        if (_useBounds) {
            _boundPivot = (_boundMax + _boundMin) / 2;
            _boundSize = _boundMax - _boundMin + new Vector2(2 * GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect, 2 * GetComponent<Camera>().orthographicSize);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(_boundPivot, _boundSize);
        }
    }
#endif
}
