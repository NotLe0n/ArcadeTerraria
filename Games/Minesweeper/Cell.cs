using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArcadeTerraria.Games.Minesweeper
{
    public class Cell
    {
        public bool revealed = false;
        public bool mine;
        public int Nearby => CountNearbyMines();
        private Rectangle rect;
        public bool flagged;

        public Cell(int x, int y)
        {
            rect = new Rectangle(x * 10, y * 10, 10, 10);
            mine = Main.rand.NextBool(8);
        }

        public void Click()
        {
            if (!flagged)
            {
                if (mine)
                {
                    if (MineSweeperGame.firstClick)
                    {
                        mine = false;
                        Click();
                        return;
                    }

                    MineSweeperGame.lose = true;
                    revealed = true;
                }
                else if (!revealed)
                {
                    revealed = true;
                    RevealNeighbors();
                }

                MineSweeperGame.firstClick = false;
            }
        }

        public void ToggleFlag()
        {
            if (!revealed)
            {
                flagged = !flagged;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.magicPixel, rect, Color.Beige);
            if (!revealed)
            {
                spriteBatch.Draw(Main.magicPixel, rect, Color.Black * 0.5f);
            }

            spriteBatch.Draw(Main.magicPixel, new Rectangle(rect.X, rect.Y, rect.Width, 1), Color.Black); // top edge
            spriteBatch.Draw(Main.magicPixel, new Rectangle(rect.X, rect.Y, 1, rect.Height), Color.Black); // left edge 
            spriteBatch.Draw(Main.magicPixel, new Rectangle(rect.X + rect.Width, rect.Y, 1, rect.Height + 1), Color.Black); // right edge (rect.Height + 1 to fill the corner)
            spriteBatch.Draw(Main.magicPixel, new Rectangle(rect.X, rect.Y + rect.Height, rect.Width, 1), Color.Black); // bottom edge

            if (mine && (revealed || MineSweeperGame.lose))
            {
                var wh = new Point(Main.itemTexture[ItemID.Bomb].Width / 4, Main.itemTexture[ItemID.Bomb].Height / 4);
                spriteBatch.Draw(Main.itemTexture[ItemID.Bomb], new Rectangle(rect.X + wh.X / 2, rect.Y + wh.Y / 2, wh.X, wh.Y), Color.White);
            }

            if (flagged)
            {
                spriteBatch.Draw(ModContent.GetTexture("ArcadeTerraria/Assets/flag"), rect, Color.White);
            }
        }

        public void DrawNum(SpriteBatch spriteBatch)
        {
            if (!mine && Nearby != 0 && revealed)
            {
                spriteBatch.DrawString(Main.fontMouseText, Nearby.ToString(), new Vector2(rect.X + rect.Width / 3, rect.Y + rect.Height / 6), GetNumColor(), 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
            }
        }

        private Color GetNumColor()
        {
            switch (Nearby)
            {
                case 1: return Color.Green;
                case 2: return Color.Blue;
                case 3: return Color.Orange;
                case 4: return Color.Red;
                case 5: return Color.Firebrick;
                case 6: return Color.DarkRed;
                case 7: return Color.Black;
                default: return Color.White;
            }
        }

        private int CountNearbyMines()
        {
            int count = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    // prevent IndexOutOfRangeException
                    if (rect.X / 10 + x < 0 || rect.Y / 10 + y < 0 || rect.X / 10 + x > MineSweeperGame.cells.GetLength(0) - 1 || rect.Y / 10 + y > MineSweeperGame.cells.GetLength(1) - 1)
                        continue;

                    if (MineSweeperGame.cells[rect.X / 10 + x, rect.Y / 10 + y].mine)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private void RevealNeighbors()
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    // prevent IndexOutOfRangeException
                    if (rect.X / 10 + x < 0 || rect.Y / 10 + y < 0 || rect.X / 10 + x > MineSweeperGame.cells.GetLength(0) - 1 || rect.Y / 10 + y > MineSweeperGame.cells.GetLength(1) - 1)
                        continue;

                    Cell neighbor = MineSweeperGame.cells[rect.X / 10 + x, rect.Y / 10 + y];

                    // don't reveal mines and don't reveal yourself (already revealed)
                    if (neighbor.mine || (x == 0 && y == 0))
                        continue;

                    if (Nearby == 0)
                    {
                        if (!neighbor.revealed)
                        {
                            neighbor.revealed = true;
                            neighbor.RevealNeighbors(); // recursion funny
                        }
                    }
                }
            }
        }
    }
}
