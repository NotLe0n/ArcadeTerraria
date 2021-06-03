using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ArcadeTerraria.Games.Snake
{
    public class SnakeBody
    {
        public Rectangle rect;

        public SnakeBody(Point position)
        {
            rect = new Rectangle(position.X, position.Y, 10, 10);
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.magicPixel, rect, Color.Blue);
        }
    }
}
