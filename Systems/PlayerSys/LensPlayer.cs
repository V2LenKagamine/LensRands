using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ID;
using System.Collections.Generic;
using LensRands.LensUtils;

namespace LensRands.Systems.PlayerSys
{
    public class LensPlayer : ModPlayer
    {

        public static LensPlayer Modplayer(Player player)
        {
            return player.GetModPlayer<LensPlayer>();
        }

        //Misc
        public int HighestBossKilled = 0;
        //RealKnife
        public bool KnifeOut = false;
        public float KnifeTimer = 0f;

        //Carrier & Carrier prime
        public bool CarrierOn;
        public readonly float CarrierChance = 0.1f;
        public bool CarrierPrimeOn;
        public readonly float CarrierPChance = 0.25f;

        //RoR stuff
        public bool UkeleleOn;
        public readonly float UkeleleChance = 0.25f;
        public readonly int UkeleleHits = 3;
        public readonly float UkeDamage = 0.125f;

        public bool BungusActive;
        public bool NearBungus;
        public readonly int BungusHeal = 4;
        public readonly int BungusRange = 150;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.active)
            { OnKillEnemy(target); }
            if (UkeleleOn && Main.rand.NextFloat(1f) < UkeleleChance)
            {
                List<int> exclude = new List<int>() { target.whoAmI };
                IEnumerable<NPC> NearNPCs = NPCUtil.FindNearbyNPCs(750, target.Center,exclude);
                int totalhit = 0;
                foreach(NPC tohit in NearNPCs)
                {
                    tohit.SimpleStrikeNPC((int)(hit.Damage * UkeDamage),hit.HitDirection,hit.Crit,hit.Knockback,hit.DamageType);
                    totalhit++;
                    if (totalhit >= UkeleleHits) { break; }
                }
            }
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

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (CarrierOn && Main.rand.NextFloat(1f) < CarrierChance)
            {
                return false;
            }
            if (CarrierPrimeOn && Main.rand.NextFloat(1f) < CarrierPChance)
            {
                return false;
            }
            return true;
        }

        public override void UpdateLifeRegen()
        {
            if(BungusActive && Player.active)
            {
                Player.lifeRegen += BungusHeal;
                for (int i = 0; i < Main.player.Length; i++)
                {
                    Player OtherPlayer = Main.player[i];
                    if (OtherPlayer.whoAmI == Player.whoAmI) { continue; }
                    if (OtherPlayer.GetModPlayer<LensPlayer>().NearBungus == true && (!OtherPlayer.active || Player.DistanceSQ(OtherPlayer.Center) > BungusRange * BungusRange))
                    {
                        OtherPlayer.GetModPlayer<LensPlayer>().NearBungus = false;
                    }
                    if (OtherPlayer.active && Player.DistanceSQ(OtherPlayer.Center) < BungusRange * BungusRange && OtherPlayer.team == Player.team && Player.team != 0)
                    {
                        OtherPlayer.GetModPlayer<LensPlayer>().NearBungus = true;
                    }
                }
            }
            if (NearBungus && Player.active)
            {
                Player.lifeRegen += BungusHeal;
            }
        }

        public override void ResetEffects()
        {
            CarrierOn = false;
            CarrierPrimeOn = false;
            UkeleleOn = false;
            BungusActive = false;
            //NearBungus = false;
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
