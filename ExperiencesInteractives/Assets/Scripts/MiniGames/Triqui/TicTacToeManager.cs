using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform gridParent;
    public TMP_Text turnText;
    public TMP_Text resultText;
    public Button restartButton;

    private Cell[,] cells = new Cell[3, 3];
    private Board board;
    private char currentPlayer;
    private bool gameOver = false;

    public void StartGame()
    {
        board = new Board();
        board.Initialize();
        currentPlayer = 'X';
        gameOver = false;
        resultText.text = "";
        turnText.text = "Turno: X";
        restartButton.onClick.AddListener(RestartGame);

        // Instanciar celdas
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                GameObject cellObj = Instantiate(cellPrefab, gridParent);
                Cell cell = cellObj.GetComponent<Cell>();
                cell.Init(r, c, this);
                cells[r, c] = cell;

                Debug.Log($"Instanciando celda {r},{c}");
            }
        }
    }

    public void MakeMove(int row, int col)
    {
        if (gameOver || !board.IsCellEmpty(row, col)) return;

        board.SetCell(row, col, currentPlayer);
        cells[row, col].SetSymbol(currentPlayer.ToString());

        if (board.CheckWinner(currentPlayer))
        {
            resultText.text = "¡Ganador: " + currentPlayer + "!";
            gameOver = true;
        }
        else if (board.IsBoardFull())
        {
            resultText.text = "¡Empate!";
            gameOver = true;
        }
        else
        {
            currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
            turnText.text = "Turno: " + currentPlayer;
        }
    }

    public void EndGame() => gameOver = true;

    public bool IsGameOver() => gameOver;

    void Start()
    {
        StartGame(); // Muy importante
    }

    private void RestartGame()
    {
        StartGame();
    }
}
