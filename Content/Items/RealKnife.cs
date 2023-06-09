using System;
using System.Collections.Generic;
using Terraria.Audio;
using LensRands.Systems.ModSys;
using LensRands.Systems.PlayerSys;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items
{
    public class RealKnife : ModItem
    {


        public override string Texture => LensRands.AssetsPath + "Items/RealKnife";


        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Real Knife");
            /* Tooltip.SetDefault("A knife from a lost timeline." + 
                "\nSeems to grow sharper as you gain strength." +
                "\n[c/FF0000:Finally.]"); */
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.WoodenSword)
                .AddIngredient(ItemID.DemoniteOre, 20)
                .AddTile(TileID.DemonAltar)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.WoodenSword)
                .AddIngredient(ItemID.CrimtaneOre, 20)
                .AddTile(TileID.DemonAltar)
                .Register();
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 56;
            Item.scale = 0.7f;
            Item.rare = ItemRarityID.Master;

            Item.useStyle = ItemUseStyleID.Swing;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 5; //Damage Assigned in ModifyDamage
            Item.crit = 12;

            Item.shoot = ModContent.ProjectileType<RealKnifeProjectile>();
            Item.shootSpeed = 20f;
            Item.UseSound = SoundID.Item1;
        }


        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.GetModPlayer<WeaponPlayer>().HighestBossKilled < 5)
            {
                return false;
            }
            if (player.altFunctionUse == 2 && player.GetModPlayer<WeaponPlayer>().KnifeOut)
            {
                player.GetModPlayer<WeaponPlayer>().KnifeOut = false;
                player.GetModPlayer<WeaponPlayer>().KnifeTimer = 0;
                return base.Shoot(player, source, position, velocity, type, damage, knockback);
            }
            if (player.altFunctionUse == 2 && !(player.GetModPlayer<WeaponPlayer>().KnifeOut) && player.statMana != 0) 
            {
                player.GetModPlayer<WeaponPlayer>().KnifeOut = true;
                player.statMana -= player.statMana;
                player.manaRegenDelay = 300;
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<WeaponPlayer>().KnifeOut)
            {
                WeaponPlayer plmp = player.GetModPlayer<WeaponPlayer>();
                int newdamage = (player.statManaMax / 2) + damage;
                int timerint = (int)Math.Round(plmp.KnifeTimer);
                int timer;
                if (timerint <= 47)
                {
                    timer = 1;
                }
                else if (timerint > 47 && timerint < 53)
                {
                    timer = 2;
                }
                else
                {
                    timer = 3;
                }
                switch (timer) //Switches suck. Proof above.
                {
                    case 1:
                        {
                            newdamage += (int)(timer * 5);
                            break;
                        }
                    case 2:
                        {
                            newdamage += (newdamage * 5);
                            SoundEngine.PlaySound(AudioSys.RealSlash, player.position);
                            break;
                        }
                    case 3:
                        {
                            newdamage += (int)((100 - timer) * 5);
                            break;
                        }
                }
                damage = newdamage;
            }
            else
            {
                type = ModContent.ProjectileType<RealKnifeProjectileT>();
            }
        }
        public static List<KeyValuePair<int, int>> KnifeDmgs = new List<KeyValuePair<int, int>>
        {
            new KeyValuePair<int, int>(0,5), //No bosses.
            new KeyValuePair<int, int>(4,17), //Slime King
            new KeyValuePair<int, int>(5,25), //Eye,Projectile Gained.
            new KeyValuePair<int, int>(6,30), //Worm/Brain
            new KeyValuePair<int, int>(7,35), //Bee
            new KeyValuePair<int, int>(8,39), //Skele
            new KeyValuePair<int, int>(9,43), //Deer
            new KeyValuePair<int, int>(10,45), //Woll
            new KeyValuePair<int, int>(11,50), //Queen
            new KeyValuePair<int, int>(12,65), //All Mechs
            new KeyValuePair<int, int>(13,80), //Plant
            new KeyValuePair<int, int>(14,105), //Golem
            new KeyValuePair<int, int>(15,130), //Feesh
            new KeyValuePair<int, int>(16,135), //Empress
            new KeyValuePair<int, int>(17,145), //CultyBoi
            new KeyValuePair<int, int>(18,155), //All towers
            new KeyValuePair<int, int>(19,195), //Moonlol
        };
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            int newbasedamage = 0;
            foreach(KeyValuePair<int,int> pair in KnifeDmgs) {
                if (pair.Key == player.GetModPlayer<WeaponPlayer>().HighestBossKilled) {
                    newbasedamage = pair.Value; 
                    break;
                }
            }
            damage.Base = newbasedamage;
        }


        public override void HoldItem(Player player)
        {

            if(player.GetModPlayer<WeaponPlayer>().KnifeTimer >= 100 && player.GetModPlayer<WeaponPlayer>().KnifeOut)
            {

                player.GetModPlayer<WeaponPlayer>().KnifeOut = false;
                player.GetModPlayer<WeaponPlayer>().KnifeTimer = 0;

            }
            if(player.GetModPlayer<WeaponPlayer>().KnifeOut)
            {
                player.GetModPlayer<WeaponPlayer>().KnifeTimer += 1;
            }

            base.HoldItem(player);
        }
    }

    public class RealKnifeProjectile : ModProjectile
    {

        public override string Texture => LensRands.AssetsPath + "Projectiles/RealKnife";
        public override void SetDefaults()
        {
            Projectile.width = 32;               //The width of projectile hitbox
            Projectile.height = 124;              //The height of projectile hitbox
            Projectile.aiStyle = 27;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 60;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.light = 1f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
        }
    }

    public class RealKnifeProjectileT : ModProjectile
    {

        public override string Texture => LensRands.AssetsPath + "Projectiles/RealKnifeT";
        public override void SetDefaults()
        {
            Projectile.width = 16;               //The width of projectile hitbox
            Projectile.height = 32;              //The height of projectile hitbox
            Projectile.aiStyle = 27;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 30;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.light = 0.25f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
    }
}
