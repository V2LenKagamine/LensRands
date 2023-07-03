using System.Collections.Generic;
using LensRands.LensUtils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Weapons
{
    public class CursedFlower : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Flowers/CursedFlower";
        public override void SetDefaults()
        {
            Item.height = 16;
            Item.width = 16;
            Item.mana = 11;
            Item.damage = 65;
            Item.crit = 4;
            Item.DamageType = DamageClass.Magic;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.knockBack = 1;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<CursedFlowerProj>();
            Item.UseSound = SoundID.Item20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Green;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CursedFlame,15)
                .AddIngredient(ItemID.FlowerofFire)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }

    public class IchorFlower : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Flowers/IchorFlower";
        public override void SetDefaults()
        {
            Item.height = 16;
            Item.width = 16;
            Item.mana = 11;
            Item.damage = 55;
            Item.crit = 4;
            Item.DamageType = DamageClass.Magic;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.knockBack = 1;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<IchorFlowerProj>();
            Item.UseSound = SoundID.Item20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Yellow;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Ichor, 15)
                .AddIngredient(ItemID.FlowerofFire)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }

    public class RainbowFlower : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Flowers/RainbowFlower";
        public override void SetDefaults()
        {
            Item.height = 16;
            Item.width = 16;
            Item.mana = 11;
            Item.damage = 120;
            Item.crit = 4;
            Item.DamageType = DamageClass.Magic;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.autoReuse = true;
            Item.knockBack = 1;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<RainbowFlowerProj>();
            Item.UseSound = SoundID.Item20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Red;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 15, 0, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<IchorFlower>())
                .AddIngredient(ItemID.FlowerofFire)
                .AddIngredient(ItemID.FlowerofFrost)
                .AddIngredient(ModContent.ItemType<CursedFlower>())
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }

    public abstract class ElementalFlowerProj : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/Flowers/";
        public float Bounces { get => Projectile.ai[0]; set => Projectile.ai[0] = value; }
        private bool setBounces = false;

        public abstract int TotalBounces { get; }
        public abstract int DebuffID { get; }
        public abstract Vector3 ProjColor { get; }
        public override void SetDefaults()
        {
            Projectile.width = 16;               //The width of projectile hitbox
            Projectile.height = 16;              //The height of projectile hitbox
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 3600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }

        public override void AI()
        {
            if (!setBounces)
            {
                Bounces = TotalBounces;
                setBounces = true;
            }
            Projectile.velocity.Y += 0.2f;
            LensUtil.ProjectileROTATE(Projectile,22.5f);
            Lighting.AddLight(Projectile.Center,ProjColor);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Bounces > 0)
            {
                Bounces--;
                LensUtil.ProjectileBounce(Projectile, oldVelocity);
                return false;
            }
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (DebuffID != 0)
            {
                target.AddBuff(DebuffID, 600);
            }
            Bounces--;
        }
    }
    public class CursedFlowerProj : ElementalFlowerProj
    {
        public override string Texture => base.Texture + "Cursed";
        public override int TotalBounces => 8;
        public override int DebuffID => BuffID.CursedInferno;

        public override Vector3 ProjColor => new(0.20f,0.52f,0.27f);
    }
    public class IchorFlowerProj : ElementalFlowerProj
    {
        public override string Texture => base.Texture + "Ichor";
        public override int TotalBounces => 6;
        public override int DebuffID => BuffID.Ichor;
        public override Vector3 ProjColor => new(0.45f, 0.47f, 0.07f);
    }

    public class RainbowFlowerProj : ElementalFlowerProj
    {
        public override string Texture => base.Texture + "Rainbow";
        public override int TotalBounces => 7;
        public override int DebuffID => 0;

        public override Vector3 ProjColor => new(1f, 1f, 1f);

        private readonly List<int> DebuffList = new() 
        {
            BuffID.OnFire,BuffID.Frostburn,BuffID.Ichor,BuffID.CursedInferno
        };
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0;i < DebuffList.Count;i++)
            {
                target.AddBuff(DebuffList[i], 600);
            }
            Bounces--;
        }
    }

}
