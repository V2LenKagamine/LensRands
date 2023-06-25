using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Weapons
{
    public class Zarr : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Zarr";

        public int ShotgunShots = 6;
        public int CannonShots = 1;
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.Boomstick)
                .AddIngredient(ItemID.Obsidian, 20)
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddTile(TileID.Hellforge)
                .Register();
        }

        public override void SetDefaults()
        {
            Item.useAmmo = AmmoID.Bullet;
            Item.autoReuse = false;
            Item.damage = 85;
            Item.rare = ItemRarityID.Green;
            Item.DamageType = DamageClass.Ranged;
            Item.crit = 8;
            Item.useTime = 54;
            Item.useAnimation = 54;
            Item.knockBack = 3;
            Item.shootSpeed = 7f;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.UseSound = SoundID.Item62; //Item7 maybe?
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;

        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < ShotgunShots; i++)
                {
                    Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                    newVel *= 1f - Main.rand.NextFloat(0.1f);

                    Projectile.NewProjectileDirect(source, position, newVel, type, damage / 4, knockback, player.whoAmI);
                }

                return false;
            }
            else
            {

                for (int i = 0; i < CannonShots; i++)
                {
                    Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(2.5f));

                    newVel *= 1f - Main.rand.NextFloat(0.1f);

                    Projectile.NewProjectileDirect(source, position, newVel, ModContent.ProjectileType<ZarrProjectile>(), damage, knockback, player.whoAmI);
                }

                return false;
            }
        }
    }
    public class KuvaZarr : ModItem
    {

        public int ShotgunShots = 10;
        public int CannonShots = 1;
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/KuvaZarr";

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ModContent.ItemType<Zarr>())
                .AddIngredient(ItemID.HellstoneBar, 20)
                .AddIngredient(ItemID.SoulofFright, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Kuva Zarr");
            /* Tooltip.SetDefault("A gun from the Void." +
                "\nThis one packs more punch."); */
        }

        public override void SetDefaults()
        {
            Item.useAmmo = AmmoID.Bullet;
            Item.autoReuse = false;
            Item.damage = 185;
            Item.rare = ItemRarityID.Blue;
            Item.DamageType = DamageClass.Ranged;
            Item.crit = 16;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.knockBack = 3;
            Item.shootSpeed = 9f;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.UseSound = SoundID.Item62; //Item7 maybe?
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < ShotgunShots; i++)
                {
                    Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                    newVel *= 1f - Main.rand.NextFloat(0.1f);

                    Projectile.NewProjectileDirect(source, position, newVel, type, damage / 4, knockback, player.whoAmI);
                }

                return false;
            }
            else
            {

                for (int i = 0; i < CannonShots; i++)
                {
                    Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(2.5f));

                    newVel *= 1f - Main.rand.NextFloat(0.1f);

                    Projectile.NewProjectileDirect(source, position, newVel, ModContent.ProjectileType<ZarrProjectile>(), damage, knockback, player.whoAmI);
                }

                return false;
            }
        }
        public override void ModifyItemScale(Player player, ref float scale)
        {
            base.ModifyItemScale(player, ref scale);
        }
    }
    public class PaddJommKuvaZarr : ModItem
    {

        public int ShotgunShots = 10;
        public int CannonShots = 1;
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/PaddJommKuvaZarr";

        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) && calamity.TryFind("DarksunFragment", out ModItem moditem))
            {
                Recipe recipe = CreateRecipe()
                .AddIngredient(ModContent.ItemType<KuvaZarr>())
                .AddIngredient(moditem.Type, 30)
                .AddIngredient(ItemID.FragmentVortex, 15)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
            }

        }
        public override void SetDefaults()
        {
            Item.useAmmo = AmmoID.Bullet;
            Item.autoReuse = false;
            Item.damage = 1500;
            Item.rare = ItemRarityID.Purple;
            Item.DamageType = DamageClass.Ranged;
            Item.crit = 24;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.knockBack = 4;
            Item.shootSpeed = 12f;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.UseSound = SoundID.Item62; //Item7 maybe?
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < ShotgunShots; i++)
                {
                    Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                    newVel *= 1f - Main.rand.NextFloat(0.1f);

                    Projectile.NewProjectileDirect(source, position, newVel, type, damage / 4, knockback, player.whoAmI);
                }

                return false;
            }
            else
            {

                for (int i = 0; i < CannonShots; i++)
                {
                    Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(2.5f));

                    newVel *= 1f - Main.rand.NextFloat(0.1f);

                    Projectile.NewProjectileDirect(source, position, newVel, ModContent.ProjectileType<ZarrProjectile>(), damage, knockback, player.whoAmI);
                }

                return false;
            }
        }
    }
    public class ZarrProjectile : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/Zarr";
        public override void SetDefaults()
        {
            Projectile.width = 6;               //The width of projectile hitbox
            Projectile.height = 6;              //The height of projectile hitbox
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 2400;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.light = 0.25f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }

        public override void Kill(int timeLeft) //Also Stolen
        {

            if (Projectile.owner == Main.myPlayer && Projectile.ai[1] == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    // Random upward vector.
                    Vector2 launchVelocity = new Vector2(Main.rand.NextFloat(-1.5f, 1.5f), Main.rand.NextFloat(-5, -3));
                    // Importantly, ai1 is set to 1 here. This is checked in OnTileCollide to prevent bouncing and here in Kill to prevent an infinite chain of splitting projectiles.
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, launchVelocity, Projectile.type, Projectile.damage / 2, Projectile.knockBack, Main.myPlayer, 0, 1);
                }
            }
            if (Projectile.ai[1] == 0)
            {
                // Smoke Dust spawn
                for (int i = 0; i < 25; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
                    dust.velocity *= 1.4f;
                }

                // Fire Dust spawn
                for (int i = 0; i < 40; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3f);
                    dust.noGravity = true;
                    dust.velocity *= 5f;
                    dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2f);
                    dust.velocity *= 3f;
                }

                // Large Smoke Gore spawn
                for (int g = 0; g < 2; g++)
                {
                    var goreSpawnPosition = new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f);
                    Gore gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                    gore.scale = 1.5f;
                    gore.velocity.X += 1.5f;
                    gore.velocity.Y += 1.5f;
                    gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                    gore.scale = 1.5f;
                    gore.velocity.X -= 1.5f;
                    gore.velocity.Y += 1.5f;
                    gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                    gore.scale = 1.5f;
                    gore.velocity.X += 1.5f;
                    gore.velocity.Y -= 1.5f;
                    gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                    gore.scale = 1.5f;
                    gore.velocity.X -= 1.5f;
                    gore.velocity.Y -= 1.5f;
                }
            }
            // Play explosion sound
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.timeLeft = 3;
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.timeLeft = 3;
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void AI() //Yea NGL copied this from Example
        {
            // The projectile is in the midst of exploding during the last 3 updates.
            if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
            {
                Projectile.tileCollide = false;
                // Set to transparent. This projectile technically lives as transparent for about 3 frames
                Projectile.alpha = 255;

                // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
                if (Projectile.ai[1] == 0)
                { Projectile.Resize(200, 200); }
                else
                { Projectile.Resize(30, 30); }
                Projectile.knockBack = 10f;
            }
            else
            {
                // Smoke and fuse dust spawn. The position is calculated to spawn the dust directly on the fuse.
                if (Main.rand.NextBool())
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1f);
                    dust.scale = 0.1f + Main.rand.Next(5) * 0.1f;
                    dust.fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
                    dust.noGravity = true;
                    dust.position = Projectile.Center + new Vector2(1, 0).RotatedBy(Projectile.rotation - 2.1f, default) * 10f;

                    dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1f);
                    dust.scale = 1f + Main.rand.Next(5) * 0.1f;
                    dust.noGravity = true;
                    dust.position = Projectile.Center + new Vector2(1, 0).RotatedBy(Projectile.rotation - 2.1f, default) * 10f;
                }
            }
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 10f)
            {
                Projectile.ai[0] = 10f;
                // Roll speed dampening. 
                if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X * 0.96f;

                    if (Projectile.velocity.X > -0.01 && Projectile.velocity.X < 0.01)
                    {
                        Projectile.velocity.X = 0f;
                        Projectile.netUpdate = true;
                    }
                }
                // Delayed gravity
                Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
            }
            // Rotation increased by velocity.X 
            Projectile.rotation += Projectile.velocity.X * 0.1f;
        }
    }
}
