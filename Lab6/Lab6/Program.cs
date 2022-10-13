using System.Collections.Generic;
using System.Diagnostics;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            Item item1 = new Item(EType.Plastic, 3.4, 10, false);
            Item item2 = new Item(EType.Glass, 5, 16, true);
            Item item3 = new Item(EType.Compost, 1.2, 5, true);
            Item item4 = new Item(EType.Paper, 444, 34, true);
            Item item5 = new Item(EType.Furniture, 10.2, 45, false);
            Item item6 = new Item(EType.Paper, 15.7, 15, true);
            Item item7 = new Item(EType.Electronics, 1.1, 15, false);
            Item item8 = new Item(EType.Furniture, 3.91, 11, true);

            List<Item> items = new List<Item>
            {
                item1,
                item2,
                item3,
                item4,
                item5,
                item6,
                item7,
                item8
            };

            Recyclebot bot = new Recyclebot();

            foreach (Item item in items)
            {
                bot.Add(item);
            }

            List<Item> expectedRecyclables = new List<Item>
            {
                item1,
                item2,
                item3,
                item8
            };

            Debug.Assert(bot.RecycleItems.Count == expectedRecyclables.Count);

            for (int i = 0; i < expectedRecyclables.Count; i++)
            {
                Debug.Assert(itemEquals(bot.RecycleItems[i], expectedRecyclables[i]));
            }

            List<Item> expectedNonRecyclables = new List<Item>
            {
                item4,
                item5,
                item6,
                item7
            };

            Debug.Assert(bot.NonRecycleItems.Count == expectedNonRecyclables.Count);

            for (int i = 0; i < expectedNonRecyclables.Count; i++)
            {
                Debug.Assert(itemEquals(bot.NonRecycleItems[i], expectedNonRecyclables[i]));
            }

            List<Item> expectedDumps = new List<Item>
            {
                item5,
                item7
            };

            List<Item> dumps = bot.Dump();

            Debug.Assert(dumps.Count == expectedDumps.Count);

            for (int i = 0; i < expectedDumps.Count; i++)
            {
                Debug.Assert(itemEquals(dumps[i], expectedDumps[i]));
            }
        }

        static bool itemEquals(Item item1, Item item2)
        {
            return (
                item1.Type == item2.Type
                && item1.Weight == item2.Weight
                && item1.Volume == item2.Volume
                && item1.IsToxicWaste == item2.IsToxicWaste
            );
        }
    }
}
