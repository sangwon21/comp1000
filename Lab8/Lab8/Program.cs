using System;
using System.Diagnostics;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] vector1 = new int[] { 4, 7, 8, -1, 2, 5, -5, -6 };
            int[] vector2 = new int[] { 5, -6, 7, 1, 4, 6, 9, -3 };

            int dotProduct = Matrix.DotProduct(vector1, vector2);

            Debug.Assert(dotProduct == 44);

            int[,] matrix1 = new int[3, 7]
            {
                { 4, -4, 6, 4, 19, -1, 2 },
                { 6, -77, 4, 2, 5, 7, 7 },
                { 5, 8, -3, -22, 6, 6, 10 }
            };

            int[,] expectedTransposed = new int[7, 3]
            {
                { 4, 6, 5 },
                { -4, -77, 8 },
                { 6, 4, -3 },
                { 4, 2, -22 },
                { 19, 5, 6 },
                { -1, 7, 6 },
                { 2, 7, 10 }
            };

            int[,] transposed = Matrix.Transpose(matrix1);
            printMatrix(transposed);

            Debug.Assert(areMatricesEqual(expectedTransposed, transposed));

            int[,] expectedIdentityMatrix = new int[5, 5]
            {
                { 1, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 0, 0, 1 }
            };

            int[,] identityMatrix = Matrix.GetIdentityMatrix(5);
            printMatrix(identityMatrix);

            Debug.Assert(areMatricesEqual(expectedIdentityMatrix, identityMatrix));

            int[] row = Matrix.GetRowOrNull(matrix1, 0);
            Debug.Assert(areVectorsEqual(new int[] { 4, -4, 6, 4, 19, -1, 2 }, row));

            row = Matrix.GetRowOrNull(transposed, 3);
            Debug.Assert(areVectorsEqual(new int[] { 4, 2, -22 }, row));

            int[] column = Matrix.GetColumnOrNull(matrix1, 1);
            Debug.Assert(areVectorsEqual(new int[] { -4, -77, 8 }, column));

            column = Matrix.GetColumnOrNull(transposed, 1);
            Debug.Assert(areVectorsEqual(new int[] { 6, -77, 4, 2, 5, 7, 7 }, column));

            int[] matVecProduct = Matrix.MultiplyMatrixVectorOrNull(matrix1, new int[] { 5, -5, 3, 5, 2, 1, -1 });
            Debug.Assert(areVectorsEqual(new int[] { 113, 447, -126 }, matVecProduct));

            matVecProduct = Matrix.MultiplyMatrixVectorOrNull(transposed, new int[] { 5, -5, 3, 5, 2, 1, -1 });
            Debug.Assert(matVecProduct == null);

            int[] vecMatProduct = Matrix.MultiplyVectorMatrixOrNull(new int[] { 5, -5, 3, 5, 2, 1, -1 }, transposed);
            Debug.Assert(areVectorsEqual(new int[] { 113, 447, -126 }, vecMatProduct));

            vecMatProduct = Matrix.MultiplyVectorMatrixOrNull(new int[] { 5, -5, 3, 5, 2, 1, -1 }, matrix1);
            Debug.Assert(vecMatProduct == null);

            int[,] matrix2 = new int[5, 4]
            {
                { 4, -4, 6, 4 },
                { 6, -77, 4, 2 },
                { 5, 8, -3, -22 },
                { 3, 2, -11, 5 },
                { 9, 1, -2, -9 }
            };

            Debug.Assert(Matrix.MultiplyOrNull(matrix1, matrix1) == null);
            Debug.Assert(Matrix.MultiplyOrNull(matrix1, matrix2) == null);

            int[,] matrix3 = new int[4, 7]
            {
                { 10, -2, 11, -4, 77, 3, 1 },
                { -1, -1, 5, -4, 4, 11, 1 },
                { 4, 6, -9, 100, 12, 56, 20 },
                { 7, 8, 6, 5, 1, -1, 6 }
            };

            int[,] expectedProduct = new int[5, 7]
            {
                { 96, 64, -6, 620, 368, 300, 144 },
                { 167, 105, -343, 694, 204, -607, 21 },
                { -124, -212, -10, -462, 359, -43, -179 },
                { 19, -34, 172, -1095, 112, -590, -185 },
                { 18, -103, 68, -285, 664, -65, -84 }
            };

            int[,] matProduct = Matrix.MultiplyOrNull(matrix2, matrix3);
            printMatrix(matProduct);

            Debug.Assert(areMatricesEqual(expectedProduct, matProduct));
        }

        private static bool areVectorsEqual(int[] expected, int[] actual)
        {
            if (expected.Length != actual.Length)
            {
                return false;
            }

            for (int i = 0; i < expected.Length; i++)
            {
                if (expected[i] != actual[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static bool areMatricesEqual(int[,] expected, int[,] actual)
        {
            if (expected.GetLength(0) != actual.GetLength(0)
                || expected.GetLength(1) != actual.GetLength(1))
            {
                return false;
            }

            int row = expected.GetLength(0);
            int column = expected.GetLength(1);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (expected[i, j] != actual[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void printMatrix(int[,] matrix)
        {
            Console.WriteLine("---------------------------------");

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0, -6} ", matrix[i, j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine("---------------------------------");
        }
    }
}