using System;
namespace Lab6
{
    public class Item
    { 
        public Item(EType type, double weight, double volume, bool isToxicWaste)
        {
            Type = type;
            Weight = weight;
            Volume = volume;
            IsToxicWaste = isToxicWaste;
        }

        public EType Type
        {
            get; private set;
        }

        public double Weight
        {
            get; private set;
        }

        public double Volume
        {
            get; private set;
        }

        public bool IsToxicWaste
        {
            get; private set;
        }
    }
}

