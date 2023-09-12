using System.Drawing;
using LensRands.LensUtils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Ammo
{
    public class SavathunRounds : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Ammo/Savathun";
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 1.5f;
            Item.value = 10;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<SavathunProjectile>();
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            CreateRecipe(250)
                .AddIngredient(ItemID.SilverBullet, 250)
                .AddIngredient(ItemID.Stinger)
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe(250)
                .AddIngredient(ItemID.TungstenBullet, 250)
                .AddIngredient(ItemID.Stinger)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
    public class SavathunProjectile : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/Savathun";
        private Vector3 color = new(0f,1f,0f);
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 600;
            Projectile.aiStyle = -1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Lighting.AddLight(Projectile.Center, color);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 300);
        }
    }
    //Hm VARIANT BELOW
    public class SavathunRoundsHM : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Ammo/SavathunHM";
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 1.5f;
            Item.value = 10;
            Item.rare = ItemRarityID.Purple;
            Item.shoot = ModContent.ProjectileType<SavathunProjectileHM>();
            Item.shootSpeed = 6f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            CreateRecipe(250)
                .AddIngredient(ModContent.ItemType<SavathunRounds>(),250)
                .AddIngredient(ItemID.SpiderFang)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
    public class SavathunProjectileHM : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/SavathunHM";
        private Vector3 color = new(0.5f,0f,5f);
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 2;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 600;
            Projectile.aiStyle = -1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Lighting.AddLight(Projectile.Center, color);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Venom, 300);
            if (!target.active)
            {
                LensVisualUtils.BombVisuals(Projectile.Center, 150, 150);
                Projectile.damage = (int)(Projectile.damage * 1.2f);
                Projectile.tileCollide = false;
                Projectile.penetrate = -1;
                Projectile.alpha = 255;
                Projectile.Resize(150, 150);
                Projectile.timeLeft = 3;
                Projectile.velocity = Vector2.Zero;
            }
        }
    }
}
