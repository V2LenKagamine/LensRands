using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace LensRands.LensUtils
{
    public static class LensVisualUtils
    {
        private static readonly EntitySource_Misc LensVisualsES = new("LensRands:VisualEffect");
        public static void BombVisuals(Vector2 Pos, int Width, int Height)
        {
            Vector2 Pos2 = new Vector2(Pos.X - (Width / 2), Pos.Y - (Height / 2));
            for (int i = 0; i < 50; i++)
            {
                Dust dust = Dust.NewDustDirect(Pos2, Width, Height, DustID.Smoke, 0f, 0f, 100, default, 2f);
                dust.velocity *= 1.4f;
            }

            for (int i = 0; i < 80; i++)
            {
                Dust dust = Dust.NewDustDirect(Pos2, Width, Height, DustID.Torch, 0f, 0f, 100, default, 3f);
                dust.noGravity = true;
                dust.velocity *= 5f;
                dust = Dust.NewDustDirect(Pos2, Width, Height, DustID.Torch, 0f, 0f, 100, default, 2f);
                dust.velocity *= 3f;
            }
            for (int g = 0; g < 2; g++)
            {
                var goreSpawnPosition = new Vector2(Pos2.X + Width / 2 - 24f, Pos2.Y + Height / 2 - 24f);
                Gore gore = Gore.NewGoreDirect(LensVisualsES, goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                gore.scale = 1.5f;
                gore.velocity.X += 1.5f;
                gore.velocity.Y += 1.5f;
                gore = Gore.NewGoreDirect(LensVisualsES, goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                gore.scale = 1.5f;
                gore.velocity.X -= 1.5f;
                gore.velocity.Y += 1.5f;
                gore = Gore.NewGoreDirect(LensVisualsES, goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                gore.scale = 1.5f;
                gore.velocity.X += 1.5f;
                gore.velocity.Y -= 1.5f;
                gore = Gore.NewGoreDirect(LensVisualsES, goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                gore.scale = 1.5f;
                gore.velocity.X -= 1.5f;
                gore.velocity.Y -= 1.5f;
            }
            // Play explosion sound
            SoundEngine.PlaySound(SoundID.Item14, Pos);
        }
    }
}
