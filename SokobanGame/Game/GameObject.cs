using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Represents a sokoban game object.
    /// </summary>
    public class GameObject
    {
        public GameObject(Position position, char body, bool isMovable)
        {
            this.Position = position;
            this.Body = body;
            this.IsMovable = isMovable;
        }

        public Position Position { get; set; }

        public char Body { get; set; }

        public bool IsMovable { get; set; }
    }
}
