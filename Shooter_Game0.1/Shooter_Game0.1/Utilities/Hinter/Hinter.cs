using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Utilities.Hinter
{
    public static class Hinter
    {
        private static bool leftCloser;
        private static bool rightCloser;
        public static string GetHint(int pointerXCoordinate,int pointerYCoordinate,string[,] terrain, Dictionary<Dictionary<int, int>, IEnemy> enemiesCoordinates)
        {
            int rows = 0;
            int cols = 0;

            Dictionary<int, int> closestEnemies = Hinter.ReturnClosestEnemy(pointerXCoordinate,pointerYCoordinate,terrain,enemiesCoordinates);
            foreach (var enemy in closestEnemies)
            {
                rows = enemy.Key;
                cols = enemy.Value;
            }
            if(leftCloser)
            {
                return string.Format(OutputMessages.HintClosestLeftEnemy, rows, cols);
            }
            return string.Format(OutputMessages.HintClosestRightEnemy, rows, cols);


        }
        private static Dictionary<int,int> EnemiesDownAndRightFromPointer(int pointerXCoordinate, int pointerYCoordinate, string[,] terrain, Dictionary<Dictionary<int, int>, IEnemy> enemiesCoordinates)
        {
            bool isFound = false;
            Dictionary<int, int> returnData = new Dictionary<int, int>();
            for (int rows = pointerXCoordinate; rows < terrain.GetLength(0); rows++)
            {
                for (int cols = pointerYCoordinate; cols < terrain.GetLength(1); cols++)
                {
                    foreach (var enemyCoordinate in enemiesCoordinates)
                    {
                        foreach (var kvp in enemyCoordinate.Key)
                        {
                            if(rows == kvp.Key && cols == kvp.Value)
                            {
                                returnData.Add(rows, cols);
                                isFound = true;
                                break;
                            }
                        }
                        if(isFound)
                        {
                            break;
                        }
                    }
                    if (isFound)
                    {
                        break;
                    }
                }
                if (isFound)
                {
                    break;
                }

            }
            return returnData;
        }
        private static Dictionary<int, int> EnemiesUpAndLeftFromPointer(int pointerXCoordinate, int pointerYCoordinate, string[,] terrain, Dictionary<Dictionary<int, int>, IEnemy> enemiesCoordinates)
        {
            bool isFound = false;
            Dictionary<int, int> returnData = new Dictionary<int, int>();
            for (int rows = pointerXCoordinate - 1; rows >= 0; rows--)
            {
                for (int cols = pointerYCoordinate - 1; cols >= 0; cols--)
                {
                    foreach (var enemyCoordinate in enemiesCoordinates)
                    {
                        foreach (var kvp in enemyCoordinate.Key)
                        {
                            if (rows == kvp.Key && cols == kvp.Value)
                            {
                                returnData.Add(rows, cols);
                                isFound = true;
                                break;
                            }
                        }
                        if(isFound)
                        {
                            break;
                        }
                    }
                    if (isFound)
                    {
                        { break; }
                    }
                }
                if(isFound)
                {
                    break;
                }
            }
            return returnData;
        }
        private static Dictionary<int,int> ReturnClosestEnemy(int pointerXCoordinate, int pointerYCoordinate, string[,] terrain, Dictionary<Dictionary<int, int>, IEnemy> enemiesCoordinates)
        {
            int leftSumSteps = 0;
            int rightSumSteps = 0;
            Dictionary<int, int> leftEnemies = Hinter.EnemiesUpAndLeftFromPointer( pointerXCoordinate,  pointerYCoordinate,  terrain, enemiesCoordinates);
            Dictionary<int, int> rightEnemies = Hinter.EnemiesDownAndRightFromPointer(pointerXCoordinate, pointerYCoordinate, terrain, enemiesCoordinates);
            foreach (var leftEnemy in leftEnemies)
            {
                foreach (var rightEnemy in rightEnemies)
                {
                    leftSumSteps = leftEnemy.Key + leftEnemy.Value;
                    rightSumSteps = rightEnemy.Key+ rightEnemy.Value;
                }
            }
            if(leftSumSteps<rightSumSteps)
            {
                leftCloser = true;
                return leftEnemies;
            }
            rightCloser = true;
            return rightEnemies;
            
        }
    }
}
