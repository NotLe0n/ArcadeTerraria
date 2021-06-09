using ArcadeTerraria.Games;
using ArcadeTerraria.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ArcadeTerraria.Tiles
{
    public abstract class ArcadeTile : ModTile
    {
        protected abstract TerrariaGame Game { get; }

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLavaDeath[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Width = 2;
            TileObjectData.addTile(Type);
        }

        public override bool NewRightClick(int i, int j)
        {
            ArcadeTerraria.ArcadeUserInterface.SetState(new ArcadeUI(Game));
            return true;
        }
    }
}

