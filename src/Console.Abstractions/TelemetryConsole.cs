using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	/// <summary>
    /// Provides telemetry info about the calls made to the console passed to it.
    /// </summary>
    public class TelemetryConsole : Console
    {
		[NotNull] private readonly Console _console;

		public TelemetryConsole([NotNull] Console console)
		{
			_console = console;

			Width = _console.Width;
			Height = _console.Height;

			XTelemetry = new Telemetry<int>(() => _console.X, value => _console.X = value);
			YTelemetry = new Telemetry<int>(() => _console.Y, value => _console.Y = value);
			ForegroundTelemetry = new Telemetry<ConsoleColor>(() => _console.Foreground, value => _console.Foreground = value);
			BackgroundTelemetry = new Telemetry<ConsoleColor>(() => _console.Background, value => _console.Background = value);
        }

		/// <inheritdoc/>
        public override ConsoleKeyInfo ReadKey(bool intercept)
			=> _console.ReadKey(intercept);

		/// <inheritdoc/>
        public override int Width { get; }

		/// <inheritdoc/>
        public override int Height { get; }

		/// <inheritdoc/>
        public override string ReadLine()
			=> _console.ReadLine();

		/// <summary>
        /// The amount of calls made to the Write function.
        /// </summary>
		public int WriteCalls { get; private set; }

		/// <inheritdoc/>
        public override void Write(char chr)
		{
			_console.Write(chr);

			WriteCalls++;
		}

		/// <inheritdoc/>
        public override void Write(string line)
		{
			_console.Write(line);

			WriteCalls++;
		}

		/// <inheritdoc/>
        public override void Clear()
			=> _console.Clear();

		/// <summary>
        /// Telemetry data about <see cref="X"/>
        /// </summary>
		public Telemetry<int> XTelemetry { get; }

		/// <inheritdoc/>
        public override int X
		{
			get => XTelemetry.Get();
			set => XTelemetry.Set(value);
		}

		/// <summary>
        /// Telemetry data about <see cref="Y"/>
        /// </summary>
		public Telemetry<int> YTelemetry { get; }

		/// <inheritdoc/>
        public override int Y
		{
			get => YTelemetry.Get();
			set => YTelemetry.Set(value);
		}

		/// <summary>
        /// Telemetry data about <see cref="Foreground"/>
        /// </summary>
		public Telemetry<ConsoleColor> ForegroundTelemetry { get; }

		/// <inheritdoc/>
        public override ConsoleColor Foreground
		{
			get => ForegroundTelemetry.Get();
			set => ForegroundTelemetry.Set(value);
		}

		/// <summary>
        /// Telemetry data about <see cref="Background"/>
        /// </summary>
		public Telemetry<ConsoleColor> BackgroundTelemetry { get; }

		/// <inheritdoc/>
        public override ConsoleColor Background
		{
			get => BackgroundTelemetry.Get();
			set => BackgroundTelemetry.Set(value);
		}

		/// <inheritdoc/>
        public override string ToString()
			=> $@"Telemetry:
	X: {XTelemetry}
	Y: {YTelemetry}
	Foreground: {ForegroundTelemetry}
	Background: {BackgroundTelemetry}";
	}

	/// <summary>
    /// Simple telemetry class for recording amount of calls made.
    /// </summary>
    /// <typeparam name="T">The type of data going through.</typeparam>
	public class Telemetry<T>
	{
		[NotNull] private readonly Func<T> _onGet;
		[NotNull] private readonly Action<T> _onSet;

		/// <summary>
        /// Creates a new instance of <see cref="Telemetry{T}"/>
        /// with the code to run on a get or set.
        /// </summary>
        /// <param name="onGet">Function to run on get.</param>
        /// <param name="onSet">Function to run on set.</param>
		public Telemetry([NotNull] Func<T> onGet, [NotNull] Action<T> onSet)
		{
			_onGet = onGet;
			_onSet = onSet;
		}

		/// <summary>
        /// The amount of calls made to the getter.
        /// </summary>
		public int GetterCalls { get; private set; }

		/// <summary>
        /// The amount of calls made to the setter.
        /// </summary>
		public int SetterCalls { get; private set; }

		/// <summary>
        /// Make a call to the getter.
        /// </summary>
        /// <returns>The value the getter returned.</returns>
		public T Get()
		{
			GetterCalls++;

			return _onGet();
		}

		/// <summary>
        /// Make a call to the setter.
        /// </summary>
        /// <param name="value">The value the setter returned.</param>
		public void Set(T value)
		{
			SetterCalls++;

			_onSet(value);
		}

		/// <inheritdoc/>
        public override string ToString()
			=> $"Getter Calls: {GetterCalls}, Setter Calls: {SetterCalls}";
	}
}
