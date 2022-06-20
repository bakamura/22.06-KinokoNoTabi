using UnityEngine;

public class AvokadoSeed : MonoBehaviour {

    private Transform _avokadoTransform;
    [HideInInspector] public Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer _sr;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        _avokadoTransform = transform.parent;
        transform.parent = null;
        transform.localScale = Vector3.one; //

        Physics2D.IgnoreCollision(col, _avokadoTransform.GetComponent<Collider2D>());
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "Ground") {
            _avokadoTransform.GetComponent<AvokadoBoss>().SetSeedPos(transform.position + new Vector3((transform.position.x - _avokadoTransform.position.x < 0 ? 1 : -1) * transform.localScale.x / 2, transform.localScale.y / 2, 1));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
            case "Player":
                PlayerData.Instance.TakeDamage(1, Mathf.Sign(rb.velocity.x) * _avokadoTransform.GetComponent<AvokadoBoss>()._seedKb);
                break;
            case "Ground":
                rb.velocity = Vector2.up; //
                rb.gravityScale = 1;
                col.isTrigger = false;
                Physics2D.IgnoreCollision(col, PlayerData.Instance.GetComponent<Collider2D>());
                break;
        }
    }

    public void Activate(bool isActive) {
        if (isActive) {
            transform.position = _avokadoTransform.position;
            rb.gravityScale = 0;
            col.isTrigger = true;
            Physics2D.IgnoreCollision(col, PlayerData.Instance.GetComponent<Collider2D>(), false);
        }
        rb.simulated = isActive;
        _sr.enabled = isActive;
    }

}
