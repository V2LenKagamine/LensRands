using System;
using Terraria;
using Terraria.ModLoader;

namespace LensRands.Content.Buffs
{
    public class Nerfed : ModBuff
    {
        public override string Texture => LensRands.AssetsPath + "Buffs/Nerfed";

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.buffTime[buffIndex] == 9)
            {
                if (npc.defense > 0)
                {
                    npc.defense = Math.Clamp((int)(npc.defense * 0.75f), 0, npc.defense);
                }
                npc.life = (int)(npc.life * 0.75f);
                npc.velocity *= 0.75f;
                npc.damage = (int)(npc.damage * 0.75f);
            }
        }
    }
}
