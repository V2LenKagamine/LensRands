using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using LensRands.LensUtils;

namespace LensRands.Content.Items.Weapons
{
    public class AsheRifle : ModItem
    {
        private int cooldown = 0;
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/AsheRifle";
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 8f;
            Item.UseSound = SoundID.Item11;
            Item.useAmmo = AmmoID.Bullet;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(gold: 3);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Musket, 1);
            recipe.AddIngredient(ItemID.Explosives, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void HoldItem(Player Player)
        {
            cooldown--;
        }
        public override bool CanUseItem(Player Player)
        {
            if (Player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noUseGraphic = true;
                Item.useTime = 15;
                Item.UseSound = SoundID.DD2_GoblinBomberThrow;
                Item.useAnimation = 15;
                if (cooldown > 0) { return false; }       
            }
            else
            {
                Item.useTime = 35;
                Item.useAnimation = 35;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noUseGraphic = false;
                Item.UseSound = SoundID.Item11;
            }

            return base.CanUseItem(Player);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15, 0);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 dir = Vector2.Normalize(velocity) * 9;
                velocity = dir;
                type = ModContent.ProjectileType<AsheDynamite>();
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                cooldown = 130;
            }
            else
            {
               Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity * 2, type, damage, knockback, player.whoAmI);
               return false;
            }
            return true;
        }
    }
    public class AsheDynamite : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/AsheDynamite";
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle= -1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 0;
            Projectile.timeLeft = 150;
            Projectile.penetrate = -1;
            DrawOffsetX = -12;
        }
        public override void AI()
        {
            if (Projectile.timeLeft < 2)
            {
                if (Projectile.timeLeft == 1)
                { LensVisualUtils.BombVisuals(Projectile.Center, 250, 250); }
                Projectile.alpha = 255;
                Projectile.velocity = Vector2.Zero;
                Projectile.friendly = true;
                Projectile.tileCollide = false;
                Projectile.knockBack = 8f;
                Projectile.Resize(250, 250);
            }
            else
            {
                var Hitbox = new Rectangle((int)Projectile.Center.X - 30, (int)Projectile.Center.Y - 30, 60, 60);
                IEnumerable<Projectile> list = Main.projectile.Where(x => x.Hitbox.Intersects(Hitbox));
                foreach (Projectile proj in list)
                {
                    if(proj.whoAmI == Projectile.whoAmI) { continue; }
                    if (proj.active && proj.owner == Projectile.owner && Projectile.damage >= 1 && Projectile.timeLeft > 2)
                    {
                        Projectile.timeLeft = 2;
                        proj.active = false;
                        Projectile.damage += proj.damage;
                        Projectile.damage = (int)(Projectile.damage * 1.5f);
                    }
                }
                DustyBoi();
                Projectile.velocity.Y += 0.125f;
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
        private void DustyBoi()
        {
            if (Main.rand.NextBool())
            {
                float DustPosMul = Projectile.velocity.X > 0 ? 16f : -16f;
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1f);
                dust.scale = 0.1f + Main.rand.Next(5) * 0.1f;
                dust.fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
                dust.noGravity = true;
                dust.position = Projectile.Center + new Vector2(1, 0).RotatedBy(Projectile.rotation, default) * DustPosMul;

                dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1f);
                dust.scale = 1f + Main.rand.Next(5) * 0.1f;
                dust.noGravity = true;
                dust.position = Projectile.Center + new Vector2(1, 0).RotatedBy(Projectile.rotation, default) * DustPosMul;
            }
        }
    }
}
