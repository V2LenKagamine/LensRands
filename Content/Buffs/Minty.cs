using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LensRands.Systems;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace LensRands.Content.Buffs
{
    public class MintyBuff : ModBuff
    {
        public override string Texture => LensRands.AssetsPath + "Buffs/Minty";
        public override void SetStaticDefaults()
        {
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<LensPlayer>().Minty = true;
        }
    }
}
