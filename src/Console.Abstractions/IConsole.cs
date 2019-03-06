// Copyright (c) 2019 Console.Abstractions. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	[PublicAPI]
	public interface IConsole
	{
		ConsoleKeyInfo ReadKey(bool intercept);

		void PutChar(char character, int x, int y, ConsoleColor foregroundColor, ConsoleColor backgroundColor);

		int Width { get; }

		int Height { get; }
	}
}