using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Memes
{
    public class Wumpa : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Misc/Wumpa";
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(7, 14));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = Item.height = 32;
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 30;
        }
    }
}
