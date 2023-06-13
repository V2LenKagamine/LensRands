using Terraria.UI;
using LensRands.UI;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LensRands.Systems.ModSys
{
    public class UIModSys : ModSystem
    {
        internal RealKnifeUIState realknifeUI;
        internal UserInterface RealKnifeInterface;

        internal UserInterface OvershieldInterface;
        internal OvershieldUIState OvershieldUI;
        public override void Load()
        {
            if (!Main.dedServ)
            {
                RealKnifeInterface = new UserInterface();

                realknifeUI = new RealKnifeUIState();
                realknifeUI.Activate();
                //If UI is being screwy remove this line
                //Also figure out how to properly render it.
                RealKnifeInterface.SetState(realknifeUI);

                OvershieldInterface = new UserInterface();
                OvershieldUI = new OvershieldUIState();
                OvershieldUI.Activate();
                //second verse,same as the first!
                OvershieldInterface.SetState(OvershieldUI);
            }
        }
        public override void Unload()
        {
            realknifeUI = null;
            OvershieldUI = null;
        }

        private GameTime _lastUpdateUiGameTime;

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (RealKnifeInterface?.CurrentState != null)
            {
                RealKnifeInterface.Update(gameTime);
            }
            if (OvershieldInterface?.CurrentState != null)
            {
                OvershieldInterface.Update(gameTime);
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "LensRands: RealKnifeInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && RealKnifeInterface?.CurrentState != null)
                        {
                            RealKnifeInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI));
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "LensRands: OvershieldInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && OvershieldInterface?.CurrentState != null)
                        {
                            OvershieldInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
               InterfaceScaleType.UI));
            }
        }
    }
}
