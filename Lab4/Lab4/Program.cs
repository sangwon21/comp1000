using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
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
            {
                MultiSet set = new MultiSet();
                set.Add("a");
                set.Add("a");
                set.Add("a");
                set.Add("a");
                set.Add("a");
                set.Add("a");
                set.Add("a");
                set.Add("a");
                set.Add("a");
                set.Add("a");
                List<MultiSet> powerSet = set.FindPowerSet();
                List<List<string>> expectedPowerSet = new List<List<string>>()
            {
                new List<string>() { },
                new List<string>() { "a" },
                new List<string>() { "a", "a" },
                new List<string>() { "a", "a", "a" },
                new List<string>() { "a", "a", "a", "a" },
                new List<string>() { "a", "a", "a", "a", "a" },
                new List<string>() { "a", "a", "a", "a", "a", "a" },
                new List<string>() { "a", "a", "a", "a", "a", "a", "a" },
                new List<string>() { "a", "a", "a", "a", "a", "a", "a", "a" },
                new List<string>() { "a", "a", "a", "a", "a", "a", "a", "a", "a" },
                new List<string>() { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a" }
            };
                CheckEqual(powerSet, expectedPowerSet);

                set = new MultiSet();
                set.Add("a");
                set.Add("a");
                set.Add("a");
                set.Add("a");
                set.Add("b");
                powerSet = set.FindPowerSet();
                expectedPowerSet = new List<List<string>>()
            {
                new List<string>() { },
                new List<string>() { "a" },
                new List<string>() { "a", "a" },
                new List<string>() { "a", "a", "a" },
                new List<string>() { "a", "a", "a", "a" },
                new List<string>() { "a", "a", "a", "a", "b" },
                new List<string>() { "a", "a", "a", "b" },
                new List<string>() { "a", "a", "b" },
                new List<string>() { "a", "b" },
                new List<string>() { "b" }
            };
                CheckEqual(powerSet, expectedPowerSet);

                set = new MultiSet();
                set.Add("a");
                set.Add("a");
                set.Add("a");
                set.Add("b");
                set.Add("b");
                powerSet = set.FindPowerSet();
                expectedPowerSet = new List<List<string>>()
            {
                new List<string>() { },
                new List<string>() { "a" },
                new List<string>() { "a", "a" },
                new List<string>() { "a", "a", "a" },
                new List<string>() { "a", "a", "a", "b" },
                new List<string>() { "a", "a", "a", "b", "b" },
                new List<string>() { "a", "a", "b" },
                new List<string>() { "a", "a", "b", "b" },
                new List<string>() { "a", "b" },
                new List<string>() { "a", "b", "b" },
                new List<string>() { "b" },
                new List<string>() { "b", "b" }
            };
                CheckEqual(powerSet, expectedPowerSet);

                set = new MultiSet();
                set.Add("a");
                set.Add("a");
                set.Add("b");
                set.Add("b");
                set.Add("c");
                powerSet = set.FindPowerSet();
                expectedPowerSet = new List<List<string>>()
            {
                new List<string>() { },
                new List<string>() { "a" },
                new List<string>() { "a", "a" },
                new List<string>() { "a", "a", "b" },
                new List<string>() { "a", "a", "b", "b" },
                new List<string>() { "a", "a", "b", "b", "c" },
                new List<string>() { "a", "a", "b", "c" },
                new List<string>() { "a", "a", "c" },
                new List<string>() { "a", "b" },
                new List<string>() { "a", "b", "b" },
                new List<string>() { "a", "b", "b", "c" },
                new List<string>() { "a", "b", "c" },
                new List<string>() { "a", "c" },
                new List<string>() { "b" },
                new List<string>() { "b", "b" },
                new List<string>() { "b", "b", "c" },
                new List<string>() { "b", "c" },
                new List<string>() { "c" }
            };
                CheckEqual(powerSet, expectedPowerSet);
            }

            {
                Console.WriteLine("------------------------------Remove()------------------------------------------------------");

                var set1 = new MultiSet();
                set1.Add("b");
                set1.Add("a");
                Console.WriteLine("\n{ b, a }");
                Console.WriteLine("MultiSet.Remove(\"a\") => { b }");
                Debug.Assert(set1.Remove("a"));
                Debug.Assert(set1.GetMultiplicity("a") == 0);

                set1.Add("a");
                set1.Add("c");
                set1.Add("a");
                Console.WriteLine("\n{ a, c, a }");
                Console.WriteLine("MultiSet.Remove(\"d\") => { a, c, a }\n");
                Debug.Assert(set1.GetMultiplicity("a") == 2);
                Debug.Assert(!set1.Remove("d"));

                Console.WriteLine("------------------------------ToList()------------------------------------------------------");

                var expectedList = new List<string> { "a", "a", "b", "c" };
                var list = set1.ToList();

                Console.WriteLine("\n{ a, a, b, c }");
                Console.WriteLine("MultiSet.ToList() => { a, a, b, c }");
                AssertEquals(expectedList, list);

                Console.WriteLine("MultiSet.Remove(\"a\") => { a, b, c }");
                Debug.Assert(set1.Remove("a"));
                expectedList = new List<string> { "a", "b", "c" };
                Console.WriteLine("MultiSet.ToList() => { a, b, c }");
                list = set1.ToList();
                AssertEquals(expectedList, list);

                expectedList = new List<string>();
                list = new MultiSet().ToList();
                Console.WriteLine("\n{   }");
                Console.WriteLine("MultiSet.ToList() => {   }\n");
                AssertEquals(expectedList, list);

                Console.WriteLine("------------------------------Union()-------------------------------------------------------");

                set1 = new MultiSet();
                set1.Add("a");
                set1.Add("b");
                set1.Add("b");
                set1.Add("c");

                var set2 = new MultiSet();
                set2.Add("a");
                set2.Add("b");
                set2.Add("c");

                list = set1.Union(set2).ToList();

                var expectedSet1 = new List<string> { "a", "b", "b", "c" };
                var expectedSet2 = new List<string> { "a", "b", "c" };
                expectedList = new List<string> { "a", "b", "b", "c" };

                Console.WriteLine("\n{ a, b, b, c } | { a, b, c }");
                AssertEquals(expectedSet1, set1.ToList());
                AssertEquals(expectedSet2, set2.ToList());
                Console.WriteLine("MultiSet.Union(other) => { a, b, b, c }");
                AssertEquals(expectedList, list);

                set1 = new MultiSet();
                set1.Add("A");
                set1.Add("C");
                set1.Add("B");

                list = set1.Union(set2).ToList();
                expectedSet1 = new List<string> { "A", "B", "C" };
                expectedSet2 = new List<string> { "a", "b", "c" };
                expectedList = new List<string> { "A", "B", "C", "a", "b", "c" };

                Console.WriteLine("\n\n{ A, B, C } | { a, b, c }");
                AssertEquals(expectedSet1, set1.ToList());
                AssertEquals(expectedSet2, set2.ToList());
                Console.WriteLine("MultiSet.Union(other) => { A, B, C, a, b, c }");
                AssertEquals(expectedList, list);

                set2 = new MultiSet();
                list = set1.Union(set2).ToList();
                expectedSet1 = new List<string> { "A", "B", "C" };
                expectedSet2.Clear();
                expectedList = new List<string> { "A", "B", "C" };

                Console.WriteLine("\n{ A, B, C } | {   }");
                AssertEquals(expectedSet1, set1.ToList());
                AssertEquals(expectedSet2, set2.ToList());
                Console.WriteLine("MultiSet.Union(other) => { A, B, C }");
                AssertEquals(expectedList, list);

                set1 = new MultiSet();
                list = set1.Union(set2).ToList();
                expectedSet1.Clear();
                expectedList.Clear();

                Console.WriteLine("\n{   } | {   }");
                AssertEquals(expectedSet1, set1.ToList());
                AssertEquals(expectedSet2, set2.ToList());
                Console.WriteLine("MultiSet.Union(other) => {   }\n");
                AssertEquals(expectedList, list);

                Console.WriteLine("------------------------------Intersect()---------------------------------------------------");

                set1 = new MultiSet();
                set1.Add("a");
                set1.Add("b");
                set1.Add("b");
                set1.Add("c");

                set2 = new MultiSet();
                set2.Add("a");
                set2.Add("b");
                set2.Add("c");

                list = set1.Intersect(set2).ToList();
                expectedSet1 = new List<string> { "a", "b", "b", "c" };
                expectedSet2 = new List<string> { "a", "b", "c" };
                expectedList = new List<string> { "a", "b", "c" };

                Console.WriteLine("\n{ a, b, b, c } | { a, b, c }");
                AssertEquals(expectedSet1, set1.ToList());
                AssertEquals(expectedSet2, set2.ToList());
                Console.WriteLine("MultiSet.Intersect(other) => { a, b, c }");
                AssertEquals(expectedList, list);

                set1 = new MultiSet();
                set1.Add("A");
                set1.Add("C");
                set1.Add("B");

                list = set1.Intersect(set2).ToList();
                expectedSet1 = new List<string> { "A", "B", "C" };
                expectedSet2 = new List<string> { "a", "b", "c" };
                expectedList.Clear();

                Console.WriteLine("\n{ A, B, C } | { a, b, c }");
                AssertEquals(expectedSet1, set1.ToList());
                AssertEquals(expectedSet2, set2.ToList());
                Console.WriteLine("MultiSet.Intersect(other) => {   }");
                AssertEquals(expectedList, list);

                set2 = new MultiSet();
                list = set1.Intersect(set2).ToList();
                expectedSet1 = new List<string> { "A", "B", "C" };
                expectedSet2.Clear();

                Console.WriteLine("\n{ A, B, C } | {   }");
                AssertEquals(expectedSet1, set1.ToList());
                AssertEquals(expectedSet2, set2.ToList());
                Console.WriteLine("MultiSet.Intersect(other) => { A, B, C }");
                AssertEquals(expectedList, list);

                set1 = new MultiSet();
                list = set1.Intersect(set2).ToList();
                expectedSet1.Clear();
                expectedList.Clear();

                Console.WriteLine("\n{   } | {   }");
                AssertEquals(expectedSet1, set1.ToList());
                AssertEquals(expectedSet2, set2.ToList());
                Console.WriteLine("MultiSet.Intersect(other) => {   }\n");
                AssertEquals(expectedList, list);

                Console.WriteLine("------------------------------Subtract()----------------------------------------------------");

                set1 = new MultiSet();

                set1.Add("d");
                set1.Add("k");
                set1.Add("f");
                set1.Add("e");
                set1.Add("e");

                set2 = new MultiSet();
                set2.Add("g");
                set2.Add("e");
                set2.Add("f");

                list = set1.Subtract(set2).ToList();
                expectedSet1 = new List<string> { "d", "e", "e", "f", "k" };
                expectedSet2 = new List<string> { "e", "f", "g" };
                expectedList = new List<string> { "d", "e", "k" };

                Console.WriteLine("\n{ d, e, e, f, k } | { e, f, g }");
                AssertEquals(expectedSet1, set1.ToList());
                AssertEquals(expectedSet2, set2.ToList());
                Console.WriteLine("MultiSet.Subtract(other) => { d, e, k }");
                AssertEquals(expectedList, list);

                set1 = new MultiSet();
                set1.Add("d");
                set1.Add("k");
                set1.Add("f");
                set1.Add("e");

                set2 = new MultiSet();
                set2.Add("g");
                set2.Add("e");
                set2.Add("f");
                set2.Add("e");

                list = set1.Subtract(set2).ToList();
                expectedSet1 = new List<string> { "d", "e", "f", "k" };
                expectedSet2 = new List<string> { "e", "e", "f", "g" };
                expectedList = new List<string> { "d", "k" };

                Console.WriteLine("\n{ d, e, f, k } | { e, e, f, g }");
                AssertEquals(expectedSet1, set1.ToList());
                AssertEquals(expectedSet2, set2.ToList());
                Console.WriteLine("MultiSet.Subtract(other) => { d, k }\n");
                AssertEquals(expectedList, list);



                List<MultiSet> setPowerSet = set2.FindPowerSet();

                List<List<string>> expectedPowerset = new List<List<string>>()
            {
                new List<string>(),
                new List<string>(){ "e" },
                new List<string>(){ "e", "e" },
                new List<string>(){ "e", "e", "f" },
                new List<string>(){ "e", "e", "f", "g" },
                new List<string>(){ "e", "e", "g" },
                new List<string>(){ "e", "f" },
                new List<string>(){ "e", "f", "g" },
                new List<string>(){ "e", "g" },
                new List<string>(){ "f" },
                new List<string>(){ "f", "g" },
                new List<string>(){ "g" }
            };
                Debug.Assert(setPowerSet.Count == expectedPowerset.Count);

                for (int i = 0; i < setPowerSet.Count; i++)
                {
                    List<string> listed = setPowerSet[i].ToList();

                    Debug.Assert(listed.Count == expectedPowerset[i].Count);

                    for (int j = 0; j < listed.Count; j++)
                    {
                        Debug.Assert(listed[j] == expectedPowerset[i][j]);
                    }
                }

                Debug.Assert(!set2.IsSubsetOf(set1));
                Debug.Assert(!set2.IsSupersetOf(set1));

            }
            Console.Write("finished!");
        }

        public static void AssertEquals(List<string> expectedList, List<string> list)
        {
            Debug.Assert(expectedList.Count == list.Count);

            for (int i = 0; i < expectedList.Count; i++)
            {
                Debug.Assert(expectedList[i] == list[i]);
            }
        }


        public static void CheckEqual(List<MultiSet> powerSet, List<List<string>> expectedPowerSet)
        {
            for (int i = 0; i < powerSet.Count; i++)
            {
                List<String> myList = powerSet[i].ToList();
                Debug.Assert(myList.Count == expectedPowerSet[i].Count, "길이가 달라요");
                for (int j = 0; j < myList.Count; j++)
                {
                    Debug.Assert(myList[j] == expectedPowerSet[i][j], "값이 달라요");
                    Console.Write($"{myList[j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------");
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