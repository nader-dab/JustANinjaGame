using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JustANinjaGame
{
    class EnemyAttack : Sprite
    {
        private float speed;    // The speed of the fireballs
        public EnemyAttack(Texture2D texture, Vector2 position, float velocity, float speed = -3f)
        {
            this.texture = texture;
            this.position = position;
            this.speed = speed;
            this.position = position;
            // The velocity of the enemy plus the speed of the fireball
            this.velocity.X = velocity + speed; 
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public Texture2D Texture
        {
            get { return this.texture; }
        }

        public int VelocityX
        {
            get { return (int)this.velocity.X; }
        }

        public override void Update()
        {
            this.position.X += this.velocity.X;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.position, Color.White);
        }
    }
}
