using LensRands.Content.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Systems
{
    public class LensNPCPlayer : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if(npc.type == NPCID.MoonLordCore)
            {
                for(int i = 0; i < Main.player.Length; i++)
                {
                    Player player = Main.player[i];
                    if (player.GetModPlayer<LensPlayer>().BeadsOn)
                    {
                        for (int f = 0; f < player.CountItem(ModContent.ItemType<RoRPearl>()); f++)
                        {
                            player.ConsumeItem(ModContent.ItemType<RoRPearl>());
                            player.QuickSpawnItem(player.GetSource_FromThis(), ModContent.ItemType<IrradiantPearl>());
                        }
                    }
                }
            }
        }
    }
}
