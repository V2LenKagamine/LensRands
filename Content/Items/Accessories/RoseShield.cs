
using LensRands.Systems;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using LensRands.Content.Buffs;
using Terraria.Localization;

namespace LensRands.Content.Items.Accessories
{
    public class RoseShield : ModItem
    {
        public readonly int DmgReduc = 5;
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/RoseShield";

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Ruby)
                .AddIngredient(ItemID.Diamond)
                .AddIngredient(ItemID.Glass, 20)
                .AddTile(TileID.GlassKiln)
                .Register();
        }

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DmgReduc);
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 3, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().RoseQuarts = true;
            if (player.whoAmI != Main.myPlayer && player.miscCounter % 15 == 0)
            {
                Player localPlayer = Main.player[Main.myPlayer];
                if (localPlayer.team == player.team && player.team != 0 && player.Distance(localPlayer.Center) <= 800 && !player.GetModPlayer<LensPlayer>().RoseQuarts)
                {
                    // The buff is used to visually indicate to the player that they are defended, and is also synchronized automatically to other players, letting them know that we were defended at the time we took the hit
                    localPlayer.AddBuff(ModContent.BuffType<RoseQuartsBuff>(), 30);
                }
            }
        }
    }
}
