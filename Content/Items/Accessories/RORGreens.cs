using LensRands.Systems.PlayerSys;
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
}
