// Copyright (c) 2019 Console.Abstractions. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	/// <summary>
	/// Extensions for an <see cref="IConsole"/>
	/// </summary>
	[PublicAPI]
	public static class IConsoleExtensions
	{
		/// <summary>
		/// Clears the console, by writing a giant blank string to it.
		/// </summary>
		/// <param name="console">The console.</param>
		/// <param name="clearData">Data about the clear.</param>
		/// <param name="clearChar">The character to use when clearing.</param>
		/// <returns>The same console.</returns>
		public static IConsole Clear
		(
			[NotNull] this IConsole console,
			PutCharData clearData,
			char clearChar = ' '
		)
		{
			if (console is Console sysConsole)
			{
				sysConsole.SetStateAsPutCharData(clearData);
				sysConsole.Clear();
			}
			else
			{
				console.Write(new string(clearChar, console.Width * console.Height), clearData);
				console.PutChar(' ', clearData);
			}

			return console;
		}

		/// <summary>
		/// Writes a line with the string given to the console.
		/// </summary>
		/// <param name="console">The console.</param>
		/// <param name="str">The string.</param>
		/// <param name="putCharData">Data about the line.</param>
		/// <returns>The same console.</returns>
		public static IConsole WriteLine
		(
			[NotNull] this IConsole console,
			[NotNull] string str,
			PutCharData putCharData
		)
		{
			if (console is Console sysConsole)
			{
				var oldState = sysConsole.GetStateAsPutCharData();
				sysConsole.SetStateAsPutCharData(putCharData);
				sysConsole.WriteLine(str);
				sysConsole.SetStateAsPutCharData(oldState);

				return console;
			}

			return console.Write(str, putCharData)
				.Write(Environment.NewLine, new PutCharData
				{
					X = putCharData.X + str.Length,
					Y = putCharData.Y,
					Background = putCharData.Background,
					Foreground = putCharData.Foreground
				});
		}

		/// <summary>
		/// Writes a string to the console.
		/// </summary>
		/// <param name="console">The console.</param>
		/// <param name="str">The string.</param>
		/// <param name="putCharData">Data about the string.</param>
		/// <returns>The same console.</returns>
		public static IConsole Write
		(
			[NotNull] this IConsole console,
			[NotNull] string str,
			PutCharData putCharData
		)
		{
			if (console is Console sysConsole)
			{
				var oldState = sysConsole.GetStateAsPutCharData();
				sysConsole.SetStateAsPutCharData(putCharData);
				sysConsole.Write(str);
				sysConsole.SetStateAsPutCharData(oldState);

				return console;
			}

			var characters = str.ToCharArray();

			for (var chrIndex = 0; chrIndex < characters.Length; chrIndex++)
			{
				var chr = characters[chrIndex];

				console.PutCharWithWrapping(chr, new PutCharData
				{
					X = chrIndex + putCharData.X,
					Y = putCharData.Y,
					Background = putCharData.Background,
					Foreground = putCharData.Foreground
				});
			}

			return console;
		}

		/// <summary>
		/// Puts a single character on the console
		/// at any given X or Y, negative or positive,
		/// by wrapping the coordinates around until
		/// they're in bounds.
		/// </summary>
		/// <param name="console">The console.</param>
		/// <param name="character">The character to put.</param>
		/// <param name="putCharData">Data about the character.</param>
		/// <returns>The same console.</returns>
		public static IConsole PutCharWithWrapping
		(
			[NotNull] this IConsole console,
			char character,
			PutCharData putCharData
		)
		{
			var boundedX = putCharData.X;
			var boundedY = putCharData.Y;

			WrapValueLower(ref boundedX, 0, console.Width, () => boundedY++);
			WrapValueUpper(ref boundedX, console.Width, console.Width, () => boundedY++);

			WrapValueLower(ref boundedY, 0, console.Height);
			WrapValueUpper(ref boundedY, console.Height, console.Height);

			console.PutChar(character, new PutCharData
			{
				X = boundedX,
				Y = boundedY,
				Background = putCharData.Background,
				Foreground = putCharData.Foreground
			});

			return console;
		}

		/// <summary>
		/// Converts an IConsole a BorderedConsole.
		/// </summary>
		/// <param name="console">The console to wrap with a border.</param>
		/// <returns>The BorderedConsole</returns>
		public static BorderedConsole ToBorderedConsole
		(
				[NotNull] this IConsole console
		)
			=> new BorderedConsole(console);

		private static void WrapValueLower
		(
			ref int value,
			int boundary,
			int modifier,
			Action onModification = null
		)
		{
			while (value < boundary)
			{
				value += modifier;
				onModification?.Invoke();
			}
		}

		private static void WrapValueUpper
		(
			ref int value,
			int boundary,
			int modifier,
			Action onModification = null
		)
		{
			while (value >= boundary)
			{
				value -= modifier;
				onModification?.Invoke();
			}
		}
	}
}