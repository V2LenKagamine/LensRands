using LensRands.Content.Items.Accessories;
using LensRands.Content.Items.Consumable;
using LensRands.Content.Items.Weapons;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Common.DropRules
{
    public class LensGlobalDrops : GlobalNPC
    {
        public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        {
            if (Main.hardMode) {
                globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<RoRCrateWhite>(), 250));
                globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<RoRCrateGreen>(), 500));
                globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<RoRCrateRed>(), 750));
                globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<LunarCoin>(), 250));
            }
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (Main.hardMode)
            {
                if (npc.type == NPCID.Golem) 
                { 
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HalcyonSeed>(), 10));
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TitanicKnurl>(), 10));
                }
                if(npc.type == NPCID.SkeletronPrime) { npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PAIStaff>(), 3)); }
                if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
                {
                    LeadingConditionRule leadingConditionRule = new LeadingConditionRule(new Conditions.MissingTwin());
                    leadingConditionRule.OnSuccess(npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmpathyCores>(), 15)));
                    leadingConditionRule.OnSuccess(npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PAIStaff>(), 3)));
                    npcLoot.Add(leadingConditionRule);
                }
                if(npc.type == NPCID.TheDestroyer) 
                { 
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChargedPerforator>(), 10));
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PAIStaff>(), 3));
                }
                if (npc.boss)
                {
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RoRCrateWhite>(), 2));
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RoRCrateGreen>(), 4));
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RoRCrateRed>(), 16));
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RoRPearl>(), 100));
                }
            }
            if(npc.type == NPCID.QueenBee) { npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<QueensGland>(), 10)); }
            if(npc.boss)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LunarPod>(), 5));
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ThrowingKnife>(), 5));
            }
        }
    }
}
