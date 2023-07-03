
using LensRands.Systems;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Accessories
{
    public class Carrier : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/Carrier";
        public static readonly int AmmoChance = 20;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoChance);

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar,20)
                .AddIngredient(ItemID.AmmoBox,1)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.value = Item.buyPrice(0, 2, 50, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().CarrierOn = true;
        }
    }
    public class CarrierPrime : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/CarrierPrime";
        public static readonly int AmmoChance = 40;
        public static readonly int DamageAddRanged = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoChance,DamageAddRanged);

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ModContent.ItemType<Carrier>())
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            Item.value = Item.buyPrice(0, 7, 50, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Ranged) += DamageAddRanged / 100f;
            player.GetModPlayer<LensPlayer>().CarrierPrimeOn = true;
        }
    }
}
