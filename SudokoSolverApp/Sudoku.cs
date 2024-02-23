public class Sudoku
{
    public int[,] Board { get; set; }
    public int Size => Board.GetLength(0);

    // Keep track of the numbers in the box, row, and column
    private bool[,] IsPuzzleValue {get; init;}
    private bool[,] RowMap;
    private bool[,] ColMap;
    private bool[,] BoxMap;

    private bool IsSolved {get; set;} = false;



    public Sudoku(int[,] board)
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

    public void Solve()
    {

        bool forwards = true;
        int val = 1;

        for (int row = 0; row < Size; row++)
        {
            for (int col = 0; col < Size; col++)
            {
                if (IsPuzzleValue[row, col] && forwards)
                {
                    val = 1;
                }
                else if (IsPuzzleValue[row, col] && !forwards)
                {
                    GetBackTrackCoords(ref val, ref row, ref col);
                } else
                {
                    while (val <= Size)
                    {
                        if (IsValid(val, row, col))
                        {
                            ResetMaps(row, col);
                            Board[row, col] = val;
                            RowMap[row, val - 1] = true;
                            ColMap[col, val - 1] = true;
                            BoxMap[GetBoxIndex(row, col), val - 1] = true;
                            PrintBoard();
                            val = 1;
                            forwards = true;
                            break;
                        } else if (val == Size)
                        {
                            ResetMaps(row, col);
                            Board[row, col] = 0;
                            forwards = false;
                            GetBackTrackCoords(ref val, ref row, ref col);
                            break;
                        } else
                        {
                            val++;
                        }
                    }
                }
            }
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

    private void GetBackTrackCoords(ref int val, ref int row, ref int col)
    {
        if (col == 0)
        {
            val = Board[row - 1, Size - 1];
            col = Size - 2;
            row--;
        }
        else
        {
            val = Board[row, col - 1];
            col -= 2;
        }
    }

    private void ResetMaps(int row, int col)
    {
        if (Board[row, col] != 0)
        {
            RowMap[row, Board[row, col] - 1] = false;
            ColMap[col, Board[row, col] - 1] = false;
            BoxMap[GetBoxIndex(row, col), Board[row, col] - 1] = false;
        }
    }

}