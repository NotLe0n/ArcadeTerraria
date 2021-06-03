using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeTerraria.Games.Tetris
{
    public enum TetrominoID
    {
        I, J, L, O, S, T, Z
    }

    public class Tetromino
    {
        private readonly TetrominoID id;
        private Point pos;
        public Block[][] blocks;
        public bool dontAddToArray = false;
        public int rotation;

        public Tetromino(TetrominoID tetrominoID)
        {
            id = tetrominoID;
            pos = new Point(4, 1);

            ConstructTetromino();
        }

        public void Update()
        {
            // Move down
            if (pos.Y < TetrisGame.blocks.GetLength(1) - 1)
            {
                pos.Y += 1;
            }

            DestroyTetromino();
            ConstructTetromino();

            if (!dontAddToArray)
            {
                foreach (var block in blocks[rotation])
                {
                    TetrisGame.blocks[block.cellPosition.X, block.cellPosition.Y] = block;
                
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var block in blocks)
            {
                block[rotation].Draw(spriteBatch);
            }
        }

        private void ConstructTetromino()
        {
            switch (id)
            {
                case TetrominoID.I:
                    blocks = new Block[][]
                    {
                        new Block[]
                        {
                            new Block(new Point(pos.X - 1, pos.Y), Color.Aqua),
                            new Block(pos, Color.Aqua),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Aqua),
                            new Block(new Point(pos.X + 2, pos.Y), Color.Aqua)
                        },
                        new Block[]
                        {
                            new Block(new Point(pos.X, pos.Y - 1), Color.Aqua),
                            new Block(pos, Color.Aqua),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Aqua),
                            new Block(new Point(pos.X, pos.Y + 2), Color.Aqua)
                        },
                        new Block[]
                        {
                            new Block(new Point(pos.X - 1, pos.Y), Color.Aqua),
                            new Block(pos, Color.Aqua),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Aqua),
                            new Block(new Point(pos.X + 2, pos.Y), Color.Aqua)
                        },
                        new Block[]
                        {
                            new Block(new Point(pos.X, pos.Y - 1), Color.Aqua),
                            new Block(pos, Color.Aqua),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Aqua),
                            new Block(new Point(pos.X, pos.Y + 2), Color.Aqua)
                        },
                    };
                    break;
                case TetrominoID.J:
                    blocks = new Block[][]
                    {
                        new Block[]
                        {
                            new Block(new Point(pos.X, pos.Y - 1), Color.Blue),
                            new Block(pos, Color.Blue),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Blue),
                            new Block(new Point(pos.X + 2, pos.Y), Color.Blue)
                        },
                        new Block[]
                        {
                            new Block(new Point(pos.X + 1, pos.Y), Color.Blue),
                            new Block(pos, Color.Blue),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Blue),
                            new Block(new Point(pos.X, pos.Y + 2), Color.Blue)
                        },
                        new Block[]
                        {
                            new Block(new Point(pos.X, pos.Y + 1), Color.Blue),
                            new Block(pos, Color.Blue),
                            new Block(new Point(pos.X - 1, pos.Y), Color.Blue),
                            new Block(new Point(pos.X - 2, pos.Y), Color.Blue)
                        },
                        new Block[]
                        {
                            new Block(new Point(pos.X - 1, pos.Y), Color.Blue),
                            new Block(pos, Color.Blue),
                            new Block(new Point(pos.X, pos.Y - 1), Color.Blue),
                            new Block(new Point(pos.X, pos.Y - 2), Color.Blue)
                        }
                    };
                    break;
                case TetrominoID.L:
                    blocks = new Block[][]
                    {
                        new Block[]
                        {
                            new Block(new Point(pos.X - 1, pos.Y), Color.Orange),
                            new Block(pos, Color.Orange),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Orange),
                            new Block(new Point(pos.X + 1, pos.Y - 1), Color.Orange)
                        },
                        new Block[]
                        {
                            new Block(new Point(pos.X, pos.Y - 1), Color.Orange),
                            new Block(pos, Color.Orange),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Orange),
                            new Block(new Point(pos.X + 1, pos.Y + 1), Color.Orange)
                        },
                        new Block[]
                        {
                            new Block(new Point(pos.X + 1, pos.Y), Color.Orange),
                            new Block(pos, Color.Orange),
                            new Block(new Point(pos.X - 1, pos.Y), Color.Orange),
                            new Block(new Point(pos.X - 1, pos.Y + 1), Color.Orange)
                        },
                        new Block[]
                        {
                            new Block(new Point(pos.X, pos.Y + 1), Color.Orange),
                            new Block(pos, Color.Orange),
                            new Block(new Point(pos.X, pos.Y - 1), Color.Orange),
                            new Block(new Point(pos.X - 1, pos.Y - 1), Color.Orange)
                        }
                    };
                    break;
                case TetrominoID.O:
                    blocks = new Block[][]
                    {
                        new Block[]
                        {
                            new Block(pos, Color.Yellow),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Yellow),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Yellow),
                            new Block(new Point(pos.X + 1, pos.Y + 1), Color.Yellow)
                        },
                        new Block[]
                        {
                            new Block(pos, Color.Yellow),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Yellow),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Yellow),
                            new Block(new Point(pos.X + 1, pos.Y + 1), Color.Yellow)
                        },
                        new Block[]
                        {
                            new Block(pos, Color.Yellow),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Yellow),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Yellow),
                            new Block(new Point(pos.X + 1, pos.Y + 1), Color.Yellow)
                        },
                        new Block[]
                        {
                            new Block(pos, Color.Yellow),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Yellow),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Yellow),
                            new Block(new Point(pos.X + 1, pos.Y + 1), Color.Yellow)
                        },
                    };
                    break;
                case TetrominoID.S:
                    blocks = new Block[][]
                    {
                        new Block[]
                        {
                            new Block(pos, Color.Green),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Green),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Green),
                            new Block(new Point(pos.X - 1, pos.Y + 1), Color.Green)
                        },
                        new Block[]
                        {
                            new Block(pos, Color.Green),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Green),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Green),
                            new Block(new Point(pos.X - 1, pos.Y + 1), Color.Green)
                        },
                        new Block[]
                        {
                            new Block(pos, Color.Green),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Green),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Green),
                            new Block(new Point(pos.X - 1, pos.Y + 1), Color.Green)
                        },
                        new Block[]
                        {
                            new Block(pos, Color.Green),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Green),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Green),
                            new Block(new Point(pos.X - 1, pos.Y + 1), Color.Green)
                        },
                    };
                    break;
                case TetrominoID.T:
                    blocks = new Block[][]
                    {
                        new Block[]
                        {
                            new Block(pos, Color.Purple),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Purple),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Purple),
                            new Block(new Point(pos.X - 1, pos.Y), Color.Purple)
                        },
                        new Block[]
                        {
                            new Block(pos, Color.Purple),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Purple),
                            new Block(new Point(pos.X, pos.Y - 1), Color.Purple),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Purple)
                        },
                        new Block[]
                        {
                            new Block(pos, Color.Purple),
                            new Block(new Point(pos.X, pos.Y - 1), Color.Purple),
                            new Block(new Point(pos.X + 1, pos.Y), Color.Purple),
                            new Block(new Point(pos.X - 1, pos.Y), Color.Purple)
                        },
                        new Block[]
                        {
                            new Block(pos, Color.Purple),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Purple),
                            new Block(new Point(pos.X, pos.Y - 1), Color.Purple),
                            new Block(new Point(pos.X - 1, pos.Y), Color.Purple)
                        },
                    };
                    break;
                case TetrominoID.Z:
                    blocks = new Block[][]
                    {
                        new Block[]
                        {
                            new Block(pos, Color.Red),
                            new Block(new Point(pos.X - 1, pos.Y), Color.Red),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Red),
                            new Block(new Point(pos.X + 1, pos.Y + 1), Color.Red)
                        },
                        new Block[]
                        {
                            new Block(pos, Color.Red),
                            new Block(new Point(pos.X - 1, pos.Y), Color.Red),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Red),
                            new Block(new Point(pos.X + 1, pos.Y + 1), Color.Red)
                        },
                        new Block[]
                        {
                            new Block(pos, Color.Red),
                            new Block(new Point(pos.X - 1, pos.Y), Color.Red),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Red),
                            new Block(new Point(pos.X + 1, pos.Y + 1), Color.Red)
                        },
                        new Block[]
                        {
                            new Block(pos, Color.Red),
                            new Block(new Point(pos.X - 1, pos.Y), Color.Red),
                            new Block(new Point(pos.X, pos.Y + 1), Color.Red),
                            new Block(new Point(pos.X + 1, pos.Y + 1), Color.Red)
                        },
                    };
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Destroys all blocks in the Tetromino
        /// </summary>
        public void DestroyTetromino()
        {
            foreach (var block in blocks[rotation])
            {
                block.Destroy();
            }
        }

        /// <summary>
        /// Moves the Tetromino to the desired direction
        /// </summary>
        /// <param name="direction">1 for right, -1 for left</param>
        public void MoveHorizontally(int direction)
        {
            // all blocks are within the horizontal boundaries of the level
            bool withinBoundaries = blocks[rotation].All(x => x.cellPosition.X + direction >= 0 && x.cellPosition.X + direction < TetrisGame.blocks.GetLength(0) - 1);

            // there are no blocks in the direction you want to move and those blocks are not your own
            bool noBlocksInDirection = blocks[rotation].Any(x => TetrisGame.blocks[x.cellPosition.X + direction, x.cellPosition.Y] == null
                && !blocks[rotation].Contains(TetrisGame.blocks[x.cellPosition.X + direction, x.cellPosition.Y]));

            if (withinBoundaries && noBlocksInDirection)
            {
                pos.X += direction;
            }
        }
    }
}
