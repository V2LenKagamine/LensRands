using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Accessories
{
    public class MasterWormGear : ModItem
    {
        public float damagemod => 0.17f;
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/WormGear";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(damagemod * 100));
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Expert;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 12, 50, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MasterNinjaGear)
                .AddIngredient(ItemID.WormScarf)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.blackBelt = true;
            player.dashType = 1;
            player.spikedBoots = 2;
            player.endurance += damagemod;
        }
    }
}
