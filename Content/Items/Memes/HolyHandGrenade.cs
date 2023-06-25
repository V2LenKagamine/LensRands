using System;
using LensRands.LensUtils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Memes
{
    public class HolyHandGrenade : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Memes/HolyHandGrenade";
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Grenade);
            Item.damage = 500;
            Item.shoot = ModContent.ProjectileType<HolyHandGrenadeProj>();
            Item.useTime = Item.useAnimation = 300;
        }
    }
    public class HolyHandGrenadeProj : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Items/Memes/HolyHandGrenade";
        public override void SetDefaults()
        {
            Projectile.width = 12;               //The width of projectile hitbox
            Projectile.height = 12;              //The height of projectile hitbox
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate.
            Projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 300;
            DrawOffsetX = -10;
            DrawOriginOffsetY = -18;
        }
        public override void AI()
        {
            if (Projectile.timeLeft < 2)
            {
                if (Projectile.timeLeft == 1)
                { LensVisualUtils.BombVisuals(Projectile.Center, 500, 500); }
                Projectile.alpha = 255;
                Projectile.velocity = Vector2.Zero;
                Projectile.friendly = true;
                Projectile.tileCollide = false;
                Projectile.knockBack = 8f;
                Projectile.Resize(500, 500);
            }
            else 
            {
                DustyBoi();
                Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
                LensUtil.ProjectileROTATE(Projectile, Projectile.velocity.X > 0 ? 1.5f * Projectile.velocity.X : 1.5f * -Projectile.velocity.X);
            }
            
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 0.05f)
            {
                Projectile.velocity.X = oldVelocity.X * -0.60f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0.05f)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.33f;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.timeLeft = 3;
            base.OnHitNPC(target, hit, damageDone);
        }
        private void DustyBoi()
        {
            if (Main.rand.NextBool())
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1f);
                dust.scale = 0.1f + Main.rand.Next(5) * 0.1f;
                dust.fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
                dust.noGravity = true;
                dust.position = Projectile.Center + new Vector2(1, 0).RotatedBy(Projectile.rotation - MathHelper.ToRadians(90), default) * 10f;

                dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1f);
                dust.scale = 1f + Main.rand.Next(5) * 0.1f;
                dust.noGravity = true;
                dust.position = Projectile.Center + new Vector2(1, 0).RotatedBy(Projectile.rotation - MathHelper.ToRadians(90), default) * 10f;
            }
        }
    }
}
