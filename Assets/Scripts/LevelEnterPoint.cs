using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnterPoint : MonoBehaviour {

    [SerializeField] private float _playerDetectionRange;
    [SerializeField] private Sprite _levelImage;
    [SerializeField] private int[] _levelNumber;

    private void Awake() {
        
    }

    private void Start() {
        
    }

    private void Update() {
        
    }

    private void FixedUpdate() {
        if ((PlayerLevelSelector.Instance.transform.position - transform.position).magnitude < _playerDetectionRange) {
            // Use tag to get the pop up Instance. change image and text. Make small pop up animation for the window.
        }
    }

}
