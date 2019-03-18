using System;

namespace Console.Abstractions
{
	public class BufferedPointConsole : IConsole, IFlushable
	{
		private readonly IConsole _console;

		public BufferedPointConsole(IConsole console)
		{
			_console = console;

			_previousBuffer = new BufferState[_console.Width, _console.Height];
			CurrentBuffer = new BufferState[_console.Width, _console.Height];
		}

        private BufferState[,] _previousBuffer;
		public BufferState[,] CurrentBuffer;

		public ConsoleKeyInfo ReadKey(bool intercept)
			=> _console.ReadKey(intercept);

		public void PutChar(char character, PutCharData putCharData)
		{
			CurrentBuffer[putCharData.X, putCharData.Y] = new BufferState
			{
				Character = character,
				PutCharData = putCharData
			};
		}

		public int Width => _console.Width;
		public int Height => _console.Height;

		public void Flush()
		{
			SwapBuffers();

			if (_console is IFlushable flushable)
			{
				flushable.Flush();
			}
		}

		private void SwapBuffers()
		{
			for (var y = 0; y < CurrentBuffer.GetLength(1); y++)
			{
				for (var x = 0; x < CurrentBuffer.GetLength(0); x++)
				{
					var currentState = CurrentBuffer[x, y];
					var previousState = _previousBuffer[x, y];

					if (!currentState.Equals(previousState))
					{
						_console.PutChar(currentState.Character, currentState.PutCharData);

						// after we set it, we also set it in the previous buffer
						// so the state gets updated
						_previousBuffer[x, y] = currentState;
					}
				}
            }

			// swap
			var tmp = _previousBuffer;
			_previousBuffer = CurrentBuffer;
			CurrentBuffer = tmp;
		}
	}
}