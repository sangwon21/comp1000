using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    public sealed class MultiSet
    {
        private Dictionary<string, int> dictionary = new Dictionary<string, int>();

        public MultiSet(Dictionary<string, int> dictionary)
        {
            this.dictionary = dictionary;
        }

        public void Add(string element)
        {
            if (dictionary.ContainsKey(element))
            {
                int value = dictionary[element];
                dictionary[element] = value++;
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
                dictionary[element] = value--;
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
                list.Add(element);
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
            return null;
        }

        public bool IsSubsetOf(MultiSet other)
        {
            return false;
        }

        public bool IsSupersetOf(MultiSet other)
        {
            return false;
        }
    }
}