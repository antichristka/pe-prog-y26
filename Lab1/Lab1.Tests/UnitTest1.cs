using System.Drawing;

namespace Lab1.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void SecondPart_Test1()
        {
            int[,] matrix = new int[5, 5];
            

            int counter = 1;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrix[i, j] = counter;
                    counter++;
                }
            }

            int[][] arrayOfArrays = new int[5][];
            for (int i = 0; i < 5; i++)
            {
                arrayOfArrays[i] = new int[5];
                for (int j = 0; j < 5; j++)
                {
                    arrayOfArrays[i][j] = matrix[i, j];
                }
            }

            SecondPart.CycleShiftMatrix(arrayOfArrays, 5);
            int[,] result = new int[5, 5]
            {
                { 21, 16, 11, 6, 1 },
                { 22, 17, 12, 7, 2 },
                { 23, 18, 13, 8, 3 },
                { 24, 19, 14, 9, 4 },
                { 25, 20, 15, 10, 5 }
            };

            int[][] resultMatrix = new int[5][];
            for (int i = 0; i < 5; i++)
            {
                resultMatrix[i] = new int[5];
                for (int j = 0; j < 5; j++)
                {
                    resultMatrix[i][j] = result[i, j];
                }
            }
            Assert.Equal(arrayOfArrays, resultMatrix);
        }

        [Fact]
        public void FirstPart_Test2()
        {
            int[] array = new int[5];


            int counter = -1;
            for (int i = 0; i < 5; i++)
            {
                array[i] = counter; 
                counter++;
            }
           
            

            Assert.Equal(1, new FirstPart().FirstTask(array));
        }

        [Fact]
        public void FirstPart_Test3()
        {
            int[] array = new int[5];


            int counter = -1;
            for (int i = 0; i < 5; i++)
            {
                array[i] = counter;
                counter++;
            }



            Assert.Equal(-1+0+1+2+3, new FirstPart().SecondTask(array));
        }
    }
}