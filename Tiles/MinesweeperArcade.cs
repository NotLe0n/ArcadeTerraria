using ArcadeTerraria.Games;
using ArcadeTerraria.Games.Minesweeper;

namespace ArcadeTerraria.Tiles
{
    class MinesweeperArcade : ArcadeTile
    {
        protected override TerrariaGame Game => new MineSweeperGame();
    }
}
