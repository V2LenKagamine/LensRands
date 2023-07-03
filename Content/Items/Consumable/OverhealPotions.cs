using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LensRands.Systems;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Consumable
{
    public abstract class OverhealPotions : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Consumable/Overheal/";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ToOverheal);
        public abstract int ToHeal { get; }
        public abstract int ToOverheal { get; }
        public abstract int RarityColor { get; }
        public override void SetDefaults()
        {
            Item.DefaultToHealingPotion(16, 16, ToHeal);
            Item.rare = RarityColor;
        }
        public override void OnConsumeItem(Player player)
        {
            player.GetModPlayer<LensPlayer>().AddOverheal(ToOverheal);
        }
    }
    public class LesserOverhealPotion : OverhealPotions
    {
        public override string Texture => base.Texture + "Lesser";
        public override int ToHeal => 25;
        public override int ToOverheal => 50;
        public override int RarityColor => ItemRarityID.White;

        public override void AddRecipes()
        {
            CreateRecipe(2)
                .AddIngredient(ItemID.LesserHealingPotion,2)
                .AddIngredient(ItemID.Mushroom)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
    public class OverhealPotion : OverhealPotions
    {
        public override string Texture => base.Texture + "Normal";
        public override int ToHeal => 50;

        public override int ToOverheal => 100;

        public override int RarityColor => ItemRarityID.Blue;

        public override void AddRecipes()
        {
            CreateRecipe(2)
                .AddIngredient(ItemID.HealingPotion,2)
                .AddIngredient(ItemID.GlowingMushroom)
                .AddTile(TileID.Bottles)
                .Register();

            CreateRecipe(2)
                .AddIngredient(ModContent.ItemType<LesserOverhealPotion>(), 2)
                .AddIngredient(ItemID.GlowingMushroom)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
    public class GreaterOverhealPotion : OverhealPotions
    {
        public override string Texture => base.Texture + "Greater";
        public override int ToHeal => 75;

        public override int ToOverheal => 150;

        public override int RarityColor => ItemRarityID.Orange;
        public override void AddRecipes()
        {
            CreateRecipe(3)
                .AddIngredient(ItemID.PixieDust,3)
                .AddIngredient(ItemID.CrystalShard)
                .AddIngredient(ItemID.GreaterHealingPotion,3)
                .AddTile(TileID.Bottles)
                .Register();

            CreateRecipe(3)
               .AddIngredient(ItemID.PixieDust, 3)
               .AddIngredient(ItemID.CrystalShard)
               .AddIngredient(ModContent.ItemType<OverhealPotion>(), 3)
               .AddTile(TileID.Bottles)
               .Register();
        }


    }
    public class SuperOverhealPotion : OverhealPotions
    {
        public override string Texture => base.Texture + "Super";
        public override int ToHeal => 100;

        public override int ToOverheal => 200;

        public override int RarityColor => ItemRarityID.Lime;
        public override void AddRecipes()
        {
            CreateRecipe(4)
                .AddIngredient(ItemID.GreaterHealingPotion,4)
                .AddIngredient(ItemID.FragmentVortex)
                .AddIngredient(ItemID.FragmentNebula)
                .AddIngredient(ItemID.FragmentSolar)
                .AddIngredient(ItemID.FragmentStardust)
                .AddTile(TileID.Bottles)
                .Register();

            CreateRecipe(4)
                .AddIngredient(ModContent.ItemType<GreaterOverhealPotion>(), 4)
                .AddIngredient(ItemID.FragmentVortex)
                .AddIngredient(ItemID.FragmentNebula)
                .AddIngredient(ItemID.FragmentSolar)
                .AddIngredient(ItemID.FragmentStardust)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
