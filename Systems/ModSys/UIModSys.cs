﻿using Terraria.UI;
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

            }
        }
        public override void Unload()
        {
            realknifeUI = null;
        }

        private GameTime _lastUpdateUiGameTime;

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (RealKnifeInterface?.CurrentState != null)
            {
                RealKnifeInterface.Update(gameTime);
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
            }
        }
    }
}