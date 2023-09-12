using LensRands.Content.Buffs;
using LensRands.LensUtils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Ammo
{
    public class MartianBouncing : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Ammo/MartianArrow";
        public override void SetDefaults()
        {
            Item.damage = 19;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 5f;
            Item.value = 10;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<MartianBouncingProj>();
            Item.shootSpeed = 3f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            CreateRecipe(250)
                .AddIngredient(ItemID.ChlorophyteArrow, 250)
                .AddIngredient(ItemID.MartianConduitPlating, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
    public class MartianBouncingProj : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Items/Ammo/MartianArrow";
        private Vector3 color = new(0f, 0f, 1f);
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 5;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 600;
            Projectile.aiStyle = -1;
            Projectile.usesLocalNPCImmunity = true;
            DrawOriginOffsetY = -12;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.ToRadians(90);
            Lighting.AddLight(Projectile.Center, color);
            LensUtil.HomeOnEnemy(Projectile, 250, 16f, out _, false, 0.15f, true);
            Projectile.velocity.Y += 0.05f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate > 0) 
            {
                Projectile.penetrate--;
                LensUtil.ProjectileBounce(Projectile,oldVelocity,1.05f,1.05f);
                return false;
            }
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<MartianShock>(), 600);
        }
    }
}
