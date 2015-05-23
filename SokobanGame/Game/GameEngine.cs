using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{

    //Тук е логиката на играта и от тук се управляват останалите класове.
    public class GameEngine
    {
        public static int Default_Field_Size = 30;
        public static char Default_Empty_Field = ' ';

        public GameEngine(char hero, char box, char wall, char elevator)
        {
            this.Hero = new Hero(hero);
            this.Box = box;
            this.Wall = wall;
            this.Elevator = elevator;

            this.MovableGameObjects = InitializeMovableGameObjects();
            this.MovableGameObjects.Add(this.Hero);
            InitializeDefaultGameField();
            this.GameField = new char[Default_Field_Size, Default_Field_Size];
            Array.Copy(this.DefaultGameField, this.GameField, Default_Field_Size * Default_Field_Size);
        }

        private List<GameObject> InitializeMovableGameObjects()
        {
            var list = new List<GameObject>();
            var initialPositions = new List<Position>();
            initialPositions.Add(new Position(4, 4));
            initialPositions.Add(new Position(8, 8));
            initialPositions.Add(new Position(8, 4));
            initialPositions.Add(new Position(4, 8));

            foreach (var pos in initialPositions)
            {
                list.Add(new GameObject(pos, this.Box));
            }

            return list;
        }

        public List<GameObject> MovableGameObjects { get; set; }

        public char[,] GameField { get; set; }

        public char[,] DefaultGameField { get; set; }

        public Position HeroPosition { get; set; }

        public Hero Hero { get; set; }

        public char Box { get; set; }

        public char Wall { get; set; }

        public char Elevator { get; set; }

        public void Start()
        {
            CustomEventHandler.OnKeyPressed += this.HandleKeyPressed;
            while (true)
            {
                CustomEventHandler.ProcessInput();
                this.UpdateGameObjects();
                Drawer.PrintField(this.GameField);
                Thread.Sleep(20);

                Drawer.Clear();
            }
        }

        private void UpdateGameObjects()
        {
            Array.Copy(this.DefaultGameField, this.GameField, Default_Field_Size * Default_Field_Size);
            foreach (var obj in this.MovableGameObjects)
            {
                this.GameField[obj.Position.Row, obj.Position.Col] = obj.Body;
            }
        }

        private void HandleKeyPressed(object o, CustomEventArgs ea)
        {
            Direction direction = Direction.None;
            if (ea.KeyPressed == ConsoleKey.UpArrow)
            {
                this.Hero.Position.Row--;
                direction = Direction.Up;
            }
            else if (ea.KeyPressed == ConsoleKey.DownArrow)
            {
                this.Hero.Position.Row++;
                direction = Direction.Down;
            }
            else if (ea.KeyPressed == ConsoleKey.LeftArrow)
            {
                this.Hero.Position.Col--;
                direction = Direction.Left;
            }
            else if (ea.KeyPressed == ConsoleKey.RightArrow)
            {
                this.Hero.Position.Col++;
                direction = Direction.Right;
            }
            else if (ea.KeyPressed == ConsoleKey.Escape)
            {
                Environment.Exit(1);
            }

            CollisionsDetector.HandleCollisions(this.MovableGameObjects, this.Hero, direction);
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

            for (int row = 0; row < Default_Field_Size; row++)
            {
                this.DefaultGameField[row, 0] = this.Wall;
                this.DefaultGameField[row, Default_Field_Size - 1] = this.Wall;
            }

            for (int col = 0; col < Default_Field_Size; col++)
            {
                this.DefaultGameField[0, col] = this.Wall;
                this.DefaultGameField[Default_Field_Size - 1, col] = this.Wall;
            }
        }
    }
}
