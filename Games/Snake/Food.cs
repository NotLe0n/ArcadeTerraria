using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ArcadeTerraria.Games.Snake
{
    public class Food
    {
        public Rectangle rect;

        public Food(Point position)
        {
            rect = new Rectangle(position.X, position.Y, 10, 10);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.magicPixel, rect, Color.Red);
        }
    }
}
