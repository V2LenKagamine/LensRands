using LensRands.Content.Buffs;
using LensRands.LensUtils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Ammo
{
    public class MartianHomingRounds : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Ammo/Martian";
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 5f;
            Item.value = 10;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<MartianHomingRoundsProj>();
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            CreateRecipe(60)
                .AddIngredient(ItemID.ChlorophyteBullet, 60)
                .AddIngredient(ItemID.MartianConduitPlating)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    public class MartianHomingRoundsProj : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/Martian";
        private Vector3 color = new(0f, 0f, 1f);
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
            LensUtil.HomeOnEnemy(Projectile, 500, 16f, out _,false, 2.5f,true);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<MartianShock>(), 600);
        }
    }
}
