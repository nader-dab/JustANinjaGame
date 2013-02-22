using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace JustANinjaGame
{
    class Player : Sprite
    {
        public int health;                  // The health of our character
        private const int MOVESPEED = 5;    // The movement speed of the character
        public Player(Texture2D texture, Rectangle form, SoundEffect sound, int health)
        {
            this.texture = texture;
            this.form = form;
            this.sound = sound;
            this.health = health;
        }

        // This will be used to determin the X coordinate of the player rectangle
        public int PositionX
        {
            get { return this.form.X; }
            set { this.form.X = value; }
        }

        // This will be used to determin the Y coordinate of the player rectangle
        public int PositionY
        {
            get { return this.form.Y; }
            set { this.form.Y = value; }
        }

        /// <summary>
        /// This property will be used to change the texture of the charecter when
        /// he attacks with a new texture
        /// </summary>
        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }
        /// <summary>
        /// The Update method checks wheather the user has pressed a button and updates 
        /// the position of the character depending on the movement speeed
        /// Supports both keyboard and Xbox controls
        /// </summary>
        public override void Update()
        {
            // Movement right
            if (Keyboard.GetState().IsKeyDown(Keys.Right)
                || GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
            {
                // the X coordinate of the rectangle will increase depending on the movement speed
                this.form.X += MOVESPEED;            
            }

            // Movement left
            if (Keyboard.GetState().IsKeyDown(Keys.Left)
                || GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
            {
                this.form.X -= MOVESPEED;
            }

            // Movement up
            if (Keyboard.GetState().IsKeyDown(Keys.Up)
                || GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
            {
                this.form.Y -= MOVESPEED;
            }

            // Movement down
            if (Keyboard.GetState().IsKeyDown(Keys.Down)
                || GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
            {
                this.form.Y += MOVESPEED;
            }
        }
    }
}
