using System;
using System.Collections.Generic;
using Gemfruit.Mod.API;

namespace Gemfruit.Mod.Internal.Transformers
{
    public class CropConverter
    {
        private static readonly Dictionary<string, ResourceKey> CropMap = new Dictionary<string, ResourceKey>()
        {
            {"273", new ResourceKey("unmilled_rice")}, 
            {"299", new ResourceKey("amaranth")},
            {"301", new ResourceKey("grape")},
            {"302", new ResourceKey("hops")},
            {"347", new ResourceKey("gem_berry")},
            {"425", new ResourceKey("fairy_rose")},
            {"427", new ResourceKey("tulip")},
            {"429", new ResourceKey("blue_jazz")},
            {"431", new ResourceKey("sunflower")},
            {"433", new ResourceKey("coffee")},
            {"453", new ResourceKey("poppy")},
            {"455", new ResourceKey("summer_spangle")},
            {"472", new ResourceKey("parsnip")},
            {"473", new ResourceKey("green_bean")},
            {"474", new ResourceKey("cauliflower")},
            {"475", new ResourceKey("potato")},
            {"476", new ResourceKey("garlic")},
            {"477", new ResourceKey("kale")},
            {"478", new ResourceKey("rhubarb")},
            {"479", new ResourceKey("melon")},
            {"480", new ResourceKey("tomato")},
            {"481", new ResourceKey("blueberry")},
            {"482", new ResourceKey("hot_pepper")},
            {"483", new ResourceKey("wheat")},
            {"484", new ResourceKey("radish")},
            {"485", new ResourceKey("red_cabbage")},
            {"486", new ResourceKey("starfruit")},
            {"487", new ResourceKey("corn")},
            {"488", new ResourceKey("eggplant")},
            {"489", new ResourceKey("artichoke")},
            {"490", new ResourceKey("pumpkin")},
            {"491", new ResourceKey("bok_choy")},
            {"492", new ResourceKey("yam")},
            {"493", new ResourceKey("cranberries")},
            {"494", new ResourceKey("beet")},
            {"495", new ResourceKey("spring_seeds")},
            {"496", new ResourceKey("summer_seeds")},
            {"497", new ResourceKey("fall_seeds")},
            {"498", new ResourceKey("winter_seeds")},
            {"499", new ResourceKey("ancient_fruit")},
            {"745", new ResourceKey("strawberry")},
            {"802", new ResourceKey("cactus_fruit")}
        };

        public ResourceKey GetResourceKeyFromCropId(string id)
        {
            if (CropMap.ContainsKey(id))
            {
                return CropMap[id];
            }

            throw new ArgumentException($"unknown vanilla crop id '{id}'!");
        }
    }
}