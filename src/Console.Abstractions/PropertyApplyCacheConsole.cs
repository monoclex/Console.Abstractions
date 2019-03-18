using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	/// <summary>
    /// Only applies properties to the console when writing or reading.
    /// </summary>
	public class PropertyApplyCacheConsole : Console
	{
		[NotNull] private readonly Console _console;

		/// <summary>
        /// Creates a new <see cref="PropertyApplyCacheConsole"/>
        /// </summary>
        /// <param name="propertyCacheConsole"></param>
		public PropertyApplyCacheConsole([NotNull] PropertyCacheConsole propertyCacheConsole)
			: this((Console)propertyCacheConsole)
		{
		}

		/// <summary>
        /// Most likely the incorrect method to use.
        /// This class is designed in mind to be used
        /// with a <see cref="PropertyCacheConsole"/>,
        /// so it may be in your best interests to use both.
        /// </summary>
        /// <param name="console">The console to use.</param>
		[Obsolete("You might be using the PropertyApplyCacheConsole incorrently."
			+ "Try paas in a PropertyCacheConsole instead to the constructor,"
			+ "instead of using the generic Console constructor, as that's the ideal usage.")]
		public PropertyApplyCacheConsole([NotNull] Console console)
		{
            _console = console;

			GetProperties();
		}

		/// <inheritdoc/>
        public override ConsoleKeyInfo ReadKey(bool intercept)
		{
			SetProperties();

			var result = _console.ReadKey(intercept);

			GetProperties();

			return result;
		}

		/// <inheritdoc/>
		public override int Width => _console.Width;

		/// <inheritdoc/>
		public override int Height => _console.Height;

		/// <inheritdoc/>
        public override string ReadLine()
		{
			SetProperties();

			var result = _console.ReadLine();

			GetProperties();

			return result;
		}

		/// <inheritdoc/>
        public override void Write(char chr)
		{
			SetProperties();

			_console.Write(chr);

			GetProperties();
        }

		/// <inheritdoc/>
        public override void Write(string line)
		{
			SetProperties();

			_console.Write(line);

			GetProperties();
        }

		/// <inheritdoc/>
        public override void Clear()
		{
			SetProperties();

			_console.Clear();

			GetProperties();
        }

		/// <inheritdoc/>
        public override int X { get; set; }

		/// <inheritdoc/>
        public override int Y { get; set; }

		/// <inheritdoc/>
        public override ConsoleColor Foreground { get; set; }

		/// <inheritdoc/>
        public override ConsoleColor Background { get; set; }

		private void GetProperties()
		{
			X = _console.X;
			Y = _console.Y;
			Foreground = _console.Foreground;
			Background = _console.Background;
        }

		private void SetProperties()
		{
			_console.X = X;
			_console.Y = Y;
			_console.Foreground = Foreground;
			_console.Background = Background;
        }
	}
}