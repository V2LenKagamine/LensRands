using LensRands.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Accessories
{
    public class StrongBone : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Accessories/StrongBone";
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Gray;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 3, 0, 0);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LensPlayer>().StrongBoneOn = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Bone,50)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }

    public class StrongBoneProj : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_21";


        public int HealAmount { get => (int)Projectile.ai[0]; set => Projectile.ai[0] = value; }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bone);
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 900;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.rotation += 0.1f;

            Projectile.velocity *= .925f;

            if (Projectile.velocity.Length() < 0.1)
            {
                Projectile.velocity = Vector2.Zero;
            }

            if (Projectile.velocity == Vector2.Zero && player.Hitbox.Intersects(Projectile.Hitbox) && HealAmount > 0)
            {
                player.GetModPlayer<LensPlayer>().AddHealth(HealAmount);
                Projectile.Kill();
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.position += Projectile.velocity;
            Projectile.velocity = Vector2.Zero;
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.GhostWhite;
        }
    }

}
