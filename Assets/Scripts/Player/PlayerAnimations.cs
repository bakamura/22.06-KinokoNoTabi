using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    public static PlayerAnimations Instance { get; private set; }

    [Header("Settings")]

    [SerializeField] private CustomAnimation[] _animations;
    private int _currentAnimation = 0;
    private int _currentFrame = 0;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        ChangeAnimation("PlayerIdle");
    }

    public void ChangeAnimation(string name) {
        for (int i = 0; i < _animations.Length; i++) if (_animations[i].name == name) {
                _currentAnimation = i;
                _currentFrame = 0;
                InvokeRepeating(nameof(ChangeFrame), 0, 1f / _animations[i].speed);
                return;
            }
        Debug.LogError("Couldn't find animation named " + name);
    }

    private void ChangeFrame() {
        if (_currentFrame + 1 < _animations[_currentAnimation].sprites.Length) _currentFrame++;
        else if (_animations[_currentAnimation].repeat) _currentFrame = 0;
        else CancelInvoke(nameof(ChangeFrame));
        PlayerData.srPlayer.sprite = _animations[_currentAnimation].sprites[_currentFrame];
    }

    public string GetCurrentAnimationName() {
        return _animations[_currentAnimation].name;
    }
}
