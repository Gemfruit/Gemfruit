using System;
using System.Linq;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Utility;
using Gemfruit.Mod.Internal;

namespace Gemfruit.Mod.Items
{
    public class Item
    {
        public RegistryKey Key { get; protected set; }
        
        public string Name { get; protected set; }
        public int Price { get; protected set; }
        public int Edibility { get; protected set; }
        public string Type { get; protected set; }
        public int Category { get; protected set; }
        public string DisplayName { get; protected set; }
        public string Description { get; protected set; }

        protected Item()
        {
            
        }
        
        protected Item(Item bas)
        {
            Key = bas.Key;
            Name = bas.Name;
            Price = bas.Price;
            Edibility = bas.Edibility;
            Type = bas.Type;
            Category = bas.Category;
            DisplayName = bas.DisplayName;
            Description = bas.Description;
        }

        internal static Result<Item, Exception> ParseFromString(string line)
        {
            var i = new Item();
            var parts = line.Split('/');

            try
            {
                // index 0 - Name
                i.Name = parts[0];
                // index 1 - Price
                i.Price = int.Parse(parts[1]);
                // index 2 - edibility
                i.Edibility = int.Parse(parts[2]);
                // index 3 - type & category
                var tc = parts[3].Split(' ');
                i.Type = tc[0];
                if (tc.Length > 1) i.Category = int.Parse(tc[1]);
                // index 4 - display name
                i.DisplayName = parts[4];
                // index 5 - description
                i.Description = parts[5];

                if (parts.Length >= 9)
                {
                    switch (parts[6].ToLower())
                    {
                        case "food":
                            i = new FoodItem(i, FoodType.Food, new FoodBuff(parts[7].Split().Select(int.Parse).ToList(), int.Parse(parts[8])));
                            break;
                        case "drink":
                            i = new FoodItem(i, FoodType.Drink, new FoodBuff(parts[7].Split().Select(int.Parse).ToList(), int.Parse(parts[8])));
                            break;
                    }
                }

                // TODO: This is fairly fragile. Possibly change this?
                if (i.Name.Contains("Geode"))
                {
                    i = new GeodeItem(i, parts[6].Split().Select(int.Parse).ToList());
                }
            }
            catch (Exception e)
            {
                return Result<Item, Exception>.FromException(e);
            }

            return Result<Item, Exception>.FromValue(i);
        }
    }
}