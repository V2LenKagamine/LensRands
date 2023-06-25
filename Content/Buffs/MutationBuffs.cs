using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using LensRands.Systems;

namespace LensRands.Content.Buffs
{
    public abstract class MutationBuffs : ModBuff
    {
        public override string Texture => LensRands.AssetsPath + "Buffs/Mutation";

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
        public override bool RightClick(int buffIndex)
        {
            return false;
        }
    }
    public class DeadInside : MutationBuffs
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 += player.statLifeMax / 2;
            player.lifeRegen -= player.lifeRegen/2;
        }
    }
    public class OpenWounds : MutationBuffs
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<LensPlayer>().OpenWoundsBuff = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.buffTime[buffIndex] % 10 == 0 && !npc.immortal)
            {
                if (npc.life != 1)
                {
                    npc.life -= 1;
                }
                else
                {
                    npc.StrikeInstantKill();
                }
            }
        }
    }
    public class SlowingStrikes : MutationBuffs
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<LensPlayer>().SlowingStrikesBuff = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.velocity *= 0.95f;
        }
    }
    public class AmmoDuplicator : MutationBuffs
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<LensPlayer>().AmmoDuplicatorBuff = true;
        }
    }
}
