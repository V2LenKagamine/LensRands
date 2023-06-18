using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ID;
using System.Collections.Generic;
using LensRands.LensUtils;
using Terraria.DataStructures;
using LensRands.Content.Buffs;
using System;
using Microsoft.Xna.Framework;
using LensRands.Content.Items.Accessories;

namespace LensRands.Systems
{
    public class LensPlayer : ModPlayer
    {

        public static LensPlayer Modplayer(Player player)
        {
            return player.GetModPlayer<LensPlayer>();
        }

        //Misc
        public int HighestBossKilled = 0;
        public bool MonikasListening;

        //RealKnife
        public bool KnifeOut = false;
        public float KnifeTimer = 0f;
        //SU References
        public bool RoseQuarts;
        public bool RoseDefended;
        public readonly float RoseDamageReduc = 0.05f;
        //Carrier & Carrier prime
        public bool CarrierOn;
        public readonly float CarrierChance = 0.1f;
        public bool CarrierPrimeOn;
        public readonly float CarrierPChance = 0.25f;

        //Overheal
        public float Overheal = 0;
        public float OverhealDecay = 0.05f;
        public float OverhealMax;
        public int LifeLastFrame; //I hate this.
        public int DamagedTimer;
        public int DamagedTimerMax = 240;//Make sure to yadda yadda ^^^ 
        public bool OverhealWentUp;

        

        //RoR stuff
        public bool UkeleleOn;
        public readonly float UkeleleChance = 0.25f;
        public readonly int UkeleleHits = 3;
        public readonly float UkeDamage = 0.125f;

        public bool BungusActive;
        public bool NearBungus;
        public readonly int BungusHeal = 4;
        public readonly int BungusRange = 150;

        public bool DiosBFOn;
        public readonly int DioMax = 10 * 60 * 60;

        public bool Pennies;
        public readonly int PenniesAmount = 3; //In copper coins.

        public bool SaferOn;
        public readonly int SaferMax = 45 * 60;

        public bool AegisOn;
        public readonly float AegisHealPercent = 0.5f;

        public bool APR;
        public readonly float APRDmg = 0.1f;

        public bool LeechSeedOn;
        public readonly float LeechSeedChance = 0.25f;

        public bool SymScorpOn;
        public readonly float SymScorpChance = 0.25f;
        public readonly int SymScorpReduc = 2;

        public bool ChargedOn;
        public readonly float ChargedChance = 0.05f;
        public readonly int ChargedDamage = 5;

        public bool LightFluxOn;
        public readonly float LightFluxDamageUp = 0.55f;
        public readonly float LightFluxSpeedDown = 0.5f;

        public bool MercRachOn;
        public readonly float MercRachDmg = 0.5f;

        public bool BeadsOn;

        public bool WillOn;
        public readonly int WillRange = 300;
        public readonly float WillDamagePercent = 0.35f;

        public bool BrilliantOn;
        public readonly int BrilliantRange = 200;
        public readonly float BrilliantDmg = 0.3f;

        public bool ATGOn;
        public readonly float ATGChance = 0.05f;
        public readonly float ATGDmg = 3f;
        public readonly Vector2 ATGDefaultVec = new(0,-10f);

        public bool TopazOn;
        public readonly int TopazOverheal = 10;

        public bool BackupMagOn;
        public readonly float BackupMagChance = 0.2f;

        public bool HarvesterScytheOn;
        public readonly int HarvestScytheHeal = 10;
        public readonly float HarvestScytheChance = 0.5f;

        public bool BrainstalksOn;
        public readonly int BrainstalkRestore = 40;

        public bool GestureOverloaded;
        public readonly float GestureDamageInc = 0.25f;

        public bool LostSeersOn;

        public int SpinelDebuffsTotal = 0;



