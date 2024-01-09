namespace Lab1;

public class SecondPart
{
    public static void CycleShiftMatrix(int[][] matrix, int size)
    {
        int[][] newMatrix = new int[size][];
        for (int m = 0; m < size; m++)
        {
            newMatrix[m] = new int[size];
        }
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                // ÷иклическа€ перестановка элементов матрицы
                newMatrix[j][size - 1 - i] = matrix[i][j];
            }
        }
        //  опирование полученной перестановки обратно в исходную матрицу
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i][j] = newMatrix[i][j];
            }
        }
    }

    public static void PrintMatrix(int[][] matrix, int size)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                // ¬ывод полученной матрицы после циклических перестановок
                Console.Write(matrix[i][j] + "\t");
            }
            Console.WriteLine();
        }
    }
}