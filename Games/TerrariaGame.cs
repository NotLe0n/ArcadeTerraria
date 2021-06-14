using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ID;
using System;

namespace ArcadeTerraria.Games
{
    public class TerrariaGame
    {
        public virtual string Name { get; }
        public byte scale = 3;
        public Color backgroundColor = Color.White;

        protected int gameTimer;
        protected int drawTimer;
        public int screenHeight;
        public int screenWidth;
        public Vector2 drawPosition;

        // input stuff
        protected MouseState Mouse => Microsoft.Xna.Framework.Input.Mouse.GetState();
        protected KeyboardState Keyboard => Microsoft.Xna.Framework.Input.Keyboard.GetState();
        protected KeyboardState lastKeyboard;
        protected MouseState lastMouse;
        protected Point MousePos => new Point(Main.mouseX - (int)drawPosition.X, Main.mouseY - (int)drawPosition.Y);

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
        }

        internal void UpdateInput()
        {
            lastMouse = Mouse;
            lastKeyboard = Keyboard;
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

        protected void WinGame()
        {
            OnWinGame?.Invoke();

            EndGame();
        }

        public event Action OnWinGame;
    }
}
