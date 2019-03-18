using System;

namespace Console.Abstractions
{
	/// <summary>
	/// Caches the properties of the console, and only writes them to the console when they change.
	/// </summary>
	public class PropertyCacheConsole : Console
	{
		private readonly Console _console;

		public PropertyCacheConsole(Console console)
		{
			_console = console;

			Width = _console.Width;
			Height = _console.Height;

			_x = _console.X;
			_y = _console.Y;
			_foreground = _console.Foreground;
			_background = _console.Background;
		}

		public override ConsoleKeyInfo ReadKey(bool intercept)
		{
			var result = _console.ReadKey(intercept);

			if (intercept)
			{
                UpdateScreenXBy(1);
			}

			return result;
		}

		public override string ReadLine()
		{
            var read = _console.ReadLine();

            UpdateScreenXBy(read.Length);

			return read;
		}

		public override void Write(char chr)
		{
            UpdateScreenXBy(1);

			_console.Write(chr);
		}

		public override void Write(string line)
		{
            // just to update the x so we don't update it every time
			UpdateScreenXBy(line.Length);

			_console.Write(line);
		}

		public override void Clear()
		{
			_console.Clear();

			_x = 0;
			_y = 0;
		}

		public override int Width { get; }
		public override int Height { get; }

		private int _x;

		public override int X
		{
			get => _x;
			set
			{
				if (value == _x) return;

				_x = value;
				_console.X = value;
			}
		}

		private int _y;

		public override int Y
		{
			get => _y;
			set
			{
				if (value == _y) return;

				_y = value;
				_console.Y = value;
			}
		}

		private ConsoleColor _foreground;

		public override ConsoleColor Foreground
		{
			get => _foreground;
			set
			{
				if (value == _foreground) return;

				_foreground = value;
				_console.Foreground = value;
			}
		}

		private ConsoleColor _background;

		public override ConsoleColor Background
		{
			get => _background;
			set
			{
				if (value == _background) return;

				_background = value;
                _console.Background = value;
			}
		}

		private void UpdateScreenXBy(int amt)
		{
			_x += amt;

			while (_x >= Width)
			{
				_x -= Width;
				_y++;
			}

			while (_y > Height)
			{
				_y -= Height;
			}
        }
	}
}