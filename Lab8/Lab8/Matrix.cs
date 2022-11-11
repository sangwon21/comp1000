using System;
namespace Lab8
{
    public static class Matrix
    {
        public static int DotProduct(int[] v1, int[] v2)
        {
            int sum = 0;

            for(int i = 0; i < v1.Length; ++i)
            {
                sum += v1[i] * v2[i];
            }

            return sum;
        }
    }
}

