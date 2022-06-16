using UnityEngine;

public class AvokadoSeed : MonoBehaviour {

    private Transform _avokadoTransform;
    [HideInInspector] public Rigidbody2D rb;
    private SpriteRenderer _sr;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        _avokadoTransform = transform.parent;
        transform.parent = null;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
            case "Player":
                PlayerData.Instance.TakeDamage(1, Mathf.Sign(rb.velocity.x) * _avokadoTransform.GetComponent<AvokadoBoss>()._seedKb);
                break;
            case "Ground":

                break;
        }
    }

    public void Activate(bool isActive) {
        if (isActive) transform.position = _avokadoTransform.position;
        rb.simulated = isActive;
        _sr.enabled = isActive;
    }

}
