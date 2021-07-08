using ArcadeTerraria.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace ArcadeTerraria.UI
{
    class GameScreen : UIElement
    {
        public TerrariaGame game;

        public GameScreen(TerrariaGame terrariaGame)
        {
            game = terrariaGame;
        }

        public override void OnInitialize()
        {
            game.screenWidth = (int)GetDimensions().Width;
            game.screenHeight = (int)GetDimensions().Height;

            game.Load();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // set drawposition to the position of the UIPanel
            game.drawPosition = GetDimensions().Position();

            spriteBatch.Draw(Main.magicPixel, new Rectangle((int)game.drawPosition.X, (int)game.drawPosition.Y, game.screenWidth, game.screenHeight), game.backgroundColor);

            spriteBatch.End();

            var gameMatrix = Matrix.CreateScale(game.scale) * Matrix.CreateTranslation(new Vector3(game.drawPosition, 0));
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, gameMatrix);

            game.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.CreateTranslation(new Vector3(game.drawPosition, 0)));

            game.DrawText(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();
        }

        uint lastGameUpdateCount = 0;
        public override void Update(GameTime gameTime)
        {
            if (Main.GameUpdateCount != lastGameUpdateCount)
            {
                game.Update(gameTime);

                game.UpdateInput();
            }

            base.Update(gameTime);

            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            lastGameUpdateCount = Main.GameUpdateCount;
        }
    }
}
