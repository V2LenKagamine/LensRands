using LensRands.Systems;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.Linq;

namespace LensRands.Content.Buffs
{
    public class SpinelBuff : ModBuff
    {
        public override string Texture => LensRands.AssetsPath + "Buffs/SpinelBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetCritChance(DamageClass.Generic) += 20;
            player.statDefense *= 1.2f;
            player.GetDamage(DamageClass.Generic) *= 1.2f;
            player.GetAttackSpeed(DamageClass.Generic) *= 1.2f;
            player.statLifeMax2 = (int)(player.statLifeMax2 * 1.2f);
            player.statManaMax2 = (int)(player.statManaMax2 * 1.2f);
            player.lifeRegen *= 3;
            player.moveSpeed *= 1.3f;
            if (player.buffTime[buffIndex] == 1 && player.GetModPlayer<LensPlayer>().SpinelDebuffsTotal < 19)
            {
                player.GetModPlayer<LensPlayer>().SpinelDebuffsTotal += 1;
                player.AddBuff(ModContent.BuffType<SpinelDebuff>(),10);
            }
        }
        public override bool RightClick(int buffIndex)
        {
            return false;
        }
    }
    public class SpinelDebuff : ModBuff
    {

        public override string Texture => LensRands.AssetsPath + "Buffs/SpinelDebuff";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            int totaldebuffs = player.GetModPlayer<LensPlayer>().SpinelDebuffsTotal;
            player.buffTime[buffIndex] = (60 * totaldebuffs) + 10;
            if(player.buffType.Contains(ModContent.BuffType<SpinelBuff>())) { return; }
            player.GetCritChance(DamageClass.Generic) *= 1f - (0.05f * totaldebuffs);
            player.statDefense *= 1f - (0.05f * totaldebuffs);
            player.statManaMax2 = (int)(player.statManaMax2 * (1f - (0.05f * totaldebuffs)));
            player.statLifeMax2 = (int)(player.statLifeMax2 * (1f - (0.05f * totaldebuffs)));
            player.moveSpeed *= 1f - (0.05f * totaldebuffs);
            player.GetAttackSpeed(DamageClass.Generic) *= 1f - (0.05f * totaldebuffs);
            player.GetDamage(DamageClass.Generic) *= 1f - (0.05f * totaldebuffs);
        }
    }
}
