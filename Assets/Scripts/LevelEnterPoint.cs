using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnterPoint : MonoBehaviour {

    [Header("Info")]

    [SerializeField] private float _playerDetectionRange;
    [SerializeField] private Sprite _levelImage;
    public int[] levelNumber = new int[2];
    [SerializeField] private string _levelTitle;

    private void FixedUpdate() {
        if ((PlayerLevelSelector.Instance.transform.position - transform.position).magnitude < _playerDetectionRange) {
            if (LevelPopUpWindow.Instance.levelImage.sprite != _levelImage) {
                LevelPopUpWindow.Instance.StartPopUpAnim(transform.position, _levelImage, levelNumber[0] + " - " + levelNumber[1] + "\n" + _levelTitle);
                PlayerLevelSelector.Instance.sceneToLoad = 1 + levelNumber[0] + (5 * (levelNumber[1] - 1)); // Supposing each world before the last has 5 levels
            }
        }
        else if (LevelPopUpWindow.Instance.levelImage.sprite == _levelImage) {
            LevelPopUpWindow.Instance.StartPopOutAnim();
            PlayerLevelSelector.Instance.sceneToLoad = 0;
        }

    }

}
