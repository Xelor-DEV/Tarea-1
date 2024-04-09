using UnityEngine;

public class WizardController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] public GameObject projectile;
    private Vector3 initialPosition;
    [SerializeField] private Rigidbody2D _compRigidbody2D;
    private bool isInRange = false;
    private float timeSinceLastShot = 0.0f;
    private float shotDelay = 1.0f;
    private bool isReturning = false;
    private Vector2 direction;
    private void Awake()
    {
        _compRigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        initialPosition = transform.position;
    }
    private void Update()
    {
        timeSinceLastShot = timeSinceLastShot + Time.deltaTime;

        if (isInRange == true)
        {
            direction = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
            _compRigidbody2D.velocity = direction * speed;

            if (timeSinceLastShot >= shotDelay)
            {
                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                newProjectile.GetComponent<BulletEnemyController>().Direction = direction;
                timeSinceLastShot = 0.0f;
            }
        }

        if (isReturning && Vector2.Distance(transform.position, initialPosition) < 0.1f)
        {
            _compRigidbody2D.velocity = Vector2.zero;
            isReturning = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInRange = true;
            isReturning = false;
            direction = (collision.gameObject.transform.position - transform.position).normalized;
            _compRigidbody2D.velocity = direction * speed;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            direction = (collision.gameObject.transform.position - transform.position).normalized;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInRange = false;
            isReturning = true;
            direction = (initialPosition - transform.position).normalized;
            _compRigidbody2D.velocity = direction * speed;
        }
    }
}
