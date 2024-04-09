using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] puntosDeControl;
    private Rigidbody2D _compRigidBody2D;
    [SerializeField] private AnimatorController animatorController;
    private SpriteRenderer _compSpriteRenderer;
    [SerializeField] private float velocidad;
    private Transform posicionActualObjetivo;
    private int posicionDelPatrullero = 0;
    private int estadoDeAnimacion = 1;
    [SerializeField] private float velocidadAumentada;
    [SerializeField] private LayerMask capaDeDeteccion;
    [SerializeField] private float distanciaDeteccion;
    private bool jugadorDetectado = false;
    void Awake()
    {
        _compRigidBody2D = GetComponent<Rigidbody2D>();
        _compSpriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void Start() {
        posicionActualObjetivo = puntosDeControl[posicionDelPatrullero];
        transform.position = posicionActualObjetivo.position;
    }

    private void Update() {
        VerificarNuevoPunto();
        animatorController.SetAnimationState(estadoDeAnimacion);
    }

    private void VerificarNuevoPunto()
    {
        if (Mathf.Abs((transform.position - posicionActualObjetivo.position).magnitude) < 0.25)
        {
            posicionDelPatrullero = posicionDelPatrullero + 1 == puntosDeControl.Length ? 0 : posicionDelPatrullero + 1;
            posicionActualObjetivo = puntosDeControl[posicionDelPatrullero];      
            CheckFlip(_compRigidBody2D.velocity.x);
        }
    }

    private void CheckFlip(float posicionX){
        _compSpriteRenderer.flipX = (posicionX - transform.position.x) < 0;
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _compRigidBody2D.velocity, distanciaDeteccion, capaDeDeteccion);
        if (hit.collider != null)
        {
            jugadorDetectado = true;
            Debug.DrawRay(transform.position, _compRigidBody2D.velocity.normalized * distanciaDeteccion, Color.green);
        }
        else
        {
            jugadorDetectado = false;
            Debug.DrawRay(transform.position, _compRigidBody2D.velocity.normalized * distanciaDeteccion, Color.red);
        }
        _compRigidBody2D.velocity = (posicionActualObjetivo.position - transform.position).normalized * (jugadorDetectado ? velocidadAumentada : velocidad);
    }
}
