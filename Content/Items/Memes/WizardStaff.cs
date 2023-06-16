using LensRands.Systems.ModSys;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using System.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace LensRands.Content.Items.Memes
{
    public class WizardStaff : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/WizardStaff";
        public override void SetDefaults()
        {
            Item.mana = 10;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.scale = 1.5f;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = Item.sellPrice(2, 50);
            Item.rare = ItemRarityID.Yellow;

            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<WizardStaffProjectile>();
            Item.shootSpeed = 16f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.difficulty != 2;
        }
    }
    public class WizardStaffProjectile : ModProjectile
    {
        public override string Texture => LensRands.AssetsPath + "smielsmol";
        public float WhatToDoToday { get=> Projectile.ai[0]; set => Projectile.ai[0] = value; }
        public bool DoneSetup { get => Projectile.ai[1] == 1; set => Projectile.ai[1] = value == true ? 1 : 0; }

        public float WaitForIt { get => Projectile.ai[2]; set => Projectile.ai[2] = value; }
        public int WhoWeHit = -1;
        public int WhoFiredIt = -1;

        private static readonly List<string> ProjTypes = new() 
        {
             "PenisBlast!"
        };
        public override void SetDefaults()
        {
            Projectile.width = 32;               //The width of projectile hitbox
            Projectile.height = 32;              //The height of projectile hitbox
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 1200;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        
        public override void AI()
        {
            Player owner = Main.player[WhoFiredIt];
            if (WhatToDoToday == 0 && Main.myPlayer == Projectile.owner)
            {
                WhatToDoToday = Main.rand.Next(0, ProjTypes.Count);
            }
            if (!DoneSetup)
            {
                SetupTheBomb();
            }
            switch (ProjTypes[(int)WhatToDoToday])
            {
                case "PenisBlast!":
                    {
                        TickTock(out bool timetodie);
                        if (timetodie && WhoWeHit != -1)
                        {
                            Main.player[WhoWeHit].Hurt(PlayerDeathReason.ByCustomReason(Main.player[WhoWeHit].name + " got their penis exploded!"),200,Projectile.direction);
                            if ((Main.player[WhoWeHit].statLife-200) > 0)
                            {
                                owner.KillMe(PlayerDeathReason.ByCustomReason(owner.name + " didn't kill with the memestaff!"),owner.statLifeMax2,Projectile.direction);
                            }
                        }
                        else if (timetodie)
                        { 
                            owner.KillMe(PlayerDeathReason.ByCustomReason(owner.name + " whiffed the memestaff!"), owner.statLifeMax2, Projectile.direction); 
                        }
                        break;
                    }
                default:
                    {
                        Main.NewText("LensRands: Who broke the fucking wizard projectile?!?!");
                        break;
                    }     
            }
            float rotate = Projectile.velocity.X > 0 ? 22.5f : -22.5f;
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
            Projectile.rotation += MathHelper.ToRadians(rotate);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            switch (ProjTypes[(int)WhatToDoToday])
            {
                case "PenisBlast!":
                    {
                        WaitForIt = (4 * 60) + 15;
                        Projectile.timeLeft = (4 * 60) + 20;
                        SoundEngine.PlaySound(AudioSys.GetBentLoser, Projectile.position);
                        YouCantSeeMe();
                        ForceSync();
                        break;
                    }
                default:
                    {
                        Main.NewText("LensRands: Who broke the fucking wizard projectile?!?!");
                        break;
                    }
            }
            return false;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            switch (ProjTypes[(int)WhatToDoToday])
            {
                case "PenisBlast!":
                    {
                        WaitForIt = (4 * 60) + 15;
                        Projectile.timeLeft = (4 * 60) + 20;
                        WhoWeHit = target.whoAmI;
                        SoundEngine.PlaySound(AudioSys.GetBentLoser, Projectile.position);
                        YouCantSeeMe();
                        ForceSync();
                        break;
                    }
                default:
                    {
                        Main.NewText("LensRands: Who broke the fucking wizard projectile?!?!");
                        break;
                    }
            }
        }
        public override bool CanHitPlayer(Player target)
        {
            return target.whoAmI != WhoFiredIt && target.difficulty != 2;
        }
        private void TickTock(out bool Dingdong)
        {
            Dingdong = false;
            if (WaitForIt > 0)
            {
                WaitForIt--;
            }
            else
            { 
                Dingdong = true; 
            }
        }
        private void SetupTheBomb()
        {
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            WaitForIt = 1200;
            DoneSetup = true;
            WhoFiredIt = Projectile.owner;
            Projectile.owner = -1;
            Projectile.damage = 1;
        }
        private void YouCantSeeMe()
        {
            Projectile.penetrate = 1;
            Projectile.velocity = Vector2.Zero;
            Projectile.alpha = 255;
            Projectile.damage = -1;
            Projectile.DamageType = DamageClass.Default;
            Projectile.tileCollide = false;
        }
        private void ForceSync()
        {
            if (Main.netMode != NetmodeID.SinglePlayer) // needs called on both sides so shooter knows when projectile hits.
            {
                Projectile.netUpdate = true;
            }
        }
        
        //Dont touch dumbass
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(WhoWeHit);
            writer.Write(WhoFiredIt);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            WhoWeHit = reader.ReadInt32();
            WhoFiredIt = reader.ReadInt32();
        }
    }
}
