using System;
using LensRands.LensUtils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Weapons
{

    public class RpgLauncher : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/RocketGuide";
        public override void SetDefaults()
        {
            Item.autoReuse = false;
            Item.damage = 666;
            Item.rare = ItemRarityID.Red;
            Item.DamageType = DamageClass.Ranged;
            Item.crit = 8;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.knockBack = 3;
            Item.shootSpeed = 4f;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<RocketGuide>();
            Item.UseSound = SoundID.Item62; //Item7 maybe?
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 6, 6, 6);

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " sold their soul to a guide hating demon!"), 20, 1, false, false, 30, false, 0, 0, 0);
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
    public class RocketGuide : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/RocketGuide";
        public override void SetDefaults()
        {
            Projectile.width = 8;               //The width of projectile hitbox
            Projectile.height = 8;              //The height of projectile hitbox
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 1800;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.light = 0.25f;            //How much light emit around the projectile
            Projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 4)
            {
                LensVisualUtils.BombVisuals(Projectile.Center, 128, 128);
            }
            if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
            {
                Projectile.tileCollide = false;
                Projectile.alpha = 255;
                Projectile.Resize(128, 128);
                Projectile.knockBack = 8f;   
            }
            else
            {
                if (Math.Abs(Projectile.velocity.X) >= 8f || Math.Abs(Projectile.velocity.Y) >= 8f)
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
                if (Math.Abs(Projectile.velocity.X) <= 15f && Math.Abs(Projectile.velocity.Y) <= 15f)
                {
                    Projectile.velocity *= 1.1f;
                }
            }
            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + MathHelper.PiOver2;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.timeLeft = 5;
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.timeLeft = 5;
        }
    }
}