        //Overrides
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (Pennies)
            {
                SpawnMonet(0, 0, 0, PenniesAmount);
            }
            if (RoseQuarts && Main.netMode == NetmodeID.SinglePlayer)
            {
                AddOverheal(hurtInfo.Damage * 0.4f);
            }
            else if (RoseQuarts && !RoseDefended)
            {
                foreach (Player player in LensUtil.FindNearbyPlayers(800, Player.position, Player, true))
                {
                    player.GetModPlayer<LensPlayer>().AddOverheal(hurtInfo.Damage * 0.6f);
                }
            }
        }
        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            if (Pennies)
            {
                SpawnMonet(0, 0, 0, PenniesAmount);
            }
            if (RoseQuarts && Main.netMode == NetmodeID.SinglePlayer)
            {
                AddOverheal(hurtInfo.Damage * 0.4f);
            }
            else if (RoseQuarts && !RoseDefended)
            {
                foreach (Player player in LensUtil.FindNearbyPlayers(800, Player.position, Player, true))
                {
                    player.GetModPlayer<LensPlayer>().AddOverheal(hurtInfo.Damage * 0.6f);
                }
            }
        }
        public override bool ConsumableDodge(Player.HurtInfo info)
        {
            if (Overheal > 0)
            {
                if (Overheal > info.Damage)
                {
                    Overheal -= info.Damage;
                    Player.SetImmuneTimeForAllTypes(Player.longInvince ? 120 : 80);
                    return true;
                }
                else
                {
                    int overdamage = info.Damage - (int)Overheal;
                    info.Damage -= overdamage;

                    Overheal = 0;
                }
            }
            if (SaferOn)
            {
                Player.SetImmuneTimeForAllTypes(Player.longInvince ? 120 : 80);
                Player.ClearBuff(ModContent.BuffType<SaferSpacesBuff>());
                Player.AddBuff(ModContent.BuffType<SaferSpacesDebuff>(), SaferMax);
                return true;
            }
            DamagedTimer = DamagedTimerMax;
            return base.ConsumableDodge(info);
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (DiosBFOn)
            {
                Player.Heal((Player.statLifeMax2 - Player.statLife) / 2);
                Player.ClearBuff(ModContent.BuffType<DiosBestFriendBuff>());
                Player.AddBuff(ModContent.BuffType<DiosBestFriendDebuff>(), DioMax);
                Main.NewText(string.Format("{0}'s Best Friend takes the blow,reviving {0} with half health!", Player.name), Color.Red);
                return false;
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.active)
            { OnKillEnemy(target,hit,damageDone); }

            if (LostSeersOn && Player.GetTotalCritChance(DamageClass.Generic) > 1f && hit.Crit)
            {
                if(Main.rand.NextFloat(1f) < Player.GetTotalCritChance(DamageClass.Generic) - 1f)
                {
                    hit.Damage *= 5;
                }
            } 
            if (UkeleleOn && Main.rand.NextFloat(1f) < UkeleleChance)
            {
                List<int> exclude = new List<int>() { target.whoAmI };
                IEnumerable<NPC> NearNPCs = LensUtil.FindNearbyNPCs(750, target.Center, exclude);
                int totalhit = 0;
                foreach (NPC tohit in NearNPCs)
                {
                    tohit.SimpleStrikeNPC((int)(hit.Damage * UkeDamage), hit.HitDirection, hit.Crit, hit.Knockback, hit.DamageType);
                    totalhit++;
                    if (totalhit >= UkeleleHits) { break; }
                }
            }
            if (!target.active && WillOn)
            {
                target.GetLifeStats(out int currentlife, out int Maxlife);
                List<int> exclude = new List<int>() { target.whoAmI };
                IEnumerable<NPC> NearNPCs = LensUtil.FindNearbyNPCs(WillRange, target.Center, exclude);
                foreach (NPC tohit in NearNPCs)
                {
                    tohit.SimpleStrikeNPC((int)(Maxlife * WillDamagePercent), hit.HitDirection, hit.Crit, hit.Knockback, hit.DamageType);
                }
                LensVisualUtils.BombVisuals(target.Center, WillRange, WillRange);
            }
            if (LeechSeedOn && Main.rand.NextFloat(1f) < LeechSeedChance)
            {
                Player.Heal(1);
            }
            if (target.active && SymScorpOn && Main.rand.NextFloat(1f) < SymScorpChance)
            {
                target.defDefense -= SymScorpReduc;
                if (target.defDefense < 0)
                {
                    target.defDefense = 0;
                }
            }
            if (BrilliantOn)
            {
                List<int> exclude = new List<int>() { target.whoAmI };
                IEnumerable<NPC> NearNPCs = LensUtil.FindNearbyNPCs(BrilliantRange, target.Center, exclude);
                foreach (NPC tohit in NearNPCs)
                {
                    tohit.SimpleStrikeNPC((int)(hit.Damage * BrilliantDmg), hit.HitDirection, hit.Crit, hit.Knockback, hit.DamageType);
                }
                LensVisualUtils.BombVisuals(target.Center, BrilliantRange, BrilliantRange);
            }
            if (ATGOn && Main.rand.NextFloat(1f) < ATGChance)
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center - new Vector2(0,target.Hitbox.Height),ATGDefaultVec,ModContent.ProjectileType<ATGMissile>(),
                    (int)(hit.Damage * ATGDmg),hit.Knockback);
            }
            if (HarvesterScytheOn && hit.DamageType == DamageClass.Melee && hit.Crit && Main.rand.NextFloat(1f) < HarvestScytheChance)
            {
                Player.Heal(HarvestScytheHeal);
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (ChargedOn && Main.rand.NextFloat(1f) < ChargedChance)
            {
                modifiers.FinalDamage *= 1 + ChargedDamage;
            }
            if (APR && target.boss)
            {
                modifiers.FinalDamage *= (int)(1f + APRDmg);
            }
        }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (LightFluxOn)
            {
                damage *= 1f + LightFluxDamageUp;
            }
            if (MercRachOn)
            {
                damage *= 1f + MercRachDmg;
            }
        }
        public override float UseSpeedMultiplier(Item item)
        {
            if (LightFluxOn)
            {
                return LightFluxSpeedDown;
            }
            return base.UseSpeedMultiplier(item);
        }
        public override void OnHurt(Player.HurtInfo info)
        {
            if (MercRachOn)
            {
                info.Damage = (int)(info.Damage * (1f + MercRachDmg));
            }
            if (GestureOverloaded) 
            {
                info.Damage = (int)(info.Damage * (1f + GestureDamageInc));
            }
            if (RoseQuarts)
            {
                info.Damage = (int)(info.Damage * (1f - RoseDamageReduc));
            }
        }

        public override void PostUpdate()
        {
            OverhealCalcs();
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
            if(BackupMagOn && Main.rand.NextFloat(1f) < BackupMagChance)
            {
                return false;
            }
            return true;
        }
        public override void UpdateLifeRegen()
        {
            if (BungusActive && Player.active)
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
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            SpinelDebuffsTotal = 0;
        }

        public override void PostNurseHeal(NPC nurse, int health, bool removeDebuffs, int price)
        {
            SpinelDebuffsTotal = 0;
        }

        public override void ResetEffects()
        {
            RoseQuarts = false;
            RoseDefended = false;
            //ROR
            CarrierOn = false;
            CarrierPrimeOn = false;
            UkeleleOn = false;
            BungusActive = false;
            DiosBFOn = false;
            SaferOn = false;
            Pennies = false;
            AegisOn = false;
            OverhealWentUp = false;
            APR = false;
            LeechSeedOn = false;
            SymScorpOn = false;
            ChargedOn = false;
            LightFluxOn = false;
            MercRachOn = false;
            BeadsOn = false;
            WillOn = false;
            BrilliantOn = false;
            ATGOn = false;
            TopazOn = false;
            BackupMagOn = false;
            BrainstalksOn = false;
            GestureOverloaded = false;
            HarvesterScytheOn = false;
            LostSeersOn = false;
            //Keep at bottom.
            OverhealMax = Player.statLifeMax / 2;
            DamagedTimerMax = 240;
        }

        //Util methods
        
        private void OverhealCalcs()
        {
            if (Player.active && AegisOn && LifeLastFrame > 0 && DamagedTimer <= 0)
            {
                if (LifeLastFrame < Player.statLife - Player.lifeRegen)
                {
                    int difference = Player.statLife - LifeLastFrame;
                    AddOverheal(difference * AegisHealPercent);
                }
                else if (Player.statLife >= Player.statLifeMax2)
                {
                    AddOverheal(Player.lifeRegen / 60f * AegisHealPercent);
                }
            }
            else if (DamagedTimer > 0)
            {
                DamagedTimer--;
            }
            if (!OverhealWentUp && Overheal > 0)
            {
                Overheal -= OverhealDecay;
            }
            Overheal = Math.Clamp(Overheal, 0, OverhealMax);
            LifeLastFrame = Player.statLife;
        }

        private void AddOverheal(float amount)
        {
            Overheal += amount;
            Overheal = Math.Clamp(Overheal, 0, OverhealMax);
            OverhealWentUp = true;
        }
        private void SpawnMonet(int plat, int gold, int silver, int copper)
        {
            if (plat > 0) { Player.QuickSpawnItem(Player.GetSource_FromThis(), ItemID.PlatinumCoin, plat); }
            if (gold > 0) { Player.QuickSpawnItem(Player.GetSource_FromThis(), ItemID.GoldCoin, gold); }
            if (silver > 0) { Player.QuickSpawnItem(Player.GetSource_FromThis(), ItemID.SilverCoin, silver); }
            if (copper > 0) { Player.QuickSpawnItem(Player.GetSource_FromThis(), ItemID.CopperCoin, copper); }
        }
        private void OnKillEnemy(NPC target,NPC.HitInfo hit,int damagedone)
        {
            if (target.boss || target.type == NPCID.WallofFlesh)
            {
                for (int i = 0; i < 1; i++) //I hate this but it lets us break out if we find a new highest kill.
                {
                    if (NPC.downedMoonlord && HighestBossKilled <= 18) { HighestBossKilled = 19; break; }
                    if (NPC.downedTowers && HighestBossKilled <= 17) { HighestBossKilled = 18; break; }
                    if (NPC.downedAncientCultist && HighestBossKilled <= 16) { HighestBossKilled = 17; break; }
                    if (NPC.downedEmpressOfLight && HighestBossKilled <= 15) { HighestBossKilled = 16; break; }
                    if (NPC.downedFishron && HighestBossKilled <= 14) { HighestBossKilled = 15; break; }
                    if (NPC.downedGolemBoss && HighestBossKilled <= 13) { HighestBossKilled = 14; break; }
                    if (NPC.downedPlantBoss && HighestBossKilled <= 12) { HighestBossKilled = 13; break; }
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && HighestBossKilled <= 11) { HighestBossKilled = 12; break; }
                    if (NPC.downedQueenSlime && HighestBossKilled <= 10) { HighestBossKilled = 11; break; }
                    if (Main.hardMode == true && HighestBossKilled <= 9) { HighestBossKilled = 10; break; }
                    if (NPC.downedDeerclops && HighestBossKilled <= 8) { HighestBossKilled = 9; break; }
                    if (NPC.downedBoss3 && HighestBossKilled <= 7) { HighestBossKilled = 8; break; }
                    if (NPC.downedQueenBee && HighestBossKilled <= 6) { HighestBossKilled = 7; break; }
                    if (NPC.downedBoss2 && HighestBossKilled <= 5) { HighestBossKilled = 6; break; }
                    if (NPC.downedBoss1 && HighestBossKilled <= 4) { HighestBossKilled = 5; break; }
                    if (NPC.downedSlimeKing && HighestBossKilled <= 3) { HighestBossKilled = 4; break; }
                }
            }
            if (TopazOn && !target.CountsAsACritter)
            {
                AddOverheal(TopazOverheal);
            }
            if (BrainstalksOn)
            {
                Player.statMana = Math.Clamp(Player.statMana + BrainstalkRestore,0,Player.statManaMax2);
            }
        }
        public override void SaveData(TagCompound tag)
        {
            tag.Add("HighestBossKill", HighestBossKilled);
            tag.Add("SpinelDebuffs", SpinelDebuffsTotal);
            base.SaveData(tag);
        }
        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("HighestBossKill")) { HighestBossKilled = tag.GetAsInt("HighestBossKill"); }
            if (tag.ContainsKey("SpinelDebuffs")) { SpinelDebuffsTotal = tag.GetAsInt("SpinelDebuffs"); }

        }

    }
}
