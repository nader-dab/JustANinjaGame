using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace JustANinjaGame
{
    class Sprite
    {
        protected Texture2D texture;
        protected Rectangle form;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 origin;
        protected SoundEffect sound;
        // Empty classes that will be inherited from all children classes 
        // which will override thier funcitonality
        public virtual void Update()
        { 

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.form, Color.White);
        }

        // Setting properties for all the fields
        protected Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        protected Rectangle Form
        {

            get { return this.form; }
            set { this.form = value; }
        }

        protected Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        protected Vector2 Velocity
        {
            get { return this.velocity; }
            set { this.velocity = value; }
        }

        protected SoundEffect Sound
        {
            get { return this.sound; }
            set { this.sound = value; }
        }

        // Used to play different sounds specific for each sprite
        public void PlaySound(float volume = 0.5f)
        {
            this.sound.Play(volume, 0.5f, 0.5f);
        }
    }
}
