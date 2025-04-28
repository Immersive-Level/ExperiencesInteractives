using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public TMP_Text symbolText;
    private int row;
    private int col;
    private TicTacToeManager gameManager;

    public void Init(int r, int c, TicTacToeManager manager)
    {
        row = r;
        col = c;
        gameManager = manager;
        GetComponent<Button>().onClick.AddListener(OnClick);
        symbolText.text = "";
    }

    private void OnClick()
    {
        gameManager.MakeMove(row, col);
    }

    public void SetSymbol(string symbol)
    {
        symbolText.text = symbol;
        GetComponent<Button>().interactable = false;
    }
}
