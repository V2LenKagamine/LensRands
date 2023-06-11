using Microsoft.Xna.Framework;
using Terraria;

namespace LensRands.LensUtils
{
    public static class CollisionHelpers
    {
        public static bool CanHit(Entity source, Vector2 targetPos, int targetWidth = 1, int targetHeight = 1) => Collision.CanHit(source.position, source.width, source.height, targetPos, targetWidth, targetHeight);
    }
}
