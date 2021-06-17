using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Graphics;

namespace ArcadeTerraria.Games.Breakout
{
    class BreakoutGame : TerrariaGame
    {
        public override string Name => "Breakout";

        private Texture2D brik;
        private byte[,] blocks;
        private Rectangle platform;
        private Vector2 ballPos;
        private Vector2 ballAngle;
        private int points;
        private int lives;

        internal override void Load()
        {
            base.Load();

            blocks = new byte[15, 6];
            points = 0;
            platform = new Rectangle((screenWidth / 2 - 50) / scale, (screenHeight / scale) - 20, 50, 10);
            ballPos = new Vector2(platform.Center.X + Main.rand.Next(-5, 5), platform.Y);
            ballAngle = ballPos - platform.Center.ToVector2();
            ballAngle.Normalize();
            lives = 3;

            brik = ModContent.GetTexture("ArcadeTerraria/Assets/brik");

            // fill blocks
            for (int x = 0; x < blocks.GetLength(0); x++)
            {
                for (int y = 0; y < blocks.GetLength(1); y++)
                {
                    blocks[x, y] = 1;
                }
            }
        }

        protected override void Unload()
        {
            base.Unload();

            blocks = null;
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (lives <= 0)
            {
                EndGame();
                return;
            }
            if (points == blocks.GetLength(0) * blocks.GetLength(1))
            {
                WinGame();
                return;
            }

            // platform movement
            if (Keyboard.IsKeyDown(Keys.A))
            {
                platform.X -= 4;
            }
            if (Keyboard.IsKeyDown(Keys.D))
            {
                platform.X += 4;
            }

            platform.X = (int)MathHelper.Clamp(platform.X, 0, (screenWidth / scale) - 50);

            // hit platform
            if (new Rectangle((int)ballPos.X, (int)ballPos.Y, 10, 10).Intersects(platform))
            {
                // mirror vertical angle
                ballAngle.Y = -ballAngle.Y;
                ballAngle += (ballPos - platform.Center.ToVector2()) / 5;
                ballAngle.Normalize();

                // if the ball is moving to the left it must hit the right side to get mirrored, vice versa.
                if (ballAngle.X < 0)
                {
                    if (ballPos.X > platform.Center.X)
                    {
                        ballAngle.X = -ballAngle.X; // mirror horizontal angle
                    }
                }
                else
                {
                    if (ballPos.X < platform.Center.X)
                    {
                        ballAngle.X = -ballAngle.X; // mirror horizontal angle
                    }
                }
            }

            // hit vertical wall
            if (ballPos.X < 0 || ballPos.X > screenWidth / scale || ballPos.X + 10 < 0 || ballPos.X + 10 > screenWidth / scale)
            {
                ballAngle.X = -ballAngle.X;
            }
            // hit horizontal wall
            if (ballPos.Y < 0 || ballPos.Y > screenHeight / scale)
            {
                ballAngle.Y = -ballAngle.Y;
            }
            if (ballPos.Y > screenHeight / scale)
            {
                lives--;
            }

            // hit block
            for (int x = 0; x < blocks.GetLength(0); x++)
            {
                for (int y = 0; y < blocks.GetLength(1); y++)
                {
                    if (blocks[x, y] == 1)
                    {
                        var blockRect = new Rectangle(x * 16, y * 8, 16, 8);
                        var ballRect = new Rectangle((int)ballPos.X, (int)ballPos.Y, 10, 10);
                        if (ballRect.Intersects(blockRect))
                        {
                            blocks[x, y] = 0;
                            ballAngle.Y = -ballAngle.Y;
                            points++;

                            goto updatePos;
                        }
                    }
                }
            }

            updatePos:
            ballPos += ballAngle * MathHelper.Clamp((float)Math.Sqrt((points + 2) / 5), 1, 10);
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Draw blocks
            for (int x = 0; x < blocks.GetLength(0); x++)
            {
                for (int y = 0; y < blocks.GetLength(1); y++)   
                {
                    if (blocks[x, y] != 0)
                        spriteBatch.Draw(brik, new Rectangle(x * 16, y * 8, 16, 8), GetColor(y));
                }
            }

            spriteBatch.Draw(Main.magicPixel, platform, Color.Black);
            spriteBatch.Draw(Main.magicPixel, new Rectangle((int)ballPos.X, (int)ballPos.Y, 10, 10), Color.Black);

            // debug draw
            /*float ankathete = ballPos.X - platform.Center.X;
            float gegenkathete = ballPos.Y - platform.Center.Y;
            double hypotenuse = Math.Sqrt(ankathete * ankathete + gegenkathete * gegenkathete);

            spriteBatch.Draw(Main.magicPixel, new Rectangle(platform.Center.X, platform.Center.Y, (int)ankathete, 1), Color.Blue);
            spriteBatch.Draw(Main.magicPixel, new Rectangle((int)ballPos.X, platform.Center.Y, 1, (int)gegenkathete), Color.Red);
            spriteBatch.Draw(Main.magicPixel, new Rectangle(platform.Center.X, platform.Center.Y, (int)hypotenuse, 1), null, Color.Green, (ballPos - platform.Center.ToVector2()).ToRotation(), Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(Main.magicPixel, new Rectangle((int)ballPos.X, (int)ballPos.Y, 10, 1), null, Color.HotPink, ballAngle.ToRotation(), Vector2.Zero, SpriteEffects.None, 0);*/
        }

        internal override void DrawText(SpriteBatch spriteBatch)
        {

            //spriteBatch.DrawString(Main.fontMouseText, $"angle: {ballAngle}\npos: {ballPos}", new Vector2(screenHeight / 3 - 50, screenHeight - 250), Color.Black);
            spriteBatch.DrawString(Main.fontMouseText, $"points: {points}", new Vector2(0, screenHeight - 20), Color.Black);

            for (int i = 0; i < lives; i++)
            {
                spriteBatch.Draw(Main.itemTexture[ItemID.Heart], new Rectangle(i * Main.itemTexture[ItemID.Heart].Width + i * 3, screenHeight - 40, Main.itemTexture[ItemID.Heart].Width, Main.itemTexture[ItemID.Heart].Height), Color.White);
            }
            base.DrawText(spriteBatch);
        }

        private Color GetColor(int layer)
        {
            switch (layer)
            {
                case 0: return Color.Red;
                case 1: return Color.DarkOrange;
                case 2: return Color.Orange;
                case 3: return Color.Yellow;
                case 4: return Color.GreenYellow;
                case 5: return Color.Blue;
                default: return Color.White;
            }
        }
    }
}
