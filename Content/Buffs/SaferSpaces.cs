using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using LensRands.Systems;

namespace LensRands.Content.Buffs
{
    public class SaferSpacesBuff : ModBuff
    {
        public override string Texture => LensRands.AssetsPath + "Buffs/SaferBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<LensPlayer>().SaferOn = true;
        }
    }
    public class SaferSpacesDebuff : ModBuff
    {
        public override string Texture => LensRands.AssetsPath + "Buffs/SaferDebuff";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<LensPlayer>().SaferOn = false;
        }
    }
}
