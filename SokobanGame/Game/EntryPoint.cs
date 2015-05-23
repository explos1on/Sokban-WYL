using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Game
    {
        //Може да се тества функциалността на класовете тук.
        static void Main()
        {
            CustomEventHandler.OnKeyPressed += HandleKeyPressed;
            while (true)
            {
                CustomEventHandler.ProcessInput();
            }
            //char[,] map = new char[3,3];
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        map[i,j] = 'c';
            //    }
            //}
            //Drawer.PrintField(map);
        }
        public static void HandleKeyPressed(object o, CustomEventArgs ea)
        {
            Console.WriteLine(ea.KeyPressed);
        }
    }

    //// public static void PrintMatrix(int[,] matrix) // this is Method who print a matrix.In our case the map of the game.
    //    {
    //        for (int row = 0; row < matrix.GetLength(0); row++)
    //        {
    //            for (int col = 0; col < matrix.GetLength(1); col++)
    //            {
    //                Console.Write(matrix[row, col]);
    //            }
    //            Console.WriteLine();
    //        }
    //    }

}
