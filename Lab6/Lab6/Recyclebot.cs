using System.Collections.Generic;

namespace Lab6
{
    public class Recyclebot
    {
        public Recyclebot()
        {
            RecycleItems = new List<Item>();
            NonRecycleItems = new List<Item>();
        }

        public List<Item> RecycleItems
        {
            get; private set;
        }

        public List<Item> NonRecycleItems
        {
            get; private set;
        }

        public void Add(Item item)
        {
            if (item.Type == EType.Paper || item.Type == EType.Furniture || item.Type == EType.Electronics)
            {
                const int MIN_ITEM_WEIGHT = 2;
                const int MAX_ITEM_WEIGHT = 5;

                if (MIN_ITEM_WEIGHT <= item.Weight && item.Weight < MAX_ITEM_WEIGHT)
                {
                    RecycleItems.Add(item);
                    return;
                }
                NonRecycleItems.Add(item);
                return;
            }
            RecycleItems.Add(item);
        }

        public List<Item> Dump()
        {
            List<Item> outItems = new List<Item>();

            const int ALLOWED_CAPACITY_1 = 10;
            const int ALLOWED_CAPACITY_2 = 11;
            const int ALLOWED_CAPACITY_3 = 15;


            foreach (Item item in NonRecycleItems)
            {
                if (!item.Volume.Equals(ALLOWED_CAPACITY_1) && !item.Volume.Equals(ALLOWED_CAPACITY_2) && !item.Volume.Equals(ALLOWED_CAPACITY_3))
                {
                    if (item.IsToxicWaste)
                    {
                        if (item.Type == EType.Electronics || item.Type == EType.Furniture)
                        {
                            outItems.Add(item);
                            continue;
                        }
                        continue;
                    }

                    outItems.Add(item);
                    continue;
                }

                if (item.Type == EType.Electronics || item.Type == EType.Furniture)
                {
                    outItems.Add(item);
                }
            }

            return outItems;
        }
    }
}

