using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4
{
    public sealed class MultiSet
    {
        private Dictionary<string, int> mDictionary;

        public MultiSet()
        {
            this.mDictionary = new Dictionary<string, int>();
        }

        public MultiSet(Dictionary<string, int> dictionary)
        {
            this.mDictionary = dictionary;
        }

        public void Add(string element)
        {
            if (mDictionary.ContainsKey(element))
            {
                int value = mDictionary[element];
                mDictionary[element] = value + 1;
                return;
            }

            mDictionary[element] = 1;
        }

        public bool Remove(string element)
        {
            if (mDictionary.ContainsKey(element) == false)
            {
                return false;
            }

            if (mDictionary[element] == 1)
            {
                mDictionary.Remove(element);
            }
            else
            {
                int value = mDictionary[element];
                mDictionary[element] = value - 1;
            }

            return true;
        }

        public uint GetMultiplicity(string element)
        {
            if (mDictionary.ContainsKey(element) == false)
            {
                return 0;
            }

            return (uint)mDictionary[element];
        }

        public List<string> ToList()
        {
            if (mDictionary.Count == 0)
            {
                return new List<string>();
            }

            List<string> list = new List<string>();

            foreach (string element in mDictionary.Keys)
            {
                for (int i = 0; i < mDictionary[element]; i++)
                {
                    list.Add(element);
                }
            }

            list.Sort((a, b) =>
            {
                return String.CompareOrdinal(a, b);
            });

            return list;
        }

        public MultiSet Union(MultiSet other)
        {
            Dictionary<string, int> unionDicitonary = new Dictionary<string, int>();

            Dictionary<string, int> otherDictionary = other.mDictionary;

            foreach (string element in otherDictionary.Keys)
            {
                unionDicitonary[element] = otherDictionary[element];
            }

            foreach (string element in this.mDictionary.Keys)
            {
                if (unionDicitonary.ContainsKey(element))
                {
                    unionDicitonary[element] = Math.Max(mDictionary[element], otherDictionary[element]);
                    continue;
                }
                unionDicitonary[element] = this.mDictionary[element];
            }

            return new MultiSet(unionDicitonary);
        }

        public MultiSet Intersect(MultiSet other)
        {
            Dictionary<string, int> intersectDicitonary = new Dictionary<string, int>();

            Dictionary<string, int> otherDictionary = other.mDictionary;

            foreach (string element in otherDictionary.Keys)
            {
                if (this.mDictionary.ContainsKey(element))
                {
                    intersectDicitonary[element] = Math.Min(mDictionary[element], otherDictionary[element]);
                }
            }



            return new MultiSet(intersectDicitonary);
        }

        public MultiSet Subtract(MultiSet other)
        {
            Dictionary<string, int> subtractDicitonary = new Dictionary<string, int>();

            Dictionary<string, int> otherDictionary = other.mDictionary;

            foreach (string element in mDictionary.Keys)
            {
                if (otherDictionary.ContainsKey(element))
                {
                    int diff = Math.Max(0, mDictionary[element] - otherDictionary[element]);

                    if (diff > 0)
                    {
                        subtractDicitonary[element] = diff;
                    }

                    continue;
                }
                subtractDicitonary[element] = mDictionary[element];
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
            string[] keys = mDictionary.Keys.ToArray();
            Array.Sort(keys);

            makePowerSet(ref output, 0, keys.Length, keys, new Dictionary<string, int>(), mDictionary);

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

                    int compare = String.CompareOrdinal(shorter[i], longer[i]);

                    if (bChanged)
                    {
                        compare *= -1;
                    }

                    return compare;
                }

                if (shorter.Count == longer.Count)
                {
                    return 0;
                }

                return bChanged ? 1 : -1;
            });

            return output;
        }

        public bool IsSubsetOf(MultiSet other)
        {
            foreach (string element in mDictionary.Keys)
            {
                if (other.mDictionary.ContainsKey(element) == false)
                {
                    return false;
                }

                if (other.mDictionary[element] < mDictionary[element])
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