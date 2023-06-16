using LensRands.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Consumable
{
    public class SpinelTonic : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Consumable/SpinelTonic";
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.autoReuse = true;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.maxStack = 1;
        }
        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                player.AddBuff(ModContent.BuffType<SpinelBuff>(), 60 * 20);
            }
            return base.UseItem(player);
        }
    }
}
