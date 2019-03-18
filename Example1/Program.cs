using System;
using System.Linq;

using Console.Abstractions;

namespace Example1
{
	class Program
	{
		static IConsole GetConsole()
			=> new PropertyApplyCacheConsole
			(
				new PropertyCacheConsole
				(
					new SystemConsole()
				)
			);

		static void Main(string[] args)
		{
			var console = GetConsole();

			var msg = Enumerable.Repeat("Hello, World!", 20)
				.Aggregate((a, b) => $"[{a}, {b}]");

			bool toggle = false;
			while (true)
			{
				toggle = !toggle;

				console.WriteLine(msg, new PutCharData
				{
					Background = toggle ? ConsoleColor.Black : ConsoleColor.White,
					Foreground = toggle ? ConsoleColor.White : ConsoleColor.Black,
					X = 0,
					Y = 0
				});
			}

			console.ReadKey(true);
		}
	}
}
