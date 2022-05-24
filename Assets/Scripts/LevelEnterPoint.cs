using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnterPoint : MonoBehaviour {

    [SerializeField] private float _playerDetectionRange;
    [SerializeField] private Sprite _levelImage;
    [SerializeField] private int[] _levelNumber = new int[2];
    [SerializeField] private string _levelTitle;

    private void FixedUpdate() {
        if ((PlayerLevelSelector.Instance.transform.position - transform.position).magnitude < _playerDetectionRange) {
            if (LevelPopUpWindow.Instance.levelImage.sprite != _levelImage) LevelPopUpWindow.Instance.StartPopUpAnim(transform.position, _levelImage, _levelNumber[0] + " - " + _levelNumber[1] + "\n" + _levelTitle);
        }
        else if (LevelPopUpWindow.Instance.levelImage.sprite == _levelImage) LevelPopUpWindow.Instance.StartPopOutAnim();
    }

}
