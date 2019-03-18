using System;
using System.Collections.Generic;
using System.Text;

namespace Console.Abstractions
{
	public class FlushableWrapper : Console, IFlushable
	{
		private readonly Console _console;
		private readonly IFlushable _flush;

		public FlushableWrapper(Console console, IFlushable needsFlushing)
		{
			_console = console;
			_flush = needsFlushing;
		}

		public override ConsoleKeyInfo ReadKey(bool intercept)
			=> _console.ReadKey(intercept);

		public override int Width => _console.Width;
		public override int Height => _console.Height;

		public override string ReadLine()
			=> _console.ReadLine();

		public override void Write(string line)
			=> _console.Write(line);

		public override void Clear()
			=> _console.Clear();

		public override int X
		{
			get => _console.X;
			set => _console.X = value;
		}

		public override int Y
		{
			get => _console.Y;
			set => _console.Y = value;
		}

		public override ConsoleColor Foreground
		{
			get => _console.Foreground;
			set => _console.Foreground = value;
		}

		public override ConsoleColor Background
		{
			get => _console.Background;
			set => _console.Background = value;
		}

		public void Flush()
			=> _flush?.Flush();
	}
}