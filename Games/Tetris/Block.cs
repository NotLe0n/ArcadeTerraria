using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ArcadeTerraria.Games.Tetris
{
    public class Block
    {
        private Rectangle rect;
        private Color color;
        public Point cellPosition;

        public Block(Point position, Color blockColor)
        {
            cellPosition = position;
            rect = new Rectangle(position.X * 10, position.Y * 10, 10, 10);
            color = blockColor;
        }

        public void Destroy()
        {
            TetrisGame.blocks[cellPosition.X, cellPosition.Y] = null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rect.Location = new Point(cellPosition.X * 10, cellPosition.Y * 10);

            spriteBatch.Draw(Main.magicPixel, rect, color);
            spriteBatch.Draw(Main.magicPixel, new Rectangle(rect.X, rect.Y, 10, 1), Color.White * 0.5f);
            spriteBatch.Draw(Main.magicPixel, new Rectangle(rect.Right - 1, rect.Y, 1, 10), Color.Black * 0.2f);
            spriteBatch.Draw(Main.magicPixel, new Rectangle(rect.X, rect.Bottom - 1, 10, 1), Color.Black * 0.5f);
        }
    }
}
