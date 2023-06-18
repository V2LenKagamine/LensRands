using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LensRands.Content.Buffs
{
    public class StunProbed : ModBuff
    {
        public override string Texture => LensRands.AssetsPath + "Buffs/StunProbed";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.velocity = Vector2.Zero;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.velocity = Vector2.Zero;
        }
    }
}
