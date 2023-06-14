using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using LensRands.Systems;

namespace LensRands.Content.Buffs
{
    public class DiosBestFriendBuff : ModBuff
    {
        public override string Texture => LensRands.AssetsPath + "Buffs/DioBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<LensPlayer>().DiosBFOn = true;
        }

    }
    public class DiosBestFriendDebuff : ModBuff
    {

        public override string Texture => LensRands.AssetsPath + "Buffs/DioDebuff";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<LensPlayer>().DiosBFOn = false;
        }
    }
}
