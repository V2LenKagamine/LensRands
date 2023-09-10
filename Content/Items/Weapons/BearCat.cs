using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace LensRands.Content.Items.Weapons
{
    public class BearCat : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Weapons/AsheRifle";
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.consumeAmmoOnLastShotOnly = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ProjectileID.GrenadeI;
            Item.shootSpeed = 12f;
            Item.UseSound = SoundID.Item61;
            Item.useAmmo = AmmoID.Rocket;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(gold: 3);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player Player)
        {
            if (Player.altFunctionUse == 2)
            {
                Item.useTime = 6;
                Item.useAnimation = 18;
                Item.UseSound = SoundID.Item61;
            }
            else
            {
                Item.useTime = 24;
                Item.useAnimation = 24;
                Item.UseSound = SoundID.Item61;
            }

            return base.CanUseItem(Player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float seperation = MathHelper.ToRadians(2.5f);
            for (int i = 0; i < 3; i++)
            {
                Vector2 newspeed = velocity.RotatedBy(MathHelper.Lerp(-seperation, seperation, i));
                Projectile.NewProjectile(source, position, newspeed, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}
