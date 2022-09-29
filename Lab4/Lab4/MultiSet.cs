using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4
{
    public sealed class MultiSet
    {
        private Dictionary<string, int> dictionary;

        public MultiSet()
        {
            this.dictionary = new Dictionary<string, int>();
        }

        public MultiSet(Dictionary<string, int> dictionary)
        {
            this.dictionary = dictionary;
        }

        public void Add(string element)
        {
            if (dictionary.ContainsKey(element))
            {
                int value = dictionary[element];
                dictionary[element] = value + 1;
                return;
            }

            dictionary[element] = 1;
        }

        public bool Remove(string element)
        {
            if (dictionary.ContainsKey(element) == false)
            {
                return false;
            }

            if (dictionary[element] == 1)
            {
                dictionary.Remove(element);
            }
            else
            {
                int value = dictionary[element];
                dictionary[element] = value - 1;
            }

            return true;
        }

        public uint GetMultiplicity(string element)
        {
            if (dictionary.ContainsKey(element) == false)
            {
                return 0;
            }

            return (uint)dictionary[element];
        }

        public List<string> ToList()
        {
            if (dictionary.Count == 0)
            {
                return new List<string>();
            }

            List<string> list = new List<string>();

            foreach (string element in dictionary.Keys)
            {
                for (int i = 0; i < dictionary[element]; i++)
                {
                    list.Add(element);
                }
            }

            list.Sort((a, b) =>
            {
                byte[] byteA = Encoding.Default.GetBytes(a);
                byte[] byteB = Encoding.Default.GetBytes(b);

                for (int i = 0; i < byteA.Length; i++)
                {
                    if (byteA[i] > byteB[i])
                    {
                        return 1;
                    }

                    if (byteA[i] < byteB[i])
                    {
                        return -1;
                    }
                }

                return 0;
            });

            return list;
        }

        public MultiSet Union(MultiSet other)
        {
            Dictionary<string, int> unionDicitonary = new Dictionary<string, int>();

            Dictionary<string, int> otherDictionary = other.dictionary;

            foreach (string element in otherDictionary.Keys)
            {
                unionDicitonary[element] = otherDictionary[element];
            }

            foreach (string element in this.dictionary.Keys)
            {
                if (unionDicitonary.ContainsKey(element))
                {
                    unionDicitonary[element] = Math.Max(dictionary[element], otherDictionary[element]);
                    continue;
                }
                unionDicitonary[element] = this.dictionary[element];
            }

            return new MultiSet(unionDicitonary);
        }

        public MultiSet Intersect(MultiSet other)
        {
            Dictionary<string, int> intersectDicitonary = new Dictionary<string, int>();

            Dictionary<string, int> otherDictionary = other.dictionary;

            foreach (string element in otherDictionary.Keys)
            {
                if (this.dictionary.ContainsKey(element))
                {
                    intersectDicitonary[element] = Math.Min(dictionary[element], otherDictionary[element]);
                }
            }



            return new MultiSet(intersectDicitonary);
        }

        public MultiSet Subtract(MultiSet other)
        {
            Dictionary<string, int> subtractDicitonary = new Dictionary<string, int>();

            Dictionary<string, int> otherDictionary = other.dictionary;

            foreach (string element in dictionary.Keys)
            {
                if (otherDictionary.ContainsKey(element))
                {
                    int diff = Math.Max(0, dictionary[element] - otherDictionary[element]);

                    if (diff > 0)
                    {
                        subtractDicitonary[element] = diff;
                    }

                    continue;
                }
                subtractDicitonary[element] = dictionary[element];
            }

            return new MultiSet(subtractDicitonary);
        }

        public List<MultiSet> FindPowerSet()
        {
            List<MultiSet> output = new List<MultiSet>();

            /**
             * 알파벳 기준으로 정렬
             * 이 순서대로 순회
             */
            string[] keys = dictionary.Keys.ToArray();
            Array.Sort(keys);

            makePowerSet(ref output, 0, keys.Length, keys, new Dictionary<string, int>(), dictionary);

            output.Sort((a, b) =>
            {
                List<string> shorter = a.ToList();
                List<string> longer = b.ToList();
                bool bChanged = false;

                if (shorter.Count > longer.Count)
                {
                    List<string> tmp = shorter;
                    shorter = longer;
                    longer = tmp;
                    bChanged = true;
                }

                for (int i = 0; i < shorter.Count; i++)
                {
                    if (shorter[i] == longer[i])
                    {
                        continue;
                    }

                    int compare = String.Compare(shorter[i], longer[i]);

                    if (bChanged)
                    {
                        compare *= -1;
                    }

                    return compare;
                }

                return bChanged ? 1 : -1;
            });

            return output;
        }

        public bool IsSubsetOf(MultiSet other)
        {
            foreach (string element in dictionary.Keys)
            {
                if (other.dictionary[element] < dictionary[element])
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsSupersetOf(MultiSet other)
        {
            return other.IsSubsetOf(this);
        }

        private void makePowerSet(ref List<MultiSet> output, int depth, int maxDepth, string[] keys, Dictionary<string, int> dict, Dictionary<string, int> fromDictionary)
        {
            if (depth >= maxDepth)
            {
                output.Add(new MultiSet(new Dictionary<string, int>(dict)));
                return;
            }

            int value = fromDictionary[keys[depth]];
            for (int i = 0; i <= value; i++)
            {
                dict[keys[depth]] = i;
                makePowerSet(ref output, depth + 1, maxDepth, keys, dict, fromDictionary);
            }


        }
    }
}