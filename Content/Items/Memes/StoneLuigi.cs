using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Memes
{
    public class StoneLuigi : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Football";


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(200)
                .AddIngredient(ItemID.StoneBlock, 20)
                .AddTile(TileID.WorkBenches)
                .Register();
        }


        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.damage = 24;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 12f;
            Item.maxStack = 9999;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Gray;
            Item.noUseGraphic = true;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ModContent.ProjectileType<FootballProjectile>();
            Item.shootSpeed = 16f;
        }

    }

    public class FootballProjectile : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Football";

        private readonly int dropdelay = 20;
        private readonly int ttl = 1200;
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = ttl;
            Projectile.light = 0f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (Projectile.timeLeft <= ttl - dropdelay)
            {
                Projectile.velocity.X *= 0.99f;
                Projectile.velocity.Y += 0.45f;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
        }

    }
}
