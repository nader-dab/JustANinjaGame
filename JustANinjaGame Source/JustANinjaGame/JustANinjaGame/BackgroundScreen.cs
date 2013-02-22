using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace JustANinjaGame
{
    /// <summary>
    /// Used to update the position and type of background depending on game state
    /// </summary>
    static class BackgroundScreen
    {
        public static void ScrollingBackgroundUpdate(ContentManager Content, Screen backgroundOne, Screen backgroundTwo, GameState CurrentGameState)
        {
            // updates the position of the bacgrounds so thath
            // they are constantly moving
            backgroundOne.Update();
            backgroundTwo.Update();

            // if the first background reaches the end of the screen 
            // it goes behind the second background
            if (backgroundOne.PositionX + backgroundOne.Texture.Width <= 0)
            {
                if (CurrentGameState == GameState.Transition)
                {
                    backgroundOne.Texture = Content.Load<Texture2D>("Images\\Background-2-transition");
                }
                if (CurrentGameState == GameState.Night)
                {
                    backgroundOne.Texture = Content.Load<Texture2D>("Images\\Background-3-dark");
                }
                if (CurrentGameState == GameState.Fire)
                {
                    backgroundOne.Texture = Content.Load<Texture2D>("Images\\Background-4-fire");
                }
                backgroundOne.PositionX = backgroundTwo.PositionX + backgroundTwo.Texture.Width;
            }

            // if the second background reaches the end of the screen 
            // it goes behind the first background
            if (backgroundTwo.PositionX + backgroundTwo.Texture.Width <= 0)
            {
                if (CurrentGameState == GameState.Transition || CurrentGameState == GameState.Night)
                {
                    backgroundTwo.Texture = Content.Load<Texture2D>("images\\Background-3-dark");
                }
                if (CurrentGameState == GameState.Fire)
                {
                    backgroundTwo.Texture = Content.Load<Texture2D>("images\\Background-4-fire");
                }
                backgroundTwo.PositionX = backgroundOne.PositionX + backgroundOne.Texture.Width;
            }
        }
    }
}
