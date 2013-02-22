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
    /// All logic concerning the collision between enemy and player sprites
    /// </summary>
    static class CollisionDetection
    {
        public static void CollisionPlayerToEnemy(ContentManager Content, List<PlayerAttack> ninjaAttacks, List<Enemy> dragons, List<Explosion> explosions)
        {
            // Going through each player attack
            for (int index1 = 0; index1 < ninjaAttacks.Count; index1++)
            {
                // Creating a rectangle that represents the desired hit box of the current attack
                Rectangle wave = new Rectangle((int)ninjaAttacks[index1].Position.X - 100,
                    (int)ninjaAttacks[index1].Position.Y, ninjaAttacks[index1].Texture.Width / 2,
                    ninjaAttacks[index1].Texture.Height / 2);

                // Going through each enemy
                for (int index2 = 0; index2 < dragons.Count; index2++)
                {
                    // Creating a rectangle that represents the desired hit box of the current enemy
                    Rectangle dragon = new Rectangle((int)dragons[index2].Position.X,
                        (int)(dragons[index2].Position.Y + 100), dragons[index2].Texture.Width / 2,
                        dragons[index2].Texture.Height / 2);

                    // If the hit boxes intersect
                    if (wave.Intersects(dragon))
                    {
                        // Generate a new explosions on the current position
                        explosions.Add(new Explosion(Content.Load<Texture2D>("Images\\Boom1"),
                            Content.Load<SoundEffect>("Sound\\Glock"), new Vector2(dragons[index2].Position.X,
                            dragons[index2].Position.Y), new Vector2(dragons[index2].VelocityX, 0)));

                        // Play sound for the explosion
                        explosions[explosions.Count - 1].PlaySound();

                        // remove the player attacks that hit the target enemy
                        ninjaAttacks.RemoveAt(index1);

                        // remove the enemy that was hit
                        dragons.RemoveAt(index2);
                        index1--;   // reduce the player attack index
                        index2--;   // reduce the enemy index
                        break;
                    }
                }
            }
        }

        public static void CollisionPlayerToFireBalls(ContentManager Content, List<PlayerAttack> ninjaAttacks, List<EnemyAttack> fireBalls, List<Explosion> explosions)
        {
            // Going through each player attack
            for (int index1 = 0; index1 < ninjaAttacks.Count; index1++)
            {
                // Creating a rectangle that represents the desired hit box of the current attack
                Rectangle wave = new Rectangle((int)ninjaAttacks[index1].Position.X - 50,
                    (int)ninjaAttacks[index1].Position.Y, ninjaAttacks[index1].Texture.Width / 2,
                    ninjaAttacks[index1].Texture.Height / 2);

                // Going through each enemy fireball
                for (int index2 = 0; index2 < fireBalls.Count; index2++)
                {
                    // Creating a rectangle that represents the desired hit box of the current fireball
                    Rectangle fireBall = new Rectangle((int)fireBalls[index2].Position.X,
                        (int)(fireBalls[index2].Position.Y + 50), fireBalls[index2].Texture.Width / 2,
                        fireBalls[index2].Texture.Height / 2);

                    // If the hit boxes intersect
                    if (wave.Intersects(fireBall))
                    {
                        // Generate a new explosions on the current position
                        explosions.Add(new Explosion(Content.Load<Texture2D>("Images\\Boom2"),
                            Content.Load<SoundEffect>("Sound\\Glock"), new Vector2(fireBalls[index2].Position.X,
                            fireBalls[index2].Position.Y), new Vector2(fireBalls[index2].VelocityX + 3, 0)));

                        // Play sound for the explosion
                        explosions[explosions.Count - 1].PlaySound();

                        // remove the player attacks that hit the target enemy
                        ninjaAttacks.RemoveAt(index1);

                        // remove the fireball that was hit
                        fireBalls.RemoveAt(index2);
                        index1--;   // reduce the player attack index
                        index2--;   // reduce the fireball index
                        break;
                    }
                }
            }
        }

        public static void CollisionEnemyFireBallsToPlayer(ContentManager Content, Player ninja, List<EnemyAttack> fireBalls, List<Explosion> explosions)
        {
            // Rectangle of the player character
            Rectangle ninjaRectangle = new Rectangle((int)ninja.PositionX - 55,
                   (int)ninja.PositionY + 60, ninja.Texture.Width,
                   ninja.Texture.Height / 2);

            // Going through each enemy fireball
            for (int index = 0; index < fireBalls.Count; index++)
            {
                // Creating a rectangle that represents the desired hit box of the current fireball
                Rectangle fireBall = new Rectangle((int)fireBalls[index].Position.X,
                    (int)(fireBalls[index].Position.Y + 50), fireBalls[index].Texture.Width / 2,
                    fireBalls[index].Texture.Height / 2);

                // If the hit boxes intersect
                if (ninjaRectangle.Intersects(fireBall))
                {
                    ninja.health--;
                    // Generate a new explosions on the current position
                    explosions.Add(new Explosion(Content.Load<Texture2D>("Images\\Boom2"),
                        Content.Load<SoundEffect>("Sound\\Glock"), new Vector2(fireBalls[index].Position.X,
                        fireBalls[index].Position.Y), new Vector2(fireBalls[index].VelocityX + 3, 0)));

                    // Play sound for the explosion
                    explosions[explosions.Count - 1].PlaySound();

                    // remove the fireball that was hit
                    fireBalls.RemoveAt(index);
                    index--;   // reduce the fireball index
                }
            }
        }

        public static void CollisionBossFireBallsToPlayer(ContentManager Content, Player ninja, List<EnemyAttack> bossFireBalls, List<Explosion> explosions)
        {
            Rectangle ninjaRectangle = new Rectangle((int)ninja.PositionX - 55,
                   (int)ninja.PositionY + 60, ninja.Texture.Width,
                   ninja.Texture.Height / 2);

            // Going through each enemy fireball
            for (int index = 0; index < bossFireBalls.Count; index++)
            {
                // Creating a rectangle that represents the desired hit box of the current fireball
                Rectangle fireBall = new Rectangle((int)bossFireBalls[index].Position.X,
                    (int)(bossFireBalls[index].Position.Y + 50), bossFireBalls[index].Texture.Width / 2,
                    bossFireBalls[index].Texture.Height / 2);

                // If the hit boxes intersect
                if (ninjaRectangle.Intersects(fireBall))
                {
                    ninja.health-= 2;
                    // Generate a new explosions on the current position
                    explosions.Add(new Explosion(Content.Load<Texture2D>("Images\\Boom1"),
                        Content.Load<SoundEffect>("Sound\\Glock"), new Vector2(bossFireBalls[index].Position.X,
                        bossFireBalls[index].Position.Y), new Vector2(bossFireBalls[index].VelocityX + 3, 0)));

                    // Play sound for the explosion
                    explosions[explosions.Count - 1].PlaySound();

                    // remove the fireball that was hit
                    bossFireBalls.RemoveAt(index);
                    index--;   // reduce the fireball index
                }
            }

        }

        public static void ExplosionUpdate(List<Explosion> explosions)
        {
            foreach (var explosion in explosions)
            {
                explosion.Update();
            }

            for (int index = 0; index < explosions.Count; index++)
            {
                if (explosions[index].Position.X + explosions[index].Texture.Width <= 0)
                {
                    explosions.RemoveAt(index);
                    index--;
                }
            }
        }

        public static void DrawExplosions(List<Explosion> explosions, SpriteBatch spriteBatch)
        {
            foreach (var explosion in explosions)
            {
                explosion.Draw(spriteBatch);
            }
        }
    }
}
