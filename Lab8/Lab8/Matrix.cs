namespace Lab8
{
    public static class Matrix
    {
        public static int DotProduct(int[] v1, int[] v2)
        {
            int sum = 0;

            for (int i = 0; i < v1.Length; ++i)
            {
                sum += v1[i] * v2[i];
            }

            return sum;
        }

        public static int[,] Transpose(int[,] matrix)
        {
            int[,] outMatrix = new int[matrix.GetLength(1), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    outMatrix[j, i] = matrix[i, j];
                }
            }

            return outMatrix;
        }
    }
}

