using LensRands.LensUtils;
using System.Collections.Generic;
using LensRands.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using LensRands.Content.Buffs;

namespace LensRands.Content.Items.Weapons
{
    public class LuckySeven : ModItem
    {

        public override string Texture => LensRands.AssetsPath + "Items/Weapons/VeiledSword";
        public int LuckySevenMaxCooldown = 4 * 60;
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 64;
            Item.scale = 1.5f;
            Item.rare = ItemRarityID.Master;

            Item.useStyle = ItemUseStyleID.Swing;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 50;
            Item.crit = 10;
            Item.knockBack = 4.5f;
            Item.UseSound = SoundID.Item1;

            Item.shoot = ModContent.ProjectileType<InfinityBlade>();
            Item.shootSpeed = 40f;
            Item.UseSound = SoundID.Item1;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2 && !player.HasBuff<LuckySevenCooldown>())
            {
                player.AddBuff(ModContent.BuffType<LuckySevenCooldown>(), LuckySevenMaxCooldown);
                return base.Shoot(player, source, position, velocity, type, damage, knockback);
            }
            return false;
        }
    }
    public class UnlimitedSword : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/UnlimitedSword";
        public int FinalLuckySevenMaxCooldown = 30 * 60;
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 64;
            Item.scale = 1.5f;
            Item.rare = ItemRarityID.Master;

            Item.useStyle = ItemUseStyleID.Swing;

            Item.noMelee = true;

            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 225;
            Item.crit = 10;
            Item.knockBack = 4.5f;
            Item.UseSound = SoundID.Item1;

            Item.shoot = ModContent.ProjectileType<TachyonSlash>();
            Item.shootSpeed = 0f;
            Item.UseSound = SoundID.Item1;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2 && !player.HasBuff<FinalLuckySevenCooldown>())
            {
                player.AddBuff(ModContent.BuffType<FinalLuckySevenCooldown>(), FinalLuckySevenMaxCooldown);
                Projectile.NewProjectile(Item.GetSource_FromThis(),player.Center,Vector2.Zero,ModContent.ProjectileType<FinalLuckySeven>(),damage,knockback);
            }
        }
    }
    public class InfinityBlade : ModProjectile
    {

        public override string Texture => LensRands.AssetsPath + "Projectiles/InfinityBlade";
        public override void SetDefaults()
        {
            Projectile.width = 32;               //The width of projectile hitbox
            Projectile.height = 32;              //The height of projectile hitbox
            Projectile.aiStyle = 27;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 5;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 30;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.light = 1f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }
    }
    public class TachyonSlash : ModProjectile
    {
        private List<Vector2> PositionsToHit = new();
        public override string Texture => LensRands.AssetsPath + "sorrynothing";
        public override void SetDefaults()
        {
            Projectile.width = 1;               //The width of projectile hitbox
            Projectile.height = 1;              //The height of projectile hitbox
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.alpha = 128;
            Projectile.timeLeft = 48;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }
        public override void AI()
        {
            Projectile.Center = Main.MouseWorld;
            if (Projectile.timeLeft % 6 == 0)
            {
                DotheDamage(Main.player[Projectile.owner],Projectile.damage);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            foreach(Vector2 pos in PositionsToHit)
            {
                DrawHit(pos);
            }
            PositionsToHit.Clear();
            return false;
        }
        private void DotheDamage(Player player, int DamageToDeal)
        {
            List<int> exclude = new List<int>() { player.whoAmI };
            IEnumerable<NPC> NearNPCs = LensUtil.FindNearbyNPCs(150, Projectile.Center, exclude);
            foreach (NPC tohit in NearNPCs)
            {
                PositionsToHit.Add(tohit.Center);
                tohit.SimpleStrikeNPC(DamageToDeal, Projectile.direction, Main.rand.Next(101) <= Projectile.CritChance, Projectile.knockBack, Projectile.DamageType);
            }
        }
        private void DrawHit(Vector2 Position)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(LensRands.AssetsPath + "Projectiles/TachyonSlash");
            Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
            Vector2 origin = sourceRectangle.Size() / 2f;
            Main.EntitySpriteDraw(texture,
                Position - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, Color.White, MathHelper.ToRadians(Main.rand.NextFloat(360f)), origin, 2f, spriteEffects, 0);
        }
    }
    public class FinalLuckySeven : ModProjectile
    {
        private List<Vector2> PositionsToHit = new();
        public override string Texture => LensRands.AssetsPath + "sorrynothing";
        public override void SetDefaults()
        {
            Projectile.width = 1;               //The width of projectile hitbox
            Projectile.height = 1;              //The height of projectile hitbox
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 60*4;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center;
            switch(Projectile.timeLeft)
            {
                case (60*4)-1:
                    {
                        DotheDamage(player, Projectile.damage * 2);
                        break;
                    }
                case 60*3: 
                    {
                        DotheDamage(player, Projectile.damage * 3);
                        break;
                    }
                case 60 * 2:
                    {
                        DotheDamage(player, Projectile.damage * 5);
                        break;
                    }
                case 1:
                    {
                        DotheDamage(player, Projectile.damage * 75);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            player.velocity = Vector2.Zero;
        }
        private void DotheDamage(Player player,int DamageToDeal)
        {
            List<int> exclude = new List<int>() { player.whoAmI };
            IEnumerable<NPC> NearNPCs = LensUtil.FindNearbyNPCs(1600, player.Center, exclude);
            foreach (NPC tohit in NearNPCs)
            {
                PositionsToHit.Add(tohit.Center);
                tohit.SimpleStrikeNPC(DamageToDeal, Projectile.direction, Main.rand.Next(101) <= Projectile.CritChance, Projectile.knockBack, Projectile.DamageType);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            foreach (Vector2 pos in PositionsToHit)
            {
                DrawHit(pos);
            }
            PositionsToHit.Clear();
            return false;
        }
        private void DrawHit(Vector2 Position)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(LensRands.AssetsPath + "Projectiles/TachyonSlash");
            Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
            Vector2 origin = sourceRectangle.Size() / 2f;
            Main.EntitySpriteDraw(texture,
                Position - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, Color.White, MathHelper.ToRadians(Main.rand.NextFloat(360f)), origin, 4f, spriteEffects, 0);
        }
    }
}
