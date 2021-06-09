using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ID;

namespace ArcadeTerraria.Games
{
    public class TerrariaGame
    {
        public virtual string Name { get; }
        public byte scale = 3;

        private bool gameEnd;
        public int gameTimer;
        public int drawTimer;
        public int screenHeight;
        public int screenWidth;
        public Vector2 drawPosition;
        public Color backgroundColor = Color.White;

        protected KeyboardState lastKeyboard;
        protected MouseState lastMouse;
        public MouseState Mouse => Microsoft.Xna.Framework.Input.Mouse.GetState();
        public Point MousePos => new Point(Main.mouseX - (int)drawPosition.X, Main.mouseY - (int)drawPosition.Y);

        internal virtual void Load()
        {
            gameTimer = 0;
            drawTimer = 0;
        }

        protected virtual void Unload()
        {
        }

        protected virtual void DrawFullscreen(On.Terraria.Main.orig_DoDraw orig, Main self, GameTime gameTime)
        {
            if (gameEnd)
            {
                orig(self, gameTime);
                return;
            }

            Main.graphics.GraphicsDevice.Clear(Color.White);

            Draw(Main.spriteBatch);

            Main.spriteBatch.End();
        }

        internal virtual void Update(GameTime gameTime)
        {
            gameTimer++;
            Main.player[Main.myPlayer].frozen = true;

            lastMouse = Mouse;
            lastKeyboard = Main.keyState;
        }

        internal virtual void Draw(SpriteBatch spriteBatch)
        {
            drawTimer++;
        }

        internal virtual void DrawText(SpriteBatch spriteBatch)
        {

        }

        public void EndGame()
        {
            gameEnd = true;
            ArcadeTerraria.ArcadeUserInterface.SetState(null);
            Main.PlaySound(SoundID.MenuClose);

            Unload();
        }
    }
}
