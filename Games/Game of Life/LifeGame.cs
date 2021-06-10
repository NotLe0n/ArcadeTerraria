using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Terraria;

namespace ArcadeTerraria.Games.Game_of_Life
{
    // This doesn't work like Conway's game of life, but it's still super cool
    public class LifeGame : TerrariaGame
    {
        public override string Name => "Leon's Game of Life";

        private int[,] cells;
        private const int cellWidth = 1;
        private bool playing = false;

        private bool MouseWithinBounds => Mouse.X > drawPosition.X && Mouse.Y > drawPosition.Y &&
            Mouse.X < (cells.GetLength(0) * 10 * scale) + drawPosition.X && Mouse.Y < (cells.GetLength(1) * 10 * scale) + drawPosition.Y;

        private Point MouseCellPos => new Point(
            (MousePos.X - MousePos.X % cellWidth) / cellWidth / scale,
            (MousePos.Y - MousePos.Y % cellWidth) / cellWidth / scale);

        private bool drawCross = true;
        private bool drawGrid = false;
        private int gameSpeed = 10;

        internal override void Load()
        {
            base.Load();

            cells = new int[150, 150];
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateInput();

            if (playing && gameTimer % gameSpeed == 0)
            {
                var nextCells = new int[cells.GetLength(0), cells.GetLength(1)];

                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    for (int y = 0; y < cells.GetLength(1); y++)
                    {
                        if (nextCells[x, y] == 0 && CountNeighbors(x, y) == 3)
                        {
                            nextCells[x, y] = 1;
                        }
                        else if (nextCells[x, y] == 1 && (CountNeighbors(x, y) < 2 || CountNeighbors(x, y) > 3))
                        {
                            nextCells[x, y] = 0;
                        }
                        else
                        {
                            nextCells[x, y] = cells[x, y];
                        }
                    }
                }
                cells = nextCells;
            }
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    // Draw Grid y
                    if (drawGrid)
                    {
                        spriteBatch.Draw(Main.magicPixel, new Rectangle(x * cellWidth, y * cellWidth, 1, cellWidth), Color.Black);
                        spriteBatch.Draw(Main.magicPixel, new Rectangle(0, y * cellWidth, cellWidth * cells.GetLength(0), 1), Color.Black);
                    }

                    // Draw Cells
                    if (cells[x, y] == 1)
                    {
                        spriteBatch.Draw(Main.magicPixel, new Rectangle(x * cellWidth, y * cellWidth, cellWidth, cellWidth), Color.Black);
                    }
                }
            }

            // Draw Border
            spriteBatch.Draw(Main.magicPixel, new Rectangle(0, 0, cellWidth * cells.GetLength(0), 1), Color.Black);
            spriteBatch.Draw(Main.magicPixel, new Rectangle(0, 0, 1, cellWidth * cells.GetLength(0)), Color.Black);
            spriteBatch.Draw(Main.magicPixel, new Rectangle(0, cellWidth * cells.GetLength(0), cellWidth * cells.GetLength(0), 1), Color.Black);
            spriteBatch.Draw(Main.magicPixel, new Rectangle(cellWidth * cells.GetLength(0), 0, 1, cellWidth * cells.GetLength(0)), Color.Black);

            // Draw Cross
            if (drawCross)
            {
                spriteBatch.Draw(Main.magicPixel, new Rectangle(0, cellWidth * cells.GetLength(0) / 2, cellWidth * cells.GetLength(0), 1), Color.Red);
                spriteBatch.Draw(Main.magicPixel, new Rectangle(cellWidth * cells.GetLength(1) / 2, 0, 1, cellWidth * cells.GetLength(0)), Color.Red);
            }

            // Draw Mouse
            if (!playing && MouseWithinBounds)
            {
                spriteBatch.Draw(Main.magicPixel, new Rectangle(MouseCellPos.X * cellWidth, MouseCellPos.Y * cellWidth, cellWidth, cellWidth), Color.Gray);
            }
        }

        private int CountNeighbors(int i, int j)
        {
            var lives = 0;

            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    if (i + x >= 0 && i + x < cells.GetLength(0) && j + y >= 0 && j + y < cells.GetLength(1))
                    {
                        if (!(x == 0 && y == 0))
                        {
                            lives += cells[i + x, j + y];
                        }
                    }
                }
            }

            return lives;
        }

        private void UpdateInput()
        {
            ToggleKeybind(Keys.Space, () => playing = !playing);
            ToggleKeybind(Keys.R, () => cells = new int[cells.GetLength(0), cells.GetLength(1)]);
            ToggleKeybind(Keys.C, () => drawCross = !drawCross);
            ToggleKeybind(Keys.G, () => drawGrid = !drawGrid);

            if (!playing && MouseWithinBounds)
            {
                if (Mouse.LeftButton == ButtonState.Pressed)
                {
                    if (MouseWithinBounds && MouseCellPos.X < cells.GetLength(0) && MouseCellPos.Y < cells.GetLength(1))
                    {
                        cells[MouseCellPos.X, MouseCellPos.Y] = 1;
                    }
                }
                else if (Mouse.RightButton == ButtonState.Pressed)
                {
                    if (MouseWithinBounds && MouseCellPos.X < cells.GetLength(0) && MouseCellPos.Y < cells.GetLength(1))
                    {
                        cells[MouseCellPos.X, MouseCellPos.Y] = 0;
                    }
                }
            }
        }

        private void ToggleKeybind(Keys key, Action action)
        {
            if (!lastKeyboard.IsKeyDown(key) && Keyboard.IsKeyDown(key))
            {
                action.Invoke();
            }
        }
    }
}
