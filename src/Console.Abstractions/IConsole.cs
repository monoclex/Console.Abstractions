// Copyright (c) 2019 Console.Abstractions. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	/// <summary>
	/// An abstract console.
	/// </summary>
	[PublicAPI]
	public interface IConsole
	{
		/// <summary>
		/// Reads a key from the console.
		/// </summary>
		/// <param name="intercept">See documentation for <see cref="System.Console.ReadKey(bool)"/></param>
		/// <returns>A <see cref="ConsoleKeyInfo"/> with info about the key.</returns>
		ConsoleKeyInfo ReadKey(bool intercept);

		/// <summary>
		/// Puts a single character on the console, at a given x/y
		/// </summary>
		/// <param name="character">The character.</param>
		/// <param name="putCharData">The data to write.</param>
		void PutChar(char character, PutCharData putCharData);

		/// <summary>
		/// The width of the console.
		/// </summary>
		int Width { get; }

		/// <summary>
		/// The height of the console.
		/// </summary>
		int Height { get; }

		/// <inheritdoc cref="System.Console.KeyAvailable"/>
		bool KeyAvailable { get; }
	}
}