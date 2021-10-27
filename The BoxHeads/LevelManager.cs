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
    //Class to help with the changing of levels, often called in Game class
    public static class LevelManager
    {
        private static int level = 0;
        private static Vector2 screenSize;

        //Properties
        public static int Enemies
        {
            get; set;
        } = 50;

        public static int Level
        {
           get
            {
                return level;

            }
           set
            {
                /*
                if (level + value < 1)
                {
                    level = 1;
                }
                else
                {
                */
                    level = value;
                //}
            }
        }

   

        //Methods

        //Returns a list of Enemy object instances
        public static List<Enemy> spawnEnemies(Random random, Game game,  (int, int) displayDimensions, Player player)
        {
            List<Enemy> enemyList = new List<Enemy>();

            for (int i = 0; i < Enemies; i++) {
                Vector2 randomPosition = randomSpawn(random, displayDimensions);
                while (Vector2.Distance(player.Position, randomPosition) < 300)
                {
                    randomPosition = randomSpawn(random, displayDimensions);
                }
                enemyList.Add(new Enemy((float) random.Next(1, Level + 1)  ,random, game, player, randomPosition));
            }
            return enemyList;
        }

        //Returns a random position on the screen
        private static Vector2 randomSpawn(Random random, (int, int) displayDimensions)
        {
            Vector2 randomPos = new Vector2(random.Next(0, displayDimensions.Item1), random.Next(0, displayDimensions.Item2));

            screenSize = new Vector2(displayDimensions.Item1, displayDimensions.Item2);

            return randomPos;


        }

        //Returns a new TargetArea with a random location and size
        public static TargetArea NewTarget(Random random, Game game)
        {
            Vector2 position = new Vector2(random.Next(200, 800), random.Next(200, 800));
            int size = (random.Next(300, 400));


            TargetArea newTargetArea = new TargetArea(position, (size, size), game);

            return newTargetArea;
        }

       
        
        
    }
}
