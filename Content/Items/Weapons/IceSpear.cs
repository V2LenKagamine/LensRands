using System;
using LensRands.Systems.ModSys;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Weapons
{
    public class IceSpear : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/IceSpear";
        public override void SetDefaults()
        {
            Item.height = 16;
            Item.width = 32;
            Item.mana = 6;
            Item.channel = true;
            Item.damage = 2800;
            Item.rare = ItemRarityID.Blue;
            Item.crit = 8;
            Item.DamageType = DamageClass.Magic;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 3;
            Item.shoot = ModContent.ProjectileType<IceSpearProj>();
            Item.UseSound = SoundID.Item13;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 7, 50, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MartianConduitPlating,50)
                .AddIngredient(ItemID.FrostStaff)
                .AddTile(TileID.CrystalBall)
                .Register();
        }
    }
    public class IceSpearProj : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/IceSpear";
        private const float MAX_CHARGE = 180f;
        public float Charge
        {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }
        public bool Fired
        {
            get => Projectile.ai[0] == 1;
            set => Projectile.ai[0] = value == true ? 1 : 0;
        }
        public bool IsAtMaxCharge => Charge == MAX_CHARGE;
        public bool damageAdjusted = false;
        public Vector3 color = new(0.1f, 0.44f, 0.44f);
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 150;
            DrawOffsetX = -12;
            DrawOriginOffsetY = -8;
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Lighting.AddLight(Projectile.Center, color * (Charge / MAX_CHARGE) * 2);
            if (!Fired)
            {
                Projectile.timeLeft = 150;
                UpdatePlayer(player);
                ChargeUp(player);
                return;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
            Vector2 diff = Main.MouseWorld - player.Center;
            diff.Normalize();
            if (Charge >= MAX_CHARGE && !damageAdjusted)
            {
                Projectile.damage = (int)(Projectile.damage * 1.25f);
                damageAdjusted = true;
                Projectile.friendly = true;
                Projectile.velocity = diff * 30f;
            }
            else if (!damageAdjusted)
            {
                Projectile.damage = (int)(Projectile.damage * (Charge / MAX_CHARGE));
                damageAdjusted = true;
                Projectile.friendly = true;
                Projectile.velocity = diff * 25f * (Charge/MAX_CHARGE);
            }
            if (Projectile.alpha <= 180)
            {
                Projectile.alpha += 2;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.boss && !target.immortal && target.life <= target.lifeMax*0.20f)
            {
                target.StrikeInstantKill();
            }
            else if (target.boss && !target.immortal && target.life <= target.lifeMax * 0.05f)
            {
                target.StrikeInstantKill();
            }
            else
            {
                target.AddBuff(BuffID.Frostburn, 600);
            }
        }

        private void UpdatePlayer(Player player)
        {
            // Multiplayer support here, only run this code if the client running it is the owner of the projectile
            if (Projectile.owner == Main.myPlayer && !Fired)
            {
                Vector2 diff = Main.MouseWorld - player.Center;
                diff.Normalize();
                int offset = player.direction == 1 ? -5 : 10;
                Projectile.position = player.position + new Vector2(offset,0);
                Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                Projectile.netUpdate = true;
                Projectile.rotation = diff.ToRotation() + MathHelper.ToRadians(45);
            }
            if (!Fired)
            {
                int dir = Projectile.direction;
                player.ChangeDir(dir);
                player.heldProj = Projectile.whoAmI;
                player.itemTime = 19;
                player.itemAnimation = 19;
                player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * dir, Projectile.velocity.X * dir);
            }
        }
        private void ChargeUp(Player player)
        {
            if (!player.channel)
            {
                Fired = true;
            }
            else if (!Fired)
            {
                if (Main.time % 10 < 1 && !player.CheckMana(player.inventory[player.selectedItem].mana, true))
                {
                    Fired = true;
                }
                else
                {
                    if (Charge < MAX_CHARGE)
                    {
                        Charge++;
                        Projectile.alpha -= 2;
                    }
                    if (Charge >= MAX_CHARGE)
                    {
                        Fired = true;
                    }
                }
            }
        }
    }
}