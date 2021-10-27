using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Monogame2
{
    //Class for checking if hitboxes are colliding
    class CollisionChecks
    {
        //Returns true if 
        public static bool AABB(IGameObject objectA, IGameObject objectB)
        {
            if (objectA == objectB)
            {
                return false;
            }
            else
            {
                Rectangle boxA = objectA.BoundingBox;
                Rectangle boxB = objectB.BoundingBox;
                return (boxA.Left < boxB.Right &&
               boxA.Right > boxB.Left &&
               boxA.Top < boxB.Bottom &&
               boxA.Bottom > boxB.Top);
            }
        }
    }
}
