using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace JustANinjaGame
{
    /// <summary>
    /// All the logic associated with generating and updating enemies
    /// </summary>
    class EnemyLogic
    {
        // Generates fireballs for each dragon thats on the screen
        private static float enemyInterval = 0;
        public static void FireBallGenerator(ContentManager Content, List<Enemy> dragons, List<EnemyAttack> fireBalls)
        {
            foreach (var dragon in dragons)
            {

                // If attack interval is larger that 2 -> fire
                if (dragon.Interval > 2)
                {
                    dragon.Interval = 0;

                    // If there are less than 10 firebals on the screen -> fire
                    if (fireBalls.Count < 10)
                    {

                        // Generate a new fireBall with position of the current dragon plus his speed
                        // The offset -40px X and 50px Y is to align the fireball with the mouth of the dragon
                        EnemyAttack fireBall = new EnemyAttack(Content.Load<Texture2D>("Images\\FireBall"),
                            new Vector2(dragon.Position.X - 40, dragon.Position.Y + 50), dragon.VelocityX);
                        fireBalls.Add(fireBall);

                        // A sound is played during each shot
                        dragon.PlaySound();

                        // The dragon texture changes during each shot
                        dragon.Texture = Content.Load<Texture2D>("Images\\Babydragon2");
                    }
                }

                if (dragon.CoolDown > 3)
                {

                    // Used to change the texture back to normal
                    dragon.CoolDown = 0;
                    dragon.Texture = Content.Load<Texture2D>("Images\\Babydragon1");
                }
            }
        }

        // Updates fireball position
        public static void FireBallUpdate(List<EnemyAttack> fireBalls)
        {
            // Updates the position for each fireball
            foreach (var fireBall in fireBalls)
            {
                fireBall.Update();
            }

            // Removes fireballs when they leave the screen
            for (int index = 0; index < fireBalls.Count; index++)
            {
                if (fireBalls[index].Position.X + fireBalls[index].Texture.Width <= 0)
                {
                    fireBalls.RemoveAt(index);
                    index--;
                }
            }
        }

        // Generates enemies on set intervals and at random position
        public static void DragonGenerator(ContentManager Content, List<Enemy> dragons, GameTime gameTime)
        {
            // calculates the interval for enemy spawning
            enemyInterval += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Adds enemies
            if (enemyInterval >= 1)
            {
                enemyInterval = 0;  // reset the counter

                // adds dragons at a random position vertical
                if (dragons.Count < 10)
                {
                    dragons.Add(new Enemy(Content.Load<Texture2D>("Images\\Babydragon1"),
                        new Vector2(1100, RandomNumber.Generate(25, 300)),
                        Content.Load<SoundEffect>("Sound\\LargeFireball"), gameTime));
                }
            }
        }

        // Updates the dragon position
        public static void DragonUpdate(GraphicsDevice graphics, List<Enemy> dragons)
        {
            // Updates the position for each dragon
            foreach (var dragon in dragons)
            {
                dragon.Update(graphics);
            }

            // Removes dragons when they leave the screen
            for (int index = 0; index < dragons.Count; index++)
            {
                if (dragons[index].Position.X + dragons[index].Texture.Width <= 0)
                {
                    dragons.RemoveAt(index);
                    index--;
                }
            }
        }

        // Draws the dragons and every fireball thats been generated
        public static void DrawDragons(List<Enemy> dragons, List<EnemyAttack> fireBalls, SpriteBatch spriteBatch)
        {
            foreach (var fireBall in fireBalls)
            {
                fireBall.Draw(spriteBatch);
            }

            foreach (var dragon in dragons)
            {
                dragon.Draw(spriteBatch);
            }
        }
    }
}
