using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using LensRands.UI;
using LensRands.Systems.ModSys;
using Terraria.ID;
using LensRands.Content.Items.Consumable;

namespace LensRands.Content.Tiles
{
    public abstract class VendorTile : ModTile
    {
        public override string Texture => LensRands.AssetsPath + "Tiles/Furniture/";
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.addTile(Type);
        }

        internal abstract List<KeyValuePair<int, int>> ItemToPrice { get; }
        public override bool RightClick(int i, int j)
        {
            if (VendorUIState.Hidden)
            {
                VendorUIState.UpdateUIState(ItemToPrice);
                VendorUIState.ShowMe();
            }
            else
            {
                VendorUIState.HideMe();
            }
            return base.RightClick(i, j);
        }
    }
    public class CombatPotVendorTile : VendorTile
    {
        public override string Texture => base.Texture + "VendingMachinePots";
        internal override List<KeyValuePair<int, int>> ItemToPrice => new()
        {
            new KeyValuePair<int, int> (ItemID.AmmoReservationPotion,Item.buyPrice(0,0,50,0)),
            new KeyValuePair<int, int> (ItemID.ArcheryPotion,Item.buyPrice(0,0,75,0)),
            new KeyValuePair<int, int> (ItemID.EndurancePotion,Item.buyPrice(0,0,50,0)),
            new KeyValuePair<int, int> (ItemID.HeartreachPotion,Item.buyPrice(0,0,50,0)),
            new KeyValuePair<int, int> (ItemID.IronskinPotion,Item.buyPrice(0,0,25,0)),
            new KeyValuePair<int, int> (ItemID.LifeforcePotion,Item.buyPrice(0,1,0,0)),
            new KeyValuePair<int, int> (ItemID.MagicPowerPotion,Item.buyPrice(0,1,0,0)),
            new KeyValuePair<int, int> (ItemID.ManaRegenerationPotion,Item.buyPrice(0,1,0,0)),
            new KeyValuePair<int, int> (ItemID.RagePotion,Item.buyPrice(0,1,0,0)),
            new KeyValuePair<int, int> (ItemID.RegenerationPotion,Item.buyPrice(0,0,50,0)),
            new KeyValuePair<int, int> (ItemID.SummoningPotion,Item.buyPrice(0,0,50,0)),
            new KeyValuePair<int, int> (ItemID.SwiftnessPotion,Item.buyPrice(0, 0, 25, 0)),
            new KeyValuePair<int, int> (ItemID.ThornsPotion,Item.buyPrice(0,0,25,0)),
            new KeyValuePair<int, int> (ItemID.TitanPotion,Item.buyPrice(0,0,25,0)),
            new KeyValuePair<int, int> (ItemID.WrathPotion,Item.buyPrice(0,1,0,0)),
            new KeyValuePair<int, int> (ItemID.BattlePotion,Item.buyPrice(0,0,25,0)),
            new KeyValuePair<int, int> (ItemID.InfernoPotion,Item.buyPrice(0,0,25,0)),
            new KeyValuePair<int, int> (ItemID.WarmthPotion,Item.buyPrice(0,0,15,0)),
            new KeyValuePair<int, int> (ItemID.Ale,Item.buyPrice(0,0,15,0)),
            new KeyValuePair<int, int> (ItemID.Sake,Item.buyPrice(0,0,20,0)),
            new KeyValuePair<int, int> (ItemID.GrapeJuice,Item.buyPrice(0,1,0,0)),
            new KeyValuePair<int, int> (ItemID.FruitJuice,Item.buyPrice(0,0,50,0)),
            new KeyValuePair<int, int> (ItemID.CookedFish,Item.buyPrice(0,0,10,0)),
        };
    }
    public class UtilPotVendorTile : VendorTile
    {
        public override string Texture => base.Texture + "VendingMachinePots";

        internal override List<KeyValuePair<int, int>> ItemToPrice => new()
        {
            new KeyValuePair<int, int> (ItemID.RecallPotion,Item.buyPrice(0,0,15,0)),
            new KeyValuePair<int, int> (ItemID.PotionOfReturn,Item.buyPrice(0,0,25,0)),
            new KeyValuePair<int, int> (ItemID.WormholePotion,Item.buyPrice(0,0,15,0)),
            new KeyValuePair<int, int> (ItemID.TeleportationPotion,Item.buyPrice(0,0,50,0)),
            new KeyValuePair<int, int> (ItemID.BuilderPotion,Item.buyPrice(0,0,25,0)),
            new KeyValuePair<int, int> (ItemID.CalmingPotion,Item.buyPrice(0,0,25,0)),
            new KeyValuePair<int, int> (ItemID.CratePotion,Item.buyPrice(0,0,30,0)),
            new KeyValuePair<int, int> (ItemID.TrapsightPotion,Item.buyPrice(0,0,15,0)),
            new KeyValuePair<int, int> (ItemID.FeatherfallPotion,Item.buyPrice(0,0,15,0)),
            new KeyValuePair<int, int> (ItemID.FishingPotion,Item.buyPrice(0,0,30,0)),
            new KeyValuePair<int, int> (ItemID.FlipperPotion,Item.buyPrice(0,0,15,0)),
            new KeyValuePair<int, int> (ItemID.GillsPotion,Item.buyPrice(0,0,25,0)),
            new KeyValuePair<int, int> (ItemID.GravitationPotion,Item.buyPrice(0,0,75,0)),
            new KeyValuePair<int, int> (ItemID.LuckPotionGreater,Item.buyPrice(0,1,0,0)),
            new KeyValuePair<int, int> (ItemID.LuckPotion,Item.buyPrice(0,0,75,0)),
            new KeyValuePair<int, int> (ItemID.LuckPotionLesser,Item.buyPrice(0,0,50,0)),
            new KeyValuePair<int, int> (ItemID.HunterPotion,Item.buyPrice(0,0,25,0)),
            new KeyValuePair<int, int> (ItemID.InvisibilityPotion,Item.buyPrice(0,0,15,0)),
            new KeyValuePair<int, int> (ItemID.LovePotion,Item.buyPrice(0,0,15,0)),
            new KeyValuePair<int, int> (ItemID.MiningPotion,Item.buyPrice(0,0,15,0)),
            new KeyValuePair<int, int> (ItemID.NightOwlPotion,Item.buyPrice(0,0,15,0)),
            new KeyValuePair<int, int> (ItemID.ObsidianSkinPotion,Item.buyPrice(0,0,40,0)),
            new KeyValuePair<int, int> (ItemID.ShinePotion,Item.buyPrice(0,0,15,0)),
            new KeyValuePair<int, int> (ItemID.SonarPotion,Item.buyPrice(0,0,15,0)),
            new KeyValuePair<int, int> (ItemID.SpelunkerPotion,Item.buyPrice(0,0,50,0)),
            new KeyValuePair<int, int> (ItemID.StinkPotion,Item.buyPrice(0,0,15,0)),
            new KeyValuePair<int, int> (ItemID.WaterWalkingPotion,Item.buyPrice(0,0,25,0)),

        };
    }
    public class GeneralBlocksVendorTile : VendorTile
    {
        public override string Texture => base.Texture + "VendingMachineBlocks";
        internal override List<KeyValuePair<int, int>> ItemToPrice => new()
        {
            new KeyValuePair<int, int> (ItemID.DirtBlock,Item.buyPrice(0,0,0,1)),
            new KeyValuePair<int, int> (ItemID.ClayBlock,Item.buyPrice(0,0,0,1)),
            new KeyValuePair<int, int> (ItemID.MudBlock,Item.buyPrice(0,0,0,1)),
            new KeyValuePair<int, int> (ItemID.AshBlock,Item.buyPrice(0,0,0,1)),
            new KeyValuePair<int, int> (ItemID.SnowBlock,Item.buyPrice(0,0,0,1)),
            new KeyValuePair<int, int> (ItemID.StoneBlock,Item.buyPrice(0,0,0,2)),
            new KeyValuePair<int, int> (ItemID.IceBlock,Item.buyPrice(0,0,0,2)),
            new KeyValuePair<int, int> (ItemID.Wood,Item.buyPrice(0,0,0,2)),
            new KeyValuePair<int, int> (ItemID.BorealWood,Item.buyPrice(0,0,0,2)),
            new KeyValuePair<int, int> (ItemID.PalmWood,Item.buyPrice(0,0,0,2)),
            new KeyValuePair<int, int> (ItemID.RichMahogany,Item.buyPrice(0,0,0,2)),
            new KeyValuePair<int, int> (ItemID.Ebonwood,Item.buyPrice(0,0,0,2)),
            new KeyValuePair<int, int> (ItemID.Shadewood,Item.buyPrice(0,0,0,2)),
            new KeyValuePair<int, int> (ItemID.Pearlwood,Item.buyPrice(0,0,0,2)),
            new KeyValuePair<int, int> (ItemID.Cactus,Item.buyPrice(0,0,0,2)),
            new KeyValuePair<int, int> (ItemID.Hay,Item.buyPrice(0,0,0,2)),
            new KeyValuePair<int, int> (ItemID.BambooBlock,Item.buyPrice(0,0,0,2)),
            new KeyValuePair<int, int> (ItemID.Pumpkin,Item.buyPrice(0,0,0,3)),
            new KeyValuePair<int, int> (ItemID.SandBlock,Item.buyPrice(0,0,0,5)),
            new KeyValuePair<int, int> (ItemID.Granite,Item.buyPrice(0,0,0,5)),
            new KeyValuePair<int, int> (ItemID.Marble,Item.buyPrice(0,0,0,5)),
            new KeyValuePair<int, int> (ItemID.Hive,Item.buyPrice(0,0,0,10)),
            new KeyValuePair<int, int> (ItemID.Cloud,Item.buyPrice(0,0,0,10)),
            new KeyValuePair<int, int> (ItemID.GreenMoss,Item.buyPrice(0,0,0,15)),
            new KeyValuePair<int, int> (ItemID.BrownMoss,Item.buyPrice(0,0,0,15)),
            new KeyValuePair<int, int> (ItemID.RedMoss,Item.buyPrice(0,0,0,15)),
            new KeyValuePair<int, int> (ItemID.BlueMoss,Item.buyPrice(0,0,0,15)),
            new KeyValuePair<int, int> (ItemID.PurpleMoss,Item.buyPrice(0,0,0,15)),
            new KeyValuePair<int, int> (ItemID.LavaMoss,Item.buyPrice(0,0,0,20)),
            new KeyValuePair<int, int> (ItemID.RainbowBrick,Item.buyPrice(0,0,0,25)),
            new KeyValuePair<int, int> (ItemID.KryptonMoss,Item.buyPrice(0,0,0,30)),
            new KeyValuePair<int, int> (ItemID.XenonMoss,Item.buyPrice(0,0,0,30)),
            new KeyValuePair<int, int> (ItemID.ArgonMoss,Item.buyPrice(0,0,0,30)),
            new KeyValuePair<int, int> (ItemID.VioletMoss,Item.buyPrice(0,0,0,30)),
            new KeyValuePair<int, int> (ItemID.RainbowMoss,Item.buyPrice(0,0,0,30))
        };
    }
    public class AccessoryVendorTile : VendorTile
    {
        public override string Texture => base.Texture + "VendingMachineMisc";
        internal override List<KeyValuePair<int, int>> ItemToPrice => new()
        {
             new KeyValuePair<int, int> (ItemID.Aglet,0),
             new KeyValuePair<int, int> (ItemID.AnkletoftheWind,0),
             new KeyValuePair<int, int> (ItemID.BlizzardinaBottle,0),
             new KeyValuePair<int, int> (ItemID.CloudinaBottle,0),
             new KeyValuePair<int, int> (ItemID.HermesBoots,0),
             new KeyValuePair<int, int> (ItemID.IceSkates,0),
             new KeyValuePair<int, int> (ItemID.LavaCharm,0),
             new KeyValuePair<int, int> (ItemID.SandstorminaBottle,0),
             new KeyValuePair<int, int> (ItemID.GoldWatch,0),
             new KeyValuePair<int, int> (ItemID.DepthMeter,0),
             new KeyValuePair<int, int> (ItemID.Compass,0),
             new KeyValuePair<int, int> (ItemID.FishermansGuide,0),
             new KeyValuePair<int, int> (ItemID.WeatherRadio,0),
             new KeyValuePair<int, int> (ItemID.Sextant,0),
             new KeyValuePair<int, int> (ItemID.MetalDetector,0),
             new KeyValuePair<int, int> (ItemID.Stopwatch,0),
             new KeyValuePair<int, int> (ItemID.DPSMeter,0),
             new KeyValuePair<int, int> (ItemID.Radar,0),
             new KeyValuePair<int, int> (ItemID.MagicMirror,0),
             new KeyValuePair<int, int> (ItemID.DemonConch,0),
             new KeyValuePair<int, int> (ItemID.MagicConch,0),
             new KeyValuePair<int, int> (ItemID.LifeformAnalyzer,0),
             new KeyValuePair<int, int> (ItemID.TallyCounter,0),
             new KeyValuePair<int, int> (ItemID.BandofRegeneration,0),
             new KeyValuePair<int, int> (ItemID.BandofStarpower,0),
             new KeyValuePair<int, int> (ItemID.AdhesiveBandage,0),
             new KeyValuePair<int, int> (ItemID.Bezoar,0),
             new KeyValuePair<int, int> (ItemID.Blindfold,0),
             new KeyValuePair<int, int> (ItemID.Megaphone,0),
             new KeyValuePair<int, int> (ItemID.Nazar,0),
             new KeyValuePair<int, int> (ItemID.ArmorPolish,0),
             new KeyValuePair<int, int> (ItemID.FastClock,0),
             new KeyValuePair<int, int> (ItemID.Vitamins,0),
             new KeyValuePair<int, int> (ItemID.TrifoldMap,0),
             new KeyValuePair<int, int> (ItemID.PocketMirror,0),
             new KeyValuePair<int, int> (ItemID.FeralClaws,0),
             new KeyValuePair<int, int> (ItemID.MagmaStone,0),
             new KeyValuePair<int, int> (ItemID.ObsidianRose,0),
             new KeyValuePair<int, int> (ItemID.SharkToothNecklace,0),
             new KeyValuePair<int, int> (ItemID.ArchitectGizmoPack,0),
             new KeyValuePair<int, int> (ItemID.AncientChisel,0),
             new KeyValuePair<int, int> (ItemID.PortableStool,0),
             new KeyValuePair<int, int> (ItemID.FlowerBoots,0),
             new KeyValuePair<int, int> (ItemID.WaterWalkingBoots,0)
        };
    }
    public class BossFightVendorTile : VendorTile
    {
        public override string Texture => base.Texture + "VendingMachineBoss";
        internal override List<KeyValuePair<int, int>> ItemToPrice =>new() 
        {
             new KeyValuePair<int, int> (ItemID.SlimeCrown,Item.buyPrice(0,1,0,0)),
             new KeyValuePair<int, int> (ItemID.SuspiciousLookingEye,Item.buyPrice(0,2,0,0)),
             new KeyValuePair<int, int> (ItemID.WormFood,Item.buyPrice(0,2,50,0)),
             new KeyValuePair<int, int> (ItemID.BloodySpine,Item.buyPrice(0,2,50,0)),
             new KeyValuePair<int, int> (ItemID.Abeemination,Item.buyPrice(0,3,0,0)),
             new KeyValuePair<int, int> (ItemID.ClothierVoodooDoll,Item.buyPrice(0,3,25,0)),
             new KeyValuePair<int, int> (ItemID.DeerThing,Item.buyPrice(0,4,0,0)),
             new KeyValuePair<int, int> (ItemID.GuideVoodooDoll,Item.buyPrice(0,5,0,0)),
             new KeyValuePair<int, int> (ItemID.QueenSlimeCrystal,Item.buyPrice(0,6,0,0)),
             new KeyValuePair<int, int> (ItemID.MechanicalEye,Item.buyPrice(0,7,50,0)),
             new KeyValuePair<int, int> (ItemID.MechanicalSkull,Item.buyPrice(0,7,50,0)),
             new KeyValuePair<int, int> (ItemID.MechanicalWorm,Item.buyPrice(0,7,50,0)),
             new KeyValuePair<int, int> (ModContent.ItemType<PlantBulb>(),Item.buyPrice(0,10,0,0)),
             new KeyValuePair<int, int> (ItemID.LihzahrdPowerCell,Item.buyPrice(0,10,0,0)),
             new KeyValuePair<int, int> (ItemID.TruffleWorm,Item.buyPrice(0,6,66,0)),
             new KeyValuePair<int, int> (ItemID.EmpressButterfly,Item.buyPrice(0,10,0,0)),
             new KeyValuePair<int, int> (ItemID.CelestialSigil,Item.buyPrice(0,15,5,0))
        };
    }
    public class FishingVendorTile : VendorTile
    {
        public override string Texture => base.Texture + "VendingMachineMisc";
        internal override List<KeyValuePair<int, int>> ItemToPrice => new() 
        {
            new KeyValuePair<int, int> (ItemID.TackleBox,0),
            new KeyValuePair<int, int> (ItemID.HighTestFishingLine,0),
            new KeyValuePair<int, int> (ItemID.AnglerEarring,0),
            new KeyValuePair<int, int> (ItemID.LavaFishingHook,0),
            new KeyValuePair<int, int> (ItemID.FishingBobber,0),
            new KeyValuePair<int, int> (ItemID.AnglerHat,0),
            new KeyValuePair<int, int> (ItemID.AnglerVest,0),
            new KeyValuePair<int, int> (ItemID.AnglerPants,0),
            new KeyValuePair<int, int> (ItemID.FishingPotion,Item.buyPrice(0,0,75,0)),
            new KeyValuePair<int, int> (ItemID.SonarPotion,Item.buyPrice(0,0,25,0)),
            new KeyValuePair<int, int> (ItemID.CratePotion,Item.buyPrice(0,0,50,0)),
            new KeyValuePair<int, int> (ItemID.FishermansGuide,0),
            new KeyValuePair<int, int> (ItemID.WeatherRadio,0),
            new KeyValuePair<int, int> (ItemID.Sextant,0),
            new KeyValuePair<int, int> (ItemID.GoldenFishingRod,0),
            new KeyValuePair<int, int> (ItemID.ApprenticeBait,0),
            new KeyValuePair<int, int> (ItemID.JourneymanBait,0),
            new KeyValuePair<int, int> (ItemID.MasterBait,0),
            new KeyValuePair<int, int> (ItemID.BottomlessBucket,0),
            new KeyValuePair<int, int> (ItemID.BottomlessHoneyBucket,0),
            new KeyValuePair<int, int> (ItemID.BottomlessLavaBucket,0),
            new KeyValuePair<int, int> (ItemID.HoneyAbsorbantSponge,0),
            new KeyValuePair<int, int> (ItemID.LavaAbsorbantSponge,0),
            new KeyValuePair<int, int> (ItemID.SuperAbsorbantSponge,0),
        };
    }
}
