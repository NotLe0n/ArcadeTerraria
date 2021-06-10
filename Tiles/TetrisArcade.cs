using ArcadeTerraria.Games;
using ArcadeTerraria.Games.Tetris;
using Microsoft.Xna.Framework;

namespace ArcadeTerraria.Tiles
{
    class TetrisArcade : ArcadeTile
    {
        protected override TerrariaGame Game => new TetrisGame
        {
            backgroundColor = Color.Black,
            scale = 2
        };
    }
}
