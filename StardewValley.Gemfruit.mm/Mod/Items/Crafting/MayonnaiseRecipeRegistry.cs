using System.Collections.Generic;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Items.Crafting.Recipe;

namespace Gemfruit.Mod.Items.Crafting
{
    public class MayonnaiseRecipeRegistry : KeyedRegistry<MayonnaiseRecipe>
    {
        private static readonly Dictionary<ResourceKey, MayonnaiseRecipe> RecipeList =
            new Dictionary<ResourceKey, MayonnaiseRecipe>();

        private void VanillaRegistration()
        {
            Register(new ResourceKey("dinosaur_egg_dinosaur_mayonnaise"), new MayonnaiseRecipe(new ResourceKey("dinosaur_egg"), new ResourceKey("dinosaur_mayonnaise"), 180));
            Register(new ResourceKey("white_egg_mayonnaise"), new MayonnaiseRecipe(new ResourceKey("white_egg"), new ResourceKey("mayonnaise"), 180));
            Register(new ResourceKey("brown_egg_mayonnaise"), new MayonnaiseRecipe(new ResourceKey("brown_egg"), new ResourceKey("mayonnaise"), 180));
            Register(new ResourceKey("large_white_egg_mayonnaise"), new MayonnaiseRecipe(new ResourceKey("large_white_egg"), new ResourceKey("mayonnaise"), 180, ItemQuality.Gold));
            Register(new ResourceKey("large_brown_egg_mayonnaise"), new MayonnaiseRecipe(new ResourceKey("large_brown_egg"), new ResourceKey("mayonnaise"), 180, ItemQuality.Gold));
            Register(new ResourceKey("void_egg_void_mayonnaise"), new MayonnaiseRecipe(new ResourceKey("void_egg"), new ResourceKey("void_mayonnaise"), 180));
            Register(new ResourceKey("duck_egg_duck_mayonnaise"), new MayonnaiseRecipe(new ResourceKey("duck_egg"), new ResourceKey("duck_mayonnaise"), 180));
        }
        
        protected override bool HasKey(ResourceKey key)
        {
            return RecipeList.ContainsKey(key);
        }

        protected override void AddItem(ResourceKey key, MayonnaiseRecipe recipe)
        {
            RecipeList.Add(key, recipe);
        }

        protected override void InitializeRecords()
        {
            VanillaRegistration();
            // TODO: Fire Events
        }
    }
}