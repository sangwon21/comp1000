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
            int minInterval = Math.Max(1, diff / maxBinCount);

            if (diff < 0)
            {
                return null;
            }

            if (diff == 0)
            {
                List<Tuple<Tuple<int, int>, int>> outList = new List<Tuple<Tuple<int, int>, int>>(1);

                Tuple<int, int> period = new Tuple<int, int>(maxValue, maxValue + 1);
                outList.Add(new Tuple<Tuple<int, int>, int>(period, data.Length));

                return outList;
            }

            for (int i = minInterval; i < diff; ++i)
            {
                List<Tuple<Tuple<int, int>, int>> outList = tryPutDataIntoHistogram(data, minValue, maxValue, i, maxBinCount);

                if (outList != null)
                {
                    return outList;
                }
            }

            return null;
        }

        private static List<Tuple<Tuple<int, int>, int>> tryPutDataIntoHistogram(int[] data, int start, int end, int interval, int maxBinCount)
        {
            int outListCount = Math.Min((end - start) / interval + 1, maxBinCount);

            List<Tuple<Tuple<int, int>, int>> outList = new List<Tuple<Tuple<int, int>, int>>(outListCount);

            for (int i = 0; i < outListCount; ++i)
            {
                Tuple<int, int> period = new Tuple<int, int>(start + i * interval, start + (i + 1) * interval);
                outList.Add(new Tuple<Tuple<int, int>, int>(period, 0));
            }

            foreach (int datum in data)
            {
                int index = (datum - start) / interval;

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