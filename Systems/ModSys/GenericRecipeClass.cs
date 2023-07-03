using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Systems.ModSys
{
    public class GenericRecipeClass : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe.Create(ItemID.RodofDiscord, 1)
                .AddIngredient(ItemID.ChaosElementalBanner, 2)
                .AddTile(TileID.TinkerersWorkbench)
                .DisableDecraft()
                .Register();

            Recipe.Create(ItemID.GreaterHealingPotion, 4)
                .AddIngredient(ItemID.PixieDust, 3)
                .AddIngredient(ItemID.CrystalShard)
                .AddIngredient(ItemID.HealingPotion, 4)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
