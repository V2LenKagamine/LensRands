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
    public class Gesture : RORLunar
    {
        public override string Texture => base.Texture + "Gesture";
        public readonly float Manarate = 0.5f;
        public readonly int DmgInc = 25;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(Manarate*100),DmgInc);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaCost *= 1f - Manarate;
            if (player.statMana >= player.statManaMax2)
            {
                player.AddBuff(ModContent.BuffType<GestureOverload>(), 2);
            }
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

    public class TitanicKnurl : RORBoss
    {
        public override string Texture => base.Texture + "TitanicKnurl";
        public readonly int KnurlHealth = 50;
        public readonly int KnurlRegen = 4;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(KnurlHealth,KnurlRegen);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += KnurlHealth;
            player.lifeRegen += KnurlRegen;
        }
    }

    public class EmpathyCores : RORBoss
    {
        public override string Texture => base.Texture + "EmpathyCores";
        public readonly int MoreMinions = 2;
        public readonly float DamagePerMinion = 0.025f;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoreMinions, DamagePerMinion * 100);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += MoreMinions;
            player.GetDamage(DamageClass.Summon) *= 1f + (player.maxMinions * DamagePerMinion);
        }
    }
    public class RoRPearl : RORBoss
    {
        public override string Texture => base.Texture + "Pearl";
        public readonly float PearlHealth = 0.1f;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(PearlHealth * 100));

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += (int)(player.statLifeMax * (1f + PearlHealth));
        }
    }

    public class IrradiantPearl : RORBoss
    {
        public override string Texture => base.Texture + "IrradiantPearl";
        public readonly float PearlHealth = 0.1f;
        public readonly float PearlMana = 0.1f;
        public readonly float PearlDmg = 0.1f;
        public readonly float PearlDefence = 0.1f;
        public readonly float PearlRunSpeed = 0.1f;
        public readonly float PearlAtkSpeed = 0.1f;
        public readonly float crit = 10f;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(PearlHealth*100), (int)(PearlMana*100),(int)(PearlDmg * 100), (int)(PearlAtkSpeed * 100),crit,(int)(PearlDefence * 100), (int)(PearlRunSpeed * 100));

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += (int)(player.statLifeMax * (1f + PearlHealth));
            player.statManaMax2 += (int)(player.statManaMax * (1f + PearlMana));
            player.statDefense *= 1f + PearlDefence;
            player.moveSpeed *= 1f + PearlRunSpeed;
            player.GetDamage(DamageClass.Generic) *= 1f + PearlDmg;
            player.GetAttackSpeed(DamageClass.Generic) *= 1f + PearlAtkSpeed;
            player.GetCritChance(DamageClass.Generic) += crit;
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

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<TougherTimes>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!player.buffType.Contains(ModContent.BuffType<SaferSpacesBuff>()) && !player.buffType.Contains(ModContent.BuffType<SaferSpacesDebuff>()))
            {
                player.AddBuff(ModContent.BuffType<SaferSpacesBuff>(), 10);
            }
        }
    }
    public class LostSeers : RORVoid
    {
        public override string Texture => base.Texture + "LostSeers";
        public readonly float amount = 10f;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)amount);

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<LensMakers>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Generic) += amount;
            player.GetModPlayer<LensPlayer>().LostSeersOn = true;
        }
    }
    public class LysateCell : RORVoid
    {
        public override string Texture => base.Texture + "LysateCell";
        public readonly int amount = 2;
        public readonly float manaregenpen = 0.5f;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(amount,(int)(manaregenpen * 100));

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<FuelCell>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += (player.statManaMax * amount) - player.statManaMax;
            player.manaRegen = (int)(player.manaRegen * (1f - manaregenpen));
        }
    }
}
