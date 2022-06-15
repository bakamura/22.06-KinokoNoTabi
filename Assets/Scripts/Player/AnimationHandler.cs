using UnityEngine;

public class AnimationHandler : MonoBehaviour {

    [Header("Settings")]

    [Tooltip("The first animation [0] will be played on start")]
    [SerializeField] private AnimationI[] _animations;
    private int _currentAnimation = 1;
    private int _currentFrame = 0;

    private void Start() {
        ChangeAnimation(_animations[0].name);
    }

    public void ChangeAnimation(string name) {
        if (name != GetCurrentAnimationName()) {
            //print(name);
            for (int i = 0; i < _animations.Length; i++) if (_animations[i].name == name) {
                    _currentAnimation = i;
                    _currentFrame = 0;
                    CancelInvoke(nameof(ChangeFrame));
                    InvokeRepeating(nameof(ChangeFrame), 0, 1f / _animations[i].speed);
                    return;
                }
            Debug.LogError(transform.name + ": Couldn't find animation named " + name);
        }
    }

    private void ChangeFrame() {
        if (_currentFrame + 1 < _animations[_currentAnimation].sprites.Length) _currentFrame++;
        else if (_animations[_currentAnimation].repeat) _currentFrame = _animations[_currentAnimation].repeatFrom;
        else CancelInvoke(nameof(ChangeFrame));
        PlayerData.srPlayer.sprite = _animations[_currentAnimation].sprites[_currentFrame];
    }

    public string GetCurrentAnimationName() {
        return _animations[_currentAnimation].name;
    }

}
