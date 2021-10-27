using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame2
{
    //The player class, handles drawing and movement of the player object
    public class Player: DrawableGameComponent, IGameObject
    {
        //Fields
        private float rotation;
        public (int, int) screenBoundries;
        private Vector2 position;
        public bool isActive = true;
        public float speed;




        //Static fields
        public static bool isAlive = true;


        //Constructors
        public Player(Game game) : base(game)
        {

        }

        //Properties
        public static Texture2D Sprite
        {
            get; set;
        }

        public static Color Color
        {
            get; set;
        }

        //Limits the rotation of the player model, *depricated
        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                if ((value % (float)(2 * Math.PI)) == 0 || (value % (float)(2 * Math.PI)) == Math.PI || (value % (float)(2 * Math.PI)) == (Math.PI / 2) || (value % (float)(2 * Math.PI)) == ((3 * Math.PI) / 2))
                {
                    rotation = (value % (float)(2 * Math.PI)) + 0.01f;
                }
                else if (value > (2 * Math.PI))
                {
                    rotation = (value % (float)(2 * Math.PI));
                }
                else
                {
                    rotation = value;
                }
            }
        }

        public bool IsActive
        {
            get; set;
        }

        //Identifies the type as a string *probably depricated because I can just use  the"is" operator
        public string Type
        {
            get
            {
                return "Player";
            }
        }

        //Creates the player's hitbox
        public Rectangle BoundingBox
        {
            get
            {
                
                return new Rectangle((int)Position.X + 9, (int)Position.Y + 9,21, 21);
            }
        }
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
            }
        }

        //Methods
        protected override void LoadContent()
        {
            Debug.WriteLine("KILL");
            base.LoadContent();
        }

        public void Initialize()
        {
            IsActive = true;
            Sprite = Game.Content.Load<Texture2D>("playerPlaceHolder");
        }

        //Update method. called every frame
        public void Update(GameTime gameTime)
        {
            //Handle mouse inputs
            MouseState mouse = Mouse.GetState();

            Position = new Vector2(mouse.X, mouse.Y);

            //Keyboard Input
            KeyboardState state = Keyboard.GetState();
            Vector2 Direction = new Vector2();
            if (state.IsKeyDown(Keys.W))
            {
                Direction.Y = -1;
            }
             else if (state.IsKeyDown(Keys.S))
            {
                Direction.Y = 1;
            }
             if (state.IsKeyDown(Keys.D))
            {
                Direction.X = 1;
            }
            if (state.IsKeyDown(Keys.A))
            {
                Direction.X = -1;
            }

            Move(Direction);
           
        }

        //Player movement
        public void Move(Vector2 direction)
        {
            Console.WriteLine(direction * setSpeed(direction));
            position += direction * setSpeed(direction);
        }

        public float setSpeed(Vector2 direction)
        {
            return (10 - ((direction.X * direction.X) + (direction.Y) * (direction.Y)));
        }

        //To be called if the player runs into an arrow
        public void Kill()
        {
            isAlive = false;
            isActive = false;
        }

        //Draw method called every frame
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Draw(Sprite, this.Position, null, Color.Green, 0, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
            }
        }
    }
}
