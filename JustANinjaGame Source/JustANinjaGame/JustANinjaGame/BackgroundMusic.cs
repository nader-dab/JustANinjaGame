using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace JustANinjaGame
{
    class BackgroundMusic
    {
        protected Song backgroundSong;  // an XNA song object
        protected float volume; 

        public BackgroundMusic(Song backgroundSong, float volume)
        {
            this.backgroundSong = backgroundSong;
            this.volume = volume;
        }

        public Song BacgroundSong
        {
            get { return this.backgroundSong; }
            set { this.backgroundSong = value; }
        }

        public float Volume
        {
            get { return this.volume; }
            set { this.volume = value; }
        }

        public void PlaySong()
        {
            MediaPlayer.Play(this.backgroundSong);  // starts the song
            MediaPlayer.IsRepeating = true;         // sets constant repeating
            MediaPlayer.Volume = this.volume;       // sets volume
        }
    }
}
