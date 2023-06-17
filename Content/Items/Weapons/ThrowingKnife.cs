using System;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using LensRands.LensUtils;

namespace LensRands.Content.Items.Weapons
{
    public class ThrowingKnife : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/ThrowingKnife";
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.LightRed;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true; 

            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 35;
            Item.crit = 15;

            Item.shoot = ModContent.ProjectileType<ThrowingKnifeProjectile>();
            Item.shootSpeed = 10f; 
            Item.UseSound = SoundID.Item1;
        }
    }
    public class ThrowingKnifeProjectile : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/ThrowingKnife";
        private readonly int dropdelay = 20;
        private readonly int ttl = 1200;
        public float Bounces { get => Projectile.ai[0]; set => Projectile.ai [0] = value; }
        private bool setBounces = false;
        public override void SetDefaults()
        {
            Projectile.width = 32;               //The width of projectile hitbox
            Projectile.height = 32;              //The height of projectile hitbox
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = ttl;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.timeLeft = 1;
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void AI()
        {
            if (!setBounces)
            {
                Bounces = 2;
                setBounces = true;
            }
            if (Projectile.timeLeft <= (ttl - dropdelay))
            {
                Projectile.velocity.Y += 0.33f;
            }
            float rotate = Projectile.velocity.X > 0 ? 22.5f : -22.5f;
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
            Projectile.rotation += MathHelper.ToRadians(rotate);
            if (Bounces <= 0)
            {
                LensUtil.HomeOnEnemy(Projectile,600f,20f,out _,false,1.5f);
            }
            else
            {
                LensUtil.HomeOnEnemy(Projectile, 600f, 20f, out _, false, 0.05f);
            }
            
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Bounces > 0)
            {
                Bounces -= 1;
                if (Bounces == 1)
                {
                    if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
                    {
                        Projectile.velocity.X = oldVelocity.X * -0.75f;
                    }
                    if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
                    {
                        Projectile.velocity.Y = oldVelocity.Y * -0.75f;
                    }
                }
                else
                {
                    if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
                    {
                        Projectile.velocity.X = oldVelocity.X * -1.5f;
                    }
                    if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
                    {
                        Projectile.velocity.Y = oldVelocity.Y * -1.5f;
                    }
                }
                Projectile.damage = (int)(Projectile.damage * 1.5f);
                return false;
            }
            return true;
        }
    }
    public class StrengthenedThrowingKnife : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/ThrowingKnife";
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.LightRed;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;

            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 255;
            Item.crit = 15;

            Item.shoot = ModContent.ProjectileType<SThrowingKnifeProjectile>();
            Item.shootSpeed = 10f;
            Item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LunarBar, 10)
                .AddIngredient(ModContent.ItemType<ThrowingKnife>())
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
    public class SThrowingKnifeProjectile : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/ThrowingKnife";
        private readonly int dropdelay = 20;
        private readonly int ttl = 1200;
        public float Bounces { get => Projectile.ai[0]; set => Projectile.ai[0] = value; }
        private bool setBounces = false;
        public override void SetDefaults()
        {
            Projectile.width = 32;               //The width of projectile hitbox
            Projectile.height = 32;              //The height of projectile hitbox
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = ttl;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.timeLeft = 1;
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void AI()
        {
            if (!setBounces)
            {
                Bounces = 2;
                setBounces = true;
            }
            if (Projectile.timeLeft <= (ttl - dropdelay))
            {
                Projectile.velocity.Y += 0.33f;
            }
            float rotate = Projectile.velocity.X > 0 ? 22.5f : -22.5f;
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
            Projectile.rotation += MathHelper.ToRadians(rotate);
            if (Bounces <= 0)
            {
                LensUtil.HomeOnEnemy(Projectile, 600f, 20f, out bool targetFound,false, 6f);
                Projectile.tileCollide = !targetFound;
            }
            else
            {
                LensUtil.HomeOnEnemy(Projectile, 600f, 20f, out _,false, 0.2f);
            }

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Bounces > 0)
            {
                Bounces -= 1;
                if (Bounces == 1)
                {
                    if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
                    {
                        Projectile.velocity.X = oldVelocity.X * -0.75f;
                    }
                    if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
                    {
                        Projectile.velocity.Y = oldVelocity.Y * -0.75f;
                    }
                }
                else
                {
                    if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
                    {
                        Projectile.velocity.X = oldVelocity.X * -1.5f;
                    }
                    if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
                    {
                        Projectile.velocity.Y = oldVelocity.Y * -1.5f;
                    }
                }
                Projectile.damage = (int)(Projectile.damage * 1.5f);
                return false;
            }
            return true;
        }
    }

}
