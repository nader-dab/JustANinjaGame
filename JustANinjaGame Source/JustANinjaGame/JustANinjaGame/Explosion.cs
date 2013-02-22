using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace JustANinjaGame
{
    class Explosion : Sprite
    {
        private int fade = 355;
        private const int RATE = 3;
        private const float SPEED = 4f;
        private Color color;
        public Explosion(Texture2D texture, SoundEffect sound, Vector2 position, Vector2 velocity)
        {
            this.texture = texture;
            this.sound = sound;
            this.position = position;
            this.velocity = velocity;
        }

        public Vector2 Position
        {
            get { return this.position; }
        }

        public Texture2D Texture
        {
            get { return this.texture; }
        }

        public override void Update()
        {
            fade -= RATE;
            position.X += velocity.X - SPEED;

            // Although color requires 255 as a max value, 355 will give 
            // us some buffer before the fading takes effect.
            // It will be automaticaly replaced with the largest value possible.
            this.color = new Color(fade, fade, fade, fade);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }
    }
}
