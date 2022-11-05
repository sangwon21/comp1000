using System;

namespace Assignment3
{
    public sealed class SineNoise : INoise
    {
        private const double BASE_SAMPLING_WIDTH = Math.PI / 4;
        private double mX = -BASE_SAMPLING_WIDTH;

        public int GetNext(int level)
        {
            mX += BASE_SAMPLING_WIDTH / Math.Pow(2, level);
            return (int)(5 * Math.Sin(mX));
        }
    }
}
