using System;
using System.Collections.Generic;
using System.Text;

namespace Console.Abstractions
{
    public class TelemetryConsole : Console
    {
		private readonly Console _console;

		public TelemetryConsole(Console console)
		{
			_console = console;

			Width = _console.Width;
			Height = _console.Height;

			XTelemetry = new Telemetry<int>(() => _console.X, value => _console.X = value);
			YTelemetry = new Telemetry<int>(() => _console.Y, value => _console.Y = value);
			ForegroundTelemetry = new Telemetry<ConsoleColor>(() => _console.Foreground, value => _console.Foreground = value);
			BackgroundTelemetry = new Telemetry<ConsoleColor>(() => _console.Background, value => _console.Background = value);
        }

		public override ConsoleKeyInfo ReadKey(bool intercept)
			=> _console.ReadKey(intercept);

		public override int Width { get; }

		public override int Height { get; }

		public override string ReadLine()
			=> _console.ReadLine();

		public override void Write(char chr)
			=> _console.Write(chr);

		public override void Write(string line)
			=> _console.Write(line);

		public override void Clear()
			=> _console.Clear();

		public Telemetry<int> XTelemetry { get; }

		public override int X
		{
			get => XTelemetry.Get();
			set => XTelemetry.Set(value);
		}

		public Telemetry<int> YTelemetry { get; }

		public override int Y
		{
			get => YTelemetry.Get();
			set => YTelemetry.Set(value);
		}

		public Telemetry<ConsoleColor> ForegroundTelemetry { get; }

		public override ConsoleColor Foreground
		{
			get => ForegroundTelemetry.Get();
			set => ForegroundTelemetry.Set(value);
		}

		public Telemetry<ConsoleColor> BackgroundTelemetry { get; }

		public override ConsoleColor Background
		{
			get => BackgroundTelemetry.Get();
			set => BackgroundTelemetry.Set(value);
		}

		public override string ToString()
			=> $@"Telemetry:
	X: {XTelemetry}
	Y: {YTelemetry}
	Foreground: {ForegroundTelemetry}
	Background: {BackgroundTelemetry}";
	}

	public class Telemetry<T>
	{
		private readonly Func<T> _onGet;
		private readonly Action<T> _onSet;

		public Telemetry(Func<T> onGet, Action<T> onSet)
		{
			_onGet = onGet;
			_onSet = onSet;
		}

		public int GetterCalls { get; private set; }

		public int SetterCalls { get; private set; }

		public T Get()
		{
			GetterCalls++;

			return _onGet();
		}

		public void Set(T value)
		{
			SetterCalls++;

			_onSet(value);
		}

		public override string ToString()
			=> $"Getter Calls: {GetterCalls}, Setter Calls: {SetterCalls}";
	}
}
