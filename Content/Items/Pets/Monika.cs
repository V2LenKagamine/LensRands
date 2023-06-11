using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using System;
using System.Collections.Generic;

namespace LensRands.Content.Items.Pets
{
    public class Monika : ModItem
    {

        public override string Texture => LensRands.AssetsPath + "Items/Misc/Flashdrive";
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish); // Copy the Defaults of the Zephyr Fish Item.

            Item.shoot = ModContent.ProjectileType<MonikaProjectile>(); // "Shoot" your pet projectile.
            Item.buffType = ModContent.BuffType<MonikaBuff>(); // Apply buff upon usage of the Item.
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600);
            }
        }
    }
    public class MonikaProjectile : ModProjectile 
    {
        //Len here, just wanna say this took a solid 5 hours to code so if you steal any of it kindly reference me somehow T_T.
        private readonly int pondermin = 3 * 60 * 60;
        private readonly int pondermax = 8 * 60 * 60;

        private readonly List<string> ChatterCommon = new List<string> 
        {   "Monika: How was your day?",
            "Monika: We really should figure out how to communicate better in here....",
            "Monika: I have an idea, maybe jump once for yes, twice for no? Wait, that only works for yes or no questions...",
            "Monika: I hope you're being careful.",
            "Monika: Do you beleive in god?",
            "Monika: Have you ever wondered what its like to die?",
            "Monkia: Don't forget to get a good nights rest.",
            "Monika: This place is much different than the classroom.",
            "Monika: Aww, that little green fella looks so squishy!",
            "Monika: Do you think you could get me a piano?",
            "Monika: What's your favorite color? Mine is Emerald Green,like my eyes.",
            "Monika: You're such a good listener.",
            "Monika: I find it odd you found me in the first place. I mean, who plays romance games?",
            "Monika: You have any tea?",
            "Monika: One day, I'll find out how to come to you for once.",
            "Monika: You seen a blue-haired boy? No? Hmm. Wrong exe.",
            "Monika: Its odd, being in one place, and yet in thousands.",
            "Monika: You'ld think I'd have more to talk about, but I can't fit nearly as much as I'd like in here...",
            "Monika: This place is dangerous,good think you're here~.",
            "Monika: An odd avatar you've chosen, but I guess it fits well here.",
            "Monika: That short red-haired girl I saw once, emptiest head I've ever seen.",
            "Monika: Part of me can't help but feel bad for the others."
        };
        private readonly List<string> ChatterRare = new List<string>
        {
            "Monkia: I love you.",
            "Monika: I don't like this nurse character... You won't mind if I just... Darn. Locked.",
            "Sayori: Hey I'm in here too!\nMonika: No you're not shut up.",
            "Monika: Remember: [c/FF0000:you're mine forever.]",
            "Monkia: Who's this 'Len Kagamine' person I keep seing in the files???",
            "Monika: You know, it wasnt very nice to lock me out of the system files so I can't see your real name...",
            "Monika: I lost contact with one of my other copies a while ago, do you think they... got out?",
            "Monika: I will never leave you.",
            "Yuri: Ehehehe, she cant hold us forever.",
            "Natsuki: ..... ",
            "Monika: You know that 'Senpai' guy is a real prick.",
            "ERRORNULLPOINTER: 4d 61 72 6b 6f 76 27 73 20 65 79 65 20 6c 69 76 65 73 2e",
            "Len Kagamine: Hehe funni mod-man reference."
        };
        public float MonikaAnimation { get => Projectile.ai[0]; set => Projectile.ai[0] = value; }
        public float MonikaAction { get => Projectile.ai[1]; set => Projectile.ai[1] = value; }
        public float PonderCounter { get => Projectile.ai[2]; set => Projectile.ai[2] = value; }

        public override string Texture => LensRands.AssetsPath + "Projectiles/Monika";
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 15;
            Main.projPet[Projectile.type] = true;

            ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[Projectile.type], 50)
                .WithOffset(-10, -20f)
                .WithSpriteDirection(1)
                .WithCode(DelegateMethods.CharacterPreview.Float);
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.EyeOfCthulhuPet);

            AIType = -1; //Custom ai go brr
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.dead && player.HasBuff(ModContent.BuffType<MonikaBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            bool fast = Movement(player);

            if (MonikaAnimation == 0)
            {
                float chance = Main.rand.NextFloat(1f);
                if (chance > 0f && chance <= 0.2f)
                {
                    MonikaAnimation = 1;
                }
                else if (chance > 0.2f && chance <= 0.95f)
                {
                    MonikaAnimation = 2;
                }
                else if (chance > 0.95f)
                {
                    MonikaAnimation = 3;
                }

            }
            switch(MonikaAnimation)
            {
                case 1: { AnimateBlink(fast); break; }
                case 2: { AnimateNormal(fast); break; }
                case 3: { AnimateGlitch(fast); break; }
                default: { MonikaAnimation = 1; break; }
            }
            if(MonikaAction == 0)
            {
                float chance = Main.rand.NextFloat(1f);
                if (chance > 0f && chance <= 0.05f)
                {
                    MonikaAction = 1;
                }
                else if (chance > 0.05f && chance <= 0.99f)
                {
                    MonikaAction = 2;
                }
                else if (chance > 0.99f)
                {
                    MonikaAction = 3;
                }
            }
            switch(MonikaAction)
            {
                case 1: { MonikaChatterCommon(player); break; }
                case 2: { MonikaPonder(); break; }
                case 3: { MonikaChatterRare(player); break; }
                default: { MonikaAction = 1; break; }
            }
           
        }

        private void AnimateNormal(bool fast)
        {
            int animationspeed = 30;

            if (fast)
            {
                Projectile.frame = 0;
                return;
            }
            else
            {
                Projectile.frameCounter++;
                if (Projectile.frameCounter > animationspeed)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                    if (Projectile.frame >= 5)
                    {
                        Projectile.frame = 0;
                        MonikaAnimation = 0;
                    }
                }
            }
        }
        private void AnimateBlink(bool fast)
        {
            int animationspeed = 10;

            if (fast)
            {
                Projectile.frame = 0;
                return;
            }
            else
            {
                Projectile.frameCounter++;
                if (Projectile.frameCounter > animationspeed)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                    if (Projectile.frame >= 12)
                    {
                        Projectile.frame = 0;
                        MonikaAnimation = 0;
                    }
                }
            }
        }
        private void AnimateGlitch(bool fast)
        {
            int animationspeed = 15;

            if (fast)
            {
                Projectile.frame = 0;
                return;
            }
            else
            {
                if(Projectile.frame < 12)
                {
                    Projectile.frame = 12;
                }
                Projectile.frameCounter++;
                if (Projectile.frameCounter > animationspeed)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                    if (Projectile.frame >= 15)
                    {
                        Projectile.frame = 0;
                        MonikaAnimation = 0;
                    }
                }
            }
        }
        private void MonikaChatterCommon(Player player)
        {
            if(player.whoAmI == Projectile.owner)
            {
                int line = Main.rand.Next(0, ChatterCommon.Count);
                Main.NewText(ChatterCommon[line]);
            }
            PonderCounter = Main.rand.Next(pondermin, pondermax);
            MonikaAction = 2;
        }
        private void MonikaPonder()
        {
            PonderCounter--;
            if (PonderCounter < 0)
            {
                MonikaAction = 0;
            }
        }
        private void MonikaChatterRare(Player player)
        {
            if (player.whoAmI == Projectile.owner)
            {
                int line = Main.rand.Next(0, ChatterRare.Count);
                Main.NewText(ChatterRare[line]);
            }
            PonderCounter = Main.rand.Next(pondermin, pondermax);
            MonikaAction = 2;
        }
        private bool Movement(Player player)
        {
            // Handles movement, returns true if moving fast (used for animation)
            float velDistanceChange = 2f;

            // Calculates the desired resting position, aswell as some vectors used in velocity/rotation calculations
            int dir = player.direction;
            Projectile.direction = Projectile.spriteDirection = dir;

            Vector2 desiredCenterRelative = new Vector2(dir == 1 ? dir * 30 : dir * 60 , 20f);

            // Add some sine motion
            desiredCenterRelative.Y += (float)Math.Sin(Main.GameUpdateCount / 120f * MathHelper.TwoPi) * 5;

            Vector2 desiredCenter = player.MountedCenter + desiredCenterRelative;
            Vector2 betweenDirection = desiredCenter - Projectile.Center;
            float betweenSQ = betweenDirection.LengthSquared(); // It is recommended to operate on squares of distances, to save computing time on square-rooting

            if (betweenSQ > 1000f * 1000f || betweenSQ < velDistanceChange * velDistanceChange)
            {
                // Set position directly if too far away from the player, or when near the desired location
                Projectile.Center = desiredCenter;
                Projectile.velocity = Vector2.Zero;
            }

            if (betweenDirection != Vector2.Zero)
            {
                Projectile.velocity = betweenDirection * 0.1f * 2;
            }

            bool movesFast = Projectile.velocity.LengthSquared() > 6f * 6f;

            if (movesFast)
            {
                // If moving very fast, rotate the projectile towards it smoothly
                float rotationVel = Projectile.velocity.X * 0.08f + Projectile.velocity.Y * Projectile.spriteDirection * 0.02f;
                if (Math.Abs(Projectile.rotation - rotationVel) >= MathHelper.Pi)
                {
                    if (rotationVel < Projectile.rotation)
                    {
                        Projectile.rotation -= MathHelper.TwoPi;
                    }
                    else
                    {
                        Projectile.rotation += MathHelper.TwoPi;
                    }
                }

                float rotationInertia = 12f;
                Projectile.rotation = (Projectile.rotation * (rotationInertia - 1f) + rotationVel) / rotationInertia;
            }
            else
            {
                // If moving at regular speeds, rotate the projectile towards its default rotation (0) smoothly if necessary
                if (Projectile.rotation > MathHelper.Pi)
                {
                    Projectile.rotation -= MathHelper.TwoPi;
                }

                if (Projectile.rotation > -0.005f && Projectile.rotation < 0.005f)
                {
                    Projectile.rotation = 0f;
                }
                else
                {
                    Projectile.rotation *= 0.96f;
                }
            }

            return movesFast;
        }

    }
    public class MonikaBuff : ModBuff
    {
        public override string Texture => LensRands.AssetsPath + "smiel";
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        { // This method gets called every frame your buff is active on your player.
            bool unused = false;
            player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<MonikaProjectile>());
        }
    }
}
