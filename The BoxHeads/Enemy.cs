using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace Monogame2
{
    public class Enemy: NPC, IGameObject
    {
        protected Random random;
        protected Player player;
        public Vector2 lastVelocity;
        public Color color = Color.White;
       // private bool doSpin = false;
        Random rd = new Random();
        private Vector2 position;
        private Vector2 velocity;
      //  private float speed = 10f;
        public Vector2 moveDirection = new Vector2(1, 1);

        //Static fields
        static public List<IGameObject> gameObjects;

        //Constructors
        public Enemy(float speed, Random rand, Game game, Player player, Vector2 position = new Vector2()) : base(game)
        {
            this.Speed = speed;
            this.random = rand;
            this.player = player;
            this.Position = position;
        }



        //Properties

        public bool IsInZone
        {
            get; set;
        } = false;
        public Color Color
        {
            get; set;
        } = Color.White;
        public string Type
        {
            get
            {
                return "Enemy";
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 10,10);
            }
        }

        public float Speed
        { get; set; } = 1f;
        public static Texture2D Sprite
        {
            get; set;
        }

        public Vector2 Position
        {
            get { return position;}
            set { if (value.Y <= 1000 && value.X < 1000)
                {
                    position = value;
                } }
        }

      



        public Vector2 Velocity
        {
            get { return velocity; }
            set
            {
                velocity = value;
            }
        }



        public Vector2 RandomPos()
        {
            return new Vector2(rd.Next(0, 800), rd.Next(0, 480));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, this.Position, null, Color, this.Rotation, Vector2.Zero, 0.01f, SpriteEffects.None, 0f);
          
        }

        public void Initialize()
        {
            Sprite = Game.Content.Load<Texture2D>("redArrow2");
            IsActive = true;

            
        }

       
 
        public void Update (GameTime gameTime)
        {
            Color = Color.White;
            IsInZone = false;

            moveTo(player.Position);
        }

        void moveTo(Vector2 location)
        {
            
            Vector2 dir = player.Position - this.Position;
            dir.Normalize();
              Rotation = (float)Math.Atan2(dir.Y, dir.X);
            //Collision detection
            foreach (IGameObject gameObject in gameObjects)
            {
              
                if (CollisionChecks.AABB(gameObject, this))
                {
                    if (gameObject == player)
                    {
                        player.Kill();
                    }
                    if (gameObject.Type != "TargetArea")
                    {
                        Position -= (gameObject.Position - Position) / 30;
                        return;
                    }
                        else
                    {
                        Color = Color.Black;
                        IsInZone = true;
                    }
                }
                  
                
                
            }
            
       

            Position += dir * Speed;
        }









    }
}
