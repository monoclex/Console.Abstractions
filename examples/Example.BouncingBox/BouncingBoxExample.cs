using System;
using System.Threading;

using Console.Abstractions;

namespace Example.BouncingBox
{
	public class BouncingBoxExample : Core.Example
	{
		public override void Run(IConsole console, Action flush)
		{
			var maxWidth = console.Width;
			var maxHeight = console.Height;

			var middleY = (console.Height / 2) - 5;
			var middleX = (console.Width / 2) - 5;

			const int boxSize = 10;

			var maxMoveRight = ((maxWidth - boxSize) * 2);
			var maxMoveLeft = ((maxHeight - boxSize) * 2);

			var horizontalTippingPoint = (maxWidth - boxSize);
			var verticalTippingPoint = (maxHeight- boxSize);

			for (var frame = 0; frame < 900; frame++)
			{
				// clear the screen
				console.Clear(new PutCharData
				{
					Background = ConsoleColor.DarkGreen,
					Foreground = ConsoleColor.Green,
					X = 0, Y = 0
				});

				/* we want to make a horizontally moving box */

				// first, we'll calculate the X of a horizontally moving box

				// magic calculations to make it move right and left
				var horizontalBoxX = frame % maxMoveRight;
				horizontalBoxX = horizontalBoxX > horizontalTippingPoint ? maxMoveRight - horizontalBoxX : horizontalBoxX;

				// get a piece of the console, and pass it to draw box
				var horizontalBox = new ConsolePiece(console, horizontalBoxX, middleY, boxSize, boxSize);

				DrawBox(horizontalBox, ConsoleColor.DarkRed, ConsoleColor.Red);

				// then we'll calc the Y of a vertical moving box

				// magic calculations to make it move right and left
				var verticalBoxY = frame % maxMoveLeft;
				verticalBoxY = verticalBoxY > verticalTippingPoint ? maxMoveLeft - verticalBoxY : verticalBoxY;

				// get a piece of the console, and pass it to draw box
				var verticalBox = new ConsolePiece(console, middleX, verticalBoxY, boxSize, boxSize);

				DrawBox(verticalBox, ConsoleColor.DarkCyan, ConsoleColor.Cyan);

				flush();
			}
		}

		public void DrawBox(IConsole console, ConsoleColor bg, ConsoleColor fg)
		{
			// set the entire box color to the background
			console.Clear(new PutCharData
			{
				X = 0,
				Y = 0,
				Background = bg,
				Foreground = fg
			});

			// four corners
			PutChar(console, '+', 0, 0, bg, fg);
			PutChar(console, '+', console.Width - 1, 0, bg, fg);
			PutChar(console, '+', 0, console.Height - 1, bg, fg);
			PutChar(console, '+', console.Width - 1, console.Height - 1, bg, fg);

			// write --- on the top and bottom
			var horizontalLine = new string('-', console.Width - 2);

			console.Write(horizontalLine, new PutCharData
			{
				X = 1,
				Y = 0,
				Background = bg,
				Foreground = fg
			});

			console.Write(horizontalLine, new PutCharData
			{
				X = 1,
				Y = console.Height - 1,
				Background = bg,
				Foreground = fg
			});

			// write | on the sides

			for (var y = 1; y < console.Height - 1; y++)
			{
				console.Write("|", new PutCharData
				{
					X = 0,
					Y = y,
					Background = bg,
					Foreground = fg
				});

				console.Write("|", new PutCharData
				{
					X = console.Width - 1,
					Y = y,
					Background = bg,
					Foreground = fg
				});
			}
		}

		private void PutChar(IConsole console, char character, int x, int y, ConsoleColor bg, ConsoleColor fg)
		{
			console.PutChar(character, new PutCharData
			{
				X = x,
				Y = y,
				Background = bg,
				Foreground = fg
			});
		}
	}
}
