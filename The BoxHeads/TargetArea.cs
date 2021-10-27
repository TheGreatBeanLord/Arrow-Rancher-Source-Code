using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame2
{
    public class TargetArea : DrawableGameComponent, IGameObject
    {
        //Fields
        (int, int) size;
        //Static fields
        public static Random random;
        public static int screenX;
        public static int screenY;
        //Constructors
        public TargetArea(Vector2 position, (int, int) size, Game game): base(game)
        {
            Position = position;
            this.size = size;
        }

        //Properties

        public (int, int) Size
        {
            get
            {
                if (size.Item1 > 100 || size.Item2 > 100)
                {
                    return size;
                }
                else
                {
                    size = (100,100);
                    return size;
                }
            }
            set
            {
                size = value;
            }
        }

        public string Type
        {
            get
            {
                return "TargetArea";
            }
        }
        public float Rotation
        {
            get; set;
        } = 0f;

        public bool IsActive
        {
            get; set;
        } = true;
        
        public Vector2 Position
        {
            get; set;
        }

        public static Color Color
        {
            get; set;
        } = Color.White;

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, size.Item1, size.Item2);
            }
        }

        public Texture2D Sprite
        {
            get; set;
        }

        //Methods

        protected override void LoadContent()
        {
            Debug.WriteLine("KILL");
          //  Sprite = Game.Content.Load<Texture2D>("playerSprite");
            base.LoadContent();
        }

        public void Initialize()
        {
            IsActive = true;
            Sprite = Game.Content.Load<Texture2D>("blackPixel");
        }

        public void Update(GameTime gameTime)
        {
        

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                spriteBatch.Draw(Sprite, this.Position, null, Color.White * 0.5f, 0, Vector2.Zero, Size.Item1, SpriteEffects.None, 0f);
            }
        }

        public void RandomTarget() {
            this.Position = new Vector2(random.Next(size.Item1, screenX - size.Item1), random.Next(size.Item2, screenY - size.Item2));
        }

    }
}
