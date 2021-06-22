using ArcadeTerraria.Games;
using ArcadeTerraria.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ArcadeTerraria.Tiles
{
    public abstract class ArcadeTile : ModTile
    {
        protected abstract TerrariaGame Game { get; }
        private TerrariaGame _game;
        private Vector2 position;

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLavaDeath[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.addTile(Type);

            _game = Game;
        }

        public override bool NewRightClick(int i, int j)
        {
            position = new Vector2(i * 16, j * 16);

            ArcadeTerraria.ArcadeUserInterface.SetState(new ArcadeUI(_game));
            _game.OnWinGame += DropCoins;

            return true;
        }

        public void DropCoins()
        {
            for (int i = 0; i < 10; i++)
            {
                Item.NewItem(position, new Vector2(10, 10), ItemID.SilverCoin, (int)(Main.rand.Next(0, 4) * Game.rewardMultiplier));
            }
        }
    }
}

