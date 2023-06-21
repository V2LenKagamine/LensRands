using LensRands.Systems;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Accessories
{
    public abstract class EndlessMunitions : ModItem
    {
        public abstract float damageMod { get; }
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/EndlessMunitions";

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(damageMod*100));
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
            Item.value = Item.buyPrice(0, 2, 50, 0);
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<LensPlayer>().EndlessMunitionsOn = true;
            player.GetDamage(DamageClass.Ranged) += damageMod;
            
        }
    }
    public class EndlessMunitions1 : EndlessMunitions
    {
        public override float damageMod => -0.15f;
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MusketBall, 999)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
    public class EndlessMunitions2 : EndlessMunitions
    {
        public override float damageMod => -0.1f;
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EndlessMunitions1>())
                .AddIngredient(ItemID.Bone, 50)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
    public class EndlessMunitions3 : EndlessMunitions
    {
        public override float damageMod => -0.05f;
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EndlessMunitions2>())
                .AddIngredient(ItemID.RangerEmblem)
                .AddIngredient(ItemID.SoulofFright,15)
                .AddIngredient(ItemID.SoulofMight, 15)
                .AddIngredient(ItemID.SoulofSight, 15)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
    public class EndlessMunitions4 : EndlessMunitions
    {
        public override float damageMod => 0f;
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EndlessMunitions3>())
                .AddIngredient(ItemID.SniperRifle)
                .AddIngredient(ItemID.DestroyerEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
    public class EndlessMunitions5 : EndlessMunitions
    {
        public override float damageMod => 0.05f;
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EndlessMunitions4>())
                .AddIngredient(ItemID.LunarBar,15)
                .AddIngredient(ItemID.FragmentVortex,15)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
    public class EndlessMunitions6 : EndlessMunitions
    {
        public override float damageMod => 0.1f;
        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) && calamity.TryFind("DarksunFragment", out ModItem moditem))
            {
                CreateRecipe()
                .AddIngredient(ModContent.ItemType<EndlessMunitions5>())
                .AddIngredient(moditem,20)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            }
        }
    }
}
