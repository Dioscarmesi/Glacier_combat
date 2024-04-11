using UnityEngine;

public class movimiento : MonoBehaviour
{
    public float jumpForce = 10f; // Fuerza del salto
    public float forwardSpeed = 5f; // Velocidad de avance en la dirección del personaje
    public float maxJumpForce = 20f; // Fuerza máxima del salto
    public float maxForwardSpeed = 20f; // Velocidad máxima de avance
    public float maxJumpTime = 1.0f; // Tiempo máximo de salto
    private Rigidbody rb;
    private bool isJumping = false;
    private bool canMove = true; // Variable para controlar si el personaje puede moverse, rotar y saltar
    private float jumpTime = 0f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el componente SpriteRenderer
        // Bloquear rotación en X y Z para que el personaje no se incline
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (canMove)
        {
            // Verificar si se presiona la tecla A (izquierda)
            if (Input.GetKeyDown(KeyCode.A))
            {
                // Girar el objeto hacia la izquierda
                spriteRenderer.flipX = true; // Invertir la escala en el eje X
            }
            // Verificar si se presiona la tecla D (derecha)
            else if (Input.GetKeyDown(KeyCode.D))
            {
                // Girar el objeto hacia la derecha
                spriteRenderer.flipX = false; // Restaurar la escala en el eje X
            }

            // Procesar el salto si puede saltar y se presiona el botón de salto
            if (canMove && Input.GetKeyDown(KeyCode.W))
            {
                isJumping = true;
                canMove = false; // Desactivar la capacidad de moverse, rotar y saltar mientras está en el aire
            }
        }

        // Controlar la duración del salto
        if (isJumping && Input.GetKey(KeyCode.W))
        {
            if (jumpTime < maxJumpTime)
            {
                jumpTime += Time.deltaTime;
            }
        }

        // Aplicar la fuerza del salto cuando se suelta el botón de salto
        if (isJumping && Input.GetKeyUp(KeyCode.W))
        {
            // Restablecer canMove incluso si el salto no se ejecuta correctamente
            canMove = true;

            // Calcular la fuerza de salto basada en el tiempo de salto
            float normalizedJumpTime = Mathf.Clamp01(jumpTime / maxJumpTime);
            jumpForce = Mathf.Lerp(jumpForce, maxJumpForce, normalizedJumpTime);

            // Calcular la velocidad de avance basada en el tiempo de salto
            float normalizedForwardTime = Mathf.Clamp01(jumpTime / maxJumpTime);
            forwardSpeed = Mathf.Lerp(forwardSpeed, maxForwardSpeed, normalizedForwardTime);

            // Calcular la dirección del salto basada en la dirección en la que mira el personaje
            Vector3 jumpDirection = (spriteRenderer.flipX ? -transform.right : transform.right) * forwardSpeed + Vector3.up * jumpForce;
            rb.AddForce(jumpDirection, ForceMode.Impulse);

            // Reiniciar el tiempo de salto fuerza hacia arriba y adelante
            jumpForce = 0f;
            forwardSpeed = 0f;
            jumpTime = 0f;
            isJumping = false;
        }


    }

    // Detectar cuando el personaje toca el suelo
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            jumpTime = 0f;
            canMove = true; // Activar la capacidad de moverse, rotar y saltar cuando toca el suelo
        }
    }
}
