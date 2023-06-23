using Terraria.UI;
using LensRands.UI;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using LensRands.Content.Items.Placeable;

namespace LensRands.Systems.ModSys
{
    public class UIModSys : ModSystem
    {
        internal RealKnifeUIState realknifeUI;
        internal UserInterface RealKnifeInterface;

        internal UserInterface OvershieldInterface;
        internal OvershieldUIState OvershieldUI;

        internal UserInterface VendorInterface;
        internal VendorUIState VendorUI;
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


                VendorInterface = new UserInterface();
                VendorUI = new VendorUIState();
                VendorUI.Activate();
            }
        }
        public override void Unload()
        {
            realknifeUI = null;
            OvershieldUI = null;
            VendorUI = null;
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
            if(VendorInterface?.CurrentState != null)
            {
                VendorInterface.Update(gameTime);
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
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "LensRands: VendorInterface",
                    delegate {
                        if (VendorInterface?.CurrentState != null)
                            VendorInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }
        public void ShowVendorUI()
        {
            VendorInterface?.SetState(VendorUI);
        }
        public void HideVendorUI()
        {
            VendorInterface?.SetState(null);
        }
    }
}
