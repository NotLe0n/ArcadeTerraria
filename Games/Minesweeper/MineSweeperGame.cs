using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Terraria;

namespace ArcadeTerraria.Games.Minesweeper
{
    public class MineSweeperGame : TerrariaGame
    {
        public static int scale = 4;
        public static Cell[,] cells;
        public static bool lose;

        private MouseState lastMouse;
        private bool MouseWithinBounds => Mouse.X > 0 && Mouse.Y > 0 && Mouse.X < cells.GetLength(0) * 10 * scale && Mouse.Y < cells.GetLength(1) * 10 * scale;
        private Point MouseCellPos => new Point(
            (Mouse.X - Mouse.X % 10) / 10 / scale,
            (Mouse.Y - Mouse.Y % 10) / 10 / scale);

        protected override void Initialize()
        {
            base.Initialize();

            cells = new Cell[10, 10];

            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cells[x, y] = new Cell(x, y);
                }
            }
        }

        protected override void Update(On.Terraria.Main.orig_DoUpdate orig, Main self, GameTime gameTime)
        {
            if (GameWon)
            {
                orig(self, gameTime);
                return;
            }

            base.Update(orig, self, gameTime);

            if (lastMouse.LeftButton != ButtonState.Pressed && Mouse.LeftButton == ButtonState.Pressed)
            {
                if (MouseWithinBounds)
                {
                    cells[MouseCellPos.X, MouseCellPos.Y].Click();
                }
            }
            if (lastMouse.RightButton != ButtonState.Pressed && Mouse.RightButton == ButtonState.Pressed)
            {
                if (MouseWithinBounds)
                {
                    cells[MouseCellPos.X, MouseCellPos.Y].ToggleFlag();
                }
            }
            lastMouse = Mouse;
        }

        protected override void Draw(On.Terraria.Main.orig_DoDraw orig, Main self, GameTime gameTime)
        {
            if (GameWon)
            {
                orig(self, gameTime);
                return;
            }

            base.Draw(orig, self, gameTime);

            Main.graphics.GraphicsDevice.Clear(Color.White);

            var gameMatrix = Matrix.CreateScale(scale) * Matrix.CreateTranslation(new Vector3(0, 0, 0));

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, gameMatrix);

            foreach (var cell in cells)
            {
                cell.Draw(Main.spriteBatch);
            }
            foreach(var cell in cells)
            {
                cell.DrawNum(Main.spriteBatch);
            }

            if (MouseWithinBounds)
            {
                Main.spriteBatch.Draw(Main.magicPixel, new Rectangle(MouseCellPos.X * 10 + 1, MouseCellPos.Y * 10 + 1, 9, 9), Color.White * 0.5f);
            }

            Main.spriteBatch.End();
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
    }
}
