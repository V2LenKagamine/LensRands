using LensRands.Systems.PlayerSys;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Accessories
{
    public class Ukelele : ModItem
    {

        public readonly float UkeleleChance = 25f;
        public readonly int UkeleleHits = 3;
        public readonly float UkeDamage = 12.5f;
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/Ukelele";

        /*
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.Wood,35)
                .AddIngredient(ItemID.LightningBug,15)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
        */

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(UkeleleChance,UkeleleHits,UkeDamage);

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().UkeleleOn = true;
        }

    }
}
