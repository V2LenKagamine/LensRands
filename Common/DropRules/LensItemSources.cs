using LensRands.Content.Items.Memes;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace LensRands.Common.DropRules
{
    public sealed class WumpaDrop : GlobalItem
    {
        public override void OnSpawn(Item item, IEntitySource source)
        {
            if (source is EntitySource_ShakeTree && Main.rand.NextBool(1000))
            {
                Item.NewItem(source,item.position,ModContent.ItemType<Wumpa>());
            } 
        }
    }
}
