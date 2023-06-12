using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Accessories
{
    public class Soldiers : ModItem
    {
        public readonly float atkspeedinc = 0.15f;
        public readonly float damagePen = 0.025f;
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/Soldiers";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(15,damagePen * 100);//Make sure attackspeed = arg 1, because float inprecision stupid.
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.Generic) *= 1f+atkspeedinc;
            player.GetDamage(DamageClass.Generic) *= 1f-damagePen;
        }
    }
}
