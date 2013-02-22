using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JustANinjaGame
{
    class Health : Sprite
    {
        public Health(Texture2D texture, Rectangle form)
        {
            this.texture = texture;
            this.form = form;
        }

        public Rectangle Form
        {
            get { return this.form; }
            set { this.form = value; }
        }

        public int  Widht
        {
            get { return this.form.Width; }
            set {this.form.Width = value; }
        }
    }
}
