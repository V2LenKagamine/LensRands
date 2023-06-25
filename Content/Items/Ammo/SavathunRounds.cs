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
        public override void SetDefaults()//Craft silver bullets plus venom thing jungle
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 1.5f;
            Item.value = 10;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<SavathunProjectile>();
            Item.shootSpeed = 2f;
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
            Projectile.extraUpdates = 3;
            Projectile.timeLeft = 1200;
            Projectile.aiStyle = -1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Lighting.AddLight(Projectile.Center, color * 0.25f);
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
        public override void SetDefaults()//Craft silver bullets plus venom thing jungle
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 1.5f;
            Item.value = 10;
            Item.rare = ItemRarityID.Purple;
            Item.shoot = ModContent.ProjectileType<SavathunProjectileHM>();
            Item.shootSpeed = 3f;
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
        private Vector3 color = new(0.5f,0f,0.5f);
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
            Projectile.extraUpdates = 4;
            Projectile.timeLeft = 1600;
            Projectile.aiStyle = -1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Lighting.AddLight(Projectile.Center, color * 0.75f);
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
