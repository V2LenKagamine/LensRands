using System;
using LensRands.Content.Buffs;
using LensRands.LensUtils;
using LensRands.Systems.ModSys;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Weapons
{
    public class PAIStaff : ModItem
    {

        public override string Texture => LensRands.AssetsPath + "Items/Weapons/ROGUS";
        public override void SetDefaults()
        {
            Item.damage = 250;
            Item.knockBack = 3f;
            Item.mana = 10; 
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.scale = 1.5f;
            Item.useStyle = ItemUseStyleID.HoldUp; 
            Item.value = Item.sellPrice(2,50);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = AudioSys.BADING;

           
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.buffType = ModContent.BuffType<PAIBuff>();
            Item.shoot = ModContent.ProjectileType<ROGUSUnit>();
            Item.value = Item.buyPrice(0, 2, 50, 0);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
            projectile.originalDamage = Item.damage;
            return false;
        }
    }

    public class ROGUSUnit : ModProjectile
    {

        private float ClaymoreTimer { get => Projectile.ai[0]; set => Projectile.ai[0] = value; }
        private readonly int ClaymoreRange = 125;
        public override string Texture => LensRands.AssetsPath + "Projectiles/ROGUS";
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true;

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.tileCollide = false; // Makes the minion go through tiles freely

            // These below are needed for a minion weapon
            Projectile.friendly = true; // Only controls if it deals damage to enemies on contact (more on that later)
            Projectile.minion = true; // Declares this as a minion (has many effects)
            Projectile.DamageType = DamageClass.Summon; // Declares the damage type (needed for it to deal damage)
            Projectile.minionSlots = 1f; // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
            Projectile.penetrate = -1; // Needed so the minion doesn't despawn on collision with enemies or tiles

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 4;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (!CheckActive(owner))
            {
                return;
            }

            GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
            Visuals();
        }
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(ModContent.BuffType<PAIBuff>());

                return false;
            }

            if (owner.HasBuff(ModContent.BuffType<PAIBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            return true;
        }

        private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        {
            Vector2 idlePosition = owner.Center;
            idlePosition.Y -= 48f;
            float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
            idlePosition.X += minionPositionOffsetX; // Go behind the player

            vectorToIdlePosition = idlePosition - Projectile.Center;
            distanceToIdlePosition = vectorToIdlePosition.Length();

            if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
            {
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }
            float overlapVelocity = 0.04f;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile other = Main.projectile[i];

                if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < other.position.X)
                    {
                        Projectile.velocity.X -= overlapVelocity;
                    }
                    else
                    {
                        Projectile.velocity.X += overlapVelocity;
                    }

                    if (Projectile.position.Y < other.position.Y)
                    {
                        Projectile.velocity.Y -= overlapVelocity;
                    }
                    else
                    {
                        Projectile.velocity.Y += overlapVelocity;
                    }
                }
            }
        }

        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            distanceFromTarget = 800f;
            targetCenter = Projectile.position;
            foundTarget = false;

            if (owner.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);

                if (between < 2000f)
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        bool closeThroughWall = between < 100f;

                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }
        }

        private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
        {
            float speed = 8f;
            float inertia = 20f;

            if(ClaymoreTimer > 1 )
            {
                ClaymoreTimer--;
                return;
            }
            else if(ClaymoreTimer == 1 )
            {
                Projectile.Resize(16, 16);
                Projectile.position = Main.player[Projectile.owner].Center;
                Projectile.alpha = 0;
                Projectile.friendly = false;
                ClaymoreTimer--;
                return;
            }

            if (foundTarget)
            {
               
                if (distanceFromTarget > 10f)
                {
                    if (Projectile.friendly)
                    {
                        Projectile.friendly = false;
                    }
                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                    Projectile.rotation = Projectile.velocity.ToRotation();
                }
                else if (ClaymoreTimer <= 0)
                {
                    Projectile.friendly = foundTarget;
                    Projectile.alpha = 255;
                    Projectile.Resize(ClaymoreRange,ClaymoreRange);
                    Projectile.velocity = Vector2.Zero;
                    LensVisualUtils.BombVisuals(Projectile.Center, ClaymoreRange,ClaymoreRange);
                    ClaymoreTimer = 3;
                }
            }
            else
            {
                if (distanceToIdlePosition > 600f)
                {
                    speed = 12f;
                    inertia = 60f;
                }
                else
                {
                    speed = 4f;
                    inertia = 80f;
                }

                if (distanceToIdlePosition > 20f)
                {
                    vectorToIdlePosition.Normalize();
                    vectorToIdlePosition *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                }
                else if (Projectile.velocity == Vector2.Zero)
                {
                    Projectile.velocity.X = -0.15f;
                    Projectile.velocity.Y = -0.05f;
                }
            }
        }

        private void Visuals()
        {

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(180f);

            int frameSpeed = 10;

            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }

            Lighting.AddLight(Projectile.Center, Color.Green.ToVector3() * 0.50f);
        }

    }

}
