using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using LensRands.Systems;

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
            Item.value = Item.sellPrice(0, 5, 0, 0);
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
        //public readonly float damagePen = 0.025f;
        public override string Texture => base.Texture + "Soldiers";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(atkspeedinc * 100));
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.Generic) *= 1f + atkspeedinc;
            //player.GetDamage(DamageClass.Generic) *= 1f - damagePen;
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
        public readonly int definc = 20;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(definc);

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<SaferSpaces>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += definc;
        }
    }
    public class ArmorPiercingRounds : RORWhites
    {
        public override string Texture => base.Texture + "APR";
        public readonly int dmginc = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(dmginc);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().APR = true;
        }
    }
    public class TopazBrooch : RORWhites
    {
        public override string Texture => base.Texture + "TopazBrooch";
        public readonly int topaz = 20;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(topaz);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().TopazOn = true;
        }
    }
    public class BackupMag : RORWhites
    {
        public override string Texture => base.Texture + "BackupMag";
        public readonly int amount = 20;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(amount);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().BackupMagOn = true;
        }
    }
    public class LensMakers : RORWhites
    {
        public override string Texture => base.Texture + "LensMakers";
        public readonly float amount = 50f;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)amount);
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<LostSeers>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Generic) += amount;
        }
    }
}
