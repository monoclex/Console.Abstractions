using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	/// <summary>
	/// Combines a Console and a flushable so that you can pass a flush call down the line, and still use the same console calls.
	/// </summary>
	public class FlushableWrapper : Console, IFlushable
	{
		[NotNull] private readonly Console _console;
		[NotNull] private readonly IFlushable _flush;

		/// <summary>
		/// Creates a new <see cref="FlushableWrapper"/>.
		/// </summary>
		/// <param name="console">The console to implement <see cref="Console"/> methods from.</param>
		/// <param name="needsFlushing">The <see cref="IFlushable"/> that needs to be flushed.</param>
		public FlushableWrapper([NotNull] Console console, [NotNull] IFlushable needsFlushing)
		{
			_console = console;
			_flush = needsFlushing;
		}

		/// <inheritdoc/>
		public override ConsoleKeyInfo ReadKey(bool intercept)
			=> _console.ReadKey(intercept);

		/// <inheritdoc/>
		public override int Width => _console.Width;

		/// <inheritdoc/>
		public override int Height => _console.Height;

		/// <inheritdoc/>
		public override string ReadLine()
			=> _console.ReadLine();

		/// <inheritdoc/>
		public override void Write(string line)
			=> _console.Write(line);

		/// <inheritdoc/>
		public override void Clear()
			=> _console.Clear();

		/// <inheritdoc/>
		public override int X
		{
			get => _console.X;
			set => _console.X = value;
		}

		/// <inheritdoc/>
		public override int Y
		{
			get => _console.Y;
			set => _console.Y = value;
		}

		/// <inheritdoc/>
		public override ConsoleColor Foreground
		{
			get => _console.Foreground;
			set => _console.Foreground = value;
		}

		/// <inheritdoc/>
		public override ConsoleColor Background
		{
			get => _console.Background;
			set => _console.Background = value;
		}

		/// <inheritdoc/>
		public void Flush()
			=> _flush?.Flush();
	}
}