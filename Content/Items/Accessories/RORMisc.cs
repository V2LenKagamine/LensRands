using System.Linq;
using LensRands.Content.Buffs;
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
