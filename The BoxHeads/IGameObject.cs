using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame2
{
    //Common interface for all game objects
    public interface IGameObject
    {
        void Initialize();
        void Update(GameTime gameTime);
        float Rotation { get; set; }
        bool IsActive { get; set; }
        Vector2 Position { get; set; }
        Rectangle BoundingBox { get; }
        string Type { get; }

        // static Texture2D Sprite { get; }
        void Draw(SpriteBatch spriteBatch);
    }
}
