using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JustANinjaGame
{
    class PlayerAttack : Sprite
    {
        private float rotation = 0;
        private const float ROTATION_SPEED = 0.5f;  // Constant for the rotation of the attack
        private const int ATTACK_SPEED = 10;        // Movement speed of the attack

        public PlayerAttack(Texture2D texture, Vector2 vector)
        {
            this.texture = texture;
            this.position = vector;
            this.origin.X = texture.Width / 2;
            this.origin.Y = texture.Height / 2;
        }

        // This property will be used to determine the position of the attack
        public Vector2 Position
        {
            get { return this.position; }
        }

        // This property will be used to used to determine the properties of the texture
        public Texture2D Texture
        {
            get { return this.texture; }
        }

        public override void Update()
        {
            // Creating a Rectangle depending on its initial position and texture dimenstions
            this.form = new Rectangle((int)position.X, (int)position.Y,
                texture.Width, texture.Height);
            rotation += ROTATION_SPEED;         // Update the angle of rotation
            this.position.X += ATTACK_SPEED;    // Updates the position of the attack
        }
        /// <summary>
        /// Overriding the Draw method to include sprite rotation
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White,
                rotation, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
