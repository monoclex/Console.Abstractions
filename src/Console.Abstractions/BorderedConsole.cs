using System;
using JetBrains.Annotations;

namespace Console.Abstractions
{
	/// <summary>
	/// Surrounds the IConsole in a border, and provides a IConsole to access the inner area.
	/// </summary>
	public class BorderedConsole : IConsole
	{
		[NotNull] private readonly IConsole _parentConsole;
		/// <inheritdoc/>
		public int Width => _parentConsole.Width;
		/// <inheritdoc/>
		public int Height => _parentConsole.Height;
		/// <inheritdoc/>
		public bool KeyAvailable => _parentConsole.KeyAvailable;
		/// <summary>
		/// The inside area of the console, excluding the border.
		/// </summary>
		public IConsole Canvas { get; }

		/// <summary>
		/// Creates a new BorderedConsole
		/// </summary>
		/// <param name="parentConsole">The parent IConsole.</param>
		public BorderedConsole([NotNull] IConsole parentConsole)
		{
			_parentConsole = parentConsole;
			Canvas = new ConsolePiece(_parentConsole, 1, 1, Width - 2, Height - 2);
		}
		/// <summary>
		/// Draws the border around the parent console.
		/// </summary>
		/// <param name="foregroundColor">The foreground of the border characters.</param>
		/// <param name="backgroundColor">The background of the border characters.</param>
		public virtual void DrawBorder(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
		{
			_parentConsole.PutChar('╔', new PutCharData { Background = backgroundColor, Foreground = foregroundColor, X = 0, Y = 0});
			_parentConsole.PutChar('╚', new PutCharData { Background = backgroundColor, Foreground = foregroundColor, X = 0, Y = Height - 1 });
			_parentConsole.PutChar('╗', new PutCharData { Background = backgroundColor, Foreground = foregroundColor, X = Width - 1, Y = 0 });
			_parentConsole.PutChar('╝', new PutCharData { Background = backgroundColor, Foreground = foregroundColor, X = Width - 1, Y = Height - 1 });

			// Draw the top and bottom straight lines
			for (int i = 1; i < _parentConsole.Width - 1; i++)
			{
				// Top
				_parentConsole.PutChar('═', new PutCharData{ Background = backgroundColor, Foreground = foregroundColor, X = i, Y = 0 });

				// Bottom
				_parentConsole.PutChar('═', new PutCharData { Background = backgroundColor, Foreground = foregroundColor, X = i, Y = Height - 1 });
			}

			// Left and right straight lines
			for (int i = 1; i < _parentConsole.Height - 1; i++)
			{
				// Left
				_parentConsole.PutChar('║', new PutCharData { Background = backgroundColor, Foreground = foregroundColor, X = 0, Y = i });

				// Right
				_parentConsole.PutChar('║', new PutCharData { Background = backgroundColor, Foreground = foregroundColor, X = Width - 1, Y = i });
			}
		}
		/// <inheritdoc/>
		public void PutChar(char character, PutCharData putCharData) => _parentConsole.PutChar(character, putCharData);
		/// <inheritdoc/>
		public ConsoleKeyInfo ReadKey(bool intercept) => _parentConsole.ReadKey(intercept);
	}
}
