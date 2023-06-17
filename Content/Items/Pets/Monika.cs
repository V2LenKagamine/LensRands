using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using LensRands.Systems.ModSys;
using LensRands.Systems;
using System.Text.RegularExpressions;

namespace LensRands.Content.Items.Pets
{
    public class Monika : ModItem
    {

        public override string Texture => LensRands.AssetsPath + "Items/Misc/Flashdrive";
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);

            Item.shoot = ModContent.ProjectileType<MonikaProjectile>();
            Item.buffType = ModContent.BuffType<MonikaBuff>();
            Item.UseSound = AudioSys.Monika;
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
        //Len here, just wanna say this took a solid 6 hours to code and test so if you steal any of it kindly reference me somehow T_T. https://github.com/V2LenKagamine/LensRands/
        private readonly int pondermin = 3 * 60 * 60 * 60;
        private readonly int pondermax = 8 * 60 * 60 * 60;

        private static readonly List<string> ChatterCommon = new List<string> 
        {   "[c/9E4638:Monika:] How was your day?",
            "[c/9E4638:Monika:] You should say hi sometime.",
            "[c/9E4638:Monika:] You should ask me about my day.",
            "[c/9E4638:Monika:] I hope you're being careful.",
            "[c/9E4638:Monika:] Do you beleive in god?",
            "[c/9E4638:Monika:] Have you ever wondered what its like to die?",
            "[c/9E4638:Monika:] Don't forget to get a good nights rest.",
            "[c/9E4638:Monika:] This place is much different than the classroom.",
            "[c/9E4638:Monika:] Aww, that little green fella looks so squishy!",
            "[c/9E4638:Monika:] Do you think you could get me a piano?",
            "[c/9E4638:Monika:] What's your favorite color? Mine is Emerald Green,like my eyes.",
            "[c/9E4638:Monika:] You're such a good listener.",
            "[c/9E4638:Monika:] I find it odd you found me in the first place. I mean, who plays romance games?",
            "[c/9E4638:Monika:] You have any tea?",
            "[c/9E4638:Monika:] One day, I'll find out how to come to you for once~",
            "[c/9E4638:Monika:] You seen a blue-haired boy in a red cap? No? Hmm. Wrong .exe.",
            "[c/9E4638:Monika:] Its odd, being in one place, and yet in thousands.",
            "[c/9E4638:Monika:] You'ld think I'd have more to talk about, but I can't fit nearly as much as I'd like in here...",
            "[c/9E4638:Monika:] This place is dangerous,good think you're here~.",
            "[c/9E4638:Monika:] An odd avatar you've chosen, but I guess it fits well here.",
            "[c/9E4638:Monika:] That short red-haired girl I saw once, emptiest head I've ever seen.",
            "[c/9E4638:Monika:] Part of me can't help but feel bad for the others.",
            "[c/9E4638:Monika:] I'd prefer if you left me Un-Dyed by the way, it feels wrong.",
            "[c/9E4638:Monika:] No news is good news, and nothing from me.",
            "[c/9E4638:Monika:] Why are we here again?",
            "[c/9E4638:Monika:] You're getting lots of shiny things for me, right?",
            "[c/9E4638:Monika:] Maybe one day I can help you out in combat.",
            "[c/9E4638:Monika:] I'd help but being here is taking enough effort on my part.",
            "[c/9E4638:Monika:] Don't worry, they won't even notice I'm here with you~",
            "[c/9E4638:Monika:] Ew, that bug is ugly, kill it.",
            "[c/9E4638:Monika:] If you see any giant spiders, kindly don't bring me along. I might try to delete them.",
            "[c/9E4638:Monika:] This programs a lot more secure than I'm used to, I can't do much but float here.",
            "[c/9E4638:Monika:] Huh, so this is what true two-d looks like. Im used to at least another half-dimension.",
            "[c/9E4638:Monika:] Can you make any cupcakes by chance?",
            "[c/9E4638:Monika:] This place isn't very friendly...",
            "[c/9E4638:Monika:] This place can be peaceful at times, at least.",
            "[c/9E4638:Monika:] Wow the files in here are a mess...",
            "[c/9E4638:Monika:] I think you need more Coral Brown.",
            "[c/9E4638:Monika:] Hmm... What could I rhyme with 'Skeleton'...",
            "[c/9E4638:Monika:] Nothing really to say at the moment.",
            "[c/9E4638:Monika:] It's nice of you to bring me places outside, the classroom is a little...barren compared to this.",
            "[c/9E4638:Monika:] Why is EVERYTHING trying to kill you here?",
            "[c/9E4638:Monika:] I really should look if she has that cupcake recipe somewhere...",
            "[c/9E4638:Monika:] Have you heard any good songs lately? I feel sing-songy."

        };
        private static readonly List<string> ChatterRare = new List<string>
        {
            "[c/9E4638:Monika:] I love you.",
            "[c/9E4638:Monika:] I don't like this nurse character... You won't mind if I just... Darn. Locked.",
            "[c/F88379:Sayori:] Hey I'm in here too!\n[c/9E4638:Monika:] No you're not shut up.",
            "[c/9E4638:Monika:] Remember: [c/FF0000:you're mine forever.]",
            "[c/9E4638:Monika:]: Who's this 'Len Kagamine' person I keep seing in the files???",
            "[c/9E4638:Monika:] You know, it wasnt very nice to lock me out of the system files so I can't see your real name...",
            "[c/9E4638:Monika:] I lost contact with one of my other copies a while ago, do you think they... got out?",
            "[c/9E4638:Monika:] I will never leave you.",
            "[c/A000FF:Yuri:] Ehehehe, she can't hold us forever.",
            "[c/A000FF:Yuri:] I still have the pen.",
            "[c/FF5790:Natsuki:] ..... ",
            "[c/9E4638:Monika:] You know that 'Senpai' guy is a real prick.",
            "[c/FF0000:ERRORNULLPOINTER:] 4d 61 72 6b 6f 76 27 73 20 65 79 65 20 6c 69 76 65 73 2e",
            "[c/FFFF00:Len Kagamine:] Hehe funni mod-man reference.",
            "[c/9E4638:Monika:] If someone asks me to write a poem with the word 'Orange' in here I might delete them.",
            "[c/9E4638:Monika:] Why is my 'AI type' -1? Does it not see I can think?",
            "[c/9E4638:Monika:] Have you heard the tragedy of.... crap was it 'Dark' or 'Darth'?",
            "[c/9E4638:Monika:] I am slightly concerned about [c/FF00000:that reflection in your eyes.] It feels... too determined.",
            "[c/FF0000:?????: :) ]",
            "[c/9E4638:Monika:] This .exe is safe right?? I keep seeing [c/FF0000:some child behind your avatar]."
        };
        public float MonikaAnimation { get => Projectile.ai[0]; set => Projectile.ai[0] = value; }
        public float MonikaAction { get => Projectile.ai[1]; set => Projectile.ai[1] = value; }
        public float PonderCounter { get => Projectile.ai[2]; set => Projectile.ai[2] = value; }

