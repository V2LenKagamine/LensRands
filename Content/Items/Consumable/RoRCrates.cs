using LensRands.Content.Items.Accessories;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Consumable
{
    public abstract class RoRCratesBase : ModItem
    {

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(plat,gold,silver,copper);
        public abstract int copper { get; }
        public abstract int silver { get; }
        public abstract int gold { get; }
        public abstract int plat { get; }
        public long PriceToOpen => Item.buyPrice(plat, gold, silver, copper);
        public abstract int[] LootTable { get; }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.White;
            Item.useStyle = 1;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.maxStack = 999;
        }

        public override bool CanRightClick()
        {
            return Main.LocalPlayer.CanAfford(PriceToOpen);
        }
        public override void OnConsumeItem(Player player)
        {
            Main.LocalPlayer.BuyItem(PriceToOpen);
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, LootTable));
        }
    }
    public class RoRCrateWhite : RoRCratesBase
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.White;
        }

        public override string Texture => LensRands.AssetsPath + "Items/Consumable/RoRCrateW";
        public override int copper => 0;

        public override int silver => 0;

        public override int gold => 20;

        public override int plat => 0;
        public override int[] LootTable => new int[] {
            ModContent.ItemType<Soldiers>(),
            ModContent.ItemType<Bungus>()
        };

    }
    public class RoRCrateGreen : RoRCratesBase
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Green;
        }
        public override string Texture => LensRands.AssetsPath + "Items/Consumable/RoRCrateG";
        public override int copper => 0;

        public override int silver => 0;

        public override int gold => 80;

        public override int plat => 0;
        public override int[] LootTable => new int[] {
            ModContent.ItemType<Ukelele>()
        };
    }
    public class RoRCrateRed : RoRCratesBase
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Red;
        }
        public override string Texture => LensRands.AssetsPath + "Items/Consumable/RoRCrateR";
        public override int copper => 0;

        public override int silver => 0;

        public override int gold => 20;

        public override int plat => 3;
        public override int[] LootTable => new int[] {
            ModContent.ItemType<DiosBestFriend>()
        };
    }
}
