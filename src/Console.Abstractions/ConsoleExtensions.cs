// Copyright (c) 2019 Console.Abstractions. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	[PublicAPI]
	public static class ConsoleExtensions
	{
		[NotNull]
		public static IConsole SetPosition
		(
			[NotNull] this IConsole console,
			int x,
			int y
		)
		{
			console.X = x;
			console.Y = y;
			return console;
		}

		[NotNull]
		public static IConsole WriteLine
		(
			[NotNull] this IConsole console,
			[NotNull] string line
		)
		{
			console.Write(line);
			console.Write(Environment.NewLine);
			return console;
		}
	}
}