public class RecursiveSudoku
{
    public int[,] Board { get; set; }
    public int Size => Board.GetLength(0);

    // Keep track of the numbers in the box, row, and column
    private bool[,] IsPuzzleValue {get; init;}
    private bool[,] RowMap;
    private bool[,] ColMap;
    private bool[,] BoxMap;

    private bool IsSolved {get; set;} = false;



    public RecursiveSudoku(int[,] board)
    {
        // Note to self, this is a shallow copy
        Board = board;

        RowMap = new bool[Size, Size];
        ColMap = new bool[Size, Size];
        BoxMap = new bool[Size, Size];
        IsPuzzleValue = new bool[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (board[i, j] != 0)
                {
                    IsPuzzleValue[i, j] = true;
                    RowMap[i, board[i, j] - 1] = true;
                    ColMap[j, board[i, j] - 1] = true;
                    BoxMap[GetBoxIndex(i, j), board[i, j] - 1] = true;
                }
            }
        }
    }

    // TODO: Exiting out of the recursion is not working properly, fix this
    public void Solve(int value = 1, int row = 0, int col = 0, bool forwards = true)
    {
        if (IsSolved)
        {
            return;
        }

        if (row == Size)
        {
            IsSolved = true;
            PrintBoard();
            return;
        }

        // Check if current cell holds an original puzzle value
        if (IsPuzzleValue[row, col] && forwards)
        {
            GetForwardCoords(ref row, ref col);
            Solve(1, row, col, true);
        } else if (IsPuzzleValue[row, col] && !forwards)
        {
            GetBackTrackCoords(ref row, ref col);
            int newValue = Board[row, col] + 1;
            Solve(newValue, row, col, false);
        }

        // Backtrack if the value is greater than the size of the board
        if (value > Size)
        {   
            ResetMaps(row, col);
            Board[row, col] = 0;

            GetBackTrackCoords(ref row, ref col);
            int newValue = Board[row, col] + 1;
            Solve(newValue, row, col, false);
        }


        // Check if the value is valid
        if (IsValid(value, row, col))
        {
            ResetMaps(row, col);

            Board[row, col] = value;

            RowMap[row, value - 1] = true;
            ColMap[col, value - 1] = true;
            BoxMap[GetBoxIndex(row, col), value - 1] = true;

            PrintBoard();

            GetForwardCoords(ref row, ref col);
            Solve(1, row, col, true);
        } else
        {
            Solve(value + 1, row, col, false);
        }
        
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

    private bool IsValid(int val, int row, int col)
    {
        if (val > Size || InRow(val, row) || InCol(val, col) || InBox(val, row, col))
        {
            return false;
        }

        return true;
    }

    // Gets the box number of current cell, indexed 0 to 8 starting from top left to bottom right
    private int GetBoxIndex(int row, int col)
    {
        int boxSize = (int)Math.Sqrt(Size);
        int boxRow = row / boxSize;
        int boxCol = col / boxSize;
        return boxCol + (boxRow * boxSize);
    }

    // Checks if the number is already in the current box
    private bool InBox(int val, int row, int col)
    {
        int boxIndex = GetBoxIndex(row, col);

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

    private void GetForwardCoords(ref int row, ref int col)
    {
        if (col == Size - 1)
        {
            col = 0;
            row++;
        }
        else
        {
            col++;
        }
    }

    private void GetBackTrackCoords(ref int row, ref int col)
    {
        if (col == 0)
        {
            col = Size - 1;
            row--;
        }
        else
        {
            col--;
        }
    }

    private void ResetMaps(int row, int col)
    {
        // Resets the tracking maps for the current cell if getting updated
        if (Board[row, col] != 0)
        {
            RowMap[row, Board[row, col] - 1] = false;
            ColMap[col, Board[row, col] - 1] = false;
            BoxMap[GetBoxIndex(row, col), Board[row, col] - 1] = false;
        }
    }

}