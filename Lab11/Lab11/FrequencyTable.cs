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
            int minInterval = diff / maxBinCount;

            if (minInterval < 0)
            {
                return null;
            }

            if (minInterval == 0)
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
            List<Tuple<Tuple<int, int>, int>> list = new List<Tuple<Tuple<int, int>, int>>(maxBinCount);

            bool isValid = true;

            for (int i = start; i < end; i += interval)
            {
                Tuple<int, int> period = new Tuple<int, int>(i, i + interval);
                list.Add(new Tuple<Tuple<int, int>, int>(period, 0));
            }

            foreach (int datum in data)
            {
                int index = (datum - start) / interval;

                if (index >= maxBinCount)
                {
                    isValid = false;
                    break;
                }

                Tuple<int, int> currentPeriod = list[index].Item1;
                int currentCount = list[index].Item2;

                list[index] = new Tuple<Tuple<int, int>, int>(currentPeriod, currentCount + 1);
            }

            if (isValid == false)
            {
                return null;
            }

            return list;
        }
    }
}