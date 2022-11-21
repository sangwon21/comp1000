using System;
using System.Diagnostics;
using Assignment4.Image;

namespace Assignment4
{
    public static class SignalProcessor
    {
        private static double getGaussianFunctionFor1D(double sigma, int x)
        {
            const double E = Math.E;
            double sigmaSquare = Math.Pow(sigma, 2);
            double xSquare = Math.Pow(x, 2);

            return Math.Pow(E, -xSquare / (2 * sigmaSquare)) * (1 / (sigma * Math.Sqrt(2 * Math.PI)));
        }

        private static double getGaussianFunctionFor2D(double sigma, int y, int x)
        {
            const double E = Math.E;
            double sigmaSquare = Math.Pow(sigma, 2);
            double xSquare = Math.Pow(x, 2);
            double ySquare = Math.Pow(y, 2);

            return Math.Pow(E, -(xSquare + ySquare) / (2 * sigmaSquare)) * (1 / (2 * sigmaSquare * Math.PI));
        }

        public static double[] GetGaussianFilter1D(double sigma)
        {
            // length가 0이 되면 안 된다
            int length = (int)Math.Ceiling(sigma * 6);
            Debug.Assert(length > 0);

            if (length % 2 == 0)
            {
                ++length;
            }

            double[] outArray = new double[length];
            int middleIndex = length / 2;

            for (int i = 0; i < length; ++i)
            {
                outArray[i] = getGaussianFunctionFor1D(sigma, i - middleIndex);
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

                    if (toMultiplySignalIndex < 0 || toMultiplySignalIndex >= signal.Length)
                    {
                        continue;
                    }

                    int filterIndex = filter.Length - 1 - j;

                    // reverse한 배열을 곱해야 한다!
                    outArray[i] += filter[filterIndex] * signal[toMultiplySignalIndex];
                }
            }

            return outArray;
        }

        public static double[,] GetGaussianFilter2D(double sigma)
        {
            // length가 0이 되면 안 된다
            int length = (int)Math.Ceiling(sigma * 6);
            Debug.Assert(length > 0);

            if (length % 2 == 0)
            {
                ++length;
            }

            double[,] outArray = new double[length, length];
            int middleIndex = length / 2;

            for (int i = 0; i < length; ++i)
            {
                for (int j = 0; j < length; ++j)
                {
                    outArray[i, j] = getGaussianFunctionFor2D(sigma, j - middleIndex, i - middleIndex);
                }
            }

            return outArray;
        }

        public static Bitmap ConvolveImage(Bitmap bitmap, double[,] filter)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            Bitmap outBitmap = new Bitmap(width, height);

            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    double r = 0;
                    double g = 0;
                    double b = 0;

                    for (int x = 0; x < filter.GetLength(0); ++x)
                    {
                        for (int y = 0; y < filter.GetLength(1); ++y)
                        {
                            int toMultiplySignalIndexX = i - y + filter.GetLength(1) / 2;
                            int toMultiplySignalIndexY = j - x + filter.GetLength(0) / 2;

                            if (isValid(toMultiplySignalIndexX, toMultiplySignalIndexY, width, height) == false)
                            {
                                continue;
                            }

                            int filterIndexX = x;
                            int filterIndexY = y;
                            Color targetColor = bitmap.GetPixel(toMultiplySignalIndexX, toMultiplySignalIndexY);

                            r += targetColor.R * filter[filterIndexX, filterIndexY];
                            g += targetColor.G * filter[filterIndexX, filterIndexY];
                            b += targetColor.B * filter[filterIndexX, filterIndexY];
                        }
                    }
                    outBitmap.SetPixel(i, j, new Color((byte)r, (byte)g, (byte)b));
                }
            }
            return outBitmap;
        }

        private static bool isValid(int targetWidth, int targetHeight, int width, int height)
        {
            return 0 <= targetWidth && targetWidth < width && 0 <= targetHeight && targetHeight < height;
        }
    }
}