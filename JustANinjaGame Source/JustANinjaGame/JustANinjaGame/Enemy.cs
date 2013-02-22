using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace JustANinjaGame
{
    class Enemy : Sprite
    {
        private int randomVelocityX;    // Random X coordinate
        private int randomVelocityY;    // Rabdom Y coordinate
        private float fireBallInterval; // Used to set attack interval
        private float coolDown = 0;     // Used for animation interval
        private GameTime gameTime;

        public Enemy(Texture2D texture, Vector2 position, SoundEffect sound, GameTime gameTime)
        { 
            this.texture = texture;
            this.position = position;
            this.sound = sound;
            this.gameTime = gameTime;

            // generates a random velocity for the enemy between -6 and -4
            // pixels for the X coordinate. Enemies can move only left.
            randomVelocityX = RandomNumber.Generate(-6, -4);

            // generates a random velocity for the enemy between -2 and 2
            // pixels for the Y coordinate.
            randomVelocityY = RandomNumber.Generate(-2, 2);

            // the final enemy velocity is a combination between the 
            // random X and Y velocities/
            this.velocity = new Vector2(randomVelocityX, randomVelocityY);
        }

        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
        }

        public int VelocityX
        {
            get { return (int)this.velocity.X; }
        }

        public float Interval
        {
            get { return this.fireBallInterval; }
            set { this.fireBallInterval = value; }
        }

        public float CoolDown
        {
            get { return this.coolDown; }
            set { this.coolDown = value; }
        }

        public void Update(GraphicsDevice graphics)
        {
            // Updates the position according to the randomly
            // generated velocity.
            this.position += this.velocity;
            if (this.position.Y <= 0 || 
                this.position.Y + this.texture.Height >= graphics.Viewport.Height)
            {
                // If the enemy reaches the top or bottom of the screen
                // its horizontal velocity will change so that it will
                // bounce in the oposite direction
                this.velocity.Y = -this.velocity.Y;
            }

            // Updates the elapsed gametime for the attack and 
            // animation intervals
            fireBallInterval += (float)gameTime.ElapsedGameTime.TotalSeconds;
            coolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.position, Color.White);
        }
    }
}
