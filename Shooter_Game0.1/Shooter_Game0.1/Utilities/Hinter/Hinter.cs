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
        private static bool UpCloser;
        private static bool DownCloser;
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
            if(UpCloser)
            {
                return string.Format(OutputMessages.HintClosestUpEnemy, rows, cols);
            }
            return string.Format(OutputMessages.HintClosestDownEnemy, rows, cols);


        }
        private static Dictionary<int,int> EnemiesDownFromPointer(int pointerXCoordinate, int pointerYCoordinate, string[,] terrain, Dictionary<Dictionary<int, int>, IEnemy> enemiesCoordinates)
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
        private static Dictionary<int, int> EnemiesUpFromPointer(int pointerXCoordinate, int pointerYCoordinate, string[,] terrain, Dictionary<Dictionary<int, int>, IEnemy> enemiesCoordinates)
        {
            bool isFound = false;
            Dictionary<int, int> returnData = new Dictionary<int, int>();

            for (int rows = pointerXCoordinate - 1; rows >= 0; rows--)
            {
                for (int cols = pointerYCoordinate; cols < terrain.GetLength(1); cols++)
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
                if (isFound)
                {
                    break;
                }
            }
                

            
            return returnData;
        }

        private static Dictionary<int,int> ReturnClosestEnemy(int pointerXCoordinate, int pointerYCoordinate, string[,] terrain, Dictionary<Dictionary<int, int>, IEnemy> enemiesCoordinates)
        {
            int upSumSteps = 0;
            int downSumSteps = 0;
           
            Dictionary<int, int> upEnemies = Hinter.EnemiesUpFromPointer(pointerXCoordinate, pointerYCoordinate, terrain, enemiesCoordinates);
            Dictionary<int, int> downEnemies = Hinter.EnemiesDownFromPointer(pointerXCoordinate, pointerYCoordinate, terrain, enemiesCoordinates);
            foreach (var upEnemy in upEnemies)
            {
                foreach (var downEnemy in downEnemies)
                {
                    upSumSteps = upEnemy.Key + upEnemy.Value;
                    downSumSteps = downEnemy.Key+ downEnemy.Value;
                }
            }
            if(upSumSteps<downSumSteps)
            {
                UpCloser = true;
                return upEnemies;
            }
            DownCloser = true;
            return downEnemies;
            
        }
    }
}
