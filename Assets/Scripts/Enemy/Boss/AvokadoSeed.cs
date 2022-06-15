using UnityEngine;

public class AvokadoSeed : MonoBehaviour {

    private Transform _avokadoTransform;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private void Start() {
        _avokadoTransform = transform.parent;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
            case "Player":
                PlayerData.Instance.TakeDamage(1, _avokadoTransform.GetComponent<AvokadoBoss>()._seedKb);
                break;
            case "Ground":

                break;
        }
    }

    public void Activate(bool isActive) {
        if (isActive) transform.position = _avokadoTransform.position;
        _rb.simulated = isActive;
        _sr.enabled = isActive;
    }

}
