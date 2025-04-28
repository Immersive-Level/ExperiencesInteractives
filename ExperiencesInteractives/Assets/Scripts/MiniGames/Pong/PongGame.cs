using UnityEngine;

public class PongGame : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Pong comenz�");
    }

    public void EndGame()
    {
        Debug.Log("Pong termin�");
    }

    public string GetGameName()
    {
        return "Pong";
    }

    void Start()
    {
        StartGame(); // se llama al iniciar la escena
    }

    public BallController ball;
    public Transform ballSpawnPoint;
    public void PlayerScored(string player)
    {
        Debug.Log("¡Punto para el jugador: " + player + "!");
        ResetBall();
    }

    void ResetBall()
    {
        // Mueve la pelota al centro
        ball.transform.position = ballSpawnPoint.position;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Espera 1 segundo y luego la relanza
        Invoke(nameof(RestartBall), 1f);
    }

    void RestartBall()
    {
        ball.Launch(); // ← Este método debe ser público en BallController
    }
}
