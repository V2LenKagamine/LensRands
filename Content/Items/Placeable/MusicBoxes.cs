﻿using LensRands.Content.Items.Consumable;
using LensRands.Content.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace LensRands.Content.Items.Placeable
{
    public class MarkovBox : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Placeables/Markov";
        public override void SetStaticDefaults()
        {
            ItemID.Sets.CanGetPrefixes[Type] = false; // music boxes can't get prefixes in vanilla
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox; // recorded music boxes transform into the basic form in shimmer

            // The following code links the music box's item and tile with a music track:
            //   When music with the given ID is playing, equipped music boxes have a chance to change their id to the given item type.
            //   When an item with the given item type is equipped, it will play the music that has musicSlot as its ID.
            //   When a tile with the given type and Y-frame is nearby, if its X-frame is >= 36, it will play the music that has musicSlot as its ID.
            // When getting the music slot, you should not add the file extensions!
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod,"Assets/Music/MattMoney-Markov"), ModContent.ItemType<MarkovBox>(), ModContent.TileType<MarkovBoxTile>());
        }

        public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<MarkovBoxTile>(), 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MusicBox)
                .AddIngredient(ModContent.ItemType<LunarCoin>())
                .Register();
        }
    }
    public class YourDemiseBox : ModItem
    {
        public override string Texture => LensRands.AssetsPath + "Items/Placeables/YourDemise";
        public override void SetStaticDefaults()
        {
            ItemID.Sets.CanGetPrefixes[Type] = false; 
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox; 
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/DPZ-YourDemiseEX"), ModContent.ItemType<YourDemiseBox>(), ModContent.TileType<YourDemiseBoxTile>());
        }

        public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<YourDemiseBoxTile>(), 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MusicBox)
                .AddIngredient(ModContent.ItemType<LunarCoin>())
                .Register();
        }
    }
}
