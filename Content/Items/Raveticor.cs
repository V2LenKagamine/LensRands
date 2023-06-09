using LensRands.Systems.ModSys;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace LensRands.Content.Items
{
    internal class Raveticor : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Opticor";
        public override void SetStaticDefaults()
        {
        }

        private int minanglex = -160;
        private int maxanglex = 161;
        private int minangley = -360;
        private int maxangley = 1;

        public override void SetDefaults()
        {
            Item.height = 16;
            Item.width = 32;
            Item.mana = 5;
            Item.autoReuse = true;
            Item.damage = 2250;
            Item.rare = ItemRarityID.Blue;
            Item.crit = 16;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.knockBack = 3;
            Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<RaveticorBeam>();
            Item.UseSound = AudioSys.Opticor;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
        }

        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) && calamity.TryFind<ModItem>("DarksunFragment", out ModItem moditem))
            {
                Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.LastPrism)
                .AddIngredient(moditem.Type, 30)
                .AddIngredient(ItemID.FragmentNebula, 20)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
            }
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            float speedX = Main.rand.Next(minanglex, maxanglex);
            float speedY = Main.rand.Next(minangley, maxangley);
            Vector2 dir = new Vector2(speedX, speedY);
            dir.Normalize();
            velocity = dir;
            speedX = Main.rand.Next(minanglex, maxanglex);
            speedY = Main.rand.Next(minangley, maxangley);
            Vector2 vel2 = new Vector2(speedX, speedY);
            vel2.Normalize();
            Projectile.NewProjectile(player.GetSource_FromThis(), position, vel2, type, damage, knockback);
            speedX = Main.rand.Next(minanglex, maxanglex);
            speedY = Main.rand.Next(minangley, maxangley);
            Vector2 vel3 = new Vector2(speedX, speedY);
            vel3.Normalize();
            Projectile.NewProjectile(player.GetSource_FromThis(), position, vel3, type, damage, knockback);
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation -= new Vector2(26 * player.direction, -12);
        }
    }
    public class RaveticorBeam : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/Opticor";
        private const float MOVE_DISTANCE = 20f;

        public float Distance
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 unit = Projectile.velocity;
            float point = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center,
                Projectile.Center + unit * Distance, 22, ref point);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.Opacity = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
            Projectile.timeLeft = 45;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            DrawLaser(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center, Projectile.velocity, 26, -1.57f, 1f, (int)MOVE_DISTANCE);
            return false;
        }

        public void DrawLaser(Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation = 0f, float scale = 1f, int transDist = 50)
        {
            float r = unit.ToRotation() + rotation;
            Color touse = Main.hslToRgb(new Vector3(Main.rand.NextFloat(0f,1f),0.75f,.53f)) * Projectile.Opacity;
            // Draws the laser 'body'
            for (float i = transDist; i <= Distance; i += step)
            {
                var origin = start + (i - step) * unit;
                Main.EntitySpriteDraw(texture, origin - Main.screenPosition,
                    new Rectangle(0, 26, 28, 26), i < transDist ? Color.Transparent : touse, r,
                    new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
            }

            // Draws the laser 'tail'
            Main.EntitySpriteDraw(texture, start + unit - Main.screenPosition,
                new Rectangle(0, 0, 28, 26), touse, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);

            // Draws the laser 'head'
            Main.EntitySpriteDraw(texture, start + Distance * unit - Main.screenPosition,
                new Rectangle(0, 52, 28, 26), touse, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Opacity -= 0.03f;
            SetLaserPosition(player);
            SpawnDusts(player);
            CastLights();
        }
        private void SpawnDusts(Player player)
        {
            Vector2 unit = Projectile.velocity * -1;
            Vector2 dustPos = Projectile.position + Projectile.velocity * Distance;

            for (int i = 0; i < 2; ++i)
            {
                float num1 = Projectile.velocity.ToRotation() + (Main.rand.NextBool(2) ? -1.0f : 1.0f) * 1.57f;
                float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
                Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
                Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, DustID.Electric, dustVel.X, dustVel.Y)];
                dust.noGravity = true;
                dust.scale = 1.2f;
                dust = Dust.NewDustDirect(Main.player[Projectile.owner].Center, 0, 0, DustID.Smoke,
                    -unit.X * Distance, -unit.Y * Distance);
                dust.fadeIn = 0f;
                dust.noGravity = true;
                dust.scale = 0.88f;
                dust.color = Main.hslToRgb(new Vector3(Main.rand.NextFloat(0f, 1f), 0.75f, .53f)) * Projectile.Opacity;
            }
        }

        /*
		* Sets the end of the laser position based on where it collides with something
		*/
        private void SetLaserPosition(Player player)
        {
            for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f)
            {
                var start = Projectile.position + Projectile.velocity * Distance;
                if (!Collision.CanHit(Projectile.position, 1, 1, start, 1, 1))
                {
                    Distance -= 5f;
                    break;
                }
            }
        }

        private void CastLights()
        {
            // Cast a light along the line of the laser
            DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (Distance - MOVE_DISTANCE), 50, DelegateMethods.CastLight);
        }
        public override bool ShouldUpdatePosition() => false;

        /*
		* Update CutTiles so the laser will cut tiles (like grass)
		*/
        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 unit = Projectile.velocity;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Distance, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
        }
    }
}
