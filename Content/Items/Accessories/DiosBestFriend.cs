
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using LensRands.Systems.PlayerSys;
using LensRands.Content.Buffs;
using System.Linq;

namespace LensRands.Content.Items.Accessories
{
    public class DiosBestFriend : ModItem
    {


        private readonly int diotimer = 10; //10 Minutes
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/Dios";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(diotimer);
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!player.buffType.Contains(ModContent.BuffType<DiosBestFriendBuff>()) && !player.buffType.Contains(ModContent.BuffType<DiosBestFriendDebuff>()))
            {
                player.AddBuff(ModContent.BuffType<DiosBestFriendBuff>(), 5);
            }
            else if (player.buffType.Contains(ModContent.BuffType<DiosBestFriendBuff>()) && !player.buffType.Contains(ModContent.BuffType<DiosBestFriendDebuff>()))
            {
                player.buffTime[player.buffType[ModContent.BuffType<DiosBestFriendBuff>()]] = 5;
            }
        }
    }
}
