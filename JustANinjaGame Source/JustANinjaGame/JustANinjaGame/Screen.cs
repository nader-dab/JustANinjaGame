using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JustANinjaGame
{
    class Screen : Sprite
    {
        private const int SCROLLING = 2; 
        private Color color = Color.White;
        public Screen(Texture2D texture, Rectangle form)
        {
            this.texture = texture;
            this.form = form;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // changing the color can make a background transparrent
            spriteBatch.Draw(this.texture, this.form, this.color);
        }
        public Color SetColor
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public override void Update()
        {
            // used to update the background position
            form.X -= SCROLLING;
        }

        // property used to get the texture properties
        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        // used to set horizontal position
        public int PositionX
        {
            get { return this.form.X; }
            set { this.form.X = value; }
        }
    }
}
