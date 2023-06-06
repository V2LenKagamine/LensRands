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
        /*
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            
            
            var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
            // Calculate quotient
            float quotient = (float)modPlayer.KnifeTimer / 100; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

            // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
            Rectangle hitbox = bar.GetInnerDimensions().ToRectangle();
            hitbox.X += 12;
            hitbox.Width -= 24;
            hitbox.Y += 16;
            hitbox.Height -= 10;

            // Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
            int left = hitbox.Left;
            int right = hitbox.Right;
            int steps = (int)((right - left) * quotient);

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
            base.DrawSelf(spriteBatch);
            
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + steps, hitbox.Y, 5, hitbox.Height), UsedColor);
            //}
        }
        */
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