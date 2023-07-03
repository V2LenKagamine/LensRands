using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Consumable
{
    public class BossfightSpecial : ModItem
    {
        private readonly int[] BuffsList = 
            {
                BuffID.Endurance,
                BuffID.Heartreach,
                BuffID.Ironskin,
                BuffID.Lifeforce,
                BuffID.Rage,
                BuffID.Swiftness,
                BuffID.Thorns,
                BuffID.Titan,
                BuffID.Wrath,
                BuffID.Warmth,
                BuffID.WellFed3
            };
        public override string Texture => LensRands.AssetsPath + "Items/Consumable/BossfightSpecial";
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.autoReuse = false;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.consumable = true;
            Item.buffType = BuffID.Regeneration;
            Item.buffTime = 12 * 60 * 60; //12 minutes
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.buyPrice(0, 6, 75, 0);
        }
        public override bool? UseItem(Player player)
        {
            for (int i = 0;i < BuffsList.Length;i++)
            {
                player.AddBuff(BuffsList[i], 12 * 60 * 60);
            }
            return null;
        }
    }
}
