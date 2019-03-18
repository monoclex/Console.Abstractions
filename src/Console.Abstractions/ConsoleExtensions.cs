// Copyright (c) 2019 Console.Abstractions. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	/// <summary>
	/// Simple extensions for a <see cref="Console"/>
	/// </summary>
	[PublicAPI]
	public static class ConsoleExtensions
	{
		/// <summary>
		/// Sets the cursor position.
		/// </summary>
		/// <param name="console">The console.</param>
		/// <param name="x">The X.</param>
		/// <param name="y">The Y.</param>
		/// <returns>The same console.</returns>
		[NotNull]
		public static Console SetPosition
		(
			[NotNull] this Console console,
			int x,
			int y
		)
		{
			console.X = x;
			console.Y = y;

			return console;
		}

		/// <summary>
		/// Writes a line to the console.
		/// </summary>
		/// <param name="console">The console.</param>
		/// <param name="line">The line to write.</param>
		/// <returns>The same console.</returns>
		[NotNull]
		public static Console WriteLine
		(
			[NotNull] this Console console,
			[NotNull] string line
		)
		{
			console.Write(line);
			console.Write(Environment.NewLine);

			return console;
		}
	}
}