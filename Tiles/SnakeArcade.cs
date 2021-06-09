using ArcadeTerraria.Games;
using ArcadeTerraria.Games.Snake;

namespace ArcadeTerraria.Tiles
{
    class SnakeArcade : ArcadeTile
    {
        protected override TerrariaGame Game => new SnakeGame();
    }
}
