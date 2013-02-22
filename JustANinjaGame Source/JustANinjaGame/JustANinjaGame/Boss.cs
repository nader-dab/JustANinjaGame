using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace JustANinjaGame
{
    class Boss : Sprite
    {
        private Texture2D[] bossAnimation = new Texture2D[4];
        private Texture2D dragonFireBall;
        private float life;
        private Color color;
 
        public Boss(Texture2D bossAnimation1, Texture2D bossAnimation2,
            Texture2D bossAnimation3, Texture2D bossAnimation4,
            Vector2 position, int life, SoundEffect sound, Color color)
        {

            // The boss will consist of multyple textrures
            this.bossAnimation[0] = bossAnimation1;
            this.bossAnimation[1] = bossAnimation2;
            this.bossAnimation[2] = bossAnimation3;
            this.bossAnimation[3] = bossAnimation4;
            this.position = position;
            this.texture = bossAnimation1;
            this.life = life;
            this.sound = sound;
            this.color = color;
        }
    
        // Setting different boss properties that we can modify
        public Texture2D[] BossAnimation
        {
            get { return this.bossAnimation; }
        }

        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value;}
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        public Color SetColor
        {
            get { return this.color; }
            set { this.color = value;}
        }

        public float Life
        {
            get { return this.life; }
            set { this.life = value; }
        }

        public SoundEffect Sound
        {
            get { return this.sound; }
            set { this.sound = value; }
        }

        // Overriding the parrent method in order to use custom colors
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.position, this.color);
        }
    }
}
