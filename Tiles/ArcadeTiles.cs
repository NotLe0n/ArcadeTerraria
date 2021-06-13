using ArcadeTerraria.Games;
using ArcadeTerraria.Games.Game_of_Life;
using ArcadeTerraria.Games.Minesweeper;
using ArcadeTerraria.Games.Snake;
using ArcadeTerraria.Games.Tetris;
using Microsoft.Xna.Framework;

namespace ArcadeTerraria.Tiles
{
    class LifeGameArcade : ArcadeTile
    {
        protected override TerrariaGame Game => new LifeGame();
    }

    class MinesweeperArcade : ArcadeTile
    {
        protected override TerrariaGame Game => new MineSweeperGame();
    }

    class SnakeArcade : ArcadeTile
    {
        protected override TerrariaGame Game => new SnakeGame();
    }

    class TetrisArcade : ArcadeTile
    {
        protected override TerrariaGame Game => new TetrisGame
        {
            backgroundColor = Color.Black,
            scale = 2
        };
    }
}
