using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using LensRands.Content.Tiles;

namespace LensRands.Content.Items.Placeable
{
    public abstract class Vendor : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Placeables/VendingMachine";

        public abstract int ToCreate { get; }
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
            Item.createTile = ToCreate;
        }
    }
    public class CombatPotionVendor : Vendor
    {
        public override int ToCreate => ModContent.TileType<CombatPotVendorTile>();
    }
    public class UtilPotionVendor : Vendor
    {
        public override int ToCreate => ModContent.TileType<UtilPotVendorTile>();
    }
    public class GeneralBlocksVendor : Vendor
    {
        public override int ToCreate => ModContent.TileType<GeneralBlocksVendorTile>();
    }
    public class AccessoriesVendor : Vendor
    {
        public override int ToCreate => ModContent.TileType<AccessoryVendorTile>();
    }
    public class BossVendor : Vendor
    {
        public override int ToCreate => ModContent.TileType<BossFightVendorTile>();
    }
    public class FishingVendor : Vendor
    {
        public override int ToCreate => ModContent.TileType<FishingVendorTile>();
    }
}
