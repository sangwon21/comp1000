using System;
using System.Collections.Generic;

namespace Lab11
{
    public static class FrequencyTable
    {
        public static List<Tuple<Tuple<int, int>, int>> GetFrequencyTable(int[] data, int maxBinCount)
        {
            int maxValue = Int32.MinValue;
            int minValue = Int32.MaxValue;

            for (int i = 0; i < data.Length; ++i)
            {
                maxValue = Math.Max(data[i], maxValue);
                minValue = Math.Min(data[i], minValue);
            }

            int diff = maxValue - minValue;

            if (diff < 0)
            {
                return null;
            }

            if (diff == 0)
            {
                List<Tuple<Tuple<int, int>, int>> tmp = new List<Tuple<Tuple<int, int>, int>>(1);

                Tuple<int, int> period = new Tuple<int, int>(maxValue, maxValue + 1);
                tmp.Add(new Tuple<Tuple<int, int>, int>(period, data.Length));

                return tmp;
            }

            int interval = (int)Math.Ceiling(diff / (double)maxBinCount + 0.0000000001);
            int binCount = (int)Math.Ceiling(diff / (double)interval);

            if (diff == interval * binCount)
            {
                ++binCount;
            }

            List<Tuple<Tuple<int, int>, int>> outList = new List<Tuple<Tuple<int, int>, int>>(binCount);

            for (int i = 0; i < binCount; ++i)
            {
                Tuple<int, int> period = new Tuple<int, int>(minValue + i * interval, minValue + (i + 1) * interval);
                outList.Add(new Tuple<Tuple<int, int>, int>(period, 0));
            }

            foreach (int datum in data)
            {
                int index = (datum - minValue) / interval;

                if (index >= maxBinCount)
                {
                    return null;
                }

                Tuple<int, int> currentPeriod = outList[index].Item1;
                int currentCount = outList[index].Item2;

                outList[index] = new Tuple<Tuple<int, int>, int>(currentPeriod, currentCount + 1);
            }


            return outList;
        }
    }
}
