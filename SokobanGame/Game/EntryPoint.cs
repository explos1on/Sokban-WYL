using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Game
    {
        /// <summary>
        /// You can test the functionallity of the classes here. Entry point for the console application.
        /// </summary>
        static void Main()
        {
            var engine = new GameEngine('x', 'o', '#', 'O');
            engine.Start();
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
