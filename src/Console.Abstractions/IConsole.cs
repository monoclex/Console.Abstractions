// Copyright (c) 2019 Console.Abstractions. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	[PublicAPI]
	public interface IConsole
	{
		[NotNull]
		string ReadLine();

		ConsoleKeyInfo ReadKey(bool intercept);

		void Write([NotNull] string line);

		void Clear();

		int X { get; set; }

		int Y { get; set; }

		int Width { get; }

		int Height { get; }

		ConsoleColor Foreground { get; set; }

		ConsoleColor Background { get; set; }
	}
}