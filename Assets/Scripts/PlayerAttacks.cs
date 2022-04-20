using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {

    public static PlayerAttacks Instance { get; private set; }

    [Header("Shot Attack")]

    public float damageShotAttack;
    public float speedShotAttack;
    [SerializeField] private GameObject _prefabShotAttack;
    public Vector3 spawnPointShotAttack;
    [SerializeField] private float _delayToSpawnShotAttack;
    [SerializeField] private int _instanceAmountShotAttack;
    [SerializeField] private BulletPlayer[] _instancesShotAttack; //

    [Header("Stream Attack")]

    public float damagePerSecondStreamAttack;
    [SerializeField] private float _speedStreamAttack;
    [SerializeField] private GameObject _prefabStreamAttack;
    [SerializeField] private Vector3 _spawnPointStreamAttack;
    [SerializeField] private float _delayToSpawnStreamAttack;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        // Should put inside a parent object for organization in unity, probably
        _instancesShotAttack = new BulletPlayer[_instanceAmountShotAttack];
        for(int i = 0; i < _instanceAmountShotAttack; i++) _instancesShotAttack[i] = Instantiate(_prefabShotAttack, Vector3.zero, Quaternion.identity).GetComponent<BulletPlayer>();
    }

    private void Update() {
        if (PlayerInputs.canInput) {
            if (PlayerInputs.shotAttackKeyPressed) StartCoroutine(ShotAttackInstantiate());
            else if (PlayerInputs.streamAttackKeyPressed) StartCoroutine(StreamAttackInstantiate());
        }
        PlayerInputs.shotAttackKeyPressed = false;
        PlayerInputs.streamAttackKeyPressed = false;
    }

    private IEnumerator ShotAttackInstantiate() {
        PlayerData.animatorPlayer.SetTrigger("ShotAttacking");

        yield return new WaitForSeconds(_delayToSpawnShotAttack);

        // TO DO: Make so that when shooting while input in the oposite direction, stop movement first, turn around, then shot in the input direction
        for (int i = 0; i < _instanceAmountShotAttack; i++) if (_instancesShotAttack[i].Activate(true)) yield break; // ???
    }

    private IEnumerator StreamAttackInstantiate() {
        PlayerData.animatorPlayer.SetTrigger("StreamAttacking");

        yield return new WaitForSeconds(_delayToSpawnStreamAttack);

        GameObject stream = Instantiate(_prefabStreamAttack, transform.position + _spawnPointStreamAttack, Quaternion.identity);
        // TO DO: Make so that when spitting while input in the oposite direction, stop movement first, then shot in the input direction
        stream.GetComponent<Rigidbody2D>().velocity = new Vector2(_speedStreamAttack * (PlayerData.srPlayer.flipX ? -1 : 1), 0);
    }
}
