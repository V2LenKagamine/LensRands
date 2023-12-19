using LensRands.Content.Buffs;
using LensRands.LensUtils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Ammo
{
    public class BounceFireArrows : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Ammo/BounceFire";
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 8f;
            Item.value = 10;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<BounceFireArrowsProj>();
            Item.shootSpeed = 3.25f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            CreateRecipe(250)
                .AddIngredient(ItemID.HellfireArrow, 250)
                .AddIngredient(ItemID.PinkGel)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
    public class BounceFireArrowsProj : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Items/Ammo/BounceFire";
        public float BouncesLeft { get => Projectile.ai[0]; set => Projectile.ai[0] = value; }
        private Vector3 color = new(1f, 0f, 0f);
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 3600;
            Projectile.aiStyle = -1;
            Projectile.usesLocalNPCImmunity = true;
            DrawOriginOffsetY = -12; 
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 600) { BouncesLeft = 3; }
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.ToRadians(90);
            Lighting.AddLight(Projectile.Center, color);
            LensUtil.HomeOnEnemy(Projectile, 250, 16f, out _, false, 0.15f, true);
            Projectile.velocity.Y += 0.05f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (BouncesLeft > 0)
            {
                BouncesLeft--;
                LensUtil.ProjectileBounce(Projectile, oldVelocity, 0.8f, 0.8f);
                Bakoom();
                return false;
            }
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(BouncesLeft > 0)
            {
                BouncesLeft--;
                LensUtil.ProjectileBounce(Projectile, Projectile.oldVelocity, 0.8f, 0.8f);
                Bakoom();
            }
        }

        public void Bakoom()
        {
            Projectile.tileCollide = false;
            Projectile.Resize(50, 50);
            LensVisualUtils.BombVisuals(Projectile.Center, 50, 50);
            Projectile.Resize(8, 8);
            Projectile.tileCollide = true;
        }
    }
}
