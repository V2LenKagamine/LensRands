using System.Linq;
using LensRands.Content.Buffs;
using LensRands.Systems;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Accessories
{
    public abstract class RORLunar : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/";
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }
    }

    public class BeadsOfFealty : RORLunar
    {
        public override string Texture => base.Texture + "Beads";
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().BeadsOn = true;
        }
    }

    public class LightFlux : RORLunar
    {
        public override string Texture => base.Texture + "LightFlux";
        public readonly int DamageIncrease = 55;
        public readonly int SpeedDecrease = 50;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageIncrease, SpeedDecrease);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().LightFluxOn = true;
        }
    }

    public class MercurialRachis : RORLunar
    {
        public override string Texture => base.Texture + "MercRach";
        public readonly int DamageIncrease = 50;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageIncrease);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().MercRachOn = true;
        }
    }
    public abstract class RORBoss : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/";
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }
    }

    public class ChargedPerforator : RORBoss
    {
        public override string Texture => base.Texture + "Charged";

        public readonly int ChargedChance = 5;
        public readonly float ChargedDamage = 500;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ChargedChance,ChargedDamage);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().ChargedOn = true;
        }
    }

    public class QueensGland : RORBoss
    {
        public override string Texture => base.Texture + "QueensGland";
        public readonly float QueensDmg = 0.1f;
        public readonly int QueensMinions = 1;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(QueensMinions, (int)(QueensDmg*100));

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += QueensMinions;
            player.GetDamage(DamageClass.Summon) *= 1f + QueensDmg;
        }
    }

    public class HalcyonSeed : RORBoss
    {
        public override string Texture => base.Texture + "Halcyon";
        public readonly float HalcyonDmg = 0.2f;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(HalcyonDmg * 100));

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) *= 1f + HalcyonDmg;
        }
    } 
    public class RoRPearl : RORBoss
    {
        public override string Texture => base.Texture + "Pearl";
        public readonly int PearlHealth = 50;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(PearlHealth);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += PearlHealth;
        }
    }

    public class IrradiantPearl : RORBoss
    {
        public override string Texture => base.Texture + "IrradiantPearl";
        public readonly int PearlHealth = 50;
        public readonly int PearlMana = 50;
        public readonly float PearlDmg = 0.1f;
        public readonly float PearlDefence = 0.1f;
        public readonly float PearlRunSpeed = 0.1f;
        public readonly float PearlAtkSpeed = 0.1f;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(PearlHealth,PearlMana,(int)(PearlDmg * 100), (int)(PearlAtkSpeed * 100), (int)(PearlDefence * 100), (int)(PearlRunSpeed * 100));

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += PearlHealth;
            player.statManaMax2 += PearlMana;
            player.statDefense *= 1f + PearlDefence;
            player.moveSpeed *= 1f + PearlRunSpeed;
            player.GetDamage(DamageClass.Generic) *= 1f + PearlDmg;
            player.GetAttackSpeed(DamageClass.Generic) *= 1f + PearlAtkSpeed;
        }
    }

    public abstract class RORVoid : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/";
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
        }
    }

    public class SaferSpaces : RORVoid
    {
        public override string Texture => base.Texture + "Safer";

        private readonly int SaferTimer = 45; //45 Seconds
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SaferTimer);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!player.buffType.Contains(ModContent.BuffType<SaferSpacesBuff>()) && !player.buffType.Contains(ModContent.BuffType<SaferSpacesDebuff>()))
            {
                player.AddBuff(ModContent.BuffType<SaferSpacesBuff>(), 10);
            }
        }
    }
}
