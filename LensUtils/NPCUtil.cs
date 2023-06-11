using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;

namespace LensRands.LensUtils
{
    public class NPCUtil
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
    }
}
