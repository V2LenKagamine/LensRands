using LensRands.Systems.PlayerSys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace LensRands.UI
{
    internal class OvershieldUIState : UIState
    {
        private UIText text;
        private UIImage bar;
        private ShieldUIElementBase area;

        public override void OnInitialize()
        {

            area = new ShieldUIElementBase();
            area.Width.Set(64, 0f);
            area.Height.Set(64, 0f);
            area.HAlign = 0.8f;
            area.VAlign = 0.02f;

            text = new UIText("", 1f);
            text.Width.Set(138, 0f);
            text.Height.Set(34, 0f);
            text.Top.Set(64f, 0f);

            bar = new UIImage(Request<Texture2D>(LensRands.AssetsPath + "UI/Overshield"));
            bar.Width.Set(64, 0f);
            bar.Height.Set(64, 0f);

            area.Append(bar);
            area.Append(text);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Main.LocalPlayer.GetModPlayer<LensPlayer>().Overheal <= 0)
            {
                return;
            }
            base.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<LensPlayer>();
            if (modPlayer.Overheal <= 0)
            {
                return;
            }
            text.SetText($"{(int)modPlayer.Overheal}");
            base.Update(gameTime);
            
        }
    }
    internal class ShieldUIElementBase : UIElement
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(IsMouseHovering)
            {
                Main.instance.MouseText("Overheal");
            }
        }
    }
}
