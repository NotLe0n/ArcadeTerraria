using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace ArcadeTerraria.Games.Game_of_Life
{
    // This doesn't work like Conway's game of life, but it's still super cool
    public class LifeGame : TerrariaGame
    {
        private int[,] cells;
        private const int cellWidth = 1;
        private bool playing = false;
        private KeyboardState lastKeyboard;
        private bool MouseWithinBounds => Mouse.X > 0 && Mouse.Y > 0 && Mouse.X < cells.GetLength(0) * cellWidth && Mouse.Y < cells.GetLength(1) * cellWidth;

        protected override void Initialize()
        {
            base.Initialize();

            cells = new int[1023, 1023];
        }

        protected override void Update(On.Terraria.Main.orig_DoUpdate orig, Main self, GameTime gameTime)
        {
            base.Update(orig, self, gameTime);

            UpdateInput();

            if (playing && gameTimer % 10 == 0)
            {
                var nextCells = new int[cells.GetLength(0), cells.GetLength(1)];

                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    for (int y = 0; y < cells.GetLength(1); y++)
                    {
                        if (nextCells[x, y] == 0 && CountNeighbors(x, y) == 1)
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

            lastKeyboard = Keyboard.GetState();
        }

        protected override void Draw(On.Terraria.Main.orig_DoDraw orig, Main self, GameTime gameTime)
        {
            base.Draw(orig, self, gameTime);
            Main.graphics.GraphicsDevice.Clear(Color.White);

            var gameMatrix = Matrix.CreateScale(1) * Matrix.CreateTranslation(new Vector3(0, 0, 0));

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, gameMatrix);

            for (int x = 0; x < cells.GetLength(0); x++)
            {
                // Draw Grid x
                //Main.spriteBatch.Draw(Main.magicPixel, new Rectangle(x * cellWidth, 0, 1, cellWidth * cells.GetLength(1)), Color.Black);
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    // Draw Grid y
                    //Main.spriteBatch.Draw(Main.magicPixel, new Rectangle(0, y * cellWidth, cellWidth * cells.GetLength(0), 1), Color.Black);

                    // Draw Cells
                    if (cells[x, y] == 1)
                    {
                        Main.spriteBatch.Draw(Main.magicPixel, new Rectangle(x * cellWidth, y * cellWidth, cellWidth, cellWidth), Color.Black);
                    }
                }
            }


            // Draw Border
            Main.spriteBatch.Draw(Main.magicPixel, new Rectangle(0, 0, cellWidth * cells.GetLength(0), 1), Color.Black);
            Main.spriteBatch.Draw(Main.magicPixel, new Rectangle(0, 0, 1, cellWidth * cells.GetLength(0)), Color.Black);
            Main.spriteBatch.Draw(Main.magicPixel, new Rectangle(0, cellWidth * cells.GetLength(0), cellWidth * cells.GetLength(0), 1), Color.Black);
            Main.spriteBatch.Draw(Main.magicPixel, new Rectangle(cellWidth * cells.GetLength(0), 0, 1, cellWidth * cells.GetLength(0)), Color.Black);

            // Draw Cross
            Main.spriteBatch.Draw(Main.magicPixel, new Rectangle(0, cellWidth * cells.GetLength(0) / 2, cellWidth * cells.GetLength(0), 1), Color.Red);
            Main.spriteBatch.Draw(Main.magicPixel, new Rectangle(cellWidth * cells.GetLength(1) / 2, 0, 1, cellWidth * cells.GetLength(0)), Color.Red);

            // Draw Mouse
            if (!playing && MouseWithinBounds)
            {
                Main.spriteBatch.Draw(Main.magicPixel, new Rectangle(Mouse.X - Mouse.X % cellWidth, Mouse.Y - Mouse.Y % cellWidth, cellWidth, cellWidth), Color.Gray); 
            }

            Main.spriteBatch.End();
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
            if (!lastKeyboard.IsKeyDown(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                playing = !playing;
            }
            if (!lastKeyboard.IsKeyDown(Keys.R) && Keyboard.GetState().IsKeyDown(Keys.R))
            {
                cells = new int[cells.GetLength(0), cells.GetLength(1)];
            }

            if (!playing && MouseWithinBounds)
            {
                if (Mouse.LeftButton == ButtonState.Pressed)
                {
                    cells[(Mouse.X - Mouse.X % cellWidth) / cellWidth, (Mouse.Y - Mouse.Y % cellWidth) / cellWidth] = 1;
                }
                else if (Mouse.RightButton == ButtonState.Pressed)
                {
                    cells[(Mouse.X - Mouse.X % cellWidth) / cellWidth, (Mouse.Y - Mouse.Y % cellWidth) / cellWidth] = 0;
                }
            }
        }
    }
}
