using System;

using Console.Abstractions;

namespace Example.MovingText
{
	public class MovingTextExample : Core.Example
	{
		public override void Run(IConsole console, Action flush)
		{
			// we're gonna scroll this down the screen as fast as we can
			var text = "Hello, World! This is a test!";

			var middleX = (console.Width / 2) - (text.Length / 2);

			for (var frame = 0; frame < console.Height * 50; frame++)
			{
				var y = frame % console.Height;

				// clears the console (the buffer of the console, to be accurate)
				console.Clear(new PutCharData
				{
					Background = ConsoleColor.Black,
					Foreground = ConsoleColor.Gray
				});

				// write the text at the given x/y
				console.Write(text, new PutCharData
				{
					X = middleX,
					Y = y,
					Background = ConsoleColor.DarkGreen,
					Foreground = ConsoleColor.Green
				});

				// flush the buffer to the real console
				flush();
			}
		}
	}
}
