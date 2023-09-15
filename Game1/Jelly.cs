using Game1.Collison;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Jelly
    {
        private Texture2D texture;
        private int frame;
        public Vector2 position;
        private Vector2 velocity;
        private double frameTimer;
        public Color color;
        private float scale;
        public BoundingCircle bounds;

        public Jelly()
        {
            Random r = new Random();
            position = new Vector2((float)r.NextDouble() * 100, (float)r.NextDouble() * 100);
            velocity = new Vector2((float)r.NextDouble() * 50, (float)r.NextDouble() * 50);
            scale = (float)r.NextDouble() * 10;
            bounds  = new BoundingCircle { Center = new Vector2(position.X + (16*scale), position.Y + (16 * scale) ), Radius = 16 * scale };
            frame = 0;
            color = Color.MediumPurple;
        }

        public Jelly(Vector2 position, Vector2 velocity, Color color, float scale,Texture2D texture)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            frame = 0;
            this.scale = scale;
            this.color = color;
            bounds = new BoundingCircle { Center = new Vector2(position.X + (16 * scale), position.Y + (16 * scale)), Radius = 16 * scale };
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("jelly2");
            
        }

        public void Update(GameTime gametime)
        {
            frameTimer += gametime.ElapsedGameTime.TotalSeconds;
            if (frameTimer > .1)
            {
                frame++;
                if (frame == 11)
                {
                    frame = 0;
                }
                position += (float)(gametime.ElapsedGameTime.TotalSeconds) * velocity;
                frameTimer -= .1;
            }
            bounds = new BoundingCircle { Center = new Vector2(position.X + (16 * scale), position.Y + (16 * scale)), Radius = 16 * scale };
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(32 * frame, 0, 32, 32), color , 0f, new Vector2(16,16) * scale, scale , SpriteEffects.None,0);
        }
    }
}
