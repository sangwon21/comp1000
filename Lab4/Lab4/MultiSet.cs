using System;
using System.Collections.Generic;
using System.Linq;

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
                return null;
            }

            List<string> list = new List<string>();

            foreach (string element in dictionary.Keys)
            {
                for (int i = 0; i < dictionary[element]; i++)
                {
                    list.Add(element);
                }
            }

            list.Sort();

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
                    int value = unionDicitonary[element];
                    value += this.dictionary[element];
                    unionDicitonary[(element)] = value;
                    continue;
                }
                unionDicitonary[element] = this.dictionary[element];
            }

            return otherDictionary.Count() == 0 ? null : new MultiSet(unionDicitonary);
        }

        public MultiSet Intersect(MultiSet other)
        {
            Dictionary<string, int> intersectDicitonary = new Dictionary<string, int>();

            Dictionary<string, int> otherDictionary = other.dictionary;

            foreach (string element in otherDictionary.Keys)
            {
                int otherValue = otherDictionary[element];
                if (this.dictionary.ContainsKey(element))
                {
                    int thisValue = dictionary[element];

                    intersectDicitonary[element] = Math.Min(thisValue, otherValue);
                }
            }



            return otherDictionary.Count() == 0 ? null : new MultiSet(intersectDicitonary);
        }

        public MultiSet Subtract(MultiSet other)
        {
            Dictionary<string, int> subtractDicitonary = new Dictionary<string, int>();

            Dictionary<string, int> otherDictionary = other.dictionary;

            foreach (string element in dictionary.Keys)
            {
                int thisValue = dictionary[element];
                if (otherDictionary.ContainsKey(element))
                {
                    int otherValue = otherDictionary[element];

                    int diff = Math.Max(0, thisValue - otherValue);

                    subtractDicitonary[element] = diff;
                }
            }

            return otherDictionary.Count() == 0 ? null : new MultiSet(subtractDicitonary);
        }

        public List<MultiSet> FindPowerSet()
        {
            List<string> elements = new List<string>();

            foreach (string element in dictionary.Keys)
            {
                int value = dictionary[element];

                while (value > 0)
                {
                    elements.Add(element);
                    value--;
                }
            }

            elements.Sort();

            List<MultiSet> output = new List<MultiSet>();

            /**
             * 알파벳 기준으로 정렬
             * 이 순서대로 순회
             */
            string[] keys = dictionary.Keys.ToArray();
            Array.Sort(keys);

            makePowerSet(ref output, 0, keys.Length, keys, new Dictionary<string, int>());

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

        private void makePowerSet(ref List<MultiSet> output, int depth, int maxDepth, string[] keys, Dictionary<string, int> dict)
        {
            if (depth > maxDepth)
            {

                output.Add(new MultiSet(dict));
                return;
            }

            for (int i = depth; i < keys.Length; i++)
            {
                int value = dict[keys[i]];

                for (int j = 0; j <= value; j++)
                {
                    dict[keys[i]] = j;
                    makePowerSet(ref output, i + 1, maxDepth, keys, dict);
                }
            }
        }
    }
}