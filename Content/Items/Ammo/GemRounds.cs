using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Ammo
{
    public abstract class GemRounds : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Ammo/GemRounds/";

        public abstract int projectiletype { get; }
        public abstract int bonusDmg { get; }
        public abstract int gem {get; }
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
            Item.rare = ItemRarityID.Blue;
            Item.shoot = projectiletype;
            Item.shootSpeed = 8f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(250)
                .AddIngredient(gem)
                .AddIngredient(ItemID.Glass)
                .AddTile(TileID.GlassKiln)
                .Register();
        }
    }
    public abstract class CrystalineGemRounds : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Ammo/GemRounds/";

        public abstract int projectiletype { get; }
        public abstract int bonusDmg { get; }
        public abstract int gem { get; }
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
            Item.rare = ItemRarityID.Blue;
            Item.shoot = projectiletype;
            Item.shootSpeed = 8f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(250)
                .AddIngredient(gem,250)
                .AddIngredient(ItemID.CrystalShard)
                .AddTile(TileID.GlassKiln)
                .Register();
        }
    }

    public class AmethystRounds : GemRounds
    {
        public override string Texture => base.Texture + "Amethyst";
        public override int projectiletype => ModContent.ProjectileType<AmethystRoundP>();

        public override int bonusDmg => 8;

        public override int gem => ItemID.Amethyst;

    }

    public class CrystalineAmethystRounds : CrystalineGemRounds
    {
        public override string Texture => base.Texture + "Amethyst";
        public override int projectiletype => ModContent.ProjectileType<CrystalineAmethystRoundP>();

        public override int bonusDmg => 12;

        public override int gem => ModContent.ItemType<AmethystRounds>();

    }

    public class RubyRounds : GemRounds
    {
        public override string Texture => base.Texture + "Ruby";
        public override int projectiletype => ModContent.ProjectileType<RubyRoundP>();

        public override int bonusDmg => 8;

        public override int gem => ItemID.Ruby;

    }

    public class CrystalineRubyRounds : CrystalineGemRounds
    {
        public override string Texture => base.Texture + "Ruby";
        public override int projectiletype => ModContent.ProjectileType<CrystalineRubyRoundP>();

        public override int bonusDmg => 12;

        public override int gem => ModContent.ItemType<RubyRounds>();

    }

    public class SapphireRounds : GemRounds
    {
        public override string Texture => base.Texture + "Sapphire";
        public override int projectiletype => ModContent.ProjectileType<SapphireRoundP>();

        public override int bonusDmg => 8;

        public override int gem => ItemID.Sapphire;

    }

    public class CrystalineSapphireRounds : CrystalineGemRounds
    {
        public override string Texture => base.Texture + "Sapphire";
        public override int projectiletype => ModContent.ProjectileType<CrystalineSapphireRoundP>();

        public override int bonusDmg => 12;

        public override int gem => ModContent.ItemType<SapphireRounds>();

    }

    public class DiamondRounds : GemRounds
    {
        public override string Texture => base.Texture + "Diamond";
        public override int projectiletype => ModContent.ProjectileType<DiamondRoundP>();

        public override int bonusDmg => 10;

        public override int gem => ItemID.Diamond;

    }

    public class CrystalineDiamondRounds : CrystalineGemRounds
    {
        public override string Texture => base.Texture + "Diamond";
        public override int projectiletype => ModContent.ProjectileType<CrystalineDiamondRoundP>();

        public override int bonusDmg => 18;

        public override int gem => ModContent.ItemType<DiamondRounds>();
    }

    public class EmeraldRounds : GemRounds
    {
        public override string Texture => base.Texture + "Emerald";
        public override int projectiletype => ModContent.ProjectileType<EmeraldRoundP>();

        public override int bonusDmg => 8;

        public override int gem => ItemID.Emerald;

    }
    public class CrystalineEmeraldRounds : CrystalineGemRounds
    {
        public override string Texture => base.Texture + "Emerald";
        public override int projectiletype => ModContent.ProjectileType<CrystalineEmeraldRoundP>();

        public override int bonusDmg => 12;

        public override int gem => ModContent.ItemType<EmeraldRounds>();

    }

    public class TopazRounds : GemRounds
    {
        public override string Texture => base.Texture + "Topaz";
        public override int projectiletype => ModContent.ProjectileType<TopazRoundP>();

        public override int bonusDmg => 5;

        public override int gem => ItemID.Topaz;

    }

    public class CrystalineTopazRounds : CrystalineGemRounds
    {
        public override string Texture => base.Texture + "Topaz";
        public override int projectiletype => ModContent.ProjectileType<CrystalineTopazRoundP>();

        public override int bonusDmg => 9;

        public override int gem => ModContent.ItemType<TopazRounds>();

    }

    //Projectiles
    public abstract class GemRoundProj : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/GemRounds/";
        public abstract int ProjDebuff { get; }
        public abstract int Debufftime { get; }
        public abstract int Penetrations { get; }
        public abstract Vector3 RGB { get; }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = Penetrations;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 0;
            Projectile.timeLeft = 600;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, RGB);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (ProjDebuff != 0 && Debufftime != 0)
            {
                target.AddBuff(ProjDebuff, Debufftime);
            }
        }
    }
    public abstract class CrystalineGemRoundProj : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/GemRounds/";
        public abstract int ProjDebuff { get; }
        public abstract int Debufftime { get; }
        public abstract Vector3 RGB { get; }
        public abstract int BoomSize { get; }
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
            Projectile.timeLeft = 600;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, RGB);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);

            if (Projectile.timeLeft <=3)
            {
                Projectile.tileCollide = false;
                Projectile.penetrate = -1;
                Projectile.alpha = 255;
                Projectile.Resize(BoomSize, BoomSize);
                Projectile.velocity = Vector2.Zero;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (ProjDebuff != 0 && Debufftime != 0)
            {
                target.AddBuff(ProjDebuff, Debufftime);
            }
            if(Projectile.timeLeft > 3)
            {
                Projectile.timeLeft = 3;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.timeLeft > 3)
            {
                Projectile.timeLeft = 3;
            }
            return false;
        }
    }

    public class AmethystRoundP : GemRoundProj
    {
        public override string Texture => base.Texture + "Amethyst";

        public override int ProjDebuff => BuffID.Confused;

        public override int Debufftime => 180;

        public override int Penetrations => 1;

        public override Vector3 RGB => new Vector3(0.6f, 0.4f, 0.8f);
    }

    public class CrystalineAmethystRoundP : CrystalineGemRoundProj
    {
        public override string Texture => base.Texture + "Amethyst";

        public override int ProjDebuff => BuffID.Confused;

        public override int Debufftime => 360;

        public override int BoomSize => 75;

        public override Vector3 RGB => new Vector3(0.6f, 0.4f, 0.8f);
    }

    public class RubyRoundP : GemRoundProj
    {
        public override string Texture => base.Texture + "Ruby";

        public override int ProjDebuff => BuffID.OnFire;

        public override int Debufftime => 180;

        public override int Penetrations => 1;

        public override Vector3 RGB => new Vector3(0.87f, 0.01f, 0.38f);
    }

    public class CrystalineRubyRoundP : CrystalineGemRoundProj
    {
        public override string Texture => base.Texture + "Ruby";

        public override int ProjDebuff => BuffID.OnFire;

        public override int Debufftime => 360;

        public override int BoomSize => 75;
        public override Vector3 RGB => new Vector3(0.87f, 0.01f, 0.38f);
    }
    public class TopazRoundP : GemRoundProj
    {
        public override string Texture => base.Texture + "Topaz";

        public override int ProjDebuff => BuffID.Ichor;

        public override int Debufftime => 180;

        public override int Penetrations => 1;

        public override Vector3 RGB => new Vector3(0.81f, 0.51f, 0.27f);
    }

    public class CrystalineTopazRoundP : CrystalineGemRoundProj
    {
        public override string Texture => base.Texture + "Topaz";

        public override int ProjDebuff => BuffID.Ichor;

        public override int Debufftime => 360;

        public override int BoomSize => 75;

        public override Vector3 RGB => new Vector3(0.81f, 0.51f, 0.27f);
    }
    public class SapphireRoundP : GemRoundProj
    {
        public override string Texture => base.Texture + "Sapphire";

        public override int ProjDebuff => BuffID.Frostburn;

        public override int Debufftime => 180;

        public override int Penetrations => 1;

        public override Vector3 RGB => new Vector3(0.06f, 0.32f, 0.73f);
    }

    public class CrystalineSapphireRoundP : CrystalineGemRoundProj
    {
        public override string Texture => base.Texture + "Sapphire";

        public override int ProjDebuff => BuffID.Frostburn;

        public override int Debufftime => 360;

        public override int BoomSize => 75;

        public override Vector3 RGB => new Vector3(0.06f, 0.32f, 0.73f);
    }
    public class DiamondRoundP : GemRoundProj
    {
        public override string Texture => base.Texture + "Diamond";

        public override int ProjDebuff => 0;

        public override int Debufftime => 0;

        public override int Penetrations => 2;

        public override Vector3 RGB => new Vector3(0.97f, 0.99f, 0.93f);
    }

    public class CrystalineDiamondRoundP : CrystalineGemRoundProj
    {
        public override string Texture => base.Texture + "Diamond";

        public override int ProjDebuff => 0;

        public override int Debufftime => 0;

        public override int BoomSize => 125;

        public override Vector3 RGB => new Vector3(0.97f, 0.99f, 0.93f);
    }
    public class EmeraldRoundP : GemRoundProj
    {
        public override string Texture => base.Texture + "Emerald";

        public override int ProjDebuff => BuffID.Poisoned;

        public override int Debufftime => 180;

        public override int Penetrations => 1;

        public override Vector3 RGB => new Vector3(0.02f, 0.39f, 0.03f);
    }
    public class CrystalineEmeraldRoundP : CrystalineGemRoundProj
    {
        public override string Texture => base.Texture + "Emerald";

        public override int ProjDebuff => BuffID.Poisoned;

        public override int Debufftime => 360;

        public override int BoomSize => 75;

        public override Vector3 RGB => new Vector3(0.02f, 0.39f, 0.03f);
    }

}
