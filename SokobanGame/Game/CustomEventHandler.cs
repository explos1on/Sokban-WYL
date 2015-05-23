using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class CustomEventArgs : EventArgs
    {
        public ConsoleKey KeyPressed { get; set; }
    }

    //Обработва и приема input и да го предава и на engine.
    public class CustomEventHandler
    {

        public  static event EventHandler<CustomEventArgs> OnKeyPressed;
        public static void ProcessInput()
        {
            if (Console.KeyAvailable)
            {
                CustomEventArgs ea = new CustomEventArgs();
                ConsoleKeyInfo ki = Console.ReadKey();
                if (ki.Key == ConsoleKey.DownArrow)
                {
                    ea.KeyPressed = ConsoleKey.DownArrow;
                }
                else if (ki.Key == ConsoleKey.UpArrow)
                {
                    ea.KeyPressed = ConsoleKey.UpArrow;
                }
                else if (ki.Key == ConsoleKey.LeftArrow)
                {
                    ea.KeyPressed = ConsoleKey.LeftArrow;
                }
                else if (ki.Key == ConsoleKey.RightArrow)
                {
                    ea.KeyPressed = ConsoleKey.RightArrow;
                }
                else if (ki.Key == ConsoleKey.Escape)
                {
                    ea.KeyPressed = ConsoleKey.Escape;
                }

                if (OnKeyPressed != null)
                {
                    OnKeyPressed.Invoke(new Object(), ea);
                }
            }

        }
    }
}
