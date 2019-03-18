using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	/// <summary>
	/// Allows an area of the original console to be drawn on.
	/// </summary>
	[PublicAPI]
	public class ConsolePiece : IConsole
	{
		[NotNull] private readonly IConsole _console;
		private readonly int _sourceX;
		private readonly int _sourceY;

		/// <summary>
		/// Creates a <see cref="ConsolePiece"/>
		/// </summary>
		/// <param name="console">The main console.</param>
		/// <param name="sourceX">The X position of the real console.</param>
		/// <param name="sourceY">The Y position of the real console.</param>
		/// <param name="sourceWidth">The width of the console piece.</param>
		/// <param name="sourceHeight">The height of the console piece.</param>
		public ConsolePiece
		(
			[NotNull] IConsole console,
			int sourceX,
			int sourceY,
			int sourceWidth,
			int sourceHeight
		)
		{
			_console = console;
			_sourceX = sourceX;
			_sourceY = sourceY;

			Width = sourceWidth;
			Height = sourceHeight;
		}

		/// <inheritdoc/>
		public ConsoleKeyInfo ReadKey(bool intercept)
			=> _console.ReadKey(intercept);

		/// <inheritdoc/>
		public void PutChar(char character, PutCharData putCharData)
			=> _console.PutChar(character, new PutCharData
			{
				X = putCharData.X + _sourceX,
				Y = putCharData.Y + _sourceY,
				Background = putCharData.Background,
				Foreground = putCharData.Foreground
			});

		/// <inheritdoc/>
		public int Width { get; }

		/// <inheritdoc/>
		public int Height { get; }
	}
}