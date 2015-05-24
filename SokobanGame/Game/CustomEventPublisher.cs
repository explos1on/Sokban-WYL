using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Custom class containing the information about what key the user pressed.
    /// We extend EventArgs so we can publish the event with there perticular parameters.
    /// </summary>
    public class CustomEventArgs : EventArgs
    {
        public ConsoleKey KeyPressed { get; set; }
    }

    /// <summary>
    /// Reads user input and publishes an event containing information about the user action (what the user pressed).
    /// </summary>
    public class CustomEventPublisher
    {
        /// <summary>
        /// This is the event we will attach to in the game engine and handle input from there
        /// </summary>
        public  static event EventHandler<CustomEventArgs> OnKeyPressed;

        /// <summary>
        /// Read from console and publish an event, if someone is subscribed.
        /// </summary>
        public static void ProcessInput()
        {
            if (Console.KeyAvailable)
            {
                CustomEventArgs eventArgs = new CustomEventArgs();
                ConsoleKeyInfo ki = Console.ReadKey();
                if (ki.Key == ConsoleKey.DownArrow)
                {
                    eventArgs.KeyPressed = ConsoleKey.DownArrow;
                }
                else if (ki.Key == ConsoleKey.UpArrow)
                {
                    eventArgs.KeyPressed = ConsoleKey.UpArrow;
                }
                else if (ki.Key == ConsoleKey.LeftArrow)
                {
                    eventArgs.KeyPressed = ConsoleKey.LeftArrow;
                }
                else if (ki.Key == ConsoleKey.RightArrow)
                {
                    eventArgs.KeyPressed = ConsoleKey.RightArrow;
                }
                else if (ki.Key == ConsoleKey.Escape)
                {
                    eventArgs.KeyPressed = ConsoleKey.Escape;
                }

                //if someone is subscribed to our event ... send him information about user input
                if (OnKeyPressed != null)
                {
                    OnKeyPressed.Invoke(new Object(), eventArgs);
                }
            }

        }
    }
}
