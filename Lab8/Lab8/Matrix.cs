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

        public static int[,] GetIdentityMatrix(int size)
        {
            int[,] outMatrix = new int[size, size];

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    outMatrix[i, j] = i == j ? 1 : 0;
                }
            }

            return outMatrix;
        }

        public static int[] GetRowOrNull(int[,] matrix, int row)
        {
            if (matrix.GetLength(0) <= row || row < 0)
            {
                return null;
            }

            int[] outRow = new int[matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(1); ++i)
            {
                outRow[i] = matrix[row, i];
            }

            return outRow;
        }

        public static int[] GetColumnOrNull(int[,] matrix, int col)
        {
            if (matrix.GetLength(1) <= col || col < 0)
            {
                return null;
            }

            int[] outCol = new int[matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                outCol[i] = matrix[i, col];
            }

            return outCol;
        }

        public static int[] MultiplyMatrixVectorOrNull(int[,] matrix, int[] vector)
        {
            if (matrix.GetLength(1) != vector.Length)
            {
                return null;
            }

            int[] outVector = new int[matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                int sum = 0;
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    sum += matrix[i, j] * vector[j];
                }
                outVector[i] = sum;
            }

            return outVector;
        }

        public static int[] MultiplyVectorMatrixOrNull(int[] vector, int[,] matrix)
        {
            if (vector.Length != matrix.GetLength(0))
            {
                return null;
            }

            int[] outVector = new int[matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(1); ++i)
            {
                int sum = 0;
                for (int j = 0; j < matrix.GetLength(0); ++j)
                {
                    sum += matrix[j, i] * vector[j];
                }
                outVector[i] = sum;
            }

            return outVector;
        }

        public static int[,] MultiplyOrNull(int[,] multiplicandMatrix, int[,] multiplierMatrix)
        {
            if (multiplicandMatrix.GetLength(1) != multiplierMatrix.GetLength(0))
            {
                return null;
            }

            int[,] product = new int[multiplicandMatrix.GetLength(0), multiplierMatrix.GetLength(1)];

            for (int i = 0; i < multiplicandMatrix.GetLength(0); ++i)
            {
                for (int j = 0; j < multiplierMatrix.GetLength(1); ++j)
                {
                    int[] row = GetRowOrNull(multiplicandMatrix, i);
                    int[] col = GetColumnOrNull(multiplierMatrix, j);

                    product[i, j] = DotProduct(row, col);
                }
            }

            return product;
        }
    }
}

