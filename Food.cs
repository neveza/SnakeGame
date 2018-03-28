using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class FoodBlock
    {
        public Vector2 Position;
        public Texture2D Texture;
        public Color Color;
        public Rectangle rect;

        public void UpdatePosition()
        {
            rect = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public FoodBlock(Texture2D texture, Vector2 position, Color color)
        {
            Position = position;
            Texture = texture;
            Color = color;

            rect = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

        }

    }
    class Food
    {
        public bool isEaten = true;

        private Texture2D _Texture;

        public FoodBlock foodblock;

        Random random;

        public void createFood()
        {
            if (isEaten)
            {
                var x = random.Next(5, 80);
                var y = random.Next(5, 50);

                foodblock = new FoodBlock(_Texture, new Vector2(x, y), Color.Chocolate);

                isEaten = false;
            }
        }

        public Food(Random Random,TextureGenerator TextureGen, int Width, int Height)
        {

            _Texture = TextureGen.CreateTexture(Width, Height, Color.White);

            random = Random;
        }
    }
}
