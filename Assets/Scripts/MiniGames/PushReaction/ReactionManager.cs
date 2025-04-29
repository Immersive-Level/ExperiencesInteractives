using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ReactionManager : MonoBehaviour
{
    public Timer Timer;
    public Text targetTimeText;
    public Button startButton;
    public List<PlayerController> players = new List<PlayerController>();
    public float TargetTime { get; private set; }
    public bool IsGameActive { get; private set; }

    private Dictionary<PlayerController, float> playerResults = new Dictionary<PlayerController, float>();

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        foreach (var player in players)
        {
            player.Initialize(this);
        }
    }

    public void StartGame()
    {
        TargetTime = Random.Range(3f, 7f);
        targetTimeText.text = "Tiempo Objetivo: " + TargetTime.ToString("F3") + "s";
        Timer.StartTimer();
        IsGameActive = true;
        playerResults.Clear();
        foreach (var player in players)
        {
            player.ResetPlayer();
        }
    }

    public void ReportResult(PlayerController player, float difference)
    {
        playerResults[player] = difference;
        if (playerResults.Count == players.Count)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Timer.StopTimer();
        IsGameActive = false;

        float minDifference = float.MaxValue;
        PlayerController winner = null;

        foreach (var result in playerResults)
        {
            if (result.Value < minDifference)
            {
                minDifference = result.Value;
                winner = result.Key;
            }
        }

        foreach (var player in players)
        {
            if (player == winner)
            {
                player.resultText.text += "<size=70><color=yellow> ¡Ganador! </color> </size>";
            }
        }
    }
}
