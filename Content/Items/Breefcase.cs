using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items
{
    public class Breefcase : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Breefcase";
        /*
         * Under no circumstance should this abomination have a recipe.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe().AddIngredient(ItemID.Beenade,10).Register();
        }
        */

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Beecase");
            // Tooltip.SetDefault("Contains approximately 500 Angry Bees.\nYes. You read right, 500.");
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Beenade);
            Item.shoot = ModContent.ProjectileType<BreefcaseProjectile>();
            Item.width = 20;
            Item.height = 16;
            Item.damage = 15;
            Item.useTime = 300;

            Item.UseSound = new Terraria.Audio.SoundStyle(LensRands.AssetsPath + "Sounds/breefcase") { MaxInstances = 1 };
        }
    }

    public class BreefcaseProjectile : ModProjectile
    {

        public override string Texture => LensRands.AssetsPath + "Projectiles/Breefcase";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Beecase");
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 16;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.timeLeft = 90;
        }
        public override void Kill(int timeLeft)
        {
            
            if(Main.myPlayer == Projectile.owner)
            {
                for (int bees = 0; bees < 500; bees++)
                {
                    float speedX = Main.rand.Next(-360, 361) * 0.015f;
                    float speedY = Main.rand.Next(-360, 361) * 0.015f;
                    Vector2 speed = new Vector2(speedX, speedY);
                    Projectile thebee = Main.projectile[Projectile.NewProjectile(Projectile.GetSource_FromThis(), Entity.position, speed, Main.player[Entity.owner].beeType(), Main.player[Entity.owner].beeDamage(Entity.damage), Main.player[Entity.owner].beeKB(.01f), Main.myPlayer)];
                    thebee.usesLocalNPCImmunity = true;
                    thebee.localNPCHitCooldown = 5;
                }
            }
            
        }
    }
}
