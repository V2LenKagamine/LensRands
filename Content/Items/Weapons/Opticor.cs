using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.Audio;
using LensRands.Systems.ModSys;

namespace LensRands.Content.Items.Weapons
{
    public class Opticor : ModItem
    {

        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Opticor";

        public override void SetDefaults()
        {
            Item.height = 16;
            Item.width = 32;
            Item.mana = 8;
            Item.channel = true;
            Item.damage = 2250;
            Item.rare = ItemRarityID.Blue;
            Item.crit = 16;
            Item.DamageType = DamageClass.Magic;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 3;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<OpticorBeam>();
            Item.UseSound = SoundID.Item13;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
        }

        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) && calamity.TryFind("DarksunFragment", out ModItem moditem))
            {
                Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.LastPrism)
                .AddIngredient(moditem.Type, 30)
                .AddIngredient(ItemID.FragmentNebula, 20)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation -= new Vector2(26 * player.direction, -12);
        }
    }

    public class OpticorBeam : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/Opticor";
        private const float MAX_CHARGE = 120f;
        private const float MOVE_DISTANCE = 20f;

        public float Distance
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public float Charge
        {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }

        public bool IsAtMaxCharge => Charge == MAX_CHARGE;
        public bool damageAdjusted = false;

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.ai[1] == 0) return false;

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
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[1] == 1)
            {
                DrawLaser(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center, Projectile.velocity, 26, -1.57f, 1f, (int)MOVE_DISTANCE);
            }
            return false;
        }

        public void DrawLaser(Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation = 0f, float scale = 1f, int transDist = 50)
        {
            float r = unit.ToRotation() + rotation;
            Color touse = Color.White * Projectile.Opacity;
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

            if (Projectile.ai[1] == 0)
            {
                Projectile.timeLeft = 90;
                Projectile.position = player.Center + Projectile.velocity * MOVE_DISTANCE;
            }
            else
            {
                Projectile.Opacity -= 0.015f;
            }

            // By separating large AI into methods it becomes very easy to see the flow of the AI in a broader sense
            // First we update player variables that are needed to channel the laser
            // Then we run our charging laser logic
            // If we are fully charged, we proceed to update the laser's position
            // Finally we spawn some effects like dusts and light

            UpdatePlayer(player);
            ChargeLaser(player);

            if (Projectile.ai[1] == 0) return;
            SetLaserPosition(player);
            SpawnDusts(player);
            CastLights();
            if (Charge >= MAX_CHARGE && !damageAdjusted)
            {
                Projectile.damage = (int)(Projectile.damage * 1.5);
                SoundEngine.PlaySound(AudioSys.Opticor, player.position);
                damageAdjusted = true;
            }
            else if (!damageAdjusted)
            {
                Projectile.damage = (int)(Projectile.damage * (Charge / MAX_CHARGE));
                SoundEngine.PlaySound(AudioSys.Opticor, player.position);
                damageAdjusted = true;
            }
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
                dust.color = Color.Cyan;
            }

            if (Main.rand.NextBool(5))
            {
                Vector2 offset = Projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
                Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, DustID.Smoke, 0.0f, 0.0f, 100, new Color(), 1.5f)];
                dust.velocity *= 0.5f;
                dust.velocity.Y = -Math.Abs(dust.velocity.Y);
                unit = dustPos - Main.player[Projectile.owner].Center;
                unit.Normalize();
                dust = Main.dust[Dust.NewDust(Main.player[Projectile.owner].Center + 55 * unit, 8, 8, DustID.Smoke, 0.0f, 0.0f, 100, new Color(), 1.5f)];
                dust.velocity = dust.velocity * 0.5f;
                dust.velocity.Y = -Math.Abs(dust.velocity.Y);
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
        private void UpdatePlayer(Player player)
        {
            // Multiplayer support here, only run this code if the client running it is the owner of the projectile
            if (Projectile.owner == Main.myPlayer && Projectile.ai[1] == 0)
            {
                Vector2 diff = Main.MouseWorld - player.Center;
                diff.Normalize();
                Projectile.velocity = diff;
                Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[1] == 0)
            {
                int dir = Projectile.direction;
                player.ChangeDir(dir); // Set player direction to where we are shooting
                player.heldProj = Projectile.whoAmI; // Update player's held projectile
                player.itemTime = 2; // Set item time to 2 frames while we are used
                player.itemAnimation = 2; // Set item animation time to 2 frames while we are used}
                player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * dir, Projectile.velocity.X * dir); // Set the item rotation to where we are shooting
            }
        }
        private void ChargeLaser(Player player)
        {
            if (!player.channel)
            {
                Projectile.ai[1] = 1;
            }
            else if (Projectile.ai[1] == 0)
            {
                // Do we still have enough mana? If not, we kill the projectile because we cannot use it anymore
                if (Main.time % 10 < 1 && !player.CheckMana(player.inventory[player.selectedItem].mana, true))
                {
                    Projectile.ai[1] = 1;
                }
                else
                {
                    Vector2 offset = Projectile.velocity;
                    offset *= MOVE_DISTANCE - 20;
                    Vector2 pos = player.Center + offset - new Vector2(10, 10);
                    if (Charge < MAX_CHARGE)
                    {
                        Charge++;
                    }
                    if (Charge >= MAX_CHARGE)
                    {
                        Projectile.ai[1] = 1;
                    }
                    int chargeFact = (int)(Charge / 20f);
                    Vector2 dustVelocity = Vector2.UnitX * 18f;
                    dustVelocity = dustVelocity.RotatedBy(Projectile.rotation - 1.57f);
                    Vector2 spawnPos = Projectile.Center + dustVelocity;
                    for (int k = 0; k < chargeFact + 1; k++)
                    {
                        Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - chargeFact * 2);
                        Dust dust = Main.dust[Dust.NewDust(pos, 20, 20, DustID.Electric, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f)];
                        dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - chargeFact * 2f) / 10f;
                        dust.noGravity = true;
                        dust.scale = Main.rand.Next(10, 20) * 0.05f;
                    }
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
