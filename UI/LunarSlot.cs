using System;
using System.Linq;
using LensRands.Content.Items.Accessories;
using Terraria;
using Terraria.ModLoader;

namespace LensRands.UI
{
    public class LunarSlot : ModAccessorySlot
    {

        public override string FunctionalTexture => LensRands.AssetsPath + "Items/Consumable/LunarPod";
        public override string FunctionalBackgroundTexture => "Terraria/Images/Inventory_Back7";
        public override string VanityBackgroundTexture => "Terraria/Images/Inventory_Back7";
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            int[] ValidItems = Mod.GetContent<RORLunar>().Select(x => x.Type).ToArray();
            if (ValidItems.Contains(checkItem.type))
            {
                return true;
            }
            return false;
        }
        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
        {
            int[] ValidItems = Mod.GetContent<RORLunar>().Select(x => x.Type).ToArray();
            if (ValidItems.Contains(item.type))
            {
                return true;
            }
            return false;
        }
        public override void OnMouseHover(AccessorySlotType context)
        {
            switch (context)
            {
                case AccessorySlotType.FunctionalSlot:
                case AccessorySlotType.VanitySlot:
                    {
                        Main.hoverItemName = "Lunar Item";
                        break;
                    }
                case AccessorySlotType.DyeSlot:
                    {
                        Main.hoverItemName = "Lunar Item Dye(Useless)";
                        break;
                    }
            }
        }
    }
}
