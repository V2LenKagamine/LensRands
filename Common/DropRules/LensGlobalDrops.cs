using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using LensRands.Content.Items.Accessories;
using LensRands.Content.Items.Consumable;
using LensRands.Content.Items.Placeable;
using LensRands.Content.Items.Weapons;
using LensRands.Systems;
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

            globalLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(),ModContent.ItemType<RoRCrateWhite>(), 500));
            globalLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(),ModContent.ItemType<RoRCrateGreen>(), 1000));
            globalLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(),ModContent.ItemType<RoRCrateRed>(), 1500));
            globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<LunarCoin>(), 500));
            globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<LunarPod>(), 1000));
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.Golem)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HalcyonSeed>(), 10));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TitanicKnurl>(), 10));
            }
            if (npc.type == NPCID.SkeletronPrime) { npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PAIStaff>(), 3)); }
            if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
            {
                LeadingConditionRule leadingConditionRule = new LeadingConditionRule(new Conditions.MissingTwin());
                leadingConditionRule.OnSuccess(npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmpathyCores>(), 15)));
                leadingConditionRule.OnSuccess(npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PAIStaff>(), 3)));
                npcLoot.Add(leadingConditionRule);
            }
            if (npc.type == NPCID.TheDestroyer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChargedPerforator>(), 10));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PAIStaff>(), 3));
            }
            if (npc.boss)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(),ModContent.ItemType<RoRCrateWhite>(), 2));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<RoRCrateGreen>(), 4));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<RoRCrateRed>(), 16));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<RoRPearl>(), 100));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LunarPod>(), 10));
            }
            if (npc.type == NPCID.VoodooDemon) { npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<RpgLauncher>(), 100)); }

            if (npc.type == NPCID.QueenBee) { npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<QueensGland>(), 10)); }
            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ThrowingKnife>(), 5));
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MutationStation>(), 2));
            }
        }

        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            Player player = Main.player[Main.myPlayer];
            if (player.GetModPlayer<LensPlayer>().Minty)
            {
                long buyprice;
                foreach (Item item in items)
                {
                    if (item is not null && !item.IsAir)
                    {
                        player.GetItemExpectedPrice(item, out _, out buyprice);
                        item.shopCustomPrice = (int?)Math.Round(buyprice * 0.95f);
                    }
                }
            }
        }
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.ArmsDealer && shop.Name == "Shop")
            {
                if (!shop.TryGetEntry(ItemID.Gel, out _))
                {
                    shop.InsertAfter(ItemID.MusketBall, ItemID.Gel);
                }
            }
        }
        public override void SetupTravelShop(int[] shop, ref int nextSlot)
        {
            int[] PossibleVendors = Mod.GetContent<Vendor>().Select(x => x.Type).ToArray();
            if (!Main.rand.NextBool(4))
            {
                shop[nextSlot] = PossibleVendors[Main.rand.Next(PossibleVendors.Length)];
                nextSlot++;
            }
        }
    }
}
