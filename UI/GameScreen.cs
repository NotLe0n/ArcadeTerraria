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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(Main.magicPixel, new Rectangle((int)game.drawPosition.X, (int)game.drawPosition.Y, game.screenWidth, game.screenHeight), game.backgroundColor);

            spriteBatch.End();

            var gameMatrix = Matrix.CreateScale(game.scale) * Matrix.CreateTranslation(new Vector3(game.drawPosition, 0));
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, gameMatrix);

            game.drawPosition = GetDimensions().Position();
            game.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.CreateTranslation(new Vector3(game.drawPosition, 0)));

            game.DrawText(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            game.Update(gameTime);

            game.UpdateInput();
        }
    }
}
