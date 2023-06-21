using System;
using Microsoft.Xna.Framework;
using rail;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Memes
{
    public class BouncyBalls : ModItem
    {

        public override string Texture => LensRands.AssetsPath + "Items/Toys/BouncyBalls";
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Pink;
            Item.height = 16;
            Item.width = 16;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.noUseGraphic = true;
            Item.damage = 0;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ModContent.ProjectileType<BouncyBallP>();
            Item.shootSpeed = 3f;
            Item.value = Item.buyPrice(0, 2, 0, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PinkGel,50)
                .Register();
        }
    }
    public class BouncyBallP : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/BouncyBall";
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 900;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            if (Projectile.timeLeft < 840)
            {
                BootlegOnHit();
            }
            Projectile.velocity.Y += 0.1f;
            float rotate = Projectile.velocity.X > 0 ? 12.25f : -12.25f;
            Projectile.rotation += MathHelper.ToRadians(rotate);
        }
        private void BootlegOnHit()
        {
            foreach (Player player in Main.player)
            {
                if (Projectile.Hitbox.Intersects(player.Hitbox))
                {
                    Boing2(Projectile.velocity);
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Boing(oldVelocity);
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Boing2(Projectile.velocity);
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return true;
        }

        private void Boing(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 0.1f)
            {
                Projectile.velocity.X = oldVelocity.X * -0.99f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0.1f)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.99f;
            }
        }
        private void Boing2(Vector2 oldVelocity)
        {

            Projectile.velocity.X = oldVelocity.X * -0.99f;

            if (Projectile.velocity.Y > 0 ? -0.1f != oldVelocity.Y : 0.1f != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0.1f)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.99f;
            }
        }

    }
}
