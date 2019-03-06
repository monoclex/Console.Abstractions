// Copyright (c) 2019 Console.Abstractions. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	[PublicAPI]
	public class SystemConsole : Console
	{
		public override string ReadLine() => System.Console.ReadLine() ?? "";

		public override ConsoleKeyInfo ReadKey(bool intercept) => System.Console.ReadKey(intercept);

		public override void Write(string line) => System.Console.Write(line);

		public override void Clear() => System.Console.Clear();

		public override int X
		{
			get => System.Console.CursorLeft;
			set => System.Console.CursorLeft = value;
		}

		public override int Y
		{
			get => System.Console.CursorTop;
			set => System.Console.CursorTop = value;
		}

		public override int Width => System.Console.WindowWidth;

		public override int Height => System.Console.WindowHeight;

		public override ConsoleColor Foreground
		{
			get => System.Console.ForegroundColor;
			set => System.Console.ForegroundColor = value;
		}

		public override ConsoleColor Background
		{
			get => System.Console.BackgroundColor;
			set => System.Console.BackgroundColor = value;
		}
	}
}