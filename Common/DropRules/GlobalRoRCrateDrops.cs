using LensRands.Content.Items.Consumable;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace LensRands.Common.DropRules
{
    public class GlobalRoRCrateDrops : GlobalNPC
    {
        public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        {
            if (Main.hardMode) {
            globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<RoRCrateWhite>(), 250));
            globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<RoRCrateGreen>(), 500));
            globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<RoRCrateRed>(), 1000));
            }
        }
    }
}
