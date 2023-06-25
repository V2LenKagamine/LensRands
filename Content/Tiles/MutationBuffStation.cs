using System.Collections.Generic;
using System.Linq;
using LensRands.Content.Buffs;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace LensRands.Content.Tiles
{
    public class MutationBuffStationTile :ModTile
    {
        public override string Texture => LensRands.AssetsPath + "Tiles/Furniture/MutationStationTile";
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
            TileObjectData.addTile(Type);
        }

        public override bool RightClick(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            IEnumerable<int> selectablebuffs = Mod.GetContent<MutationBuffs>().Select(x => x.Type);
            foreach (int buff in selectablebuffs) 
            {
                if (player.HasBuff(buff))
                {
                    return base.RightClick(i,j);
                }
            }
            player.AddBuff(selectablebuffs.ElementAt(Main.rand.Next(selectablebuffs.Count())), 24 * 60 * 60);
            
            return base.RightClick(i, j);
        }
    }
}
