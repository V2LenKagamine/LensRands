using System;
using System.Collections.Generic;
using Terraria.Audio;
using LensRands.Systems.ModSys;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using LensRands.Systems;

namespace LensRands.Content.Items.Weapons
{
    public class RealKnife : ModItem
    {


        public override string Texture => LensRands.AssetsPath + "Items/Weapons/RealKnife";


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.WoodenSword)
                .AddIngredient(ItemID.DemoniteOre, 20)
                .AddTile(TileID.DemonAltar)
                .AddCondition(Condition.InMasterMode)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.WoodenSword)
                .AddIngredient(ItemID.CrimtaneOre, 20)
                .AddTile(TileID.DemonAltar)
                .AddCondition(Condition.InMasterMode)
                .Register();
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 56;
            Item.scale = 0.7f;
            Item.rare = ItemRarityID.Master;
            Item.knockBack = 0.5f;

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

            Item.value = Item.buyPrice(0, 2, 50, 0);
        }


        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.GetModPlayer<LensPlayer>().HighestBossKilled < 8)
            {
                return false;
            }
            if (player.altFunctionUse == 2 && player.GetModPlayer<LensPlayer>().KnifeOut)
            {
                player.GetModPlayer<LensPlayer>().KnifeOut = false;
                player.GetModPlayer<LensPlayer>().KnifeTimer = 0;
                return base.Shoot(player, source, position, velocity, type, damage, knockback);
            }
            if (player.altFunctionUse == 2 && !player.GetModPlayer<LensPlayer>().KnifeOut && player.statMana == player.statManaMax2)
            {
                player.GetModPlayer<LensPlayer>().KnifeOut = true;
                player.statMana -= player.statMana;
                player.manaRegenDelay = 300;
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<LensPlayer>().KnifeOut)
            {
                LensPlayer plmp = player.GetModPlayer<LensPlayer>();
                int newdamage = player.statManaMax2 / 10 + damage;
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
                            newdamage += (int)(newdamage * (timer/100f));
                            break;
                        }
                    case 2:
                        {
                            newdamage *= 3;
                            player.statMana = player.statManaMax2;
                            SoundEngine.PlaySound(AudioSys.RealSlash, player.position);
                            break;
                        }
                    case 3:
                        {
                            newdamage += (int)(newdamage * ((100 - timer)/100f));
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
            new KeyValuePair<int, int>(5,25), //Eye.
            new KeyValuePair<int, int>(6,30), //Worm/Brain
            new KeyValuePair<int, int>(7,35), //Bee
            new KeyValuePair<int, int>(8,28), //Skele, projectile gained
            new KeyValuePair<int, int>(9,32), //Deer
            new KeyValuePair<int, int>(10,40), //Woll
            new KeyValuePair<int, int>(11,45), //Queen
            new KeyValuePair<int, int>(12,55), //All Mechs
            new KeyValuePair<int, int>(13,75), //Plant
            new KeyValuePair<int, int>(14,90), //Golem
            new KeyValuePair<int, int>(15,100), //Feesh
            new KeyValuePair<int, int>(16,120), //Empress
            new KeyValuePair<int, int>(17,130), //CultyBoi
            new KeyValuePair<int, int>(18,140), //All towers
            new KeyValuePair<int, int>(19,185), //Moonlol
        };
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            int newbasedamage = 0;
            foreach (KeyValuePair<int, int> pair in KnifeDmgs)
            {
                if (pair.Key == player.GetModPlayer<LensPlayer>().HighestBossKilled)
                {
                    newbasedamage = pair.Value;
                    break;
                }
            }
            damage.Base = newbasedamage;
        }


        public override void HoldItem(Player player)
        {

            if (player.GetModPlayer<LensPlayer>().KnifeTimer >= 100 && player.GetModPlayer<LensPlayer>().KnifeOut)
            {

                player.GetModPlayer<LensPlayer>().KnifeOut = false;
                player.GetModPlayer<LensPlayer>().KnifeTimer = 0;

            }
            if (player.GetModPlayer<LensPlayer>().KnifeOut)
            {
                player.GetModPlayer<LensPlayer>().KnifeTimer += 1;
            }

            base.HoldItem(player);
        }
    }

    public class RealKnifeProjectile : ModProjectile
    {

        public override string Texture => LensRands.AssetsPath + "Projectiles/RealKnife";
        public override void SetDefaults()
        {
            Projectile.width = 124;               //The width of projectile hitbox
            Projectile.height = 124;              //The height of projectile hitbox
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 60;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 0f, 0f);
            Projectile.alpha += 4;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Vector2 vel2 = new(Main.rand.NextFloat(-0.5f, 0.5f), 1f);
                Projectile p = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(),target.Center, vel2, ModContent.ProjectileType<RealKnife999>(), 0, 0);
                p.rotation = MathHelper.ToRadians(Main.rand.NextFloat(-15f, 16f));
                p.Center = target.Center + new Vector2(0, -60);
            }
        }
    }

    public class RealKnifeProjectileT : ModProjectile
    {

        public override string Texture => LensRands.AssetsPath + "Projectiles/RealKnifeT";
        public override void SetDefaults()
        {
            Projectile.width = 16;               //The width of projectile hitbox
            Projectile.height = 16;              //The height of projectile hitbox
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 30;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame

            DrawOffsetX = DrawOriginOffsetY = -8;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 0f, 0f);
            Projectile.alpha += 7;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
        }
    }
    public class RealKnife999 : ModProjectile
    {

        public override string Texture => LensRands.AssetsPath + "Projectiles/RealKnife999";
        public override void SetDefaults()
        {
            Projectile.width = 192;               //The width of projectile hitbox
            Projectile.height = 28;              //The height of projectile hitbox
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 90;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }
        public override void AI()
        {
            Projectile.alpha += 3;
            Lighting.AddLight(Projectile.Center, 1f, 0f, 0f);
            Projectile.velocity *= 0.8f;
        }
    }
}
