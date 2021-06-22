using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using Terraria;

namespace ArcadeTerraria.Games.Minesweeper
{
    public class MineSweeperGame : TerrariaGame
    {
        public override string Name => "MineSweeper";

        public static Cell[,] cells;
        public static bool lose;
        public static bool firstClick;
        private int gameEndTimer;

        private bool MouseWithinBounds => Mouse.X > drawPosition.X && Mouse.Y > drawPosition.Y &&
            Mouse.X < (cells.GetLength(0) * 10 * scale) + drawPosition.X && Mouse.Y < (cells.GetLength(1) * 10 * scale) + drawPosition.Y;

        private Point MouseCellPos => new Point(
            (MousePos.X - MousePos.X % 10) / 10 / scale,
            (MousePos.Y - MousePos.Y % 10) / 10 / scale);

        internal override void Load()
        {
            base.Load();

            gameEndTimer = 100;
            lose = false;
            cells = new Cell[15, 15];
            firstClick = true;

            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cells[x, y] = new Cell(x, y);
                }
            }
        }

        protected override void Unload()
        {
            lose = false;
            cells = null;
        }

        internal override void Update(GameTime gameTime)
        {
            if (GameWon)
            {
                gameEndTimer--;
                if (gameEndTimer <= 0)
                {
                    rewardMultiplier = NumMines / 2;
                    WinGame();
                    return;
                }
            }
            if (lose)
            {
                gameEndTimer--;
                if (gameEndTimer <= 0)
                {
                    EndGame();
                    return;
                }
            }


            base.Update(gameTime);

            if (gameTimer > 10)
            {
                if (lastMouse.LeftButton != ButtonState.Pressed && Mouse.LeftButton == ButtonState.Pressed)
                {
                    if (MouseWithinBounds && MouseCellPos.X < cells.GetLength(0) && MouseCellPos.Y < cells.GetLength(1))
                    {
                        cells[MouseCellPos.X, MouseCellPos.Y].Click();
                    }
                }
                if (lastMouse.RightButton != ButtonState.Pressed && Mouse.RightButton == ButtonState.Pressed)
                {
                    if (MouseWithinBounds && MouseCellPos.X < cells.GetLength(0) && MouseCellPos.Y < cells.GetLength(1))
                    {
                        cells[MouseCellPos.X, MouseCellPos.Y].ToggleFlag();
                    }
                }
            }
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            if (GameWon || lose)
            {
                if (gameEndTimer <= 0)
                {
                    return;
                }
            }

            base.Draw(spriteBatch);

            foreach (var cell in cells)
            {
                cell.Draw(Main.spriteBatch);
            }
            foreach (var cell in cells)
            {
                cell.DrawNum(Main.spriteBatch);
            }

            if (MouseWithinBounds)
            {
                Main.spriteBatch.Draw(Main.magicPixel, new Rectangle(MouseCellPos.X * 10 + 1, MouseCellPos.Y * 10 + 1, 9, 9), Color.White * 0.5f);
            }
        }

        internal override void DrawText(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Main.fontMouseText, NumMines.ToString(), Vector2.Zero, Color.Black);
        }

        private bool GameWon
        {
            get
            {
                foreach (var cell in cells)
                {
                    if (cell.mine) continue;

                    if (!cell.revealed)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private int NumMines
        {
            get
            {
                int num = 0;
                foreach (var cell in cells)
                {
                    if (cell.mine)
                    {
                        num++;
                    }
                }
                return num;
            }
        }
    }
}
