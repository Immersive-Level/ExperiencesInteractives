using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 8f;
    public float randomBounceAngle = 0.25f; // Máximo ángulo de variación
    public float randomChance = 0.2f; // 20% de probabilidad de rebote aleatorio
    private Rigidbody2D rb;

    public PongGame pongGame; // Referencia al manager para reiniciar o mostrar derrota

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();
    }

    public void Launch()
    {
        // Dirección aleatoria izquierda o derecha
        float x = Random.value < 0.5f ? -1 : 1;
        float y = Random.Range(-1f, 1f);
        rb.velocity = new Vector2(x, y).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float paddleY = collision.transform.position.y;
        float ballY = transform.position.y;

        float difference = ballY - paddleY;
        float maxOffset = collision.collider.bounds.size.y / 2f;

        // Limitar la inclinación vertical (máximo ángulo)
        float directionY = Mathf.Clamp(difference / maxOffset, -0.8f, 0.8f);

        // Dirección horizontal actual
        float directionX = Mathf.Sign(rb.velocity.x);

        float currentSpeed = rb.velocity.magnitude;

        Vector2 newDirection = new Vector2(directionX, directionY).normalized;

        rb.velocity = newDirection * currentSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GoalLeft"))
        {
            pongGame.PlayerScored("Right");
        }
        else if (other.CompareTag("GoalRight"))
        {
            pongGame.PlayerScored("Left");
        }
    }
}
