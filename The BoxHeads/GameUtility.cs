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
    public sealed class GameUtility
    {
        
        private static GameUtility instance = null;
        private static readonly object _lock = new object();

        public ContentManager ContentManager { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public static GameUtility Instance
        {
            get
            {
                Debug.Write("getting instance!");
                lock (_lock)
                {
                    if (instance == null)
                    {
                        
                        instance = new GameUtility();
                    }

                    return instance;
                }
            }
        }

        public void SetContentManager(ContentManager contentManager)
        {
            this.ContentManager = contentManager;
        }

        public void SetSpriteBatch(SpriteBatch spriteBatch)
        {
            this.SpriteBatch = spriteBatch;
        }

        public GameUtility(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            this.ContentManager = contentManager;
            this.SpriteBatch = spriteBatch;
        }
        public GameUtility()
        {
          
        }
    }
}
