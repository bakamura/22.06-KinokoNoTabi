using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPopUpWindow : MonoBehaviour {

    public static LevelPopUpWindow Instance { get; private set; }
    
    [Header("Info")]

    private RectTransform _rectTransform;
    public Image levelImage;
    [SerializeField] private TextMeshProUGUI _levelText;

    private float _currentPopUpTime = 0;
    [SerializeField] private float _popUpDuration;
    private float _popUpSymbol = 0;

    private void Awake() {
        Instance = this;
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start() {
        _rectTransform.localScale = Vector2.zero;
    }

    private void Update() {
        if (_popUpSymbol != 0) {
            _currentPopUpTime += _popUpSymbol * Time.deltaTime / _popUpDuration;
            _rectTransform.localScale = new Vector2(Mathf.Clamp((_currentPopUpTime - 0.5f) * 2, 0.1f, 1), Mathf.Clamp(_currentPopUpTime * 1.5f, 0, 1));
            if (_popUpSymbol == 1 && _currentPopUpTime >= 1) _popUpSymbol = 0;
            else if (_popUpSymbol == -1 && _currentPopUpTime <= 0) {
                _popUpSymbol = 0;
                levelImage.sprite = null;
                _rectTransform.localScale = Vector2.zero;
            }
        }
    }

    public void StartPopUpAnim(Vector3 position, Sprite image, string levelName) {
        _rectTransform.position = position;
        levelImage.sprite = image;
        _levelText.text = levelName;

        _currentPopUpTime = 0;
        _popUpSymbol = 1;
    }

    public void StartPopOutAnim() {
        _popUpSymbol = -1;
    }
}
