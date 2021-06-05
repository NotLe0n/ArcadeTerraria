using ArcadeTerraria.Games.Game_of_Life;
using ArcadeTerraria.Games.Snake;
using ArcadeTerraria.Games.Tetris;
using Terraria.ModLoader;

namespace ArcadeTerraria
{
    public class ArcadeTerraria : Mod
    {
        public override void Load()
        {
            //SnakeGame snake = new SnakeGame();
            //snake.Load();

            /*TetrisGame tetris = new TetrisGame();
            tetris.Load();*/

            LifeGame gameOfLife = new LifeGame();
            gameOfLife.Load();


            base.Load();
        }
    }
}