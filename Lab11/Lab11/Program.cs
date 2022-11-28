using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lab11
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] data = new int[] { 4 };
            List<Tuple<Tuple<int, int>, int>> frequencyTable = FrequencyTable.GetFrequencyTable(data, 1);

            Debug.Assert(getTotalCount(frequencyTable) == data.Length);

            Debug.Assert(frequencyTable.Count == 1);
            Debug.Assert(frequencyTable[0].Item1.Item1 == 4);
            Debug.Assert(frequencyTable[0].Item1.Item2 == 5);
            Debug.Assert(frequencyTable[0].Item2 == 1);

            frequencyTable = FrequencyTable.GetFrequencyTable(data, 5);

            Debug.Assert(getTotalCount(frequencyTable) == data.Length);

            Debug.Assert(frequencyTable.Count == 1);
            Debug.Assert(frequencyTable[0].Item1.Item1 == 4);
            Debug.Assert(frequencyTable[0].Item1.Item2 == 5);
            Debug.Assert(frequencyTable[0].Item2 == 1);

            data = new int[] { 4, 4, 4, 4 };
            frequencyTable = FrequencyTable.GetFrequencyTable(data, 3);

            Debug.Assert(getTotalCount(frequencyTable) == data.Length);

            Debug.Assert(frequencyTable.Count == 1);
            Debug.Assert(frequencyTable[0].Item1.Item1 == 4);
            Debug.Assert(frequencyTable[0].Item1.Item2 == 5);
            Debug.Assert(frequencyTable[0].Item2 == 4);

            data = new int[] { 4, 5, 6, 7, 8, 9 };
            frequencyTable = FrequencyTable.GetFrequencyTable(data, 5);

            Debug.Assert(getTotalCount(frequencyTable) == data.Length);
            Debug.Assert(frequencyTable.Count == 3);

            Debug.Assert(frequencyTable[0].Item1.Item1 == 4);
            Debug.Assert(frequencyTable[0].Item1.Item2 == 6);
            Debug.Assert(frequencyTable[0].Item2 == 2);

            Debug.Assert(frequencyTable[1].Item1.Item1 == 6);
            Debug.Assert(frequencyTable[1].Item1.Item2 == 8);
            Debug.Assert(frequencyTable[1].Item2 == 2);

            Debug.Assert(frequencyTable[2].Item1.Item1 == 8);
            Debug.Assert(frequencyTable[2].Item1.Item2 == 10);
            Debug.Assert(frequencyTable[2].Item2 == 2);

            data = new int[] { 1, 12, 14, 15, 21, 22, 22, 30, 33, 41, 41, 61, 69, 70, 81, 90, 92, 101 };
            frequencyTable = FrequencyTable.GetFrequencyTable(data, 5);

            Debug.Assert(getTotalCount(frequencyTable) == data.Length);
            Debug.Assert(frequencyTable.Count == 5);

            Debug.Assert(frequencyTable[0].Item1.Item1 == 1);
            Debug.Assert(frequencyTable[0].Item1.Item2 == 22);
            Debug.Assert(frequencyTable[0].Item2 == 5);

            Debug.Assert(frequencyTable[1].Item1.Item1 == 22);
            Debug.Assert(frequencyTable[1].Item1.Item2 == 43);
            Debug.Assert(frequencyTable[1].Item2 == 6);

            Debug.Assert(frequencyTable[2].Item1.Item1 == 43);
            Debug.Assert(frequencyTable[2].Item1.Item2 == 64);
            Debug.Assert(frequencyTable[2].Item2 == 1);

            Debug.Assert(frequencyTable[3].Item1.Item1 == 64);
            Debug.Assert(frequencyTable[3].Item1.Item2 == 85);
            Debug.Assert(frequencyTable[3].Item2 == 3);

            Debug.Assert(frequencyTable[4].Item1.Item1 == 85);
            Debug.Assert(frequencyTable[4].Item1.Item2 == 106);
            Debug.Assert(frequencyTable[4].Item2 == 3);

            data = new int[] { 10, 11, 20, 1, 30, 20, 22, 25, 48, 43, 23, 44, 49, 21, 31, 35, 33, 36, 2, 4, 3 };
            frequencyTable = FrequencyTable.GetFrequencyTable(data, 5);

            Debug.Assert(getTotalCount(frequencyTable) == data.Length);
            Debug.Assert(frequencyTable.Count == 5);

            Debug.Assert(frequencyTable[0].Item1.Item1 == 1);
            Debug.Assert(frequencyTable[0].Item1.Item2 == 11);
            Debug.Assert(frequencyTable[0].Item2 == 5);

            Debug.Assert(frequencyTable[1].Item1.Item1 == 11);
            Debug.Assert(frequencyTable[1].Item1.Item2 == 21);
            Debug.Assert(frequencyTable[1].Item2 == 3);

            Debug.Assert(frequencyTable[2].Item1.Item1 == 21);
            Debug.Assert(frequencyTable[2].Item1.Item2 == 31);
            Debug.Assert(frequencyTable[2].Item2 == 5);

            Debug.Assert(frequencyTable[3].Item1.Item1 == 31);
            Debug.Assert(frequencyTable[3].Item1.Item2 == 41);
            Debug.Assert(frequencyTable[3].Item2 == 4);

            Debug.Assert(frequencyTable[4].Item1.Item1 == 41);
            Debug.Assert(frequencyTable[4].Item1.Item2 == 51);
            Debug.Assert(frequencyTable[4].Item2 == 4);

            data = new int[] { 33, 43, 36, 35, 39, 38, 43, 34, 43, 39, 34, 43, 37, 33, 33, 38, 34, 43, 34, 41 };
            frequencyTable = FrequencyTable.GetFrequencyTable(data, 11);

            Debug.Assert(getTotalCount(frequencyTable) == data.Length);
            Debug.Assert(frequencyTable.Count == 11);

            Debug.Assert(frequencyTable[0].Item1.Item1 == 33);
            Debug.Assert(frequencyTable[0].Item1.Item2 == 34);
            Debug.Assert(frequencyTable[0].Item2 == 3);

            Debug.Assert(frequencyTable[1].Item1.Item1 == 34);
            Debug.Assert(frequencyTable[1].Item1.Item2 == 35);
            Debug.Assert(frequencyTable[1].Item2 == 4);

            Debug.Assert(frequencyTable[2].Item1.Item1 == 35);
            Debug.Assert(frequencyTable[2].Item1.Item2 == 36);
            Debug.Assert(frequencyTable[2].Item2 == 1);

            Debug.Assert(frequencyTable[3].Item1.Item1 == 36);
            Debug.Assert(frequencyTable[3].Item1.Item2 == 37);
            Debug.Assert(frequencyTable[3].Item2 == 1);

            Debug.Assert(frequencyTable[4].Item1.Item1 == 37);
            Debug.Assert(frequencyTable[4].Item1.Item2 == 38);
            Debug.Assert(frequencyTable[4].Item2 == 1);

            Debug.Assert(frequencyTable[5].Item1.Item1 == 38);
            Debug.Assert(frequencyTable[5].Item1.Item2 == 39);
            Debug.Assert(frequencyTable[5].Item2 == 2);

            Debug.Assert(frequencyTable[6].Item1.Item1 == 39);
            Debug.Assert(frequencyTable[6].Item1.Item2 == 40);
            Debug.Assert(frequencyTable[6].Item2 == 2);

            Debug.Assert(frequencyTable[7].Item1.Item1 == 40);
            Debug.Assert(frequencyTable[7].Item1.Item2 == 41);
            Debug.Assert(frequencyTable[7].Item2 == 0);

            Debug.Assert(frequencyTable[8].Item1.Item1 == 41);
            Debug.Assert(frequencyTable[8].Item1.Item2 == 42);
            Debug.Assert(frequencyTable[8].Item2 == 1);

            Debug.Assert(frequencyTable[9].Item1.Item1 == 42);
            Debug.Assert(frequencyTable[9].Item1.Item2 == 43);
            Debug.Assert(frequencyTable[9].Item2 == 0);

            Debug.Assert(frequencyTable[10].Item1.Item1 == 43);
            Debug.Assert(frequencyTable[10].Item1.Item2 == 44);
            Debug.Assert(frequencyTable[10].Item2 == 5);


        }

        private static int getTotalCount(List<Tuple<Tuple<int, int>, int>> frequencyTable)
        {
            int count = 0;

            foreach (var tup in frequencyTable)
            {
                count += tup.Item2;
            }

            return count;
        }
    }
}