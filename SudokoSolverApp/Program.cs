﻿int[,] numbers = {{0, 0, 5, 7, 0, 9, 3, 0, 0},
                  {0, 6, 0, 0, 0, 0, 0, 0, 1},
                  {0, 2, 3, 4, 6, 0, 0, 0, 0},
                  {5, 3, 0, 8, 2, 7, 4, 6, 0},
                  {4, 7, 0, 0, 0, 0, 0, 0, 2},
                  {9, 8, 0, 6, 5, 0, 1, 3, 7},
                  {0, 0, 7, 0, 9, 0, 0, 0, 0},
                  {0, 0, 0, 2, 0, 6, 0, 1, 5},
                  {0, 5, 0, 0, 0, 0, 6, 9, 0}
                };


// int[,] numbers = {{0, 0, 2, 0},
//                   {1, 0, 0, 0},
//                   {0, 0, 0, 4},
//                   {0, 4, 0, 0},
//                 };


Sudoku sudoku = new Sudoku(numbers);
sudoku.Solve();

// RecursiveSudoku recursiveSudoku = new RecursiveSudoku(numbers);
// recursiveSudoku.Solve();




