using System.Collections;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {

    public static PlayerAttacks Instance { get; private set; }

    [Header("Shot Attack")]

    public float damageShotAttack;
    public Vector3 knockBackShotAttack;
    public float speedShotAttack;
    [SerializeField] private GameObject _prefabShotAttack;
    public Vector3 spawnPointShotAttack;
    [SerializeField] private float _delayToSpawnShotAttack;
    [SerializeField] private float _delayAfterShotAttack;
    [SerializeField] private int _instanceAmountShotAttack;
    private BulletPlayer[] _instancesShotAttack;
    public float timeToDespawnShotAttack;

    //[Header("Stream Attack")]
    //
    //public float damagePerSecondStreamAttack;
    //[SerializeField] private float _speedStreamAttack;
    //[SerializeField] private GameObject _prefabStreamAttack;
    //[SerializeField] private Vector3 _spawnPointStreamAttack;
    //[SerializeField] private float _delayToSpawnStreamAttack;

    [Header("AnimationName")]

    const string PlayerIdle = "PlayerIdle";
    const string PlayerShotAttack = "PlayerShotAttack";

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GameObject parentObject = new GameObject("BulletInstancesParent");
        _instancesShotAttack = new BulletPlayer[_instanceAmountShotAttack];
        for (int i = 0; i < _instanceAmountShotAttack; i++) _instancesShotAttack[i] = Instantiate(_prefabShotAttack, Vector3.zero, Quaternion.identity, parentObject.transform).GetComponent<BulletPlayer>();
    }

    private void Update() {
        if (PlayerInputs.canInput) {
            if (PlayerInputs.shotAttackKeyPressed > 0) StartCoroutine(StartShotAttackAnimation());
            //else if (PlayerInputs.streamAttackKeyPressed > 0 && GameManager.poisonBlowUpgrade) Invoke(nameof(StartStreamAttackAnimation), 0);
        }
    }

    private IEnumerator StartShotAttackAnimation() {
        PlayerInputs.canInput = false;
        PlayerInputs.shotAttackKeyPressed = 0;
        float direction = PlayerInputs.horizontalAxis;
        PlayerInputs.horizontalAxis = 0;

        yield return new WaitUntil(CheckPlayerMovement);

        if (direction != 0) PlayerData.srPlayer.flipX = direction < 0;
        PlayerData.animPlayer.FromAnyTo(PlayerShotAttack);

        yield return new WaitForSeconds(_delayToSpawnShotAttack);

        PlayerInputs.canInput = true;
        for (int i = 0; i < _instanceAmountShotAttack; i++) if (_instancesShotAttack[i].Activate(true)) { 
                yield return new WaitForSeconds(_delayAfterShotAttack);

                PlayerData.animPlayer.FromAnyTo(PlayerIdle);
                yield break; // ???
            }
        Debug.LogWarning("Bullet could not be instantiated: Number of bullets would exceed pool's quantity");
    }

    private bool CheckPlayerMovement() {
        return Mathf.Abs(PlayerData.rbPlayer.velocity.x) < 0.1f;
    }

    //private void StartStreamAttackAnimation() {
    //    PlayerInputs.streamAttackKeyPressed = 0;
    //    // TO DO: Make so that when spitting while input in the oposite direction, stop movement first, then shot in the input direction
    //    PlayerData.animPlayer.FromAnyTo("PlayerStreamAttack");

    //    Invoke(nameof(StreamAttackInstantiate), _delayToSpawnStreamAttack);
    //}

    //private void StreamAttackInstantiate() {
    //    // Make instance reuse
    //    GameObject stream = Instantiate(_prefabStreamAttack, transform.position + _spawnPointStreamAttack, Quaternion.identity);
    //    stream.GetComponent<Rigidbody2D>().velocity = new Vector2(_speedStreamAttack * (PlayerData.srPlayer.flipX ? -1 : 1), 0);
    //}

}
