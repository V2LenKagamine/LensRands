using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using LensRands.LensUtils;

namespace LensRands.Content.Items.Weapons
{
    public class HellShock : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Hellshock";
        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 10;
            Item.scale = 2;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0.3f;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 18f;
            Item.UseSound = SoundID.Item11;
            Item.useAmmo = AmmoID.Bullet;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(gold: 5);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.PhoenixBlaster, 1)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-18, 0);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet)
                type = ModContent.ProjectileType<HellshockProj>();
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
    public class HellshockProj : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/Hellshock";
        public int BouncesLeft { get => (int)Projectile.ai[0]; set => Projectile.ai[0] = value; }
        private Vector3 colorA = new(0.1f,0.9f,0.32f);
        private Vector3 colorB = new(0.84f,0.98f,0.07f);
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 720;
            Projectile.light = 0f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            BouncesLeft = 5;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Lighting.AddLight(Projectile.Center, BouncesLeft % 2 == 0 ? colorA : colorB);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (BouncesLeft > 0)
            {
                BouncesLeft--;
                LensUtil.ProjectileBounce(Projectile, oldVelocity);
                return false;
            }
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (BouncesLeft > 0)
            {
                BouncesLeft--;
            }
            target.AddBuff(BouncesLeft % 2 == 0 ? BuffID.CursedInferno : BuffID.Ichor,300);
            if (BouncesLeft <= 0)
            {
                Projectile.timeLeft = 0;
            }
        }

    }
}
