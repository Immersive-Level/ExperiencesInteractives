using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa TextMeshPro

public class PlayerController : MonoBehaviour
{
    public TMP_Text resultText; // Cambia el tipo a TMP_Text
    public KeyCode inputKey = KeyCode.None;

    private float pressTime;
    private bool hasPressed = false;
    private ReactionManager gameManager;

    public void Initialize(ReactionManager manager)
    {
        gameManager = manager;
        resultText.text = "";
    }

    private void Update()
    {
        if (!hasPressed && Input.GetKeyDown(inputKey))
        {
            pressTime = gameManager.Timer.currentTime;
            float difference = Mathf.Abs(pressTime - gameManager.TargetTime);

            resultText.text =
                "<size=60>" + pressTime.ToString("F3") + "s</size>\n" +
                "<size=30>Diferencia: " + difference.ToString("F3") + "s</size>";

            hasPressed = true;
            gameManager.ReportResult(this, difference);
        }
    }

    public void ResetPlayer()
    {
        hasPressed = false;
        resultText.text = "";
    }
}