using JetBrains.Annotations;

using System;

namespace Console.Abstractions
{
    [PublicAPI]
    public class SystemConsole : IConsole
    {
        public string ReadLine() => System.Console.ReadLine();

        public ConsoleKeyInfo ReadKey(bool intercept) => System.Console.ReadKey(intercept);

        public void Write(string line) => System.Console.Write(line);

        public void Clear() => System.Console.Clear();

        public int X
        {
            get => System.Console.CursorLeft;
            set => System.Console.CursorLeft = value;
        }

        public int Y
        {
            get => System.Console.CursorTop;
            set => System.Console.CursorTop = value;
        }

        public int Width => System.Console.WindowWidth;

        public int Height => System.Console.WindowHeight;

        public ConsoleColor Foreground
        {
            get => System.Console.ForegroundColor;
            set => System.Console.ForegroundColor = value;
        }

        public ConsoleColor Background
        {
            get => System.Console.BackgroundColor;
            set => System.Console.BackgroundColor = value;
        }
    }
}