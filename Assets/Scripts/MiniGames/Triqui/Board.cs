using UnityEngine;

public class Board
{
    private char[,] grid = new char[3, 3];

    public void Initialize()
    {
        for (int r = 0; r < 3; r++)
            for (int c = 0; c < 3; c++)
                grid[r, c] = ' ';
    }

    public bool IsCellEmpty(int row, int col)
    {
        return grid[row, col] == ' ';
    }

    public void SetCell(int row, int col, char player)
    {
        grid[row, col] = player;
    }

    public bool CheckWinner(char player)
    {
        // Check rows, columns, and diagonals
        for (int i = 0; i < 3; i++)
            if ((grid[i, 0] == player && grid[i, 1] == player && grid[i, 2] == player) ||
                (grid[0, i] == player && grid[1, i] == player && grid[2, i] == player))
                return true;

        return (grid[0, 0] == player && grid[1, 1] == player && grid[2, 2] == player) ||
               (grid[0, 2] == player && grid[1, 1] == player && grid[2, 0] == player);
    }

    public bool IsBoardFull()
    {
        for (int r = 0; r < 3; r++)
            for (int c = 0; c < 3; c++)
                if (grid[r, c] == ' ')
                    return false;
        return true;
    }

    public void PrintBoard()
    {
        string display = "";
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                display += $"[{grid[r, c]}]";
            }
            display += "\n";
        }
        Debug.Log(display);
    }
}
