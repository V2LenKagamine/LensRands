using LensRands.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Consumable
{
    public class Mimnt : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Consumable/mints";
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 0, 2, 50);
            Item.rare = ItemRarityID.White;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.consumable = true;
            Item.buffType = ModContent.BuffType<MintyBuff>();
            Item.buffTime = 300*60;
        }
        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ItemID.Daybloom)
                .AddIngredient(ItemID.Bottle)
                .AddIngredient(ItemID.Shiverthorn)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
