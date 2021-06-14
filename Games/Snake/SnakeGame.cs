using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;

namespace ArcadeTerraria.Games.Snake
{
    public class SnakeGame : TerrariaGame
    {
        public override string Name => "Snake";

        public Snake snake;
        public static Food food;
        public static int points;
        public static bool lose;

        public static int screenCellHeight;
        public static int screenCellWidth;

        internal override void Load()
        {
            base.Load();

            lose = false;
            points = 0;
            snake = new Snake(new Point(10, 100));
            food = new Food(new Point(10, 20));
        }

        protected override void Unload()
        {
            food = null;
            points = 0;
            lose = false;
        }

        internal override void Update(GameTime gameTime)
        {
            if (lose)
            {
                if (points >= 2)
                {
                    WinGame();
                    return;
                }

                EndGame();
                return;
            }

            base.Update(gameTime);

            screenCellWidth = 150;
            screenCellHeight = 140;

            snake.UpdateDirection();
            if (gameTimer % 10 == 0)
            {
                snake.Update();
            }
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            snake.Draw(spriteBatch);
            food.Draw(spriteBatch);
        }

        internal override void DrawText(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Main.fontMouseText, "points: " + points, Vector2.One, Color.Black);
        }
    }
}
