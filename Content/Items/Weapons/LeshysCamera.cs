using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Weapons
{
    public class LeshysCamera : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Misc/LeshyCamera";
        public override void SetDefaults()
        {
            Item.height = 16;
            Item.width = 16;
            Item.mana = 5;
            Item.damage = 1;
            Item.crit = 4;
            Item.DamageType = DamageClass.Magic;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.knockBack = 1;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<LeshysCameraProj>();
            Item.UseSound = SoundID.Item13;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Shadewood,12)
                .AddIngredient(ItemID.ShinePotion)
                .AddTile(TileID.DemonAltar)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.Ebonwood, 12)
                .AddIngredient(ItemID.ShinePotion)
                .AddTile(TileID.DemonAltar)
                .Register();
        }

    }
    public class LeshysCameraProj : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/LeshyCam";
        public override void SetDefaults()
        {
            Projectile.width = 224;
            Projectile.height = 112;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 20;
            Projectile.light = 0f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            if(Projectile.timeLeft == 20)
            {
                Projectile.Center = Main.MouseWorld;
                Projectile.velocity = Vector2.Zero;
            }
            Projectile.alpha += 12;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.immortal && !target.boss && target.life < (target.lifeMax * 0.1f))
            {
                target.StrikeInstantKill();
                if (Main.myPlayer == Projectile.owner) { SpawnCard(target.lifeMax, target.type); }

            }
            else if (!target.immortal && target.boss && target.life < (target.lifeMax * 0.025))
            {
                target.StrikeInstantKill();
                if (Main.myPlayer == Projectile.owner) { SpawnCard(target.lifeMax, target.type); }
            }
        }
        private void SpawnCard(int targetmaxlife,int targettype) // 14 to 8k
        {
            if (targettype == NPCID.MoonLordCore)
            {
                Item.NewItem(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ItemType<TheMoonCard>());
            }
            else
            {
                switch (targetmaxlife)
                {
                    case int x when x <= 250 && x >= 25:
                        {
                            Item.NewItem(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ItemType<CommonCard>());
                            break;
                        }
                    case int x when x > 250 && x <= 2500:
                        {
                            Item.NewItem(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ItemType<UncommonCard>());
                            break;
                        }
                    case int x when x > 2500 && x <= 10000:
                        {
                            Item.NewItem(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ItemType<RareCard>());
                            break;
                        }
                    case int x when x > 10000:
                        {
                            Item.NewItem(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ItemType<BossCard>());
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
    }
    public abstract class CameraCard : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Misc/LeshyCard";
        public abstract int CardValue { get; }
        public abstract int CardRarity { get; }
        public override void SetDefaults()
        {
            Item.height = Item.width = 16;
            Item.value = CardValue;
            Item.rare = CardRarity;
        }
    }
    public class CommonCard : CameraCard
    {
        public override int CardValue => Item.sellPrice(gold:1);

        public override int CardRarity => ItemRarityID.White;
    }
    public class UncommonCard : CameraCard
    {
        public override int CardValue => Item.sellPrice(gold: 10);

        public override int CardRarity => ItemRarityID.Green;
    }

    public class RareCard : CameraCard
    {
        public override int CardValue => Item.sellPrice(gold: 50);

        public override int CardRarity => ItemRarityID.Blue;
    }
    public class BossCard : CameraCard
    {
        public override int CardValue => Item.sellPrice(platinum:1 );

        public override int CardRarity => ItemRarityID.Red;
    }
    public class TheMoonCard : CameraCard
    {
        public override int CardValue => Item.sellPrice(platinum: 10);

        public override int CardRarity => ItemRarityID.Master;
    }
}
