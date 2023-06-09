using LensRands.Systems.PlayerSys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace LensRands.UI
{
    internal class RealKnifeUIState : UIState
    {
        // For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
        // Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
        private UIText text;
        private UIElement area;
        private UIImage bar;
        private UIImage barHit;

        private Color colorA;
        private Color colorB;
        private Color UsedColor;

        public override void OnInitialize()
        {
            // Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
            // UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
            area = new UIElement();
            area.Top.Set(120, 0f); 
            area.Width.Set(182, 0f); 
            area.Height.Set(60, 0f);
            area.HAlign = area.VAlign = 0.5f; // 1

            text = new UIText("", 1f); // text to show stat
            text.Width.Set(138, 0f);
            text.Height.Set(34, 0f);
            text.Top.Set(40, 0f);
            text.Left.Set(65, 0f);

            bar = new UIImage(Request<Texture2D>(LensRands.AssetsPath + "UI/RealKnifeHitFrame"));
            bar.Left.Set(22, 0f);
            bar.Top.Set(0, 0f);
            bar.Width.Set(138, 0f);
            bar.Height.Set(34, 0f);

            barHit = new UIImage(Request<Texture2D>(LensRands.AssetsPath + "UI/RealKnifeHitBar"));
            barHit.Left.Set(-64, 0f);//This is overrided in Update()!
            barHit.Top.Set(106, 0f);
            barHit.Width.Set(5,0f);
            barHit.Height.Set(28,0f);
            barHit.HAlign = barHit.VAlign = 0.5f;

            colorA = new Color(255, 255, 255); // Nocrit
            colorB = new Color(255, 0, 0); // Crit

            area.Append(text);
            area.Append(bar);
            Append(area);
            Append(barHit);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!(Main.LocalPlayer.GetModPlayer<WeaponPlayer>().KnifeOut))
            {
                return;
            }
            base.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            if (!(Main.LocalPlayer.GetModPlayer<WeaponPlayer>().KnifeOut)) 
            {
                barHit.Left.Set(-64, 0f);
                return;
            }
            var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();

            if (modPlayer.KnifeTimer >= 48 && modPlayer.KnifeTimer <= 52)
            {
                UsedColor = colorB;
            }
            else
            {
                UsedColor = colorA;
            }
            //for (int i = 0; i < steps; i += 1) {
            //float percent = (float)i / steps; // Alternate Gradient Approach
            //float percent = (float)steps / (right - left);

            float oldloc = barHit.Left.Pixels;
            barHit.Left.Set(oldloc+1.3f, 0f);
            barHit.Color = UsedColor;
            // Setting the text per tick to update and show our resource values.
            //text.SetText($"{modPlayer.KnifeTimer}"); //Debug
            base.Update(gameTime);
        }
    }
}