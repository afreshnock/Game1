using System;
using System.Collections.Generic;
using Game1.Collison;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Spawner
    {
        private Vector2 position;
        private Color color;
        public List<Jelly> jellies = new List<Jelly>();
        
        Texture2D texture;
        private float spawnTimer=0;
        private Random r = new Random();
        private float spawnOverflow;
        private Texture2D jellyTexture;
        private float scale;

        public Spawner(Vector2 pos , Color color,Texture2D texture , Texture2D jellyTexture, float scale)
        {
            position = pos;
            this.color = color;
            this.texture = texture;
            this.jellyTexture = jellyTexture;
            spawnOverflow = (float) (r.NextDouble() * 5) + 2f;
            color = ColorArray.RandomColor();
            this.scale = scale;
        }    

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("coral");
            jellyTexture = contentManager.Load<Texture2D>("jelly2");
        }

        public void Update(GameTime gameTime)
        {
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (spawnTimer > spawnOverflow)
            {
                Jelly jelly = new Jelly(position, RandoVector2(), ColorArray.RandomColor(), scale, jellyTexture);
                jellies.Add(jelly);


                spawnTimer -= spawnOverflow;
            }
            BoundingRectangle rect = new BoundingRectangle(0,0,1920,1080);
            List<Jelly> trash = new List<Jelly>();
            foreach(Jelly jelly in jellies)
            {
                jelly.Update(gameTime);
                if (!jelly.bounds.CollidesWith(rect))
                {
                    trash.Add(jelly);
                }
                
            }
            foreach(Jelly jelly in trash)
            {
                jellies.Remove(jelly);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null,color,0,new Vector2(32,32),scale,SpriteEffects.None,0);
            foreach (Jelly jelly in jellies)
            {
                jelly.Draw(spriteBatch);
            }
            
        }

        private Vector2 RandoVector2()
        {
            int sign1 = r.Next() % 2;
            int sign2 = r.Next() % 2;
            if(sign1==0) sign1 = -1;
            if (sign2 == 0) sign2 = -1;
            return new Vector2(sign1* (float)r.NextDouble() * 500 + 25, sign2 * (float)r.NextDouble() * 500 + 25);
        }
    }
}
