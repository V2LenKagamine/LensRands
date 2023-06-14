using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace LensRands.Content.Buffs
{
    public class LuckySevenCooldown : ModBuff
    {
        public override string Texture => LensRands.AssetsPath + "Buffs/Lucky7";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
    }
    public class FinalLuckySevenCooldown : ModBuff
    {
        public override string Texture => LensRands.AssetsPath + "Buffs/FinalLucky7";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
    }
}
