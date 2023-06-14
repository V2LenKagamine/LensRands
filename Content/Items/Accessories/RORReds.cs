using System;
using System.Linq;
using LensRands.Content.Buffs;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using LensRands.Systems;

namespace LensRands.Content.Items.Accessories
{
    public abstract class RORReds : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/";
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }
    }
    public class DiosBestFriend : RORReds
    {
        private readonly int diotimer = 10; //10 Minutes
        public override string Texture => base.Texture + "Dios";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(diotimer);
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!player.buffType.Contains(ModContent.BuffType<DiosBestFriendBuff>()) && !player.buffType.Contains(ModContent.BuffType<DiosBestFriendDebuff>()))
            {
                player.AddBuff(ModContent.BuffType<DiosBestFriendBuff>(), 10);
            }
        }
    }
    public class Aegis : RORReds
    {
        public override string Texture => base.Texture + "Aegis";

        public readonly int AegisHealPercent = 50;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AegisHealPercent);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().AegisOn = true;
        }
    }
    public class SymScorp : RORReds
    {
        public readonly int SymScorpReduc = 2;
        public readonly int SymScorpChance = 25;
        public override string Texture => base.Texture + "SymScorp";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SymScorpChance,SymScorpReduc);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().SymScorpOn = true;
        }
    }
    public class SpareDroneParts : RORReds
    {
        public readonly int SpareDroneMinions = 2;
        public readonly float SpareDroneDmg = 0.05f;
        public override string Texture => base.Texture + "SpareDroneParts";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SpareDroneMinions,(int)(SpareDroneDmg * 100));

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += SpareDroneMinions;
            player.GetDamage(DamageClass.Summon) *= 1f + SpareDroneDmg;
        }
    }
    public class AlienHead : RORReds
    {
        public readonly int AlienMana = 50;
        public readonly float AlienRegen = 0.25f;

        public override string Texture => base.Texture + "AlienHead";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AlienMana, (int)(AlienRegen * 100));
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += AlienMana;
            player.manaRegen *= (int)(1f + AlienRegen);
        }
    }
    public class Brilliant : RORReds
    {
        public override string Texture => base.Texture + "Brilliant";

        public readonly int BrilliantDamage = 30;
        public readonly float BrilliantDamageReduc = 0.15f;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BrilliantDamage, (int)(BrilliantDamageReduc * 100));

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) *= 1f - BrilliantDamageReduc;
            player.GetModPlayer<LensPlayer>().BrilliantOn = true;
        }
    }
}
