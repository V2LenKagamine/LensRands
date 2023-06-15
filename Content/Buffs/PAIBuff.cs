using Terraria.ModLoader;
using Terraria;
using LensRands.Content.Items.Weapons;

namespace LensRands.Content.Buffs
{
    public class PAIBuff : ModBuff
    {
        public override string Texture => LensRands.AssetsPath + "Buffs/ROGUS";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<ROGUSUnit>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
