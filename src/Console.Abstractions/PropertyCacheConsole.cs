using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	/// <summary>
	/// Caches the properties of the given console,
	/// and only updates them when they change.
	/// </summary>
	public class PropertyCacheConsole : Console
	{
		[NotNull] private readonly Console _console;

		/// <summary>
		/// Create a new <see cref="PropertyCacheConsole"/>
		/// to cache the properties of the console.
		/// </summary>
		/// <param name="console">The console to use.</param>
		public PropertyCacheConsole([NotNull] Console console)
		{
			_console = console;

			Width = _console.Width;
			Height = _console.Height;

			_x = _console.X;
			_y = _console.Y;
			_foreground = _console.Foreground;
			_background = _console.Background;
		}

		/// <inheritdoc/>
		public override ConsoleKeyInfo ReadKey(bool intercept)
		{
			var result = _console.ReadKey(intercept);

			if (intercept)
			{
				UpdateScreenXBy(1);
			}

			return result;
		}

		/// <inheritdoc/>
		public override string ReadLine()
		{
			var read = _console.ReadLine();

			UpdateScreenXBy(read.Length);

			return read;
		}

		/// <inheritdoc/>
		public override void Write(char chr)
		{
			UpdateScreenXBy(1);

			_console.Write(chr);
		}

		/// <inheritdoc/>
		public override void Write(string line)
		{
			// just to update the x so we don't update it every time
			UpdateScreenXBy(line.Length);

			_console.Write(line);
		}

		/// <inheritdoc/>
		public override void Clear()
		{
			_console.Clear();

			_x = 0;
			_y = 0;
		}

		/// <inheritdoc/>
		public override int Width { get; }

		/// <inheritdoc/>
		public override int Height { get; }

		private int _x;

		/// <inheritdoc/>
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

		/// <inheritdoc/>
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

		/// <inheritdoc/>
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

		/// <inheritdoc/>
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