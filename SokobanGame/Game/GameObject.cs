using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameObject
    {
        public GameObject(Position position, char body)
        {
            this.Position = position;
            this.Body = body;
        }

        public Position Position { get; set; }

        public char Body { get; set; }
    }
}
