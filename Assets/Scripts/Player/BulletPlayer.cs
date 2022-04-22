using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour {

    [Header("Instance Reusing")]

    [SerializeField] private float _timeToDespawn;
    private bool _isActive = false;

    [Header("Components")]

    private Rigidbody2D _rbBullet;
    private SpriteRenderer _srBullet;

    private void Awake() {
        _rbBullet = GetComponent<Rigidbody2D>();
        _srBullet = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        Activate(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (_isActive) {
            if (collision.tag == "Enemy") {
                // Uncoment when "EnemyData" is created
                // collision.GetComponent<EnemyData>().TakeDamage(PlayerAttacks.Instance.damageShotAttack);
                Activate(false);
            }
            else if (collision.tag == "Ground") Activate(false);
        }
    }

    // Instance reusing
    public bool Activate(bool isActivating) {
        if (_isActive && isActivating) return false;
        _isActive = isActivating;
        _rbBullet.simulated = isActivating;
        _srBullet.enabled = isActivating;
        if (isActivating) {
            transform.position = PlayerData.transformPlayer.position + PlayerAttacks.Instance.spawnPointShotAttack;
            _rbBullet.velocity = new Vector2(PlayerAttacks.Instance.speedShotAttack * (PlayerData.srPlayer.flipX ? -1 : 1), 0);
            StopAllCoroutines();
            StartCoroutine(AutoDeactivate());
        }
        return true;
    }

    private IEnumerator AutoDeactivate() {
        yield return new WaitForSeconds(_timeToDespawn);

        if(_isActive) Activate(false);

    }

}
