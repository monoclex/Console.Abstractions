using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	/// <summary>
	/// Used for writing a bunch of data in random places to the console,
	/// and then upon flushing it, all the changed data gets written sequentially.
	/// </summary>
	[PublicAPI]
	public class BufferedPointConsole : IConsole, IFlushable
	{
		[NotNull] private readonly IConsole _console;

		/// <summary>
		/// Creates a new buffered point console.
		/// </summary>
		/// <param name="console">Where to flush data to.</param>
		public BufferedPointConsole([NotNull] IConsole console)
		{
			_console = console;

			_previousBuffer = new BufferState[_console.Width, _console.Height];
			CurrentBuffer = new BufferState[_console.Width, _console.Height];

			if (_console is IFlushable flushable)
			{
				flushable.Flush();
			}
		}

		private BufferState[,] _previousBuffer;

		/// <summary>
		/// The current buffer in use for writing data.
		/// </summary>
		public BufferState[,] CurrentBuffer;

		/// <inheritdoc/>
		public ConsoleKeyInfo ReadKey(bool intercept)
			=> _console.ReadKey(intercept);

		/// <inheritdoc/>
		public void PutChar(char character, PutCharData putCharData)
			=> CurrentBuffer[putCharData.X, putCharData.Y] = new BufferState
			{
				Character = character,
				PutCharData = putCharData
			};

		/// <inheritdoc/>
		public int Width => _console.Width;

		/// <inheritdoc/>
		public int Height => _console.Height;

		/// <inheritdoc/>
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