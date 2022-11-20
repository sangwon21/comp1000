﻿using System;
using System.Diagnostics;
using Assignment4.Image;

namespace Assignment4
{
    public static class SignalProcessor
    {
        private static double getGaussianFunction(double sigma, int x)
        {
            const double e = Math.E;
            double sigmaSquare = Math.Pow(sigma, 2);
            double xSquare = Math.Pow(x, 2);

            return Math.Pow(e, -xSquare / (2 * sigmaSquare)) * (1 / (sigma * Math.Sqrt(2 * Math.PI)));
        }

        public static double[] GetGaussianFilter1D(double sigma)
        {
            // length가 0이 되면 안 된다
            int length = (int)Math.Floor(sigma * 6);
            Debug.Assert(length > 0);

            if (length % 2 == 0)
            {
                ++length;
            }

            double[] outArray = new double[length];
            int middleIndex = length / 2;

            for (int i = 0; i < length; ++i)
            {
                outArray[i] = getGaussianFunction(sigma, i - middleIndex);
            }

            return outArray;
        }

        // f: signal, g: filter
        public static double[] Convolve1D(double[] signal, double[] filter)
        {
            double[] outArray = new double[signal.Length];

            for (int i = 0; i < signal.Length; ++i)
            {
                for (int j = 0; j < filter.Length; ++j)
                {
                    int toMultiplySignalIndex = i - j + filter.Length / 2;

                    if (toMultiplySignalIndex < 0 || toMultiplySignalIndex >= filter.Length)
                    {
                        continue;
                    }

                    // reverse한 배열을 곱해야 한다!
                    outArray[i] += filter[filter.Length - j] * signal[toMultiplySignalIndex];
                }
            }

            return outArray;
        }

        public static double[,] GetGaussianFilter2D(double sigma)
        {
            return null;
        }

        public static Bitmap ConvolveImage(Bitmap bitmap, double[,] filter)
        {
            return null;
        }
    }
}