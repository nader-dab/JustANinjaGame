using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace JustANinjaGame
{
    /// <summary>
    /// All logic associated with the player
    /// When the player presses the attack button an attack object is generated for the current position
    /// accompanied by animation of the player and a sound effect
    /// </summary>
    static class PlayerLogic
    {
        // Adds an attack when the attack button is pressed accompanied by sound and animation
        public static void AttackAdd(ContentManager Content, Player ninja, List<PlayerAttack> ninjaAttacks, 
            KeyboardState presentKey, KeyboardState pastKey,
            GamePadState pressentButton, GamePadState pastButton)
        {
            if (presentKey.IsKeyDown(Keys.Space) && pastKey.IsKeyUp(Keys.Space)
                || pressentButton.IsButtonDown(Buttons.A) && pastButton.IsButtonUp(Buttons.A))
            {
                // if the attack button is pressed a new attack will be added to the list
                ninjaAttacks.Add(new PlayerAttack(Content.Load<Texture2D>("Images\\Attack"),
                    new Vector2(ninja.PositionX + (int)(ninja.Texture.Width * 0.8),
                    ninja.PositionY + (int)(ninja.Texture.Height / 2.25))));

                // A sound effect will be played each time we press the attack button
                ninja.PlaySound();

            }

            // The animation texture of the character will change with each attack
            if (presentKey.IsKeyDown(Keys.Space) || pressentButton.IsButtonDown(Buttons.A))
            {
                ninja.Texture = Content.Load<Texture2D>("Images\\NinjaFrame1-2");
            }
            else
            {
                ninja.Texture = Content.Load<Texture2D>("Images\\NinjaFrame1-1");
            }
        }

        // Updates the position of each individual player attacks and removes it if its off screen
        public static void AttackUpdate(GraphicsDevice graphics,Player ninja, List<PlayerAttack> ninjaAttacks,
            KeyboardState presentKey, KeyboardState pastKey,
            GamePadState pressentButton, GamePadState pastButton)
        {
            foreach (var attack in ninjaAttacks)
            {
                attack.Update();
            }

            for (int index = 0; index < ninjaAttacks.Count; index++)
            {
                if (ninjaAttacks[index].Position.X > graphics.Viewport.Width +
                    ninjaAttacks[index].Texture.Width)
                {
                    // if the attack is off screen it will be removed from the list
                    ninjaAttacks.RemoveAt(index);
                    index--;
                }
            }
        }

        // Restricts the movement of the player to the screen size
        public static void RestrictMovement(GraphicsDevice graphics, Player ninja)
        {
            // Current screen dimensions
            int screenWidth = graphics.Viewport.Width;
            int screenHeight = graphics.Viewport.Height;

            // Cannot move beyond the left side
            if (ninja.PositionX <= 0)
            {
                ninja.PositionX = 0;
            }

            // Cannot move beyond the right side
            if (ninja.PositionX + ninja.Texture.Width >= screenWidth)
            {
                ninja.PositionX = screenWidth - ninja.Texture.Width;
            }

            // Cannot move beyond the top
            if (ninja.PositionY <= 0)
            {
                ninja.PositionY = 0;
            }

            // Cannot move beyond the bottom
            if (ninja.PositionY + ninja.Texture.Height >= screenHeight)
            {
                ninja.PositionY = screenHeight - ninja.Texture.Height;
            }
        }

        // Draws the player character and every attack thats been generated
        public static void DrawNinjaAndAttacks(Player ninja, List<PlayerAttack> ninjaAttacks, SpriteBatch spriteBatch, SpriteFont font)
        {
            ninja.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Lives " + ninja.health, new Vector2(10, 10), Color.Black);
            foreach (var attack in ninjaAttacks)
            {
                attack.Draw(spriteBatch);
            }
        }
    }
}
