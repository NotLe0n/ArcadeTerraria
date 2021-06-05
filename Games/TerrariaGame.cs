using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;

namespace ArcadeTerraria.Games
{
    public class TerrariaGame
    {
        public static int gameTimer = 0;
        public static int drawTimer = 0;
        public static int screenHeight;
        public static int screenWidth;
        protected MouseState Mouse => Microsoft.Xna.Framework.Input.Mouse.GetState();

        internal void Load()
        {
            Initialize();
            LoadContent();

            Main.musicVolume = 0;
            On.Terraria.Main.DoUpdate += Update;
            On.Terraria.Main.DoDraw += Draw;
        }

        protected virtual void Initialize() { }

        protected virtual void LoadContent() { }

        protected virtual void Draw(On.Terraria.Main.orig_DoDraw orig, Main self, GameTime gameTime) 
        {
            drawTimer++;
        }

        protected virtual void Update(On.Terraria.Main.orig_DoUpdate orig, Main self, GameTime gameTime) 
        {
            gameTimer++;

            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        }
    }
}
