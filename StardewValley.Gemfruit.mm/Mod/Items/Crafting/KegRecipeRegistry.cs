using System.Collections.Generic;
using System.Linq;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Exceptions;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Internal;
using Gemfruit.Mod.Items.Crafting.Recipe;

namespace Gemfruit.Mod.Items.Crafting
{
    public class KegRecipeRegistry : KeyedRegistry<KegRecipe>
    {
        private static readonly Dictionary<ResourceKey, KegRecipe> RecipeList =
            new Dictionary<ResourceKey, KegRecipe>();

        private void VanillaRegistration()
        {
            Register(new ResourceKey("amaranth_juice"),
                new KegRecipe(new ResourceKey("amaranth"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("artichoke_juice"),
                new KegRecipe(new ResourceKey("artichoke"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("beet_juice"),
                new KegRecipe(new ResourceKey("beet"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("bok_choy_juice"),
                new KegRecipe(new ResourceKey("bok_choy"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("cauliflower_juice"),
                new KegRecipe(new ResourceKey("cauliflower"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("corn_juice"),
                new KegRecipe(new ResourceKey("corn"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("eggplant_juice"),
                new KegRecipe(new ResourceKey("eggplant"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("fiddlehead_fern_juice"),
                new KegRecipe(new ResourceKey("fiddlehead_fern"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("garlic_juice"),
                new KegRecipe(new ResourceKey("garlic"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("green_bean_juice    "),
                new KegRecipe(new ResourceKey("green_bean"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("hops_pale_ale"),
                new KegRecipe(new ResourceKey("hops"), new ResourceKey("pale_ale"), 2250));
            Register(new ResourceKey("kale_juice"),
                new KegRecipe(new ResourceKey("kale"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("parsnip_juice"),
                new KegRecipe(new ResourceKey("parsnip"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("potato_juice"),
                new KegRecipe(new ResourceKey("potato"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("pumpkin_juice"),
                new KegRecipe(new ResourceKey("pumpkin"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("radish_juice"),
                new KegRecipe(new ResourceKey("radish"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("red_cabbage_juice"),
                new KegRecipe(new ResourceKey("red_cabbage"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("tea_leaves_green_tea"),
                new KegRecipe(new ResourceKey("tea_leaves"), new ResourceKey("green_tea"), 180));
            Register(new ResourceKey("tomato_juice"),
                new KegRecipe(new ResourceKey("tomato"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("unmilled_rice_juice"),
                new KegRecipe(new ResourceKey("unmilled_rice"), new ResourceKey("juice"), 6000));
            Register(new ResourceKey("wheat_beer"),
                new KegRecipe(new ResourceKey("wheat"), new ResourceKey("beer"), 1750));
            Register(new ResourceKey("yam_juice"),
                new KegRecipe(new ResourceKey("yam"), new ResourceKey("juice"), 6000));

            Register(new ResourceKey("ancient_fruit_wine"),
                new KegRecipe(new ResourceKey("ancient_fruit"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("apple_wine"),
                new KegRecipe(new ResourceKey("apple"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("apricot_wine"),
                new KegRecipe(new ResourceKey("apricot"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("blackberry_wine"),
                new KegRecipe(new ResourceKey("blackberry"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("blueberry_wine"),
                new KegRecipe(new ResourceKey("blueberry"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("cactus_fruit_wine"),
                new KegRecipe(new ResourceKey("cactus_fruit"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("cherry_wine"),
                new KegRecipe(new ResourceKey("cherry"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("coconut_wine"),
                new KegRecipe(new ResourceKey("coconut"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("cranberries_wine"),
                new KegRecipe(new ResourceKey("cranberries"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("crystal_fruit_wine"),
                new KegRecipe(new ResourceKey("crystal_fruit"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("grape_wine"),
                new KegRecipe(new ResourceKey("grape"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("hot_pepper_wine"),
                new KegRecipe(new ResourceKey("hot_pepper"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("melon_wine"),
                new KegRecipe(new ResourceKey("melon"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("orange_wine"),
                new KegRecipe(new ResourceKey("orange"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("peach_wine"),
                new KegRecipe(new ResourceKey("peach"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("pomegranate_wine"),
                new KegRecipe(new ResourceKey("pomegranate"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("rhubarb_wine"),
                new KegRecipe(new ResourceKey("rhubarb"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("salmonberry_wine"),
                new KegRecipe(new ResourceKey("salmonberry"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("spice_berry_wine"),
                new KegRecipe(new ResourceKey("spice_berry"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("starfruit_wine"),
                new KegRecipe(new ResourceKey("starfruit"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("strawberry_wine"),
                new KegRecipe(new ResourceKey("strawberry"), new ResourceKey("wine"), 10000));
            Register(new ResourceKey("wild_plum_wine"),
                new KegRecipe(new ResourceKey("wild_plum"), new ResourceKey("wine"), 10000));
            
            Register(new ResourceKey("coffee_bean_coffee"),
                new KegRecipe(new ResourceKey("coffee_bean"), new ResourceKey("coffee"), 120, 5));

        }

        protected override bool HasKey(ResourceKey key)
        {
            return RecipeList.ContainsKey(key);
        }

        protected override void AddItem(ResourceKey key, KegRecipe item)
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