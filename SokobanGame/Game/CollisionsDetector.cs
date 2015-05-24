using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Deals with all objects interaction
    /// </summary>
    public class CollisionsDetector
    {
        /// <summary>
        /// Handles all kinds of collisions. Moves object positions if necessary.
        /// </summary>
        /// <param name="gameObjects">All game objects ... boxes, elevators etc.</param>
        /// <param name="hero">Our sokoban hero.</param>
        /// <param name="direction">The direction of the hero's movement.</param>
        public static void HandleCollisions(List<GameObject> gameObjects, Hero hero, Direction direction, char elevator)
        {
            foreach (var obj in gameObjects)
            {
                if (obj.Body == hero.Body)
                {
                    continue;
                }

                if (obj.Position == hero.Position && obj.IsMovable)
                {
                    Position futurePositionOfObject;
                    if (direction == Direction.Up)
                    {
                        futurePositionOfObject = new Position(obj.Position.Row - 1, obj.Position.Col);

                        if (InteractsWithAnyObject(futurePositionOfObject, gameObjects, elevator))
                        {
                            hero.Position.Row++;
                            continue;
                        }
                        obj.Position.Row--;
                    }
                    else if (direction == Direction.Down)
                    {
                        futurePositionOfObject = new Position(obj.Position.Row + 1, obj.Position.Col);

                        if (InteractsWithAnyObject(futurePositionOfObject, gameObjects, elevator))
                        {
                            hero.Position.Row--;
                            continue;
                        }
                        obj.Position.Row++;
                    }
                    else if (direction == Direction.Left)
                    {
                        futurePositionOfObject = new Position(obj.Position.Row, obj.Position.Col - 1);

                        if (InteractsWithAnyObject(futurePositionOfObject, gameObjects, elevator))
                        {
                            hero.Position.Col++;
                            continue;
                        }
                        obj.Position.Col--;
                    }
                    else if (direction == Direction.Right)
                    {
                        futurePositionOfObject = new Position(obj.Position.Row, obj.Position.Col + 1);

                        if (InteractsWithAnyObject(futurePositionOfObject, gameObjects, elevator))
                        {
                            hero.Position.Col--;
                            continue;
                        }
                        obj.Position.Col++;
                    }
                }
                else if (obj.Position == hero.Position && !obj.IsMovable && obj.Body != elevator)
                {
                    if (direction == Direction.Up)
                    {
                        hero.Position.Row++;
                    }
                    else if (direction == Direction.Down)
                    {
                        hero.Position.Row--;
                    }
                    else if (direction == Direction.Left)
                    {
                        hero.Position.Col++;
                    }
                    else if (direction == Direction.Right)
                    {
                        hero.Position.Col--;
                    }
                }
            }
        }

        private static bool InteractsWithAnyObject(Position futurePositionOfObject, List<GameObject> gameObjects, char elevator)
        {
            foreach (var obj in gameObjects)
            {
                if (futurePositionOfObject == obj.Position && obj.Body != elevator)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
