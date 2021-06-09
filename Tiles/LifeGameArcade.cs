using ArcadeTerraria.Games;
using ArcadeTerraria.Games.Game_of_Life;

namespace ArcadeTerraria.Tiles
{
    class LifeGameArcade : ArcadeTile
    {
        protected override TerrariaGame Game => new LifeGame();
    }
}
