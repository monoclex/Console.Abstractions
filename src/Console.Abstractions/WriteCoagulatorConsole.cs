using System;
using System.Collections.Generic;
using System.Text;

namespace Console.Abstractions
{
	/// <summary>
    /// Coagulates single Write calls into a single big Write call
    /// </summary>
	public class WriteCoagulatorConsole : Console, IFlushable
	{
		private readonly Console _console;

		public WriteCoagulatorConsole(Console console)
		{
			_console = console;
		}

		public override ConsoleKeyInfo ReadKey(bool intercept)
		{
			Flush();

			return _console.ReadKey(intercept);
		}

		public override int Width => _console.Width;
		public override int Height => _console.Height;

		public override string ReadLine()
		{
			Flush();

			return _console.ReadLine();
		}

		public override void Write(char chr)
		{
			_buffer.Append(chr);
		}

		public override void Write(string line)
		{
			_buffer.Append(line);
		}

		public override void Clear()
		{
			_console.Clear();

			_buffer.Clear();
		}

		public override int X
		{
			get => _console.X;
			set
			{
                Flush();

				_console.X = value;
            }
		}

		public override int Y
		{
			get => _console.Y;
			set
			{
                Flush();

				_console.Y = value;
			}
		}

		public override ConsoleColor Foreground
		{
			get => _console.Foreground;
			set
			{
                Flush();

				_console.Foreground = value;
			}
		}

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