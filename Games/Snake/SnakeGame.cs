using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;

namespace ArcadeTerraria.Games.Snake
{
    public class SnakeGame : TerrariaGame
    {
        public SpriteBatch spriteBatch => Main.spriteBatch;
        public Matrix gameMatrix;

        public Snake snake;
        public static Food food;
        public static int points = 0;
        public static bool lose = false;

        public static int screenCellHeight;
        public static int screenCellWidth;

        protected override void Initialize()
        {
            snake = new Snake(new Point(10, 100));
            food = new Food(new Point(10, 20));
        }

        protected override void Update(On.Terraria.Main.orig_DoUpdate orig, Main self, GameTime gameTime)
        {
            if (points >= 10)
            {
                orig(self, gameTime);
                return;
            }

            base.Update(orig, self, gameTime);

            // values for 1080p screen (windowed): 630, 330
            screenCellHeight = screenHeight / 3 - 10 - (!Main.graphics.IsFullScreen ? 20 : 0);
            screenCellWidth = screenWidth / 3 - 10;

            snake.Update();

            if (lose)
            {
                Main.instance.Exit();
            }
        }

        protected override void Draw(On.Terraria.Main.orig_DoDraw orig, Main self, GameTime gameTime)
        {
            if (points >= 10)
            {
                orig(self, gameTime);
                return;
            }
            
            base.Draw(orig, self, gameTime);

            Main.graphics.GraphicsDevice.Clear(Color.White);

            gameMatrix = Matrix.CreateScale(3);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, gameMatrix);

            snake.Draw(spriteBatch);
            food.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();

            if (gameTimer < 200)
            {
                spriteBatch.DrawString(Main.fontDeathText, "get 10 points to play terraria", new Vector2(screenWidth / 3, screenHeight / 3), Color.Black * (10f / drawTimer));
            }

            spriteBatch.DrawString(Main.fontMouseText, "points: " + points, Vector2.One, Color.Black);
            spriteBatch.End();
        }
    }
}
