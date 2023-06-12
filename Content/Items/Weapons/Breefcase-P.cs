using System.Linq;
using LensRands.Systems.ModSys;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Weapons
{
    public class BreefcaseP : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Breefcase";
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe().AddIngredient(ItemID.Beenade, 25).Register();
        }


        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Beecase-P");
            // Tooltip.SetDefault("Contains approximately 10 Perfect Bee's.\nStingers have been reinforced for multiplied pain!");
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Beenade);
            Item.shoot = ModContent.ProjectileType<BreefcaseProjectileP>();
            Item.width = 20;
            Item.height = 16;
            Item.damage = 15;
            Item.useTime = 60;
            Item.UseSound = AudioSys.BEES;
        }
    }

    public class BreefcaseProjectileP : ModProjectile
    {

        public override string Texture => LensRands.AssetsPath + "Projectiles/Breefcase";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Beecase-P");
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 16;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.timeLeft = 90;
            Projectile.damage = 150;
        }
        public override void Kill(int timeLeft)
        {

            if (Main.myPlayer == Projectile.owner)
            {
                for (int bees = 0; bees < 10; bees++)
                {
                    float speedX = Main.rand.Next(-360, 361) * 0.015f;
                    float speedY = Main.rand.Next(-360, 361) * 0.015f;
                    Vector2 speed = new Vector2(speedX, speedY);
                    Projectile thebee = Main.projectile[Projectile.NewProjectile(Projectile.GetSource_FromThis(), Entity.position, speed, Main.player[Entity.owner].beeType(), Main.player[Entity.owner].beeDamage(Entity.damage), Main.player[Entity.owner].beeKB(.01f), Main.myPlayer)];
                    thebee.usesLocalNPCImmunity = true;
                    thebee.localNPCHitCooldown = 5;
                    thebee.penetrate = 50;
                }
            }

        }
    }
}
