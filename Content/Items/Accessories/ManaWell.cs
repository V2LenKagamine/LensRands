using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LensRands.Systems;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;

namespace LensRands.Content.Items.Accessories
{
    public abstract class ManaWell : ModItem
    {
        public abstract float damageMod { get; }
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/ManaWell";

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(damageMod * 100));
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
            player.GetModPlayer<LensPlayer>().ManaWellOn = true;
            player.GetDamage(DamageClass.Magic) += damageMod;
            player.manaCost *= 0;

        }
    }
    public class ManaWell1 : ManaWell
    {
        public override float damageMod => -0.3f;
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FallenStar, 50)
                .AddIngredient(ItemID.LesserManaPotion,50)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
    public class ManaWell2 : ManaWell
    {
        public override float damageMod => -0.2f;
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ManaWell1>())
                .AddIngredient(ItemID.Bone, 50)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
    public class ManaWell3 : ManaWell
    {
        public override float damageMod => -0.1f;
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ManaWell2>())
                .AddIngredient(ItemID.AvengerEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
    public class ManaWell4 : ManaWell
    {
        public override float damageMod => 0f;
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ManaWell3>())
                .AddIngredient(ItemID.MagnetSphere)
                .AddIngredient(ItemID.DestroyerEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
    public class ManaWell5 : ManaWell
    {
        public override float damageMod => 0.1f;
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ManaWell4>())
                .AddIngredient(ItemID.LunarBar, 5)
                .AddIngredient(ItemID.FragmentNebula, 15)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
    public class ManaWell6 : ManaWell
    {
        public override float damageMod => 0.2f;
        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) && calamity.TryFind("DarksunFragment", out ModItem moditem))
            {
                CreateRecipe()
                .AddIngredient(ModContent.ItemType<ManaWell5>())
                .AddIngredient(moditem, 20)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            }
        }
    }
}
