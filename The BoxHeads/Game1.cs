using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;



namespace Monogame2
{
    //The main/central game class.  Runs first and creates instances of other classes
    public class Game1 : Game
    {
        //Fields
        public bool isFirstFrame;

        //Declare fonts
        public SpriteFont font;
        public SpriteFont smallFont;

        //Declare game objects 
        public Player player;
        public List<IGameObject> gameObjects;
        public List<Enemy> Enemies;

        //Declare Graphics and content
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ContentManager contentManager;
        private TargetArea targetArea;

        private bool isStartScreen;

        //Universal Random Seed
        Random random;

        //Game constructor, is called in Program.cs
        //Set values for fields
        public Game1()
        {
            isStartScreen = true;

            //Set create instances for gameobjects
            random = new Random();
            player = new Player(this);
            gameObjects = new List<IGameObject>();
            Enemies = new List<Enemy>();
            _graphics = new GraphicsDeviceManager(this);
            targetArea = new TargetArea(new Vector2(100, 500), (1, 1), this);


            TargetArea.random = random;

            //Settings for monogame
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(16);

        }

     

        protected override void Initialize()
        {
         
            //Set the size of the screen
            _graphics.PreferredBackBufferWidth = 1000; 

            _graphics.PreferredBackBufferHeight = 1000;

            _graphics.ApplyChanges();

            //Pass screen size info to targetarea class
            TargetArea.screenX = _graphics.PreferredBackBufferWidth;
            TargetArea.screenY = _graphics.PreferredBackBufferHeight;

            //Spawn enemies (add to active array)
            Enemies.AddRange(LevelManager.spawnEnemies(random, this, (_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), player));
          
            //Add game objects to the gameObjects list
            gameObjects.Add(targetArea);
            gameObjects.AddRange(Enemies);
            gameObjects.Add(player);
          
            //Call "initialize" in gameobjects
            foreach (IGameObject gameObject in gameObjects)
            {
                gameObject.Initialize();
                gameObject.IsActive = false;
            }

            player.IsActive = true;

           //Pass gameObjects list into Enemy class
            Enemy.gameObjects = gameObjects;


            base.Initialize();
            
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            smallFont = Content.Load<SpriteFont>("myFont2");

            font = Content.Load<SpriteFont>("myFont");
  
        }

        protected override void Update(GameTime gameTime)
        {
            bool hasWon = true;

            //Handle simple mouse input to detect click
            MouseState mouse = Mouse.GetState();
       
            //Start game with mouse click
            if ((isStartScreen && mouse.LeftButton == ButtonState.Pressed)  || Player.isAlive == false)
            {
                Debug.WriteLine("staring game!");
                player.isActive = true;
                Player.isAlive = true;
                LevelManager.Level = 0;
                isStartScreen = false;
                NewLevel();
            }

            //Close game if requested
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


          
            //Call the "update" method in all game objects
            foreach(IGameObject gameObject in gameObjects)
            {
                if (gameObject.IsActive)
                {
                    gameObject.Update(gameTime);
                }
                
                
            }

            //Check if the player has cleared the level
            foreach(Enemy enemy in Enemies)
            {
               if (!enemy.IsInZone)
                {
                    hasWon = false;
                }
            }

            if (hasWon)
            {
                NewLevel();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
        

            //Clear the canvas
            GraphicsDevice.Clear(Color.AntiqueWhite);

          
            _spriteBatch.Begin();

            //Draw starting text
            if (isStartScreen)
            {
                _spriteBatch.DrawString(font, "Arrow Rancher!", new Vector2(100, 200), Color.Black);
                _spriteBatch.DrawString(smallFont, "Click To Start", new Vector2(100, 260), Color.Black);
                _spriteBatch.DrawString(smallFont, "Lure all the arrows into the box without letting them hit you", new Vector2(100, 290), Color.Black);
            }

            _spriteBatch.DrawString(font, LevelManager.Level.ToString(), new Vector2(870, 70), Color.Black);

           //Draw gameobjects by calling the "Draw" method in all
            foreach (IGameObject gameObject in gameObjects)
            {
               if (gameObject.IsActive || gameObject is Player || gameObject is TargetArea)
               {
          
                    gameObject.Draw(_spriteBatch);
    

                }
            }
           
           _spriteBatch.End();

            base.Draw(gameTime);
        }

        //Called upon winning or loosing a level
        public void NewLevel()
        {
            //Remove enemies from the gameObjects list
            int i;
            for (i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i] is Enemy || gameObjects[i] is TargetArea)
                { 
                    gameObjects.Remove(gameObjects[i]);
                    i--;
                }
            }
            
            //Clear the "Enemies" list
            Enemies.Clear();

            //Create new target box and add to gameObjects list
            targetArea = LevelManager.NewTarget(random, this);

            targetArea.Initialize();

            gameObjects.Add(targetArea);

            //Spawn new enemies with the LevelManager class and add to gameObjects list
            Enemies.AddRange(LevelManager.spawnEnemies(random, this, (_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), player));

            gameObjects.AddRange(Enemies);

            //Pass the gameObjects list into the enemy class

            Enemy.gameObjects = (gameObjects);

            //Tell LevelManager class about the change of level
            LevelManager.Level++;


        }
    }
}