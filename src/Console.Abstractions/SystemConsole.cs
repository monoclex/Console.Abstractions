// Copyright (c) 2019 Console.Abstractions. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	/// <summary>
	/// The most direct interface with <see cref="System.Console"/> possible.
	/// </summary>
	[PublicAPI]
	public class SystemConsole : Console
	{
		/// <inheritdoc/>
		public override string ReadLine()
			=> System.Console.ReadLine() ?? "";

		/// <inheritdoc/>
		public override ConsoleKeyInfo ReadKey(bool intercept)
			=> System.Console.ReadKey(intercept);

		/// <inheritdoc/>
		public override void Write(char chr)
			=> System.Console.Write(chr);

		/// <inheritdoc/>
		public override void Write(string line)
			=> System.Console.Write(line);

        /// <inheritdoc cref="System.Console.KeyAvailable"/>
        public bool KeyAvailable
            => System.Console.KeyAvailable;

		/// <inheritdoc/>
		public override void Clear()
			=> System.Console.Clear();

		/// <inheritdoc/>
		public override int X
		{
			get => System.Console.CursorLeft;
			set => System.Console.CursorLeft = value;
		}

		/// <inheritdoc/>
		public override int Y
		{
			get => System.Console.CursorTop;
			set => System.Console.CursorTop = value;
		}

		/// <inheritdoc/>
		public override int Width => System.Console.WindowWidth;

		/// <inheritdoc/>
		public override int Height => System.Console.WindowHeight;

		/// <inheritdoc/>
		public override ConsoleColor Foreground
		{
			get => System.Console.ForegroundColor;
			set => System.Console.ForegroundColor = value;
		}

		/// <inheritdoc/>
		public override ConsoleColor Background
		{
			get => System.Console.BackgroundColor;
			set => System.Console.BackgroundColor = value;
		}
	}
}