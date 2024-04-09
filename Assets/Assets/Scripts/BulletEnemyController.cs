using UnityEngine;

public class BulletEnemyController : MonoBehaviour
{
    [SerializeField] public Vector2 Direction { get; set; }
    [SerializeField] private float speed;
    private Rigidbody2D _compRigidbody2D;
    private void Awake()
    {
        _compRigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _compRigidbody2D.velocity = Direction * speed;
    }
}
