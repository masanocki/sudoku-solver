public class Sudoku_Solver
{
    private int[,] Matrix;

    public Sudoku_Solver(string path)
    {
        Matrix = new int[9, 9];

        string content = File.ReadAllText(path);
        int counter = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (content[counter] == '.')
                {
                    Matrix[i, j] = 0;
                }
                else
                {
                    Matrix[i, j] = int.Parse(content[counter].ToString());
                }
                counter++;
            }
        }
    }

    public void printSudoku()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Console.Write("{0} ", Matrix[i, j]);
                if (j == 2 || j == 5)
                {
                    Console.Write('|');
                }
            }
            Console.WriteLine();
            if (i == 2 || i == 5)
            {
                Console.WriteLine("------+------+-----");
            }
        }
    }

    private bool checkValue(int[,] matrix, int value, int row, int column)
    {
        // check row
        for (int i = 0; i < 9; i++)
        {
            if (matrix[row, i] == value)
            {                
                return false;
            }
        }

        // check column
        for (int i = 0; i < 9; i++)
        {
            if (matrix[i, column] == value)
            {
                return false;
            }
        }

        // check 3x3 box
        int row3 = Convert.ToInt32((Math.Floor((double)(row/3))*3));
        int col3 = Convert.ToInt32((Math.Floor((double)(column/3))*3));

        for (int i = row3; i < row3+3; i++)
        {
            for (int j = col3; j < col3+3; j++)
            {
                if (matrix[i, j] == value)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool solveSudoku(int row = 0, int col = 0)
    {
        int[,]? checkMatrix = Matrix.Clone() as int[,];
        if (col == 9)
        {
            if (row == 8)
            {
                printSudoku();
                return true;
            }
            col = 0;
            row++;
        }
        if (Matrix[row, col] == 0)
        {
            for (int val = 1; val < 10; val++)
            {
                Matrix[row, col] = val;
                if (checkValue(checkMatrix!, val, row, col))
                {
                    solveSudoku(row, col+1);
                }
            }
            Matrix[row, col] = 0;
        }
        else
            solveSudoku(row, col+1);
        return false;
    }

}