using System;
using LensRands.LensUtils;
using LensRands.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace LensRands.Content.Items.Accessories
{
    public abstract class RORGreens : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/";
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }
    }
    public class Ukelele : RORGreens
    {

        public readonly float UkeleleChance = 25f;
        public readonly int UkeleleHits = 3;
        public readonly float UkeDamage = 12.5f;
        public override string Texture => base.Texture + "Ukelele";

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(UkeleleChance, UkeleleHits, UkeDamage);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().UkeleleOn = true;
        }
    }
    public class LeechSeed : RORGreens
    {
        public readonly int leechpercent = 25;
        public override string Texture => base.Texture + "LeechSeed";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(leechpercent);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().LeechSeedOn = true;
        }
    }
    public class WillOWisp : RORGreens
    {
        public readonly int WillRange = 35;
        public readonly int WillDamage = 35;
        public override string Texture => base.Texture + "WillOWisp";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(WillDamage);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().WillOn = true;
        }
    }
    public class FuelCell : RORGreens 
    {
        public readonly int FuelMana = 50;
        public readonly float FuelRegen = 0.25f;

        public override string Texture => base.Texture + "FuelCell";
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(FuelMana,(int)(FuelRegen*100));

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += FuelMana;
            player.manaRegen *= (int)(1f + FuelRegen);
        }

    }
    public class ATGMK1 : RORGreens
    {
        public readonly int ATGChance = 5;
        public readonly int ATGDmg = 300;

        public override string Texture => base.Texture + "ATGMK1";

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ATGChance,ATGDmg);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().ATGOn = true;
        }
    }
    public class ATGMissile : ModProjectile //Deal with it going in ror greens.
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/ATG";

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 15 * 60;
            Projectile.light = 0.25f;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override void AI()
        {
            if (Projectile.timeLeft <= 3)
            {
                Projectile.tileCollide = false;
                Projectile.alpha = 255; 
                Projectile.Resize(128, 128);
                Projectile.knockBack = 8f;
                LensVisualUtils.BombVisuals(Projectile.Center, 128, 128);
            }
            else
            {
                DoDusts();
            }
            if (Projectile.velocity != Vector2.Zero)
            {
                LensUtil.HomeOnEnemy(Projectile, 1200, 10f, false, 0.67f);
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
                
            }
            
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.timeLeft > 3)
            {
                Projectile.timeLeft = 3;
            }

            Projectile.direction = target.position.X + (target.width / 2) < Projectile.position.X + (Projectile.width / 2) ?  -1 : 1;

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity *= 0f; 
            Projectile.timeLeft = 3; 
            return false; 
        }
        private void DoDusts()
        {
            for (int i = 0; i < 2; i++)
            {
                float posOffsetX = 0f;
                float posOffsetY = 0f;
                if (i == 1)
                {
                    posOffsetX = Projectile.velocity.X * 0.5f;
                    posOffsetY = Projectile.velocity.Y * 0.5f;
                }
                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 3f + posOffsetX, Projectile.position.Y + 3f + posOffsetY) - Projectile.velocity * 0.5f,
                    Projectile.width - 8, Projectile.height - 8, DustID.Torch, 0f, 0f, 100);
                fireDust.scale *= 2f + Main.rand.Next(10) * 0.1f;
                fireDust.velocity *= 0.2f;
                fireDust.noGravity = true;
                Dust smokeDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 3f + posOffsetX, Projectile.position.Y + 3f + posOffsetY) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, DustID.Smoke, 0f, 0f, 100, default, 0.5f);
                smokeDust.fadeIn = 1f + Main.rand.Next(5) * 0.1f;
                smokeDust.velocity *= 0.05f;
            }
            
        }
    }
    public class HarvestScythe : RORGreens
    {
        public readonly float ScytheChance = 5f;
        public readonly int ScytheHeal = 10;
        public readonly int ScytheProc = 50;

        public override string Texture => base.Texture + "HarvesterScythe";

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)ScytheChance,ScytheProc,ScytheHeal);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().HarvesterScytheOn = true;
            player.GetCritChance(DamageClass.Melee) += ScytheChance;
        }
    }
}
