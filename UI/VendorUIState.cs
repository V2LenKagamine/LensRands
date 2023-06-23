using System;
using System.Collections.Generic;
using LensRands.Systems.ModSys;
using LensRands.UI.BaseUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace LensRands.UI
{
    public class VendorUIState : UIState
    {
        public static LensDragPanel shoppanel;

        public static UIGrid BuyButtons;

        internal static List<LensItemNoSlot> theitemsforsale;
        internal static bool UpdateShop = false;

        internal static List<KeyValuePair<int, int>> ItemToPrice = new()
        {
            new KeyValuePair<int, int> (ItemID.Dirtfish,1)
        };

        public VendorUIState(List<KeyValuePair<int, int>> SetList = null)
        {
            if (SetList != null)
            {
                ItemToPrice = SetList;
            }
            else
            {
                ItemToPrice = new();
            }
            
        }

        public static bool Hidden = false;
        public override void OnInitialize()
        {
            shoppanel = new();
            shoppanel.Top.Set(300f, 0f);
            shoppanel.Left.Set(700f, 0f);
            shoppanel.Width.Set(400f, 0f);
            shoppanel.Height.Set(400f, 0f);
            shoppanel.SetPadding(0);
            shoppanel.BackgroundColor = new Color(73, 94, 171) * 0.25f;

            BuyButtons = new();
            BuyButtons.Top.Set(10, 0);
            BuyButtons.Left.Set(10, 0);
            BuyButtons.Width.Set(380f, 0);
            BuyButtons.Height.Set(3800f, 0);

            theitemsforsale = new();
            for (int i = 0;i < ItemToPrice.Count;i++)
            {
                theitemsforsale.Add(new LensItemNoSlot(ItemToPrice[i].Key, ItemToPrice[i].Value));
            }
            BuyButtons.AddRange(theitemsforsale);
            shoppanel.Append(BuyButtons);

            Asset<Texture2D> buttonDeleteTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonDelete");
            LensHoverImageButton closeButton = new(buttonDeleteTexture, Language.GetTextValue("LegacyInterface.52"));
            closeButton.Top.Set(5f, 0f);
            closeButton.Left.Set(375f, 0f);
            closeButton.Width.Set(22f, 0f);
            closeButton.Height.Set(22f, 0f);
            closeButton.OnLeftClick += new MouseEvent(CloseButtonClicked);
            shoppanel.Append(closeButton);


            Append(shoppanel);

        }

        public static void UpdateUIState(List<KeyValuePair<int, int>> options)
        {
            ModContent.GetInstance<UIModSys>().VendorUI = new(options);
        }
        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            Hidden = true;
            ModContent.GetInstance<UIModSys>().HideVendorUI();
        }

        public static void ShowMe()
        {
            Main.playerInventory = true;
            Hidden = false;
            SoundEngine.PlaySound(SoundID.MenuOpen);
            ModContent.GetInstance<UIModSys>().ShowVendorUI();
        }
        public static void HideMe()
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            Main.blockInput = false;
            Hidden = true;
            ModContent.GetInstance<UIModSys>().HideVendorUI();
        }
    }
}
