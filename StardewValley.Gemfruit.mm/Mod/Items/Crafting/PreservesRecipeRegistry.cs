using System.Collections.Generic;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Exceptions;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Items.Crafting.Recipe;

namespace Gemfruit.Mod.Items.Crafting
{
    public class PreservesRecipeRegistry : KeyedRegistry<PreservesRecipe>
    {
        private static readonly Dictionary<ResourceKey, PreservesRecipe> RecipeList =
            new Dictionary<ResourceKey, PreservesRecipe>();

        private void VanillaRegistration()
        {
            Register(new ResourceKey("amaranth_pickles"),
                new PreservesRecipe(new ResourceKey("amaranth"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("artichoke_pickles"),
                new PreservesRecipe(new ResourceKey("artichoke"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("beet_pickles"),
                new PreservesRecipe(new ResourceKey("beet"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("bok_choy_pickles"),
                new PreservesRecipe(new ResourceKey("bok_choy"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("cauliflower_pickles"),
                new PreservesRecipe(new ResourceKey("cauliflower"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("corn_pickles"),
                new PreservesRecipe(new ResourceKey("corn"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("eggplant_pickles"),
                new PreservesRecipe(new ResourceKey("eggplant"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("fiddlehead_fern_pickles"),
                new PreservesRecipe(new ResourceKey("fiddlehead_fern"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("garlic_pickles"),
                new PreservesRecipe(new ResourceKey("garlic"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("green_bean_pickles"),
                new PreservesRecipe(new ResourceKey("green_bean"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("hops_pickles"),
                new PreservesRecipe(new ResourceKey("hops"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("kale_pickles"),
                new PreservesRecipe(new ResourceKey("kale"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("parsnip_pickles"),
                new PreservesRecipe(new ResourceKey("parsnip"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("potato_pickles"),
                new PreservesRecipe(new ResourceKey("potato"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("pumpkin_pickles"),
                new PreservesRecipe(new ResourceKey("pumpkin"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("radish_pickles"),
                new PreservesRecipe(new ResourceKey("radish"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("red_cabbage_pickles"),
                new PreservesRecipe(new ResourceKey("red_cabbage"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("tea_leaves_pickles"),
                new PreservesRecipe(new ResourceKey("tea_leaves"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("tomato_pickles"),
                new PreservesRecipe(new ResourceKey("tomato"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("unmilled_rice_pickles"),
                new PreservesRecipe(new ResourceKey("unmilled_rice"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("wheat_pickles"),
                new PreservesRecipe(new ResourceKey("wheat"), new ResourceKey("pickles"), 4000));
            Register(new ResourceKey("yam_pickles"),
                new PreservesRecipe(new ResourceKey("yam"), new ResourceKey("pickles"), 4000));

            Register(new ResourceKey("ancient_fruit_jelly"),
                new PreservesRecipe(new ResourceKey("ancient_fruit"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("apple_jelly"),
                new PreservesRecipe(new ResourceKey("apple"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("apricot_jelly"),
                new PreservesRecipe(new ResourceKey("apricot"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("blackberry_jelly"),
                new PreservesRecipe(new ResourceKey("blackberry"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("blueberry_jelly"),
                new PreservesRecipe(new ResourceKey("blueberry"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("cactus_fruit_jelly"),
                new PreservesRecipe(new ResourceKey("cactus_fruit"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("cherry_jelly"),
                new PreservesRecipe(new ResourceKey("cherry"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("coconut_jelly"),
                new PreservesRecipe(new ResourceKey("coconut"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("cranberries_jelly"),
                new PreservesRecipe(new ResourceKey("cranberries"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("crystal_fruit_jelly"),
                new PreservesRecipe(new ResourceKey("crystal_fruit"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("grape_jelly"),
                new PreservesRecipe(new ResourceKey("grape"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("hot_pepper_jelly"),
                new PreservesRecipe(new ResourceKey("hot_pepper"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("melon_jelly"),
                new PreservesRecipe(new ResourceKey("melon"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("orange_jelly"),
                new PreservesRecipe(new ResourceKey("orange"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("peach_jelly"),
                new PreservesRecipe(new ResourceKey("peach"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("pomegranate_jelly"),
                new PreservesRecipe(new ResourceKey("pomegranate"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("rhubarb_jelly"),
                new PreservesRecipe(new ResourceKey("rhubarb"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("salmonberry_jelly"),
                new PreservesRecipe(new ResourceKey("salmonberry"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("spice_berry_jelly"),
                new PreservesRecipe(new ResourceKey("spice_berry"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("starfruit_jelly"),
                new PreservesRecipe(new ResourceKey("starfruit"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("strawberry_jelly"),
                new PreservesRecipe(new ResourceKey("strawberry"), new ResourceKey("jelly"), 4000));
            Register(new ResourceKey("wild_plum_jelly"),
                new PreservesRecipe(new ResourceKey("wild_plum"), new ResourceKey("jelly"), 4000));
        }

        protected override bool HasKey(ResourceKey key)
        {
            return RecipeList.ContainsKey(key);
        }

        protected override void AddItem(ResourceKey key, PreservesRecipe item)
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