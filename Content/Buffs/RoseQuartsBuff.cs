using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using LensRands.Systems;

namespace LensRands.Content.Buffs
{
    public class RoseQuartsBuff : ModBuff
    {
        public override string Texture => LensRands.AssetsPath + "Buffs/RoseBuff";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<LensPlayer>().RoseDefended = true;
        }
    }
}
