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

        public int gameTimer;
        public int drawTimer;
        public int screenHeight;
        public int screenWidth;
        public Vector2 drawPosition;
        public Color backgroundColor = Color.White;

        // input stuff
        public MouseState Mouse => Microsoft.Xna.Framework.Input.Mouse.GetState();
        public KeyboardState Keyboard => Main.keyState;
        protected KeyboardState lastKeyboard;
        protected MouseState lastMouse;
        public Point MousePos => new Point(Main.mouseX - (int)drawPosition.X, Main.mouseY - (int)drawPosition.Y);

        internal virtual void Load()
        {
            gameTimer = 0;
            drawTimer = 0;
        }

        protected virtual void Unload()
        {
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
            ArcadeTerraria.ArcadeUserInterface.SetState(null);
            Main.PlaySound(SoundID.MenuClose);

            Unload();
        }
    }
}
