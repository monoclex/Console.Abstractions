using System;

namespace Console.Abstractions.Tests
{
	public class MockConsole : Console
	{
		public static MockConsole Create()
			=> new MockConsole(3, 4, 1, 2, ConsoleColor.Black, ConsoleColor.Green);

		public MockConsole
		(
			int width,
			int height,
			int x,
			int y,
			ConsoleColor foreground,
			ConsoleColor background
		)
		{
			SizeWidth = width;
			SizeHeight = height;
			X = x;
			Y = y;
			Foreground = foreground;
			Background = background;
		}

		public override ConsoleKeyInfo ReadKey(bool intercept)
			=> throw new NotImplementedException();

		public override int Width => SizeWidth;
		public override int Height => SizeHeight;

		public int SizeWidth;
		public int SizeHeight;

		public override string ReadLine()
			=> throw new NotImplementedException();

		public override void Write(char chr)
		{
		}

		public override void Write(string line)
		{
		}

		public override void Clear()
			=> throw new NotImplementedException();

		public override int X { get; set; }
		public override int Y { get; set; }
		public override ConsoleColor Foreground { get; set; }
		public override ConsoleColor Background { get; set; }
	}
}