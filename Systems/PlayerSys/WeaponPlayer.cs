using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ID;

namespace LensRands.Systems.PlayerSys
{
    public class WeaponPlayer : ModPlayer
    {
        public static WeaponPlayer Modplayer(Player player)
        {
            return player.GetModPlayer<WeaponPlayer>();
        }

        //Misc
        public int HighestBossKilled = 0;
        //RealKnife
        public bool KnifeOut = false;
        public float KnifeTimer = 0f;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.active)
            { OnKillEnemy(target); }
        }
        private void OnKillEnemy(NPC target)
        {
            if(target.boss || target.type == NPCID.WallofFlesh)
            {
                for(int i = 0;i < 1;i++) //I hate this but it lets us break out if we find a new highest kill.
                {
                    if (NPC.downedMoonlord && HighestBossKilled <= 18) { HighestBossKilled = 19; break; }
                    if (NPC.downedTowers && HighestBossKilled <= 17) { HighestBossKilled = 18; break; }
                    if (NPC.downedAncientCultist && HighestBossKilled <= 16) { HighestBossKilled = 17; break; }
                    if (NPC.downedEmpressOfLight && HighestBossKilled <= 15) { HighestBossKilled = 16; break; }
                    if (NPC.downedFishron && HighestBossKilled <= 14) { HighestBossKilled = 15; break; }
                    if (NPC.downedGolemBoss && HighestBossKilled <= 13) { HighestBossKilled = 14; break;  }
                    if (NPC.downedPlantBoss && HighestBossKilled <= 12) { HighestBossKilled = 13; break; }
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && HighestBossKilled <= 11) {  HighestBossKilled = 12; break; }
                    if (NPC.downedQueenSlime && HighestBossKilled <= 10) { HighestBossKilled = 11;  break; }
                    if (Main.hardMode == true && HighestBossKilled <= 9) { HighestBossKilled = 10; break; }
                    if (NPC.downedDeerclops && HighestBossKilled <= 8) { HighestBossKilled = 9; break; }
                    if (NPC.downedBoss3 && HighestBossKilled <= 7) { HighestBossKilled = 8; break; }
                    if (NPC.downedQueenBee && HighestBossKilled <= 6) { HighestBossKilled = 7; break; }
                    if (NPC.downedBoss2 && HighestBossKilled <= 5) { HighestBossKilled = 6; break; }
                    if (NPC.downedBoss1 && HighestBossKilled <= 4) { HighestBossKilled = 5; break; }
                    if (NPC.downedSlimeKing && HighestBossKilled <= 3) { HighestBossKilled = 4; break; }
                }
            }

        }


        public override void SaveData(TagCompound tag)
        {
            tag.Add("HighestBossKill", HighestBossKilled);
            base.SaveData(tag);
        }
        public override void LoadData(TagCompound tag)
        {
            if(tag.ContainsKey("HighestBossKill"))
            {
                HighestBossKilled = tag.GetAsInt("HighestBossKill");
                base.LoadData(tag);
            }
            
        }

    }
}
