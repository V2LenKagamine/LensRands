using LensRands.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Placeable
{
    public class MutationStation : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Placeables/MutationStation";
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item1;
            Item.createTile = ModContent.TileType<MutationBuffStationTile>();
        }
    }
}
