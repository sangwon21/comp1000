using System.Collections.Generic;
using System.Diagnostics;


namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiSet set = new MultiSet();

            set.Add("cattle");
            set.Add("bee");
            set.Add("cattle");
            set.Add("bee");
            set.Add("happy");
            set.Add("zachariah");

            Debug.Assert(set.Remove("zachariah"));
            Debug.Assert(!set.Remove("fun"));

            Debug.Assert(set.GetMultiplicity("cattle") == 2);

            List<string> expectedList = new List<string> { "bee", "bee", "cattle", "cattle", "happy" };
            List<string> list = set.ToList();

            Debug.Assert(list.Count == 5);

            for (int i = 0; i < expectedList.Count; i++)
            {
                Debug.Assert(expectedList[i] == list[i]);
            }

            MultiSet set2 = new MultiSet();

            set2.Add("cattle");
            set2.Add("cattle");
            set2.Add("bee");

            list = set.Union(set2).ToList();
            Debug.Assert(list.Count == 5);

            for (int i = 0; i < expectedList.Count; i++)
            {
                Debug.Assert(expectedList[i] == list[i]);
            }

            expectedList = new List<string> { "bee", "cattle", "cattle" };
            list = set.Intersect(set2).ToList();
            Debug.Assert(list.Count == 3);

            for (int i = 0; i < expectedList.Count; i++)
            {
                Debug.Assert(expectedList[i] == list[i]);
            }

            expectedList = new List<string> { "bee", "happy" };
            list = set.Subtract(set2).ToList();
            Debug.Assert(list.Count == 2);

            for (int i = 0; i < expectedList.Count; i++)
            {
                Debug.Assert(expectedList[i] == list[i]);
            }

            List<MultiSet> expectedPowerset = getExpectedPowerset();
            List<MultiSet> set2PowerSet = set2.FindPowerSet();
            Debug.Assert(set2PowerSet.Count == expectedPowerset.Count);

            for (int i = 0; i < expectedPowerset.Count; i++)
            {
                expectedList = expectedPowerset[i].ToList();
                list = set2PowerSet[i].ToList();

                Debug.Assert(expectedList.Count == list.Count);

                for (int j = 0; j < expectedList.Count; j++)
                {
                    Debug.Assert(expectedList[j] == list[j]);
                }
            }

            Debug.Assert(!set.IsSubsetOf(set2));
            Debug.Assert(set.IsSupersetOf(set2));
        }

        private static List<MultiSet> getExpectedPowerset()
        {
            List<MultiSet> powerset = new List<MultiSet>();

            MultiSet set = new MultiSet();
            powerset.Add(set);

            set = new MultiSet();
            set.Add("bee");

            powerset.Add(set);

            set = new MultiSet();
            set.Add("bee");
            set.Add("cattle");

            powerset.Add(set);

            set = new MultiSet();
            set.Add("bee");
            set.Add("cattle");
            set.Add("cattle");

            powerset.Add(set);

            set = new MultiSet();
            set.Add("cattle");

            powerset.Add(set);

            set = new MultiSet();
            set.Add("cattle");
            set.Add("cattle");

            powerset.Add(set);

            return powerset;
        }
    }
}
