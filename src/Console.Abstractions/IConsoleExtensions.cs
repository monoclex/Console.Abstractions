// Copyright (c) 2019 Console.Abstractions. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	[PublicAPI]
	public static class IConsoleExtensions
	{
		public static IConsole Clear(this IConsole console, PutCharData clearData, char clearChar = ' ')
			=> console.Write(new string(clearChar, console.Width * console.Height), clearData);

		public static IConsole WriteLine
		(
			this IConsole console,
			string str,
			PutCharData putCharData
		)
		{
			/*
			if (console is Console sysConsole)
			{
				sysConsole.SetStateAsPutCharData(putCharData);
				sysConsole.WriteLine(str);

				return console;
			}
			*/

			return console.Write(str, putCharData)
				.Write(Environment.NewLine, new PutCharData
				{
					X = putCharData.X + str.Length,
					Y = putCharData.Y,
					Background = putCharData.Background,
					Foreground = putCharData.Foreground
				});
		}

		public static IConsole Write
		(
			this IConsole console,
			string str,
			PutCharData putCharData
		)
		{
			/*
			if (console is Console sysConsole)
			{
				sysConsole.SetStateAsPutCharData(putCharData);
				sysConsole.Write(str);

				return console;
			}
			*/

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

		public static IConsole PutCharWithWrapping
		(
			this IConsole console,
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