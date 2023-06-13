using System;
using System.Linq;
using LensRands.Content.Buffs;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using LensRands.Systems.PlayerSys;

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
}
