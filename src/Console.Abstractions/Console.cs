// Copyright (c) 2019 Console.Abstractions. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using JetBrains.Annotations;

namespace Console.Abstractions
{
	public abstract class  Console : IConsole
	{
		public abstract ConsoleKeyInfo ReadKey(bool intercept);

		public void PutChar(char character, PutCharData putCharData)
		{
			var oldPutCharData = GetStateAsPutCharData();

			SetStateAsPutCharData(putCharData);

			Write(character);

			SetStateAsPutCharData(oldPutCharData);
		}

		public abstract int Width { get; }

		public abstract int Height { get; }

		[NotNull] public abstract string ReadLine();

        public virtual void Write(char chr)
			=> Write(chr.ToString());

        public abstract void Write([NotNull] string line);

		public abstract void Clear();

		public abstract int X { get; set; }

		public abstract int Y { get; set; }

		public abstract ConsoleColor Foreground { get; set; }

		public abstract ConsoleColor Background { get; set; }

		public PutCharData GetStateAsPutCharData()
			=> new PutCharData
			{
				X = X,
				Y = Y,
				Background = Background,
				Foreground = Foreground
			};

		public void SetStateAsPutCharData(PutCharData putCharData)
		{
			X = putCharData.X;
			Y = putCharData.Y;
			Background = putCharData.Background;
			Foreground = putCharData.Foreground;
		}
	}
}