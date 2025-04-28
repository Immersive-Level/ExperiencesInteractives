using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public bool isLeftPlayer = true; // Activa esta casilla en el Inspector para el jugador izquierdo
    public float moveSpeed = 10f;

    private Camera mainCamera;
    private float screenHalfWidth;

    private void Start()
    {
        mainCamera = Camera.main;
        screenHalfWidth = Screen.width / 2f;
    }

    void Update()
    {
           SimulateTouchWithMouse();
    }
    void SimulateTouchWithMouse()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            if (IsTouchOnCorrectSide(mousePos.x))
            {
                MovePaddle(mousePos);
            }
        }
    }

    bool IsTouchOnCorrectSide(float xPos)
    {
        return (isLeftPlayer && xPos < screenHalfWidth) || (!isLeftPlayer && xPos >= screenHalfWidth);
    }

    void MovePaddle(Vector3 screenPosition)
    {
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPosition);
        Vector3 paddlePos = transform.position;

        paddlePos.y = Mathf.Lerp(paddlePos.y, worldPos.y, Time.deltaTime * moveSpeed);
        paddlePos.y = Mathf.Clamp(paddlePos.y, -3.5f, 3.5f);

        transform.position = paddlePos;
    }
}
