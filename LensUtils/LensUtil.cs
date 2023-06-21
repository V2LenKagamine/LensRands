using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;

namespace LensRands.LensUtils
{
    public class LensUtil
    {
        public static IEnumerable<NPC> FindNearbyNPCs(float range, Vector2 worldPos, List<int> ignoredNPCs = null,bool lookforfriendly = false)
        {
            ignoredNPCs ??= new List<int>();
            if (lookforfriendly)
            {
                return Main.npc.SkipLast(1).Where(npc => npc.DistanceSQ(worldPos) < range * range && npc.active && !npc.CountsAsACritter && npc.friendly && !npc.dontTakeDamage && !npc.immortal && !ignoredNPCs.Contains(npc.type));
            }
            else
            {
                return Main.npc.SkipLast(1).Where(npc => npc.DistanceSQ(worldPos) < range * range && npc.active && !npc.CountsAsACritter && !npc.friendly && !npc.dontTakeDamage && !npc.immortal && !ignoredNPCs.Contains(npc.type));
            }
        }

        public static IEnumerable<Player> FindNearbyPlayers(float range,Vector2 worldPos, Player sourcePlayer = null,bool CheckTeam = false, List<int> ignorePlayers = null)
        {
            ignorePlayers ??= new List<int>();
            if (CheckTeam)
            {
                return Main.player.Where(player => player.DistanceSQ(worldPos) < range * range && sourcePlayer.team == player.team && !ignorePlayers.Contains(player.whoAmI));
            }
            else
            {
                return Main.player.Where(player => player.DistanceSQ(worldPos) < range * range && !ignorePlayers.Contains(player.whoAmI));
            }
        }

        public static NPC FindClosestNPC(float range, Vector2 worldPos, bool checkCollision = true, List<int> excludedNPCs = null)
        {
            excludedNPCs ??= new List<int>();
            NPC closestNPC = null;
            float closestNPCDistance = float.PositiveInfinity;

            foreach (NPC npc in Main.npc.SkipLast(1))
            {
                float distance = Vector2.Distance(npc.Center, worldPos);
                if (!npc.CanBeChasedBy() || distance > range || distance > closestNPCDistance || excludedNPCs.Contains(npc.whoAmI))
                {
                    continue;
                }

                if (checkCollision && !CollisionHelpers.CanHit(npc, worldPos))
                {
                    continue;
                }

                closestNPC = npc;
                closestNPCDistance = distance;
            }

            return closestNPC;
        }

        public static void ProjectileROTATE(Projectile projectile,float rotationpertick)
        {
            float rotate = projectile.velocity.X > 0 ? rotationpertick : -rotationpertick;
            projectile.spriteDirection = projectile.velocity.X > 0 ? 1 : -1;
            projectile.rotation += MathHelper.ToRadians(rotate);
        }

        //From https://github.com/Zeodexic/tsorcRevamp/blob/main/, modfied.
        ///<summary> 
        ///Call in a projectile's AI to allow the projectile to home on enemies
        ///</summary>         
        ///<param name="projectile">The current projectile</param>
        ///<param name="homingRadius">The homing radius</param>
        ///<param name="topSpeed">The projectile's maximum velocity</param>
        ///<param name="rotateTowards">Should the projectile maintain topSpeed speed and rotate towards targets, instead of standard homing?</param>
        ///<param name="homingStrength">The homing strength coefficient. Unused if rotateTowards.</param>
        ///<param name="needsLineOfSight">Does the projectile need line of sight to home on a target?</param>
        public static void HomeOnEnemy(Projectile projectile, float homingRadius, float topSpeed, out bool foundTarget,bool rotateTowards = false, float homingStrength = 1f, bool needsLineOfSight = false)
        {
            if (!projectile.active || !projectile.friendly) { foundTarget = false; return; };
            const int BASE_STRENGTH = 30;

            Vector2 targetLocation = Vector2.UnitY;
            foundTarget = false;
            float distance = 9999999;

            for (int i = 0; i < 200; i++)
            {
                if (!Main.npc[i].active) continue;
                float toNPCEdge = (Main.npc[i].width / 2) + (Main.npc[i].height / 2); //make homing on larger targets more consistent
                float npcDistance = projectile.Distance(Main.npc[i].Center);
                //WithinRange is just faster Distance (skips sqrt)
                if (Main.npc[i].CanBeChasedBy(projectile) && projectile.WithinRange(Main.npc[i].Center, homingRadius + toNPCEdge) && (!needsLineOfSight || Collision.CanHitLine(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1)) && npcDistance < distance)
                {
                    targetLocation = Main.npc[i].Center;
                    foundTarget = true;
                    distance = npcDistance;
                }
            }

            if (foundTarget)
            {
                Vector2 homingDirection = Vector2.Normalize(targetLocation - projectile.Center);
                projectile.velocity = (projectile.velocity * (BASE_STRENGTH / homingStrength) + homingDirection * topSpeed) / ((BASE_STRENGTH / homingStrength) + 1);
            }
            if (rotateTowards)
            {
                if (projectile.velocity.Length() < topSpeed)
                {
                    projectile.velocity *= topSpeed / projectile.velocity.Length();
                }
            }
            if (projectile.velocity.Length() > topSpeed)
            {
                projectile.velocity *= topSpeed / projectile.velocity.Length();
            }
        }

    }
}
