using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// The main part of the game's logic is here...all other classes are controlled from here
    /// </summary>
    public class GameEngine
    {
        public static int Default_Field_Size = 20;
        public static int Default_Elevator_count = 4;
        public static char Default_Empty_Field = ' ';

        //constructor
        public GameEngine(char hero, char box, char wall, char elevator)
        {
            this.Hero = new Hero(hero);
            this.Box = box;
            this.Wall = wall;
            this.Elevator = elevator;

            InitializeAllGameObjects();
            InitializeGameField();
        }

        /// <summary>
        /// All game objects including the walls, hero, elevators and boxes are in this list.
        /// </summary>
        public List<GameObject> GameObjects { get; set; }

        /// <summary>
        /// This is the field that will be drawn on the console.
        /// </summary>
        public char[,] GameField { get; set; }

        /// <summary>
        /// This is used to reset the game field to its original state (empty)
        /// </summary>
        public char[,] DefaultGameField { get; set; }

        public Position HeroPosition { get; set; }

        public Hero Hero { get; set; }

        public char Box { get; set; }

        public char Wall { get; set; }

        public char Elevator { get; set; }

        /// <summary>
        /// Starting point of the game (facade). 
        /// First of all we add a handler (HandleKeyPressed) so that we know what the user wants to do.
        /// After we know how the user moved we update position of the game objects in the handler, if necessary.
        /// The repeating steps after that are the following.
        /// 1) Publish what the user pressed. (ProcessInput).
        /// 2) Set GameObjects on field (UpdateGameObjects).
        /// 3) Draw field on console.
        /// </summary>
        public void Start()
        {
            //subscribe for user input and provide a handler for all user actions
            CustomEventPublisher.OnKeyPressed += this.HandleKeyPressed;

            while (true)
            {
                CustomEventPublisher.ProcessInput();
                this.UpdateGameObjects();
                Drawer.PrintField(this.GameField);

                if (GameEnded())
                {
                    break;
                }
                Thread.Sleep(20);
                Drawer.Clear();
            }

            Console.WriteLine("You won!");
        }

        private bool GameEnded()
        {
            var count = 0;
            foreach (var elevator in this.GameObjects)
            {
                if (elevator.Body == this.Elevator)
                {
                    foreach (var box in this.GameObjects)
                    {
                        if (box.Body == this.Box && box.Position == elevator.Position)
                        {
                            count++;
                        }
                    }
                }
            }
            if (count == Default_Elevator_count)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This method sets the game objects on the field as chars ... so they can be drawn later on.
        /// </summary>
        private void UpdateGameObjects()
        {
            Array.Copy(this.DefaultGameField, this.GameField, Default_Field_Size * Default_Field_Size);
            foreach (var obj in this.GameObjects)
            {
                this.GameField[obj.Position.Row, obj.Position.Col] = obj.Body;
            }
        }

        /// <summary>
        /// This method is called when the user presses any key. 
        /// It handles the user actions.
        /// </summary>
        /// <param name="o">Sender of the event.</param>
        /// <param name="eventArgs">The passed eventArgs holding the information about what key the user pressed.</param>
        private void HandleKeyPressed(object o, CustomEventArgs eventArgs)
        {
            Direction direction = Direction.None;
            if (eventArgs.KeyPressed == ConsoleKey.UpArrow)
            {
                this.Hero.Position.Row--;
                direction = Direction.Up;
            }
            else if (eventArgs.KeyPressed == ConsoleKey.DownArrow)
            {
                this.Hero.Position.Row++;
                direction = Direction.Down;
            }
            else if (eventArgs.KeyPressed == ConsoleKey.LeftArrow)
            {
                this.Hero.Position.Col--;
                direction = Direction.Left;
            }
            else if (eventArgs.KeyPressed == ConsoleKey.RightArrow)
            {
                this.Hero.Position.Col++;
                direction = Direction.Right;
            }
            else if (eventArgs.KeyPressed == ConsoleKey.Escape)
            {
                //if user pressed "Esc" exit the program... (1 - because it's expected behavior)
                Environment.Exit(1);
            }

            //the user moved so we update objects based on the user action
            CollisionsDetector.HandleCollisions(this.GameObjects, this.Hero, direction, this.Elevator);
        }

        /// <summary>
        /// Sets an empty field.
        /// </summary>
        private void InitializeGameField()
        {
            this.GameField = new char[Default_Field_Size, Default_Field_Size];
            Array.Copy(this.DefaultGameField, this.GameField, Default_Field_Size * Default_Field_Size);
        }

        private void InitializeAllGameObjects()
        {
            this.GameObjects = new List<GameObject>();
            AddMovableGameObjects();
            AddHeroToGameObjects();
            AddImmovableObjects();
            InitializeDefaultGameField();
        }

        private void AddHeroToGameObjects()
        {
            this.GameObjects.Add(this.Hero);
        }

        /// <summary>
        /// Adds walls and elevators.
        /// </summary>
        private void AddImmovableObjects()
        {
            AddWalls();
            AddElevators();
        }

        private void AddElevators()
        {
            for (int col = Default_Field_Size / 2 - Default_Elevator_count; col < Default_Field_Size / 2; col++)
			{
                var currentElevatorPosition = new Position(Default_Field_Size - 4, col);
                var currentElevatorObject = new GameObject(currentElevatorPosition, this.Elevator, false);
                this.GameObjects.Add(currentElevatorObject);
			}
            
        }

        private void AddWalls()
        {
            for (int row = 1; row < Default_Field_Size - 1; row++)
            {
                //add left wall
                var currentPositionLeft = new Position(row, 0);
                this.GameObjects.Add(new GameObject(currentPositionLeft, this.Wall, false));

                //add right wall
                var currentPositionRight = new Position(row, Default_Field_Size - 1);
                this.GameObjects.Add(new GameObject(currentPositionRight, this.Wall, false));
            }

            for (int col = 0; col < Default_Field_Size; col++)
            {
                //add up wall
                var currentPositionUp = new Position(0, col);
                this.GameObjects.Add(new GameObject(currentPositionUp, this.Wall, false));

                //add bottom wall
                var currentPositionBottom = new Position(Default_Field_Size - 1, col);
                this.GameObjects.Add(new GameObject(currentPositionBottom, this.Wall, false));
            }
        }

        /// <summary>
        /// Adds boxes.
        /// </summary>
        private void AddMovableGameObjects()
        {
            var initialPositions = new List<Position>();
            initialPositions.Add(new Position(4, 4));
            initialPositions.Add(new Position(8, 8));
            initialPositions.Add(new Position(8, 4));
            initialPositions.Add(new Position(4, 8));

            foreach (var pos in initialPositions)
            {
                this.GameObjects.Add(new GameObject(pos, this.Box, true));
            }
        }

        private void InitializeDefaultGameField()
        {
            this.DefaultGameField = new char[Default_Field_Size, Default_Field_Size];
            for (int row = 0; row < Default_Field_Size; row++)
            {
                for (int col = 0; col < Default_Field_Size; col++)
                {
                    this.DefaultGameField[row, col] = Default_Empty_Field;
                }
            }
        }
    }
}
