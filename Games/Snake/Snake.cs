using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Terraria;

namespace ArcadeTerraria.Games.Snake
{
    public class Snake
    {
        public Rectangle head;
        public List<SnakeBody> body = new List<SnakeBody>();

        public (sbyte x, sbyte y) direction;
        public int speed = 10;

        public Snake(Point position)
        {
            head = new Rectangle(position.X, position.Y, 10, 10);
        }

        public void Update()
        {
            UpdatePostion();

            FoodCollision();

            // Body Collsion
            if (body.Exists(x => x.rect.Contains(head)))
            {
                SnakeGame.lose = true;
            }
        }

        public void UpdateDirection()
        {
            // Update Direction
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (body.Count == 0 || direction.y != 1)
                    direction = (0, -1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (body.Count == 0 || direction.y != -1)
                    direction = (0, 1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (body.Count == 0 || direction.x != 1)
                    direction = (-1, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (body.Count == 0 || direction.x != -1)
                    direction = (1, 0);
            }
        }

        private void UpdatePostion()
        {
            if (body.Count > 0)
            {
                // Update Body Segments
                for (int i = body.Count - 1; i >= 1; i--)
                {
                    body[i].rect.Location = body[i - 1].rect.Location;
                }

                body[0].rect.Location = head.Location;
            }

            // Update Head Position
            head.X += direction.x * speed;
            head.Y += direction.y * speed;

            if (head.X > SnakeGame.screenCellWidth)
            {
                head.X = 0;
            }
            if (head.X < 0)
            {
                head.X = SnakeGame.screenCellWidth;
            }

            if (head.Y > SnakeGame.screenCellHeight)
            {
                head.Y = 0;
            }
            if (head.Y < 0)
            {
                head.Y = SnakeGame.screenCellHeight;
            }
        }

        private void FoodCollision()
        {
            // Food Collision
            if (SnakeGame.food.rect.Contains(head))
            {
                SnakeGame.points++;

                // Add new Body Segment
                if (body.Count > 0)
                {
                    body.Add(new SnakeBody(body[0].rect.Location));
                }
                else
                {
                    body.Add(new SnakeBody((head.Location.ToVector2() - new Vector2(direction.x, direction.y) * speed).ToPoint()));
                }

                GenerateFood();
            }
        }

        private void GenerateFood()
        {
            // Generate new Food
            // "/ 10 * 10" is there to make it divideable by 10
            var randPos = new Point(Main.rand.Next(0, SnakeGame.screenCellWidth) / 10 * 10, Main.rand.Next(0, SnakeGame.screenCellHeight) / 10 * 10);

            if (!body.Exists(x => x.rect.Contains(randPos)) && !head.Contains(randPos))
            {
                SnakeGame.food = new Food(randPos);
            }
            else
            {
                GenerateFood();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.magicPixel, head, Color.Black);

            foreach (var b in body)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
