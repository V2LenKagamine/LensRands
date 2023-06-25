using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Ammo
{
    public abstract class SpecialGels : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "smiel";
        public abstract int projectiletype { get; }
        public abstract int bonusDmg { get; }
        public override void SetDefaults()
        {
            Item.damage = bonusDmg;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 1.5f;
            Item.value = 10;
            Item.rare = ItemRarityID.Green;
            Item.shoot = projectiletype;
            Item.shootSpeed = 8f;
            Item.ammo = AmmoID.Gel;
        }
    }
    public class CryoGel : SpecialGels
    {
        public override string Texture => LensRands.AssetsPath + "Items/Ammo/CryoGel";
        public override int projectiletype => ModContent.ProjectileType<CryoGelProjectile>();
        public override int bonusDmg => -1;

        public override void AddRecipes()
        {
            CreateRecipe(50)
                .AddIngredient(ItemID.Gel,50)
                .AddIngredient(ItemID.IceBlock)
                .Register();
        }
    }

    public class PoisonGel : SpecialGels
    {
        public override string Texture => LensRands.AssetsPath + "Items/Ammo/PoisonGel";
        public override int projectiletype => ModContent.ProjectileType<PoisonGelProjectile>();

        public override int bonusDmg => -1;

        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient(ItemID.Gel, 100)
                .AddIngredient(ItemID.Stinger)
                .Register();
        }
    }

    public class CursedGel : SpecialGels
    {
        public override string Texture => LensRands.AssetsPath + "Items/Ammo/CursedGel";
        public override int projectiletype => ModContent.ProjectileType<CursedGelProjectile>();

        public override int bonusDmg => 5;

        public override void AddRecipes()
        {
            CreateRecipe(250)
                .AddIngredient(ItemID.Gel,250)
                .AddIngredient(ItemID.CursedFlame)
                .Register();
        }
    }
    public class IchorGel : SpecialGels
    {
        public override string Texture => LensRands.AssetsPath + "Items/Ammo/IchorGel";
        public override int projectiletype => ModContent.ProjectileType<IchorGelProjectile>();

        public override int bonusDmg => 3;

        public override void AddRecipes()
        {
            CreateRecipe(250)
                .AddIngredient(ItemID.Gel, 250)
                .AddIngredient(ItemID.Ichor)
                .Register();
        }
    }

    //Projectiles

    public abstract class SpecialGelsProjectile : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "sorrynothing";
        public abstract int projectileDust { get; }
        public abstract int TTL { get; }
        public abstract int ProjDebuff { get; }
        public abstract int Debufftime { get; }
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
            Projectile.extraUpdates = 0;
            Projectile.timeLeft = TTL;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            var dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, projectileDust, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);

            dust.noGravity = true;
            dust.scale *= 1.75f;
            dust.velocity.X *= 2f;
            dust.velocity.Y *= 2f;

            var dust2 = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, projectileDust, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100);

            dust2.noGravity = true;
            dust2.scale *= 3f;
            dust2.velocity.X *= 2.5f;
            dust2.velocity.Y *= 2.5f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (ProjDebuff!=0 && Debufftime != 0)
            {
                target.AddBuff(ProjDebuff, Debufftime);
            }
        }
    }

    public class CryoGelProjectile : SpecialGelsProjectile
    {
        public override int projectileDust => DustID.IceTorch;

        public override int TTL => 30;

        public override int ProjDebuff => BuffID.Frostburn;

        public override int Debufftime => 150;
    }

    public class PoisonGelProjectile : SpecialGelsProjectile
    {
        public override int projectileDust => DustID.GreenTorch;

        public override int TTL => 25;

        public override int ProjDebuff => BuffID.Poisoned;

        public override int Debufftime => 150;
    }

    public class CursedGelProjectile : SpecialGelsProjectile
    {
        public override int projectileDust => DustID.JungleTorch;

        public override int TTL => 45;

        public override int ProjDebuff => BuffID.CursedInferno;

        public override int Debufftime => 180;
    }

    public class IchorGelProjectile : SpecialGelsProjectile
    {
        public override int projectileDust => DustID.YellowTorch;

        public override int TTL => 40;

        public override int ProjDebuff => BuffID.Ichor;

        public override int Debufftime => 180;
    }

}
