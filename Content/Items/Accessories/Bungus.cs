using LensRands.Systems.PlayerSys;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Accessories
{
    public class Bungus : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/Bungus";

        public readonly int BungusHeal = 4;
        public readonly int BungusRange = 150;
        public readonly int StillForMax = 180; 
        public int stilltimer = 0;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BungusHeal,BungusRange,StillForMax/60);

        /*
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.GreenMushroom)
                .AddIngredient(ItemID.Glowstick,5)
                .AddIngredient(ItemID.HealingPotion,5)
                .AddTile(TileID.DyeVat)
                .Register();
        }
        */
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.velocity.X == 0 && player.velocity.Y == 0 && player.active)
            {
                if (stilltimer >= StillForMax)
                {
                    player.GetModPlayer<LensPlayer>().BungusActive = true;
                }
                else
                {
                    stilltimer++;
                }
            }
            else
            {
                stilltimer = 0;
                player.GetModPlayer<LensPlayer>().BungusActive = false;
            }
            
        }
    }
}
