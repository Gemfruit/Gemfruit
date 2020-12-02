using System.Collections.Generic;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Items.Crafting.Recipe;

namespace Gemfruit.Mod.Items.Crafting
{
    public class CrystalariumRecipeRegistry : KeyedRegistry<CrystalariumRecipe>
    {
        private static readonly Dictionary<ResourceKey, CrystalariumRecipe> RecipeList =
            new Dictionary<ResourceKey, CrystalariumRecipe>();

        private void VanillaRegistration()
        {
            Register(new ResourceKey("emerald"), new CrystalariumRecipe(new ResourceKey("emerald"), 3000));
            Register(new ResourceKey("aquamarine"), new CrystalariumRecipe(new ResourceKey("aquamarine"), 2240));
            Register(new ResourceKey("ruby"), new CrystalariumRecipe(new ResourceKey("ruby"), 3000));
            Register(new ResourceKey("amethyst"), new CrystalariumRecipe(new ResourceKey("amethyst"), 1360));
            Register(new ResourceKey("topaz"), new CrystalariumRecipe(new ResourceKey("topaz"), 1120));
            Register(new ResourceKey("jade"), new CrystalariumRecipe(new ResourceKey("jade"), 2400));
            Register(new ResourceKey("diamond"), new CrystalariumRecipe(new ResourceKey("diamond"), 7200));
            Register(new ResourceKey("quartz"), new CrystalariumRecipe(new ResourceKey("quartz"), 420));
            Register(new ResourceKey("fire_quartz"), new CrystalariumRecipe(new ResourceKey("fire_quartz"), 1300));
            Register(new ResourceKey("frozen_tear"), new CrystalariumRecipe(new ResourceKey("frozen_tear"), 1120));
            Register(new ResourceKey("earth_crystal"), new CrystalariumRecipe(new ResourceKey("earth_crystal"), 800));
            Register(new ResourceKey("tigerseye"), new CrystalariumRecipe(new ResourceKey("tigerseye"), 5000));
            Register(new ResourceKey("opal"), new CrystalariumRecipe(new ResourceKey("opal"), 5000));
            Register(new ResourceKey("fire_opal"), new CrystalariumRecipe(new ResourceKey("fire_opal"), 5000));
            Register(new ResourceKey("alamite"), new CrystalariumRecipe(new ResourceKey("alamite"), 5000));
            Register(new ResourceKey("bixite"), new CrystalariumRecipe(new ResourceKey("bixite"), 5000));
            Register(new ResourceKey("baryte"), new CrystalariumRecipe(new ResourceKey("baryte"), 5000));
            Register(new ResourceKey("aerinite"), new CrystalariumRecipe(new ResourceKey("aerinite"), 5000));
            Register(new ResourceKey("calcite"), new CrystalariumRecipe(new ResourceKey("calcite"), 5000));
            Register(new ResourceKey("dolomite"), new CrystalariumRecipe(new ResourceKey("dolomite"), 5000));
            Register(new ResourceKey("esperite"), new CrystalariumRecipe(new ResourceKey("esperite"), 5000));
            Register(new ResourceKey("fluorapatite"), new CrystalariumRecipe(new ResourceKey("fluorapatite"), 5000));
            Register(new ResourceKey("geminite"), new CrystalariumRecipe(new ResourceKey("geminite"), 5000));
            Register(new ResourceKey("helvite"), new CrystalariumRecipe(new ResourceKey("helvite"), 5000));
            Register(new ResourceKey("jamborite"), new CrystalariumRecipe(new ResourceKey("jamborite"), 5000));
            Register(new ResourceKey("jagoite"), new CrystalariumRecipe(new ResourceKey("jagoite"), 5000));
            Register(new ResourceKey("kyanite"), new CrystalariumRecipe(new ResourceKey("kyanite"), 5000));
            Register(new ResourceKey("lunarite"), new CrystalariumRecipe(new ResourceKey("lunarite"), 5000));
            Register(new ResourceKey("malachite"), new CrystalariumRecipe(new ResourceKey("malachite"), 5000));
            Register(new ResourceKey("neptunite"), new CrystalariumRecipe(new ResourceKey("neptunite"), 5000));
            Register(new ResourceKey("lemon_stone"), new CrystalariumRecipe(new ResourceKey("lemon_stone"), 5000));
            Register(new ResourceKey("nekoite"), new CrystalariumRecipe(new ResourceKey("nekoite"), 5000));
            Register(new ResourceKey("orpiment"), new CrystalariumRecipe(new ResourceKey("orpiment"), 5000));
            Register(new ResourceKey("petrified_slime"),new CrystalariumRecipe(new ResourceKey("petrified_slime"), 5000));
            Register(new ResourceKey("thunder_egg"), new CrystalariumRecipe(new ResourceKey("thunder_egg"), 5000));
            Register(new ResourceKey("pyrite"), new CrystalariumRecipe(new ResourceKey("pyrite"), 5000));
            Register(new ResourceKey("ocean_stone"), new CrystalariumRecipe(new ResourceKey("ocean_stone"), 5000));
            Register(new ResourceKey("ghost_crystal"), new CrystalariumRecipe(new ResourceKey("ghost_crystal"), 5000));
            Register(new ResourceKey("jasper"), new CrystalariumRecipe(new ResourceKey("jasper"), 5000));
            Register(new ResourceKey("celestine"), new CrystalariumRecipe(new ResourceKey("celestine"), 5000));
            Register(new ResourceKey("marble"), new CrystalariumRecipe(new ResourceKey("marble"), 5000));
            Register(new ResourceKey("sandstone"), new CrystalariumRecipe(new ResourceKey("sandstone"), 5000));
            Register(new ResourceKey("granite"), new CrystalariumRecipe(new ResourceKey("granite"), 5000));
            Register(new ResourceKey("basalt"), new CrystalariumRecipe(new ResourceKey("basalt"), 5000));
            Register(new ResourceKey("limestone"), new CrystalariumRecipe(new ResourceKey("limestone"), 5000));
            Register(new ResourceKey("soapstone"), new CrystalariumRecipe(new ResourceKey("soapstone"), 5000));
            Register(new ResourceKey("hematite"), new CrystalariumRecipe(new ResourceKey("hematite"), 5000));
            Register(new ResourceKey("mudstone"), new CrystalariumRecipe(new ResourceKey("mudstone"), 5000));
            Register(new ResourceKey("obsidian"), new CrystalariumRecipe(new ResourceKey("obsidian"), 5000));
            Register(new ResourceKey("slate"), new CrystalariumRecipe(new ResourceKey("slate"), 5000));
            Register(new ResourceKey("fairy_stone"), new CrystalariumRecipe(new ResourceKey("fairy_stone"), 5000));
            Register(new ResourceKey("star_shards"), new CrystalariumRecipe(new ResourceKey("star_shards"), 5000));
        }

        protected override bool HasKey(ResourceKey key)
        {
            return RecipeList.ContainsKey(key);
        }

        protected override void AddItem(ResourceKey key, CrystalariumRecipe item)
        {
            RecipeList.Add(key, item);
        }

        protected override void InitializeRecords()
        {
            VanillaRegistration();
            // TODO: Fire Events
        }
    }
}