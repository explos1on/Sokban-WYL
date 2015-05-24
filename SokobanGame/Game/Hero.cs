using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Hero : GameObject
    {
        public Hero(char body)
            : base(new Position(1, 1), body, true)
        {

        }
    }
}
