using System;
using System.Collections.Generic;
using System.Text;

namespace Console.Abstractions
{
    public class ConsolePiece : IConsole
    {
		private readonly IConsole _console;
		private readonly int _sourceX;
		private readonly int _sourceY;

		public ConsolePiece
		(
			IConsole console,
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

		public ConsoleKeyInfo ReadKey(bool intercept)
			=> _console.ReadKey(intercept);

		public void PutChar(char character, PutCharData putCharData)
			=> _console.PutChar(character, new PutCharData
			{
				X = putCharData.X + _sourceX,
				Y = putCharData.Y + _sourceY,
				Background = putCharData.Background,
				Foreground = putCharData.Foreground
			});

		public int Width { get; }
		public int Height { get; }
	}
}
