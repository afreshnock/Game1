using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Game1.Collison;

namespace Game1
{
    public class Player
    {
        private Vector2 posistion;
        private Vector2 velocity;
        private float maxVelocity = 750;
        private float velocityStep = 5;
        private Texture2D texture;
        private float rotation;
        private float lastRotation = 0;
        public BoundingRectangle bounds;
        public Color color;
        private float scale;

        public Player(Vector2 position, Color color, float scale)
        {
            this.posistion = position;
            this.color = color;
            this.scale = scale;
            bounds= new BoundingRectangle((posistion.X + 30) *scale, (posistion.Y+ 40) * scale, 70 * scale, 40 * scale);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("fish");
        }

        public void Update(GameTime gametime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            float dt = (float)gametime.ElapsedGameTime.TotalSeconds;


            //player input
            if (keyboardState.IsKeyDown(Keys.D))
            {
               velocity.X += velocityStep;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                velocity.X -= velocityStep;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                velocity.Y -= velocityStep;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                velocity.Y += velocityStep;
            }
            
            if(velocity.X != 0 && keyboardState.IsKeyUp(Keys.D) && keyboardState.IsKeyUp(Keys.A))
            {
                if(velocity.X > 0) 
                {
                    velocity.X -= velocityStep;
                }
                else // velocity < 0
                {
                    velocity.X += velocityStep;
                }
            }

            if (velocity.Y != 0 && keyboardState.IsKeyUp(Keys.W) && keyboardState.IsKeyUp(Keys.S))
            {
                if (velocity.Y > 0)
                {
                    velocity.Y -= velocityStep;
                }
                else // velocity < 0
                {
                    velocity.Y += velocityStep;
                }
            }

            if(velocity.Y > -velocityStep && velocity.Y < velocityStep)
            {
                velocity.Y = 0;
            }
            if (velocity.X > -velocityStep && velocity.X < velocityStep)
            {
                velocity.X = 0;
            }
            if (velocity.Length() > maxVelocity)
            {
                velocity.Normalize();
                velocity *= maxVelocity;
            }
            posistion += dt * velocity;


            //rotation calculation
            lastRotation = rotation;
            if (velocity.Y != 0 && velocity.X != 0 )
            {
                rotation = (float)Math.Atan2(velocity.Y , velocity.X);
            }
            else if (velocity.Y == 0 && velocity.X != 0)
            {
                if (velocity.X > 0)
                {
                    rotation = 0;
                }
                else
                {
                    rotation = (float)Math.PI;
                }

            }
            else if (velocity.Y != 0 && velocity.X == 0)
            {
                if (velocity.Y > 0)
                {
                    rotation = (float)Math.PI / 2;
                }
                else
                {
                    rotation = -(float)Math.PI / 2;
                }
            }
            else
            {
                rotation = lastRotation;
            }

            if (posistion.X > 1920) posistion.X = 0;
            if (posistion.X < 0) posistion.X = 1920;
            if (posistion.Y > 1080) posistion.Y = 0;
            if (posistion.Y < 0) posistion.Y = 1080;


            bounds = new BoundingRectangle((posistion.X + 30) * scale, (posistion.Y + 40) * scale, 70 * scale, 40 * scale);


        }

        public void Draw(GameTime gametime , SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(texture, posistion, null,color,rotation,new Vector2(64,64) * scale, scale ,SpriteEffects.None,0f);
        }
    }
}
