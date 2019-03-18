using System;
using System.Collections.Generic;
using System.Text;

namespace Console.Abstractions
{
	/// <summary>
    /// Only applies properties to the console when writing or reading.
    /// </summary>
	public class PropertyApplyCacheConsole : Console
	{
		private readonly Console _console;

		public PropertyApplyCacheConsole(PropertyCacheConsole propertyCacheConsole)
			: this((Console)propertyCacheConsole)
		{
		}

		[Obsolete("You might be using the PropertyApplyCacheConsole incorrently."
			+ "Try paas in a PropertyCacheConsole instead to the constructor,"
			+ "instead of using the generic Console constructor, as that's the ideal usage.")]
		public PropertyApplyCacheConsole(Console console)
		{
            _console = console;

			Width = _console.Width;
			Height = _console.Height;

			SetProperties();
		}

		public override ConsoleKeyInfo ReadKey(bool intercept)
		{
			SetProperties();

			var result = _console.ReadKey(intercept);

			GetProperties();

			return result;
		}

		public override int Width { get; }
		public override int Height { get; }

		public override string ReadLine()
		{
			SetProperties();

			var result = _console.ReadLine();

			GetProperties();

			return result;
		}

		public override void Write(char chr)
		{
			SetProperties();

			_console.Write(chr);

			GetProperties();
        }

		public override void Write(string line)
		{
			SetProperties();

			_console.Write(line);

			GetProperties();
        }

		public override void Clear()
		{
			SetProperties();

			_console.Clear();

			GetProperties();
        }

		public override int X { get; set; }
		public override int Y { get; set; }
		public override ConsoleColor Foreground { get; set; }
		public override ConsoleColor Background { get; set; }

		private void GetProperties()
		{
			X = _console.X;
			Y = _console.Y;
			Foreground = _console.Foreground;
			Background = _console.Background;
        }

		private void SetProperties()
		{
			_console.X = X;
			_console.Y = Y;
			_console.Foreground = Foreground;
			_console.Background = Background;
        }
	}
}