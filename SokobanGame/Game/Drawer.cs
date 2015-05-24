using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Class to deal with console
    /// </summary>
    public class Drawer
    {
        /// <summary>
        /// Draw on console passed field.
        /// </summary>
        /// <param name="field">The preset field to be drawn.</param>
        public static void PrintField(char[,] field)
        {
            StringBuilder fillTheField = new StringBuilder();
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    fillTheField.Append(field[i, j]);
                }
                fillTheField.AppendLine();
            }
            Console.WriteLine(fillTheField);
        }

        public static void Clear()
        {
            Console.Clear();
        }
    }
}
