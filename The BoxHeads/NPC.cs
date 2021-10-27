using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame2
{
    //Superclass for all non player characters
    public abstract class NPC: DrawableGameComponent
    {
        //Fields
        public bool isActive;
        public float rotation;



        //Constructors
        public NPC(Game game) : base(game)
        {

        }


        //Properties
        public bool IsActive
        {
            get; set;
        } = true;

        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                //Limit rotation to 0 <= rotation < 360
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

        //Methods

 
    }
}
