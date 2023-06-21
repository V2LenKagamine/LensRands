using LensRands.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Memes
{
    public class SCP3108 : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Misc/Nerf";
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.scale = 0.5f;
            Item.damage = 1;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.LightRed;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<NerfingGun>();
            Item.shootSpeed = 6f;
            Item.value = Item.buyPrice(0, 7, 50, 0);
        }
    }
    public class NerfingGun : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "Projectiles/Nerf";
        public override void SetDefaults()
        {
            Projectile.width = 4;               //The width of projectile hitbox
            Projectile.height = 4;              //The height of projectile hitbox
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 300;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.boss)
            {
                target.AddBuff(ModContent.BuffType<Nerfed>(), 10);
            }
        }
        public override void AI()
        {
            if (Projectile.timeLeft <= 280)
            {
                Projectile.velocity.Y += 0.05f;
            }
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}