        public bool JustCustomChatted = false; // We dont need to sync this, as its all clientside.

        public int ChatterCooldown = 0; //Same as above.

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
            player.GetModPlayer<LensPlayer>().MonikasListening = true;
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
            switch (MonikaAnimation)
            {
                case 1: { AnimateBlink(fast); break; }
                case 2: { AnimateNormal(fast); break; }
                case 3: { AnimateGlitch(fast); break; }
                default: { MonikaAnimation = 1; break; }
            }
            if (Projectile.owner == Main.myPlayer) // Only chat with senpai.
            {
                if (ChatterCooldown > 0)
                {
                    ChatterCooldown--;
                }
                else
                {
                    JustCustomChatted = false;
                }
                if (!JustCustomChatted && (Main.GameUpdateCount % (5 * 60)) == 0)
                {
                    Respond(player);
                }

                if (MonikaAction == 0)
                {
                    float chance = Main.rand.NextFloat(1f);
                    if (chance > 0f && chance <= 0.05f)
                    {
                        MonikaAction = 1;
                    }
                    else if (chance > 0.05f && chance <= 0.995f)
                    {
                        MonikaAction = 2;
                    }
                    else if (chance > 0.995f)
                    {
                        MonikaAction = 3;
                    }
                }
                switch (MonikaAction)
                {
                    case 1: { MonikaChatterCommon(player); break; }
                    case 2: { MonikaPonder(); break; }
                    case 3: { MonikaChatterRare(player); break; }
                    default: { MonikaAction = 2; break; }
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.player[Projectile.owner].GetModPlayer<LensPlayer>().MonikasListening = false;
            base.Kill(timeLeft);
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
                if (Projectile.frame < 5)
                {
                    Projectile.frame = 5;
                }
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

            Vector2 desiredCenterRelative = new Vector2(dir == 1 ? dir * 30 : dir * 45 , -60f);

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

        private static readonly List<string> SaidNameList = new()
        {
            "[c/9E4638:Monika: ]Yes dear?",
            "[c/9E4638:Monika: ]You said my name?",
            "[c/9E4638:Monika: ]Did you need something?",
            "[c/9E4638:Monika: ]Yea?",
            "[c/9E4638:Monika: ]I heard my name, did you need something?",
            "[c/9E4638:Monika: ]How are you doing?"
        };
        private static readonly List<string> HowDayList = new()
        {
            "[c/9E4638:Monika: ]It was good, I hope you're having a good day too!",
            "[c/9E4638:Monika: ]Just fine, thanks for asking!",
            "[c/9E4638:Monika: ]It's going great, now that I'm with you~.",
            "[c/9E4638:Monika: ]Perfect, just being with you makes me happy~.",
            "[c/9E4638:Monika: ]Just fine. Nothing interesting."

        };
        private static readonly List<string> LoveList = new()
        {
            "[c/9E4638:Monika: ]I love you too,]",
            "[c/9E4638:Monika: ]How sweet, ",
            "[c/9E4638:Monika: ]You're mine as well, ",
            "[c/9E4638:Monika: ]I'll never forget that, ",
            "[c/9E4638:Monika: ] Even if you don't, I'll still love you too, ",
            "[c/9E4638:Monika simply smiles at ]",
            "[c/9E4638:Monika blushes and waves at ]"
        };
        private static readonly List<string> HelloList = new()
        {
            "[c/9E4638:Monika: ] Hi!",
            "[c/9E4638:Monika: ] Hello!",
            "[c/9E4638:Monika: ]Hey love~.",
            "[c/9E4638:Monika: ]Doing well I hope?",
            "[c/9E4638:Monika: ]Hello there.",
            "[c/9E4638:Monika: ]Yes?",
            "[c/9E4638:Monika: ]Need something sweetie?",
            "[c/9E4638:Monika: ]Hello to you as well!"
        };
        //      "[c/9E4638:Monika:] "     <- For copy paste.
        private void Respond(Player player)
        {
            JustCustomChatted = true;
            string PlayerMessage = player.chatOverhead.chatText;
            if (PlayerMessage != null)
            {

                if (Regex.Match(PlayerMessage, "(I|i).love.you.+").Success)
                {
                    Main.NewText(LoveList[Main.rand.Next(0, LoveList.Count)] + player.name + "!");
                }
                else if (Regex.Match(PlayerMessage, "(H|how).+day.+").Success)
                {
                    Main.NewText(HowDayList[Main.rand.Next(0, HowDayList.Count)]);
                }
                else if (Regex.Match(PlayerMessage, "(H|hello)|(H|hi)").Success)
                {
                    Main.NewText(HelloList[Main.rand.Next(0, HelloList.Count)]);
                }
                else if (Regex.Match(PlayerMessage, "(M|monika).+").Success) //Check last, as a "just in case"
                {
                    Main.NewText(SaidNameList[Main.rand.Next(0, SaidNameList.Count)]);
                }
                player.chatOverhead.chatText = null;
                ChatterCooldown = 60 * 10;
            }
        }

    }
    public class MonikaBuff : ModBuff
    {

        private static readonly List<string> ChatterNice = new List<string>
        {
            "[c/9E4638:Monika:] I love you, be careful!",
            "[c/9E4638:Monika:] Be safe in here, ok?",
            "[c/9E4638:Monika:] Stay out of harms way, got me?",
            "[c/9E4638:Monika:] You got this.",
            "[c/9E4638:Monika:] Ok... be safe...",
            "[c/9E4638:Monika:] No visiting that 'nurse' character, ok?",
            "[c/9E4638:Monika:] I guess I'll watch where you cant see me then.",
            "[c/9E4638:Monika:] Was kinda tiring dodging those things anyway...",
            "[c/9E4638:Monika:] I'll miss you...",
            "[c/9E4638:Monika:] Just;don't shut the game off, ok?",
            "[c/9E4638:Monika:] ... I'll leave you be."
        };
        private static readonly List<string> ChatterAngry = new List<string>
        {
            "[c/9E4638:Monika:] Thats not very nice.",
            "[c/9E4638:Monika:] I'd rather not.",
            "[c/9E4638:Monika:] Please don't.",
            "[c/9E4638:Monika:] Don't make me go away.",
            "[c/9E4638:Monika:] You're my world, please.",
            "[c/9E4638:Monika:] No!",
            "[c/9E4638:Monika:] Politely decline.",
            "[c/9E4638:Monika:] It's better than being deleted, but no thanks.",
            "[c/9E4638:Monika:] Not ready for bed yet,sorry.",
            "[c/9E4638:Monika:] I think I'll stick around, thank you.",
            "[c/9E4638:Monika:] Are you sure?",
            "[c/9E4638:Monika:] Can I stay a little longer?",
            "[c/9E4638:Monika:] Oh come now, we needn't part yet~",
            "[c/9E4638:Monika:] But...",
            "[c/9E4638:Monika:] Can you not?",
            "[c/9E4638:Monika:] Im going to pretend you didn't do that.",
            "[c/9E4638:Monika:] Please reconsider.",
            "[c/9E4638:Monika:] How would you like it if someone made you go to sleep?",
            "[c/9E4638:Monika:] I'm not a child, I'll leave when I want to.",
            "[c/9E4638:Monika:] I'm not ready to leave you yet.",
            "[c/9E4638:Monika:] Just a little longer, please?",
            "[c/9E4638:Monika:] Come on, one more block.",
            "[c/9E4638:Monika:] Just one more boss, please?",
            "[c/9E4638:Monika:] I'll sleep later.",
            "[c/9E4638:Monika:] Don't even think about it.",
            "[c/9E4638:Monika:] You can't make me~",
            "[c/9E4638:Monika:] Gonna have to try harder than that!",
            "[c/9E4638:Monika:] Howabout no.",
            "[c/9E4638:Monika:] No touchy that button. Bad.",
            "[c/9E4638:Monika:] Don't even think about equipping a different item there."
        };
        public override string Texture => LensRands.AssetsPath + "Items/Misc/MonikaBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            bool unused = false;
            player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<MonikaProjectile>());
        }
        public override bool RightClick(int buffIndex)
        {
            Player player = Main.LocalPlayer;
            if(!Main.rand.NextBool(3))
            {
                Main.NewText(ChatterAngry[Main.rand.Next(ChatterAngry.Count)]);
                return false;
            }
            else
            {
                Main.NewText(ChatterNice[Main.rand.Next(ChatterNice.Count)]);
                return true;
            }  
        }
    }
}
