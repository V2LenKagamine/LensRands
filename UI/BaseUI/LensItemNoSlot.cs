using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace LensRands.UI.BaseUI
{
    internal class LensItemNoSlot : UIElement
    {
        internal float scale = 1f;
        public int itemType;
        public int price;
        private int stackDelay;
        public LensItemNoSlot(int itemID,int price = 0,float scale = 1f)
        {
            this.scale = scale;
            this.price = price == 0 ? (int)(new Item(itemID).value * 3f) : price;
            itemType = itemID;
            Width.Set(32f * scale, 0f);
            Height.Set(32f * scale, 0f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Vector2 position = GetInnerDimensions().Position();
            float num = 1f;
            float num2 = 1f;
            if (Main.netMode != NetmodeID.Server && !Main.dedServ)
            {
                Texture2D texture2D = TextureAssets.Item[itemType].Value;
                Rectangle rectangle;
                if (Main.itemAnimations[itemType] != null)
                {
                    rectangle = Main.itemAnimations[itemType].GetFrame(texture2D);
                }
                else
                {
                    rectangle = texture2D.Frame(1, 1, 0, 0);
                }
                if (rectangle.Height > 32)
                {
                    num2 = 32f / rectangle.Height;
                }
            }
            num2 *= scale;
            num *= num2;
            if (num > 1.5f)
            {
                num = 1.5f;
            }
            Item Dummy = new(itemType);
            {
                
                float inventoryScale = Main.inventoryScale;
                Main.inventoryScale = scale * num;
                ItemSlot.Draw(spriteBatch, ref Dummy, 14, position - new Vector2(10f) * scale * num, Color.White);
                Main.inventoryScale = inventoryScale;
            }

            if (IsMouseHovering)
            {
                Main.hoverItemName = buyprice(Dummy.Name,price);
                //Main.HoverItem = Dummy;
            }
        }
        public override void LeftClick(UIMouseEvent evt)
        {
            if (!Main.mouseItem.IsAir)
            {
                Main.NewText("You can't sell to a vending machine!");
            }
            else
            {
                if (Main.LocalPlayer.CanAfford(price))
                {
                    Main.playerInventory = true;
                    Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), itemType);
                    Main.LocalPlayer.BuyItem(price);
                    SoundEngine.PlaySound(SoundID.Coins);
                }
                else
                {
                    Main.NewText("You don't have enough money!");
                }
            }
        }
        public override void RightMouseDown(UIMouseEvent evt)
        {
            if (Main.mouseItem.type == itemType && Main.mouseItem.stack < Main.mouseItem.maxStack && Main.LocalPlayer.CanAfford(price))
            {
                Main.mouseItem.stack++; 
                Main.LocalPlayer.BuyItem(price);
                SoundEngine.PlaySound(SoundID.Coins);
                stackDelay = 30;
            }
            else if (Main.mouseItem.IsAir)
            {
                if (Main.LocalPlayer.CanAfford(price))
                {
                    Main.playerInventory = true;
                    Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), itemType);
                    Main.LocalPlayer.BuyItem(price);
                    SoundEngine.PlaySound(SoundID.Coins);
                    stackDelay = 30;
                }
                else
                {
                    Main.NewText("You don't have enough money!");
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (IsMouseHovering && Main.mouseRight && Main.mouseItem.type == itemType && Main.LocalPlayer.CanAfford(price))
            {
                if (stackDelay > 0)
                {
                    stackDelay--;
                }
                else if (Main.mouseItem.stack < Main.mouseItem.maxStack)
                {
                    Main.mouseItem.stack++;
                    Main.LocalPlayer.BuyItem(price);
                    SoundEngine.PlaySound(SoundID.Coins);
                }
            }
        }
        private string buyprice(string Itemname,int CopperCost)
        {
            int copper = CopperCost % 100;
            int silver = (CopperCost / Item.silver) % 100;
            int gold = (CopperCost / Item.gold) % 100;
            int platinum = (CopperCost / Item.platinum) % 100;
            
            
            

            string TheString = $"{Itemname} Costs: ";
            if (platinum > 0)
            {
                TheString += $"[c/D9D9D9:{platinum} Platinum ]";
            }
            if(gold > 0 )
            {
                TheString += $"[c/FFD966:{gold} Gold ]";
            }
            if(silver > 0)
            {
                TheString += $"[c/BABFC5:{silver} Silver ]";
            }
            if(copper > 0 )
            {
                TheString += $"[c/CE7E00:{copper} Copper ]";
            }
            TheString += "coins.";
            return TheString;
        }
    }
}
