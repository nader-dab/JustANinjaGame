using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace JustANinjaGame
{
    class BossLogic
    {
        static float frame = 0;
        static int appear = 0;
        static int shadow = 0;
        static float coolDown;
        static bool shot = false;
        static Color color;  
        static EnemyAttack bossFireBall;

        public static void BossUpdate(ContentManager Content, GameTime gameTime, Boss bossDragon, List<EnemyAttack> bossFireBalls, GameState CurrentGameState, List<PlayerAttack> ninjaAttacks, Health healthRectangle)
        {
            healthRectangle.Widht = (int)bossDragon.Life;
            BossSummon(Content, gameTime, bossDragon, bossFireBall, bossFireBalls);
            BossAnimation(gameTime, bossDragon);
            BossCollision(bossDragon, CurrentGameState, ninjaAttacks);
        }

        private static void Attack(ContentManager Content, GameTime gameTime, Boss bossDragon, EnemyAttack bossFireBall, List<EnemyAttack> bossFireBalls)
        {
            // The boss will attack on set intervals
            coolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            // Fireballs are added to a list to allow the boss to shoot more than once
            foreach (var fireBall in bossFireBalls)
            {
                fireBall.Update();
            }

            if (coolDown > 3)
            {
                // A new fireball is generated in a position where it matches the boss mouth
                bossFireBall = new EnemyAttack(Content.Load<Texture2D>("Images\\Dragonfire"), new Vector2(bossDragon.Position.X - 65, bossDragon.Position.Y + bossDragon.Texture.Height / 2 - 40), 0f, -10f);
                bossFireBalls.Clear();
                coolDown = 0;

                // The fireball is added to the list of fireballs
                bossFireBalls.Add(bossFireBall);

                // this variable is used as a trigger to change the boss animation
                shot = true;

                // A sound is played each time the boss fires
                bossDragon.PlaySound();
            }
        }

        private static void BossSummon(ContentManager Content, GameTime gameTime, Boss bossDragon, EnemyAttack bossFireBall, List<EnemyAttack> bossFireBalls)
        {
            if (shadow != 255)
            {
                // At the beginig only the shadow of the poss will be visible
                // This can be acheive by setting all the colors to zero and 
                // gradually increasing the alpha channel till he is visible
                color = new Color(0, 0, 0, shadow);
                shadow += 1;
                bossDragon.SetColor = color;
            }
            else if (appear != 255)
            {
                // After the boss is visible the colors start appearing
                color = new Color(appear, appear, appear, shadow);
                appear += 1;
                bossDragon.SetColor = color;
            }
            else
            {
                // The boss won't be able to attack until he is completely colored and visible
                Attack(Content, gameTime, bossDragon, bossFireBall, bossFireBalls);
            }
        }

        // Collision between the player attacks and the boss
        private static void BossCollision(Boss bossDragon, GameState CurrentGameState, List<PlayerAttack> ninjaAttacks)
        {
            if (CurrentGameState == GameState.Boss)
            {
                for (int index = 0; index < ninjaAttacks.Count; index++)
                {
                    Rectangle wave = new Rectangle((int)ninjaAttacks[index].Position.X - 100, (int)ninjaAttacks[index].Position.Y, ninjaAttacks[index].Texture.Width / 2, ninjaAttacks[index].Texture.Height / 2);
                    Rectangle boss = new Rectangle((int)bossDragon.Position.X, (int)(bossDragon.Position.Y + 250), bossDragon.Texture.Width / 2, bossDragon.Texture.Height / 2);
                    if (wave.Intersects(boss))
                    {
                        // The bosss life gradually decreases when he is hit
                        bossDragon.Life -= 1.5f;
                        ninjaAttacks.RemoveAt(index);
                        index--;
                    }

                }
            }
        }

        private static void BossAnimation(GameTime gameTime, Boss bossDragon)
        {
            // A variable used as a timer to change the animation
            frame += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // If the boss has fired a shot recently
            if (shot == true)
            {
                // Set firring textures according to the timer
                if (frame > 0.5)
                {
                    bossDragon.Texture = bossDragon.BossAnimation[2];
                }
                if (frame > 1)
                {
                    bossDragon.Texture = bossDragon.BossAnimation[3];
                    frame = 0;

                    // Change the trigger to false
                    shot = false;
                }
            }
            else // If he hasn't fired recently
            {
                // Set default textures according to the timer
                if (frame > 0.5)
                {
                    bossDragon.Texture = bossDragon.BossAnimation[0];
                }
                if (frame > 1)
                {
                    bossDragon.Texture = bossDragon.BossAnimation[1];
                    frame = 0;
                }
            }
        }

        public static void DrawBoss(SpriteBatch spriteBatch, Boss bossDragon, List<EnemyAttack> bossFireBalls, Health healthRectangle, SpriteFont font)
        { 
            // Draw boss
            bossDragon.Draw(spriteBatch); 
            foreach (var fireBall in bossFireBalls)
            {
                // Draw fireballs
                fireBall.Draw(spriteBatch);
            }

            // Draw health bar
            healthRectangle.Draw(spriteBatch);

            // Draw Boss name
            spriteBatch.DrawString(font, "Seesh A'rpin Terme Diatee Xam II ", new Vector2(400, 10), Color.Black);
        }
    }
}
