using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

/*В одномерном массиве, состоящем из N вещественных элементов, вычислить:
1) количество элементов массива, равных нулю
2) сумму элементов массива, расположенных после минимального элемента.
*/


namespace Lab1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var objectFirstTask = new FirstPart();

            int[] nums3 = new[] { 1, 2, 3, 5, 2 };
            for (int i = 0; i < nums3.Length; i++)
            {
                Console.WriteLine(nums3[i]);
            }
            Console.WriteLine();

            Console.WriteLine("Количество: " + objectFirstTask.FirstTask(nums3));
            Console.WriteLine("Сумма: " + objectFirstTask.SecondTask(nums3));


            int size, count;

            Console.WriteLine("Пожалуйста, введите размерность матрицы:");
            size = Convert.ToInt32(Console.ReadLine());

            int[][] matrix = new int[size][];
            for (int i = 0; i < size; i++)
            {
                matrix[i] = new int[size];
            }

            Console.WriteLine("Введите элементы матрицы:");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // Ввод элементов матрицы с клавиатуры
                    matrix[i][j] = Convert.ToInt32(Console.ReadLine());
                }
            }

            Console.WriteLine("Введите количество перестановок:");
            count = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Исходная матрица:");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // Вывод исходной матрицы
                    Console.Write(matrix[i][j] + "\t");
                }
                Console.WriteLine();
            }

            // Выполнение указанного количества циклических перестановок
            for (int i = 0; i < count; i++)
            {
                SecondPart.CycleShiftMatrix(matrix, size);
            }

            Console.WriteLine("Новая матрица после перестановок:");
            SecondPart.PrintMatrix(matrix, size);
            Console.ReadKey();
        }
    }
}