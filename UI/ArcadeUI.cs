using ArcadeTerraria.Games;
using Terraria.UI;

namespace ArcadeTerraria.UI
{
    public class ArcadeUI : UIState
    {
        public TerrariaGame game;

        public ArcadeUI(TerrariaGame terrariaGame)
        {
            game = terrariaGame;
        }

        public override void OnInitialize()
        {
            game.Load();

            var panel = new DragableUIPanel(game.Name);
            panel.Width.Set(500, 0);
            panel.Height.Set(500, 0);
            panel.Left.Set(600, 0);
            panel.Top.Set(400, 0);
            panel.OnCloseBtnClicked += () => game.EndGame();
            Append(panel);

            var screen = new GameScreen(game);
            screen.Top.Set(35, 0);
            screen.Left.Set(10, 0);
            screen.Width.Set(500, 0);
            screen.Height.Set(455, 0);
            panel.Append(screen);

            base.OnInitialize();
        }
    }
}
