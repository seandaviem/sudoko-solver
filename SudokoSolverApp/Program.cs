/*int[,] numbers = {{0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0}
                };
*/

int[,] numbers = {{0, 0, 2, 0},
                  {1, 0, 0, 0},
                  {0, 0, 0, 4},
                  {0, 4, 0, 0},
                };

Sudoku sudoku = new Sudoku(numbers);


public class Sudoku
{
    public int[,] Board { get; set; }
    public int Size => Board.GetLength(0);

    // Keep track of the numbers in the box, row, and column
    private bool[,] BoxMap => new bool[Size, Size];
    private bool[,] RowMap => new bool[Size, Size];
    private bool[,] ColMap => new bool[Size, Size];



    public Sudoku(int[,] board)
    {
        // Note to self, this is a shallow copy
        Board = board;
    }

    private void PrintBoard()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Console.Write(Board[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    // Gets the box number of current cell, indexed 0 to 8 starting from top left to bottom right
    private int GetBoxIndex(int col, int row)
    {
        int boxSize = (int)Math.Sqrt(Size);
        int boxRow = row / boxSize;
        int boxCol = col / boxSize;
        return boxCol + (boxRow * boxSize);
    }

    // Checks if the number is already in the current box
    private bool InBox(int val, int col, int row)
    {
        int boxIndex = GetBoxIndex(col, row);

        return BoxMap[boxIndex, val - 1];
    
    }

    // Checks if the number is already in the current row
    private bool InRow(int val, int row)
    {
        return RowMap[row, val - 1];
    }

    // Checks if the number is already in the current column
    private bool InCol(int val, int col)
    {
        return ColMap[col, val - 1];
    }

}

