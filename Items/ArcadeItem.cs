using Terraria.ID;
using Terraria.ModLoader;

namespace ArcadeTerraria.Items
{
    abstract class ArcadeItem<T> : ModItem where T : ModTile
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.AlphabetStatue0);
            item.createTile = ModContent.TileType<T>();
        }
    }
}
