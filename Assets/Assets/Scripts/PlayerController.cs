using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _compRigidbody2D;
    private Vector2 direccion;
    private Vector2 ultimaDireccion = new Vector2(1, 0);
    [SerializeField] private int velocidad;
    private int estadoDeAnimacion = 0;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private GameObject bulletPrefab;
    void Awake()
    {
        _compRigidbody2D = GetComponent<Rigidbody2D>(); 
    }
    private void Start()
    {
        animatorController.SetAnimationState(estadoDeAnimacion);
    }
    public void Movimiento(InputAction.CallbackContext context)
    {
        direccion = context.ReadValue<Vector2>();
        if (direccion != Vector2.zero)
        {
            ultimaDireccion = direccion;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = ultimaDireccion * velocidad;
            BulletController bulletController = bullet.GetComponent<BulletController>();
            bulletController.Direction = ultimaDireccion;
        }
    }
    void FixedUpdate()
    {
        //Movimiento
        if (direccion.x == 0 && direccion.y == 0)
        {
            estadoDeAnimacion = 0;
            animatorController.SetAnimationState(estadoDeAnimacion);
        }
        else
        {
            estadoDeAnimacion = 1;
            animatorController.SetAnimationState(estadoDeAnimacion);
        }
        _compRigidbody2D.velocity = new Vector2(direccion.x * velocidad, direccion.y * velocidad);
    }
}
