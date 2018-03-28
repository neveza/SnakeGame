using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TextureGenerator textureGen;
        Random random;

        Snake snake;
        Walls walls;
        Food food;

        int foodToEat;

        void CheckCollision()
        {
            var headRect = snake.segmentsDictionary["Head"].rect;
            var headPos = snake.segmentsDictionary["Head"].Position;

            foreach (var key in snake.segmentsDictionary.Keys)
            {
                if (key != "Head")
                {
                    if (snake.segmentsDictionary[key].Position == headPos)
                    {
                        snake.isDead = true;
                        
                    }
                }
            }

            var wallList = walls.DrawOrder();

           foreach (var wall in wallList)
           {
                    if (headRect.Intersects(wall.rect))
                    {
                        snake.isDead = true;
                    }
                if (food.foodblock.Position == wall.Position)
                {
                    food.createFood();
                }
           }


            if (headRect.Intersects(food.foodblock.rect))
            {
                food.isEaten = true;
                snake.SnakeAteCounter++;
                snake.addSegment();
            }

        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            random = new Random();
            textureGen = new TextureGenerator(GraphicsDevice);
            snake = new Snake(textureGen, 1, 1);
            walls = new Walls(textureGen, 1, 1);
            food = new Food(random, textureGen, 2, 2);
            food.createFood();
            foodToEat = 1;

            walls.addWalls("North", Color.Brown, 0, 0, 100, 0);
            walls.addWalls("South", Color.Green, 0,0 , 0, 100);
            walls.addWalls("West", Color.Pink, 0, 59, 100, 59);
            walls.addWalls("East", Color.Orange, 99, 0, 99, 60);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            snake.Movement();

            CheckCollision();

            food.createFood();
            

            if (foodToEat == snake.SnakeAteCounter)
            {
                var x = random.Next(1, 99);
                var y = random.Next(1, 60);
                var addToEat = random.Next(1, 6);
                var headPosition = snake.segmentsDictionary["Head"].Position;
                var distance = headPosition - new Vector2(x, y);
                //Console.WriteLine(distance);
                if (Math.Abs(distance.X) < 2 || Math.Abs(distance.Y) < 2)
                {
                    walls.addWalls("Random", Color.Black, x, y);
                }
                else
                {
                    x = x + random.Next(4, 9);
                    y = y + random.Next(4, 9);
                    walls.addWalls("Random", Color.Black, x, y);
                }

                foodToEat = foodToEat + addToEat;
            }

            if (snake.isDead)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    snake.isDead = false;
                    snake = new Snake(textureGen, 1, 1);
                    walls.DeleteWalls("Random");
                    foodToEat = 1;
                    snake.SnakeAteCounter = 0;
                }
            }



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var SnakeDrawList = snake.DrawOrder();
            var WallDrawList = walls.DrawOrder();
 
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateScale(8f));

            foreach (var wall in WallDrawList)
            {
                spriteBatch.Draw(wall.Texture, wall.rect, wall.Color);
            }


            spriteBatch.Draw(food.foodblock.Texture, food.foodblock.rect, food.foodblock.Color);

            if (snake.isDead != true)
            {
                foreach (var segment in SnakeDrawList)
                {
                    spriteBatch.Draw(segment.Texture, segment.rect, segment.Color);
                }
            }
            
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
