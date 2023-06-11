using System;
using LensRands.Systems.PlayerSys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace LensRands.UI
{
    public class BungusDrawLayer : PlayerDrawLayer
    {

        private Asset<Texture2D> BungusRing;

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.GetModPlayer<LensPlayer>().BungusActive;
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Wings);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (BungusRing == null) 
            {
                BungusRing = ModContent.Request<Texture2D>(LensRands.AssetsPath + "Effects/BungusRing");
            }

            var pos = drawInfo.Center - Main.screenPosition;
            pos = new Vector2((int)pos.X, (int)pos.Y);

            drawInfo.DrawDataCache.Add(new DrawData(
                BungusRing.Value,
                pos,
                null,
                Color.White * 0.33f,
                0f,
                BungusRing.Size() * 0.5f,
                1f,
                SpriteEffects.None,
                0));

        }
    }
}
