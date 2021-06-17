using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using System;
using System.Linq;
using Terraria;

namespace ArcadeTerraria.Games.Tetris
{
    public class TetrisGame : TerrariaGame
    {
        public override string Name => "Tetris";

        public static int gameSpeed = 50;

        public static Block[,] blocks;
        private Tetromino currentTetromino;
        private Tetromino nextTetromino;
        public int score;

        internal override void Load()
        {
            nextTetromino = GenerateTetromino();
            blocks = new Block[10, 16];
        }

        protected override void Unload()
        {
            score = 0;
            blocks = null;
        }

        internal override void Update(GameTime gameTime)
        {
            if (score >= 2000)
            {
                EndGame();
                return;
            }

            base.Update(gameTime);

            if (currentTetromino != null)
            {
                UpdateInput2();
            }

            if (gameTimer % gameSpeed == 0)
            {
                // destroy tetromino if lands on floor
                if (currentTetromino != null && currentTetromino.blocks[currentTetromino.rotation].Any(x => x.cellPosition.Y == blocks.GetLength(1) - 1))
                {
                    currentTetromino = null;
                }

                // destroy tetromino if lands on other blocks
                if (currentTetromino != null
                    && currentTetromino.blocks[currentTetromino.rotation].Any(x => blocks[x.cellPosition.X, x.cellPosition.Y + 1] != null
                    && !currentTetromino.blocks[currentTetromino.rotation].Contains(blocks[x.cellPosition.X, x.cellPosition.Y + 1])))
                {
                    currentTetromino = null;

                    if (CheckGameLost()) return;
                }

                if (currentTetromino == null)
                {
                    FallWhenRowIsFull();

                    currentTetromino = nextTetromino;
                    currentTetromino.dontAddToArray = false;

                    nextTetromino = GenerateTetromino();
                    nextTetromino.dontAddToArray = true;
                }

                currentTetromino.Update();
            }
        }

        private void UpdateInput2()
        {
            if (!lastKeyboard.IsKeyDown(Keys.W) && Keyboard.IsKeyDown(Keys.W))
            {
                currentTetromino.rotation = (currentTetromino.rotation + 1) % 4;
            }

            gameSpeed = Keyboard.IsKeyDown(Keys.S) ? 2 : 10;

            if (!lastKeyboard.IsKeyDown(Keys.A) && Keyboard.IsKeyDown(Keys.A))
            {
                currentTetromino.MoveHorizontally(-1);
            }

            if (!lastKeyboard.IsKeyDown(Keys.D) && Keyboard.IsKeyDown(Keys.D))
            {
                currentTetromino.MoveHorizontally(1);
            }

            if (gameTimer % 10 == 0)
            {
                if (Keyboard.IsKeyDown(Keys.A))
                {
                    currentTetromino.MoveHorizontally(-1);
                }

                if (Keyboard.IsKeyDown(Keys.D))
                {
                    currentTetromino.MoveHorizontally(1);
                }
            }
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (var block in blocks)
            {
                if (block != null)
                {
                    block.Draw(spriteBatch);
                }
            }

            // Draw Border
            spriteBatch.Draw(Main.magicPixel, new Rectangle(0, 0, blocks.GetLength(0) * 10, 1), Color.White);
            spriteBatch.Draw(Main.magicPixel, new Rectangle(0, blocks.GetLength(1) * 10, blocks.GetLength(0) * 10, 1), Color.White);
            spriteBatch.Draw(Main.magicPixel, new Rectangle(0, 0, 1, blocks.GetLength(1) * 10), Color.White);
            spriteBatch.Draw(Main.magicPixel, new Rectangle(blocks.GetLength(0) * 10, 0, 1, blocks.GetLength(1) * 10), Color.White);

            nextTetromino.Draw(spriteBatch);
        }

        internal override void DrawText(SpriteBatch spriteBatch)
        {
            // Draw Score
            spriteBatch.DrawString(Main.fontMouseText, "Score: " + score, new Vector2(blocks.GetLength(0) * 10 * 2 + 20, 0), Color.White);
            spriteBatch.DrawString(Main.fontMouseText, "next Tetromino: ", new Vector2(blocks.GetLength(0) * 10 * 2+ 20, 20), Color.White);
        }

        private Tetromino GenerateTetromino()
        {
            int rand = new Random().Next(0, 6);
            return new Tetromino((TetrominoID)rand);
        }

        private bool CheckGameLost()
        {
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                if (blocks[i, 2] != null)
                {
                    EndGame();
                    return true;
                }
            }
            return false;
        }

        private bool IsRowFull(int row)
        {
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                if (blocks[i, row] == null)
                    return false;
            }
            return true;
        }

        private void FallWhenRowIsFull()
        {
            for (int i = blocks.GetLength(1) - 1; i >= 0; i--)
            {
                if (IsRowFull(i))
                {
                    for (int y = blocks.GetLength(1) - 1; y > 0; y--)
                    {
                        for (int x = 0; x < blocks.GetLength(0); x++)
                        {
                            blocks[x, y] = blocks[x, y - 1];
                        }
                        score += blocks.GetLength(0);
                    }

                    FallWhenRowIsFull();
                }
            }
        }
    }
}
