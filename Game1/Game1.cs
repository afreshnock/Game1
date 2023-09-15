using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private List<Spawner> spawners = new List<Spawner>();
        private Texture2D background;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.PreferredBackBufferWidth = 1920;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D texture = Content.Load<Texture2D>("coral");
            Texture2D jellyTexture = Content.Load<Texture2D>("jelly2");
            background = Content.Load<Texture2D>("sea"); 
            spawners.Add(new Spawner(new Vector2(300, 575), ColorArray.RandomColor(),texture,jellyTexture , 1.5f));
            spawners.Add(new Spawner(new Vector2(800, 750), ColorArray.RandomColor(), texture, jellyTexture, 2));
            spawners.Add(new Spawner(new Vector2(1700, 550), ColorArray.RandomColor(), texture, jellyTexture,1));
            player = new Player(new Vector2(100, 500), ColorArray.RandomColor(),1);
            player.LoadContent(Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach(Spawner spawner in spawners)
            {
                spawner.Update(gameTime);
                List<Jelly> trash = new List<Jelly>();
                foreach(Jelly j in spawner.jellies)
                {
                    if (player.bounds.CollidesWith(j.bounds))
                    {
                        trash.Add(j);
                    }
                }
                foreach(Jelly j in trash)
                {
                    spawner.jellies.Remove(j);
                    player.color = j.color;
                }
            }
            player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            // TODO: Add your drawing code here
            foreach(Spawner spawner in spawners)
            {
                spawner.Draw(_spriteBatch);
            }
            player.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}