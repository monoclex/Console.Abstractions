using System;
using System.Text;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	/// <summary>
	/// Coagulates a bunch of Write calls into a single write call
	/// to minimize on the amount of calls.
	/// </summary>
	public class WriteCoagulatorConsole : Console, IFlushable
	{
		[NotNull] private readonly Console _console;

		/// <summary>
		/// Creates a new <see cref="WriteCoagulatorConsole"/> with the console.
		/// </summary>
		/// <param name="console">The console to coagulate write calls on.</param>
		public WriteCoagulatorConsole([NotNull] Console console)
			=> _console = console;

		/// <inheritdoc/>
		public override ConsoleKeyInfo ReadKey(bool intercept)
		{
			Flush();

			return _console.ReadKey(intercept);
		}

		/// <inheritdoc/>
		public override int Width => _console.Width;

		/// <inheritdoc/>
		public override int Height => _console.Height;

		/// <inheritdoc/>
		public override string ReadLine()
		{
			Flush();

			return _console.ReadLine();
		}

		/// <inheritdoc/>
		public override void Write(char chr)
			=> _buffer.Append(chr);

		/// <inheritdoc/>
		public override void Write(string line)
			=> _buffer.Append(line);

		/// <inheritdoc/>
		public override void Clear()
		{
			_console.Clear();

			_buffer.Clear();
		}

		/// <inheritdoc/>
		public override int X
		{
			get => _console.X;
			set
			{
				Flush();

				_console.X = value;
			}
		}

		/// <inheritdoc/>
		public override int Y
		{
			get => _console.Y;
			set
			{
				Flush();

				_console.Y = value;
			}
		}

		/// <inheritdoc/>
		public override ConsoleColor Foreground
		{
			get => _console.Foreground;
			set
			{
				Flush();

				_console.Foreground = value;
			}
		}

		/// <inheritdoc/>
		public override ConsoleColor Background
		{
			get => _console.Background;
			set
			{
				Flush();

				_console.Background = value;
			}
		}

		private StringBuilder _buffer = new StringBuilder(0xFFFF);

		/// <inheritdoc/>
		public void Flush()
		{
			// the fg will get set, then the bg
			// they will both trigger flush
			// so this is needed to not trigger flush
			// if there's nothing to flush
			// prevents empty write calls

			if (_buffer.Length <= 0)
			{
				return;
			}

			_console.Write(_buffer.ToString());

			_buffer.Clear();
		}
	}
}