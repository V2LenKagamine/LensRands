using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace LensRands.UI.BaseUI
{
    internal class LensHoverImageButton : UIImageButton
    {
		internal string hoverText;

        public LensHoverImageButton(Asset<Texture2D> texture, string hoverText) : base(texture)
        {
            this.hoverText = hoverText;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            if (IsMouseHovering)
                Main.hoverItemName = hoverText;
        }
    }
}
