using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour {

    [Header("Instance Reusing")]

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
                collision.GetComponent<EnemyData>().TakeDamage(PlayerAttacks.Instance.damageShotAttack, Mathf.Sign(_rbBullet.velocity.x) * PlayerAttacks.Instance.knockBackShotAttack);
                Activate(false);
            }
            else if (collision.tag == "Ground") Activate(false);
        }
    }

    // Instance reusing
    public bool Activate(bool isActivating) {
        if (isActivating) {
            if (_isActive) return false;
            transform.position = PlayerData.transformPlayer.position + new Vector3(PlayerAttacks.Instance.spawnPointShotAttack.x * (PlayerData.srPlayer.flipX ? -1 : 1), PlayerAttacks.Instance.spawnPointShotAttack.y);
            _rbBullet.velocity = new Vector2(PlayerAttacks.Instance.speedShotAttack * (PlayerData.srPlayer.flipX ? -1 : 1), 0);
            CancelInvoke(nameof(AutoDeactivate));
            Invoke(nameof(AutoDeactivate), PlayerAttacks.Instance.timeToDespawnShotAttack);
        }
        _isActive = isActivating;
        _rbBullet.simulated = isActivating;
        _srBullet.enabled = isActivating;
        return true;
    }

    private void AutoDeactivate() {
        if(_isActive) Activate(false);
    }

}
