﻿using Terraria.ID;
using Terraria.ModLoader;

namespace ArcadeTerraria.Items
{
    class MinesweeperArcade : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.AlphabetStatue0);
            item.createTile = ModContent.TileType<Tiles.MinesweeperArcade>();
        }
    }
}