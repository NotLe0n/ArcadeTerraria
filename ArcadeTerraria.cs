using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ArcadeTerraria
{
    public class ArcadeTerraria : Mod
    {
        internal static UserInterface ArcadeUserInterface;

        public override void Load()
        {
            ArcadeUserInterface = new UserInterface();
            ArcadeUserInterface.SetState(null);

            base.Load();
        }

        public override void Unload()
        {
            ArcadeUserInterface = null;
        }

        private GameTime _lastUpdateUiGameTime;
        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;

            if (ArcadeUserInterface.CurrentState != null)
            {
                ArcadeUserInterface.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ArcadeTerraria: UI",
                    delegate
                    {

                        if (ArcadeUserInterface.CurrentState != null)
                        {
                            ArcadeUserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    }, InterfaceScaleType.UI));
            }
        }
    }
}