using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class CollisionsDetector
    {
        public static void HandleCollisions(List<GameObject> gameObjects, Hero hero, Direction direction)
        {
            foreach (var obj in gameObjects)
            {
                if (obj.Body == hero.Body)
                {
                    continue;
                }

                if (obj.Position.Row == hero.Position.Row && obj.Position.Col == hero.Position.Col)
                {
                    if (direction == Direction.Up)
                    {
                        obj.Position.Row--;
                    }
                    else if (direction == Direction.Down)
                    {
                        obj.Position.Row++;
                    }
                    else if (direction == Direction.Left)
                    {
                        obj.Position.Col--;
                    }
                    else if (direction == Direction.Right)
                    {
                        obj.Position.Col++;
                    }
                }
            }
        }
    }
}
