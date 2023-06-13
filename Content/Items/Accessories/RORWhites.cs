﻿using LensRands.Systems.PlayerSys;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;

namespace LensRands.Content.Items.Accessories
{
    public abstract class RORWhites : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/";
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
        }
    }
    public class Pennies : RORWhites
    {

        public override string Texture => base.Texture + "Pennies";
        public readonly int PenniesAmount = 3; //In copper coins.
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(PenniesAmount);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().Pennies = true;
        }
    }
    public class Soldiers : RORWhites
    {
        public readonly float atkspeedinc = 0.15f;
        public readonly float damagePen = 0.025f;
        public override string Texture => base.Texture + "Soldiers";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(15, damagePen * 100);//Make sure attackspeed = arg 1, because float inprecision stupid.
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.Generic) *= 1f + atkspeedinc;
            player.GetDamage(DamageClass.Generic) *= 1f - damagePen;
        }
    }
    public class Bungus : RORWhites
    {
        public override string Texture => base.Texture + "Bungus";

        public readonly int BungusHeal = 4;
        public readonly int BungusRange = 150;
        public readonly int StillForMax = 180;
        public int stilltimer = 0;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BungusHeal, BungusRange, StillForMax / 60);
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
    public class TougherTimes : RORWhites
    {
        public override string Texture => base.Texture + "Tougher";
        public readonly int definc = 8;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(definc);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += definc;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddCustomShimmerResult(ModContent.ItemType<SaferSpaces>())
                .AddCondition(Condition.InJourneyMode)
                .Register();
        }
    }
    public class ArmorPiercingRounds : RORWhites
    {
        public override string Texture => base.Texture + "APR";
        public readonly int dmginc = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(dmginc);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().APR = true;
        }
    }
}