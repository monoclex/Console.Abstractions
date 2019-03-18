// Copyright (c) 2019 Console.Abstractions. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	/// <summary>
	/// An abstract Console, following the style
	/// of the very traditional and more familiar <see cref="System.Console"/>
	/// </summary>
	[PublicAPI]
	public abstract class Console : IConsole
	{
		/// <inheritdoc/>
		public abstract ConsoleKeyInfo ReadKey(bool intercept);

		/// <inheritdoc/>
		public void PutChar(char character, PutCharData putCharData)
		{
			var oldPutCharData = GetStateAsPutCharData();

			SetStateAsPutCharData(putCharData);

			Write(character);

			SetStateAsPutCharData(oldPutCharData);
		}

		/// <inheritdoc/>
		public abstract int Width { get; }

		/// <inheritdoc/>
		public abstract int Height { get; }

		/// <summary>
		/// Reads a line from the console.
		/// </summary>
		/// <returns>The read line.</returns>
		[NotNull]
		public abstract string ReadLine();

		/// <summary>
		/// Writes a character to the console.
		/// </summary>
		/// <param name="chr">The character</param>
		public virtual void Write(char chr)
			=> Write(chr.ToString());

		/// <summary>
		/// Writes a string to the console.
		/// </summary>
		/// <param name="line">The string.</param>
		public abstract void Write([NotNull] string line);

		/// <summary>
		/// Clears the console.
		/// </summary>
		public abstract void Clear();

		/// <summary>
		/// The X position of the cursor of the console.
		/// </summary>
		public abstract int X { get; set; }

		/// <summary>
		/// The Y position of the cursor of the console.
		/// </summary>
		public abstract int Y { get; set; }

		/// <summary>
		/// The color of the foreground of the console.
		/// </summary>
		public abstract ConsoleColor Foreground { get; set; }

		/// <summary>
		/// The color of the background of the console.
		/// </summary>
		public abstract ConsoleColor Background { get; set; }

		/// <summary>
		/// Gets the current info about the console (x/y/fg/bg) as a <see cref="PutCharData"/>.
		/// </summary>
		/// <returns><see cref="PutCharData"/></returns>
		public PutCharData GetStateAsPutCharData()
			=> new PutCharData
			{
				X = X,
				Y = Y,
				Background = Background,
				Foreground = Foreground
			};

		/// <summary>
		/// Sets the current info about the console (x/y/fg/bg) as a <see cref="PutCharData"/>
		/// </summary>
		/// <param name="putCharData">The state information to set.</param>
		public void SetStateAsPutCharData(PutCharData putCharData)
		{
			X = putCharData.X;
			Y = putCharData.Y;
			Background = putCharData.Background;
			Foreground = putCharData.Foreground;
		}
	}
}