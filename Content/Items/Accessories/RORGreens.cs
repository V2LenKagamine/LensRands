using LensRands.Systems;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Accessories
{
    public abstract class RORGreens : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/";
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }
    }
    public class Ukelele : RORGreens
    {

        public readonly float UkeleleChance = 25f;
        public readonly int UkeleleHits = 3;
        public readonly float UkeDamage = 12.5f;
        public override string Texture => base.Texture + "Ukelele";

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(UkeleleChance, UkeleleHits, UkeDamage);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().UkeleleOn = true;
        }
    }
    public class LeechSeed : RORGreens
    {
        public readonly int leechpercent = 25;
        public override string Texture => base.Texture + "LeechSeed";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(leechpercent);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().LeechSeedOn = true;
        }
    }
    public class WillOWisp : RORGreens
    {
        public readonly int WillRange = 35;
        public readonly int WillDamage = 35;
        public override string Texture => base.Texture + "WillOWisp";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(WillDamage);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().WillOn = true;
        }
    }
    public class FuelCell : RORGreens 
    {
        public readonly int FuelMana = 100;
        public readonly float FuelRegen = 0.15f;

        public override string Texture => base.Texture + "FuelCell";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(FuelMana,(int)(FuelRegen*100));

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += FuelMana;
            player.manaRegen *= (int)(1f + FuelRegen);
        }

    }

}
