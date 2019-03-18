using System;

namespace Console.Abstractions
{
	public class BufferedPointConsole : IConsole
	{
		private readonly IConsole _console;

		public BufferedPointConsole(IConsole console)
		{
			_console = console;
		}

		public ConsoleKeyInfo ReadKey(bool intercept)
			=> _console.ReadKey(intercept);

		public void PutChar(char character, PutCharData putCharData)
			=> throw new NotImplementedException();

		public int Width => _console.Width;
		public int Height => _console.Height;

		public void FlushBuffer()
		{

		}
	}
}