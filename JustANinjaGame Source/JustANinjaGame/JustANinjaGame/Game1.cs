using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JustANinjaGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player ninja;
        List<PlayerAttack> ninjaAttacks = new List<PlayerAttack>();
        List<Enemy> dragons = new List<Enemy>();
        List<EnemyAttack> fireBalls = new List<EnemyAttack>();
        List<Explosion> explosions = new List<Explosion>();
        KeyboardState presentKey;
        KeyboardState pastKey;
        GamePadState pressentButton;
        GamePadState pastButton;
        Screen backgroundOne;
        Screen backgroundTwo;
        BackgroundMusic music;
        GameState CurrentGameState = GameState.Title;
        Boss bossDragon;
        List<EnemyAttack> bossFireBalls = new List<EnemyAttack>();
        Health healthRectangle;
        SpriteFont font;
        float playTime;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Loading ninja texture, initial position, sound and health
            ninja = new Player(Content.Load<Texture2D>("Images\\NinjaFrame1-1"), 
                new Rectangle(0, 0, 140, 140), Content.Load<SoundEffect>("Sound\\SwordEffect"), 7);

            // Loading first background initial texture and position
            backgroundOne = new Screen(Content.Load<Texture2D>("Images\\Background-1-normal"), 
                new Rectangle(0, 0, 1281, 500));

            // Loading second background initial texture and position
            backgroundTwo = new Screen(Content.Load<Texture2D>("Images\\Background-1-normal"), 
                new Rectangle(1281, 0, 1281, 500));

            // Loading oss dragon textures, position, health, sound, initial color
            bossDragon = new Boss(Content.Load<Texture2D>("Images\\Dragon1"), Content.Load<Texture2D>("Images\\Dragon2"), 
                Content.Load<Texture2D>("Images\\Dragon3"), Content.Load<Texture2D>("Images\\Dragon4"), 
                new Vector2(600, 0), 380, Content.Load<SoundEffect>("Sound\\Roaring"), new Color(0, 0, 0, 0));

            // Loading health rectangle texture and initial dimensions
            healthRectangle = new Health(Content.Load<Texture2D>("Images\\Health"), 
                new Rectangle(400, 5, (int)bossDragon.Life, 40));

            // Loading the font of the game
            font = Content.Load<SpriteFont>("font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) 
                || GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Key states at the begginig
            presentKey = Keyboard.GetState();
            pressentButton = GamePad.GetState(PlayerIndex.One);

            // Updates the cuurent playtime
            playTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Changes the game logic depending on the gamestate
            switch (CurrentGameState)
            {
                case GameState.Title:
                    LevelTitle();
                    break;
                case GameState.Beggining:
                    LevelBeggining(gameTime);
                    break;
                case GameState.Transition:
                    LevelTransition(gameTime);
                    break;
                case GameState.Night:
                    LevelNight(gameTime);
                    break;
                case GameState.Fire:
                    LevelFire(gameTime);
                    break;
                case GameState.Boss:
                    LevelBoss(gameTime);
                    break;
                case GameState.GameOver:
                    GameOverScreen();
                    break;
                case GameState.Credits:
                    EndCredits();
                    break;
                default:
                    break;
            }

            // Key states at the end
            pastKey = presentKey;
            pastButton = GamePad.GetState(PlayerIndex.One);

            base.Update(gameTime);
        }


        // Logic for Gamestate GameOver
        private void GameOverScreen()
        {
            Scree3 = new Screen(Content.Load<Texture2D>("Images\\GameOver"), new Rectangle(0, 0, 800, 500));
        }

        // Logic for Gamestate Title
        private void LevelTitle()
        {
            IntroTitles();
            // playtime is set to zero after the end of the intro
            playTime = 0;

            // When the last title screen finishes play music and begin game
            if (titleScreen == 3)
            {
                music = new BackgroundMusic(Content.Load<Song>("Sound\\OckeroidSamurai"), 1f);
                music.PlaySong();
                CurrentGameState = GameState.Beggining;
            }
        }

        // Logic for Gamestate Beggining
        private void LevelBeggining(GameTime gameTime)
        {
            // Play Default gae logic
            DefaultGameLogic(gameTime);

            // If playtime reaches 50 change the gamestate
            if (playTime > 50)
            {
                CurrentGameState = GameState.Transition;
            }
        }

        // Logic for Gamestate Transition
        private void LevelTransition(GameTime gameTime)
        {
            DefaultGameLogic(gameTime);
            if (playTime > 70)
            {
                CurrentGameState = GameState.Night;
            }
        }

        // Logic for Gamestate Night
        private void LevelNight(GameTime gameTime)
        {
            DefaultGameLogic(gameTime);
            if (playTime > 110)
            {
                CurrentGameState = GameState.Fire;
            }
        }
        // Logic for Gamestate Fire
        private void LevelFire(GameTime gameTime)
        {
            DefaultGameLogic(gameTime);

            // When the playtime reaches the boss fight play sound and change gamestate
            if (playTime > 140)
            {
                bossDragon.PlaySound(0.8f);
                bossDragon.Sound = Content.Load<SoundEffect>("Sound\\SmallFireball");
                music = new BackgroundMusic(Content.Load<Song>("Sound\\LogicalDefiance"), 1f);
                music.PlaySong();
                CurrentGameState = GameState.Boss;
            }
        }

        // Logic for Gamestate Boss
        private void LevelBoss(GameTime gameTime)
        {
            DefaultGameLogic(gameTime);
            BossLogic.BossUpdate(Content, gameTime, bossDragon, bossFireBalls, CurrentGameState, ninjaAttacks, healthRectangle);
            if ((int)bossDragon.Life == 0)
            {
                // When the boss is defeated play credits
                CurrentGameState = GameState.Credits;
            }
        }

        // Ending credist screen switching
        int credit = 0;
        private void EndCredits()
        {
            if (credit == 0)
            {
                credit += FadeScreen("Images\\title", "Images\\Credits1");
            }
            if (credit == 1)
            {
                credit += FadeScreen("Images\\Credits1", "Images\\Credits2");
            }
            if (credit == 2)
            {
                credit += FadeScreen("Images\\Credits2", "Images\\Credits3");
            }
        }

        // Opening title screen switching
        int titleScreen = 0;
        private void IntroTitles()
        {
            if (titleScreen == 0)
            {
                titleScreen += FadeScreen("Images\\title", "Images\\title1");
            }
            if (titleScreen == 1)
            {
                titleScreen += FadeScreen("Images\\title1", "Images\\title2");
            }
            if (titleScreen == 2)
            {
                titleScreen += FadeScreen("Images\\title2", "Images\\title3");
            }
        }

        // Screen switching logic
        int fade = 0;
        Screen Screen1;
        Screen Screen2;
        Screen Scree3;
        protected int FadeScreen(string imageOne, string imageTwo)
        {
            Screen1 = new Screen(Content.Load<Texture2D>(imageOne), new Rectangle(0, 0, 800, 500));
            Screen2 = new Screen(Content.Load<Texture2D>(imageTwo), new Rectangle(0, 0, 800, 500));
            if (fade < 355)
            {
                fade += 2;
            }
            else
            {
                fade = 0;
                return 1;
            }
            Screen2.SetColor = new Color(fade, fade, fade, fade);
            return 0;
        }

        // The default game logic
        private void DefaultGameLogic(GameTime gameTime)
        {
            // If the player dies -> game over
            if (ninja.health < 0)
            {
                CurrentGameState = GameState.GameOver;
            }

            // Cheat to increase health
            if (Keyboard.GetState().IsKeyDown(Keys.D1) || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                ninja.health = 99;
            }
                
            // Update the movement of the player depending on the input
            ninja.Update();

            // Restrict the free movement of the player to the size of the window
            PlayerLogic.RestrictMovement(graphics.GraphicsDevice, ninja);

            // Adds attacks, animation and sound effect
            PlayerLogic.AttackAdd(Content, ninja, ninjaAttacks, presentKey, pastKey, pressentButton, pastButton);

            // Updates the position of Ninja attacks
            PlayerLogic.AttackUpdate(graphics.GraphicsDevice, ninja, ninjaAttacks, presentKey, pastKey, pressentButton, pastButton);

            // Updates the scrolling background
            BackgroundScreen.ScrollingBackgroundUpdate(Content, backgroundOne, backgroundTwo, CurrentGameState);

            // Used to generate enemies on the screen
            EnemyLogic.DragonGenerator(Content, dragons, gameTime);

            // Updates dragon position
            EnemyLogic.DragonUpdate(graphics.GraphicsDevice, dragons);

            // Used to generate enemy fireballs
            EnemyLogic.FireBallGenerator(Content, dragons, fireBalls);

            // Update the position of each fireball
            EnemyLogic.FireBallUpdate(fireBalls);

            // Collision between player attacks and enemies
            CollisionDetection.CollisionPlayerToEnemy(Content, ninjaAttacks, dragons, explosions);

            // Collision between player attacks and enemy attacks
            CollisionDetection.CollisionPlayerToFireBalls(Content, ninjaAttacks, fireBalls, explosions);

            // Update Explosions
            CollisionDetection.ExplosionUpdate(explosions);

            // Collision of fireballs with player
            CollisionDetection.CollisionEnemyFireBallsToPlayer(Content, ninja, fireBalls, explosions);

            // Collision of the bossfireballs with the player
            CollisionDetection.CollisionBossFireBallsToPlayer(Content, ninja, bossFireBalls, explosions);
        } 

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            switch (CurrentGameState)
            {
                case GameState.Title:
                    Screen1.Draw(spriteBatch);
                    Screen2.Draw(spriteBatch);
                    break;
                case GameState.Beggining:
                case GameState.Transition:
                case GameState.Night:
                case GameState.Fire:
                case GameState.Boss:
                    backgroundOne.Draw(spriteBatch);
                    backgroundTwo.Draw(spriteBatch);
                    CollisionDetection.DrawExplosions(explosions, spriteBatch);
                    EnemyLogic.DrawDragons(dragons, fireBalls, spriteBatch);
            
                    PlayerLogic.DrawNinjaAndAttacks(ninja, ninjaAttacks, spriteBatch, font);
                    if (CurrentGameState == GameState.Boss)
                    {
                        BossLogic.DrawBoss(spriteBatch, bossDragon, bossFireBalls, healthRectangle, font);
                    }
                    break;
                case GameState.GameOver:
                    Scree3.Draw(spriteBatch);
                    break;
                case GameState.Credits:
                    Screen1.Draw(spriteBatch);
                    Screen2.Draw(spriteBatch);
                    break;
                default:
                    break;
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        } 
    }
}
