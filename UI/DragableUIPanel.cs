using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace ArcadeTerraria.UI
{
    public class DragableUIPanel : UIPanel
    {
        public bool active = false;
        public event Action OnCloseBtnClicked;
        private string _headingText;

        public DragableUIPanel(string headingText)
        {
            _headingText = headingText;
        }

        public override void OnInitialize()
        {
            SetPadding(0);

            var header = new UIPanel();
            header.SetPadding(0);
            header.Width.Set(0, 1f);
            header.Height.Set(30, 0f);
            header.BackgroundColor.A = 255;
            header.OnMouseDown += Header_OnMouseDown;
            header.OnMouseUp += Header_OnMouseUp;
            Append(header);

            var heading = new UIText(_headingText, 0.9f);
            heading.Top.Set(0, 0.25f);
            heading.Left.Set(16, 0);
            header.Append(heading);

            var closeBtn = new UITextPanel<char>('X');
            closeBtn.SetPadding(7);
            closeBtn.Width.Set(40, 0);
            closeBtn.Left.Set(-40, 1f);
            closeBtn.BackgroundColor.A = 255;
            closeBtn.OnClick += (evt, elm) => OnCloseBtnClicked?.Invoke();
            header.Append(closeBtn);
        }
        #region Drag code yoiked from ExampleMod 

        private Vector2 offset;
        public bool dragging;
        public Vector2 lastPos;

        public void Header_OnMouseDown(UIMouseEvent evt, UIElement elm)
        {
            base.MouseDown(evt);
            offset = evt.MousePosition - GetDimensions().Position();
            dragging = true;
        }

        public void Header_OnMouseUp(UIMouseEvent evt, UIElement elm)
        {
            base.MouseUp(evt);
            dragging = false;

            Left.Set(evt.MousePosition.X - offset.X, 0f);
            Top.Set(evt.MousePosition.Y - offset.Y, 0f);
            Recalculate();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); // don't remove.

            // Checking ContainsPoint and then setting mouseInterface to true is very common. This causes clicks on this UIElement to not cause the player to use current items. 
            if (ContainsPoint(Main.MouseScreen))
                Main.LocalPlayer.mouseInterface = true;


            if (dragging)
            {
                Left.Set(Main.mouseX - offset.X, 0f);
                Top.Set(Main.mouseY - offset.Y, 0f);
                Recalculate();

                lastPos = GetDimensions().Position();
            }

            // Here we check if the DragableUIPanel is outside the Parent UIElement rectangle. 
            // (In our example, the parent would be ExampleUI, a UIState. This means that we are checking that the DragableUIPanel is outside the whole screen)
            // By doing this and some simple math, we can snap the panel back on screen if the user resizes his window or otherwise changes resolution.
            var parentSpace = Parent.GetDimensions().ToRectangle();
            if (!GetDimensions().ToRectangle().Intersects(parentSpace))
            {
                Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
                Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
                // Recalculate forces the UI system to do the positioning math again.
                Recalculate();
            }
        }
        #endregion
    }
}
