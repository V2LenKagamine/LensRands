using System;
using System.Collections.Generic;
using LensRands.LensUtils;
using Microsoft.Xna.Framework;
using SteelSeries.GameSense;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Weapons
{
    public abstract class GemBase : ModItem 
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Gems/";
        public abstract int ShootsThis { get; }
        public abstract int GemID { get; }
        public override void SetDefaults()
        {
            Item.height = 16;
            Item.width = 16;
            Item.mana = 5;
            Item.damage = 15;
            Item.crit = 4;
            Item.DamageType = DamageClass.Magic;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.knockBack = 1;
            Item.shootSpeed = 8f;
            Item.shoot = ShootsThis;
            Item.UseSound = SoundID.Item13;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 0, 25, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(GemID)
                .AddIngredient(ItemID.FallenStar, 3)
                .Register();
        }
    }
    public class ManadDiamond : GemBase
    {
        public override string Texture => base.Texture + "Diamond";
        public override int ShootsThis => ModContent.ProjectileType<DiamondP>();
        public override int GemID => ItemID.Diamond;
    }
    public class ManadTopaz : GemBase
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.damage = 5;
        }
        public override string Texture => base.Texture + "Topaz";
        public override int ShootsThis => ModContent.ProjectileType<TopazP>();
        public override int GemID => ItemID.Topaz;
    }
    public class ManadRuby : GemBase
    {
        public override string Texture => base.Texture + "Ruby";
        public override int ShootsThis => ModContent.ProjectileType<RubyP>();
        public override int GemID => ItemID.Ruby;
    }
    public class ManadSapphire : GemBase
    {
        public override string Texture => base.Texture + "Sapphire";
        public override int ShootsThis => ModContent.ProjectileType<SapphireP>();
        public override int GemID => ItemID.Sapphire;
    }
    public class ManadEmerald : GemBase
    {
        public override string Texture => base.Texture + "Emerald";
        public override int ShootsThis => ModContent.ProjectileType<EmeraldP>();
        public override int GemID => ItemID.Emerald;
    }
    public class ManadAmethyst : GemBase
    {
        public override string Texture => base.Texture + "Amethyst";
        public override int ShootsThis => ModContent.ProjectileType<AmethystP>();
        public override int GemID => ItemID.Amethyst;
    }

    public abstract class GemProjectile : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/Gems/";
        public abstract int BuffType { get; }
        public abstract Vector3 RGB { get; }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 60;
            Projectile.light = 0f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            Projectile.alpha += 4;
            Lighting.AddLight(Projectile.Center, RGB);
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.velocity *= 0.98f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (BuffType != 0)
            {
                target.AddBuff(BuffType, 600);
            }
        }
    }
    public class DiamondP : GemProjectile
    {
        public override string Texture => base.Texture + "Diamond";
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.penetrate = 2;
        }
        public override int BuffType => 0;

        public override Vector3 RGB => new(1f,1f,1f);
    }
    public class TopazP : GemProjectile
    {

        public override string Texture => base.Texture + "Topaz";
        public override int BuffType => BuffID.Ichor;

        public override Vector3 RGB => new(1f, 1f, 0f);
    }
    public class RubyP : GemProjectile
    {
        public override string Texture => base.Texture + "Ruby";
        public override int BuffType => BuffID.OnFire;

        public override Vector3 RGB => new(1f, 0f, 0f);
    }
    public class EmeraldP : GemProjectile
    {
        public override string Texture => base.Texture + "Emerald";
        public override int BuffType => BuffID.Poisoned;

        public override Vector3 RGB => new(0f, 1f, 0f);
    }
    public class SapphireP : GemProjectile
    {
        public override string Texture => base.Texture + "Sapphire";
        public override int BuffType => BuffID.Frostburn;

        public override Vector3 RGB => new(0f, 0f, 1f);
    }
    public class AmethystP : GemProjectile
    {
        public override string Texture => base.Texture + "Amethyst";
        public override int BuffType => BuffID.Confused;

        public override Vector3 RGB => new(0.62f, 0.12f, 0.94f);
    }

    public class GemBoulder : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Gems/Boulder";
        public override void SetDefaults()
        {
            Item.height = 16;
            Item.width = 16;
            Item.mana = 20;
            Item.damage = 45;
            Item.crit = 4;
            Item.DamageType = DamageClass.Magic;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.knockBack = 6;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<GemBoulderP>();
            Item.UseSound = SoundID.Item13;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.value = Item.sellPrice(gold: 5);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ManadAmethyst>())
                .AddIngredient(ModContent.ItemType<ManadSapphire>())
                .AddIngredient(ModContent.ItemType<ManadRuby>())
                .AddIngredient(ModContent.ItemType<ManadTopaz>())
                .AddIngredient(ModContent.ItemType<ManadEmerald>())
                .AddIngredient(ModContent.ItemType<ManadDiamond>())
                .AddIngredient(ItemID.Amber)
                .AddIngredient(ItemID.FallenStar, 3)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if(player.altFunctionUse == 2)
            {
                type = ModContent.ProjectileType<RollingGemBoulderP>();
                damage = (int)(damage * 1.5f);
                velocity *= 0.25f;
            }
        }
    }

    public class GemBoulderP : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Gems/Boulder";
        public int MaxTime = 120;
        public List<int> TheGems = new()
        {
            ModContent.ProjectileType<AmethystP>(),
            ModContent.ProjectileType<SapphireP>(),
            ModContent.ProjectileType<RubyP>(),
            ModContent.ProjectileType<TopazP>(),
            ModContent.ProjectileType<EmeraldP>(),
            ModContent.ProjectileType<DiamondP>()
        };
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = MaxTime;
            Projectile.light = 0f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            DrawOffsetX = -4;
            DrawOriginOffsetY = -4;
        }
        public override void AI()
        {
            float colorratio = MathHelper.Clamp(Projectile.timeLeft /(float)MaxTime, 0f, 1f);
            Vector3 colortouse;
            switch (colorratio) {
                case float x when x > 0.66f:
                    {
                        colortouse = new(colorratio*2,0f,0f);
                        break;
                    }
                case float x when x >= 0.33f && x <= 0.66f:
                    {
                        colortouse = new(colorratio*1.5f, colorratio*1.5f, 0f);
                        break;
                    }
                case float x when x < 0.33f:
                    {
                        colortouse = new( colorratio, colorratio,colorratio);
                        break;
                    }
                default:
                    {
                        colortouse = new(0f, 0f, 0f);
                        break;
                    }
            }
            Lighting.AddLight(Projectile.Center,colortouse);
            LensUtil.ProjectileROTATE(Projectile, 12.5f);
            Projectile.velocity *= 0.98f;
        }
        public override void Kill(int timeLeft)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                float spread = MathHelper.ToRadians(180f);
                Vector2 Basevector = Vector2.Normalize(new(Main.rand.NextFloat(-1f,1f), Main.rand.NextFloat(-1f,1f)));
                for (int i = 0; i < TheGems.Count; i++)
                {
                    Vector2 VelocityMini = Basevector.RotatedBy(MathHelper.Lerp(-spread, spread, i / (float)(TheGems.Count))) * 6;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, VelocityMini, TheGems[i], Projectile.damage / 6, Projectile.knockBack);
                }
            }
        }
    }
    public class RollingGemBoulderP : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Gems/Boulder";
        public int MaxTime = 360;
        public List<int> TheGems = new()
        {
            ModContent.ProjectileType<AmethystP>(),
            ModContent.ProjectileType<SapphireP>(),
            ModContent.ProjectileType<RubyP>(),
            ModContent.ProjectileType<TopazP>(),
            ModContent.ProjectileType<EmeraldP>(),
            ModContent.ProjectileType<DiamondP>()
        };
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 3;
            Projectile.timeLeft = MaxTime;
            Projectile.light = 0f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            DrawOffsetX = -4;
            DrawOriginOffsetY = -4;
        }
        public override void AI()
        {
            float colorratio = MathHelper.Clamp(Projectile.timeLeft / (float)MaxTime, 0f, 1f);
            Vector3 colortouse;
            switch (colorratio)
            {
                case float x when x > 0.66f:
                    {
                        colortouse = new(colorratio * 2, 0f, 0f);
                        break;
                    }
                case float x when x >= 0.33f && x <= 0.66f:
                    {
                        colortouse = new(colorratio * 1.5f, colorratio * 1.5f, 0f);
                        break;
                    }
                case float x when x < 0.33f:
                    {
                        colortouse = new(colorratio, colorratio, colorratio);
                        break;
                    }
                default:
                    {
                        colortouse = new(0f, 0f, 0f);
                        break;
                    }
            }
            Lighting.AddLight(Projectile.Center, colortouse);
            LensUtil.ProjectileROTATE(Projectile,Projectile.velocity.X > 0 ? 1.5f * Projectile.velocity.X : 1.5f * -Projectile.velocity.X);
            Projectile.velocity.Y += 0.25f;
            if (Math.Abs(Projectile.velocity.X) < 8f)
            {
                Projectile.velocity.X += Projectile.velocity.X > 0 ? 0.05f : -0.05f;
            }
        }
        public override void Kill(int timeLeft)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                float spread = MathHelper.ToRadians(180f);
                Vector2 Basevector = Vector2.Normalize(new(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)));
                for (int i = 0; i < TheGems.Count; i++)
                {
                    Vector2 VelocityMini = Basevector.RotatedBy(MathHelper.Lerp(-spread, spread, i / (float)(TheGems.Count))) * 6;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, VelocityMini, TheGems[i], Projectile.damage / 6, Projectile.knockBack);
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            LensUtil.ProjectileBounce(Projectile, oldVelocity, .125f, .125f);
            return false;
        }
    }

    public class GemBoulderHM : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Gems/Boulder";
        public override void SetDefaults()
        {
            Item.height = 16;
            Item.width = 16;
            Item.mana = 20;
            Item.damage = 125;
            Item.crit = 4;
            Item.DamageType = DamageClass.Magic;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.knockBack = 6;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<GemBoulderHMP>();
            Item.UseSound = SoundID.Item13;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.value = Item.sellPrice(gold: 5);
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<GemBoulder>())
                .AddIngredient(ItemID.SoulofLight,10)
                .AddIngredient(ItemID.SoulofNight,10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                type = ModContent.ProjectileType<RollingGemBoulderHMP>();
                damage = (int)(damage * 1.5f);
                velocity *= 0.75f;
            }
        }
    }
    public class GemBoulderHMP : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Gems/Boulder";
        public int MaxTime = 240;
        public List<int> TheGems = new()
        {
            ModContent.ProjectileType<AmethystP>(),
            ModContent.ProjectileType<SapphireP>(),
            ModContent.ProjectileType<RubyP>(),
            ModContent.ProjectileType<TopazP>(),
            ModContent.ProjectileType<EmeraldP>(),
            ModContent.ProjectileType<DiamondP>()
        };
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 5;
            Projectile.timeLeft = MaxTime;
            Projectile.light = 0f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            DrawOffsetX = -4;
            DrawOriginOffsetY = -4;
        }
        public override void AI()
        {
            float colorratio = MathHelper.Clamp(Projectile.timeLeft / (float)MaxTime, 0f, 1f);
            Vector3 colortouse;
            switch (colorratio)
            {
                case float x when x > 0.66f:
                    {
                        colortouse = new(colorratio * 2, 0f, 0f);
                        break;
                    }
                case float x when x >= 0.33f && x <= 0.66f:
                    {
                        colortouse = new(colorratio * 1.5f, colorratio * 1.5f, 0f);
                        break;
                    }
                case float x when x < 0.33f:
                    {
                        colortouse = new(colorratio, colorratio, colorratio);
                        break;
                    }
                default:
                    {
                        colortouse = new(0f, 0f, 0f);
                        break;
                    }
            }
            Lighting.AddLight(Projectile.Center, colortouse);
            LensUtil.ProjectileROTATE(Projectile, 12.5f);
            Projectile.velocity *= 0.99f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate > 0)
            {
                Projectile.penetrate--;
                LensUtil.ProjectileBounce(Projectile, oldVelocity);
                return false;
            }
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SpawnGemlets();
        }

        public override void Kill(int timeLeft)
        {
            SpawnGemlets();
        }

        private void SpawnGemlets()
        {
            if (Projectile.owner == Main.myPlayer)
            {
                float spread = MathHelper.ToRadians(180f);
                Vector2 Basevector = Vector2.Normalize(new(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)));
                for (int i = 0; i < TheGems.Count; i++)
                {
                    Vector2 VelocityMini = Basevector.RotatedBy(MathHelper.Lerp(-spread, spread, i / (float)(TheGems.Count))) * 6;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, VelocityMini, TheGems[i], Projectile.damage / 6, Projectile.knockBack);
                }
            }
        }

    }
    public class RollingGemBoulderHMP : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/Gems/Boulder";
        public int MaxTime = 360;
        public List<int> TheGems = new()
        {
            ModContent.ProjectileType<AmethystP>(),
            ModContent.ProjectileType<SapphireP>(),
            ModContent.ProjectileType<RubyP>(),
            ModContent.ProjectileType<TopazP>(),
            ModContent.ProjectileType<EmeraldP>(),
            ModContent.ProjectileType<DiamondP>()
        };
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 7;
            Projectile.timeLeft = MaxTime;
            Projectile.light = 0f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            DrawOffsetX = -4;
            DrawOriginOffsetY = -4;
        }
        public override void AI()
        {
            float colorratio = MathHelper.Clamp(Projectile.timeLeft / (float)MaxTime, 0f, 1f);
            Vector3 colortouse;
            switch (colorratio)
            {
                case float x when x > 0.66f:
                    {
                        colortouse = new(colorratio * 2, 0f, 0f);
                        break;
                    }
                case float x when x >= 0.33f && x <= 0.66f:
                    {
                        colortouse = new(colorratio * 1.5f, colorratio * 1.5f, 0f);
                        break;
                    }
                case float x when x < 0.33f:
                    {
                        colortouse = new(colorratio, colorratio, colorratio);
                        break;
                    }
                default:
                    {
                        colortouse = new(0f, 0f, 0f);
                        break;
                    }
            }
            Lighting.AddLight(Projectile.Center, colortouse);
            LensUtil.ProjectileROTATE(Projectile, Projectile.velocity.X > 0 ? 1.5f * Projectile.velocity.X : 1.5f * -Projectile.velocity.X);
            Projectile.velocity.Y += 0.15f;
        }
        public override void Kill(int timeLeft)
        {
            SpawnGemlets();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if(Projectile.penetrate > 0)
            {
                Projectile.penetrate--;
                SpawnGemlets();
                LensUtil.ProjectileBounce(Projectile, oldVelocity, 0.95f, .95f);
                return false;
            }
            return true;
        }
        private void SpawnGemlets()
        {
            if (Projectile.owner == Main.myPlayer)
            {
                float spread = MathHelper.ToRadians(180f);
                Vector2 Basevector = Vector2.Normalize(new(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)));
                for (int i = 0; i < TheGems.Count; i++)
                {
                    Vector2 VelocityMini = Basevector.RotatedBy(MathHelper.Lerp(-spread, spread, i / (float)(TheGems.Count))) * 6;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, VelocityMini, TheGems[i], Projectile.damage / 6, Projectile.knockBack);
                }
            }
        }
    }
}
