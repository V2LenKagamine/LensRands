using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LensRands.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Weapons
{
    public class StunProd : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/StunBaton";
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 0.7f;
            Item.rare = ItemRarityID.Master;

            Item.useStyle = ItemUseStyleID.Swing;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 1;
            Item.crit = 100;

            Item.UseSound = SoundID.Item1;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<StunProbed>(), 300);
        }
    }
}
