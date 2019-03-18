using System;
using System.Linq;
using System.Threading;

using Console.Abstractions;

namespace Example1
{
	class Program
	{
		static void Main(string[] args)
		{
			var sysConsole = new SystemConsole();
			var console = Helpers.CacheEnMasse(sysConsole, out var sysTelemetry, out var frontTelemetry);

			var msg = Enumerable.Repeat("Hello, World!", 20)
				.Aggregate((a, b) => $"[{a}, {b}]");

			bool toggle = false;
			for(var i = 0; i < 1001; i++)
			{
				toggle = !toggle;

				console.Write(msg, new PutCharData
				{
					Background = ConsoleColor.DarkGreen,
					Foreground = ConsoleColor.Green,
					X = 0,
					Y = 0
				});

				console.Flush();
			}

			console.Clear(new PutCharData
			{
				Background = ConsoleColor.White,
				Foreground = ConsoleColor.Black,
				X = 0,
				Y = 0
			});

			console.Flush();

			console.Write("Front  " + frontTelemetry + "\r\n", new PutCharData
			{
				Background = ConsoleColor.Black,
				Foreground = ConsoleColor.Gray,
				X = 0,
				Y = 0
			});

			console.Flush();

			console.Write("System " + sysTelemetry + "\r\n", new PutCharData
			{
				Background = ConsoleColor.Black,
				Foreground = ConsoleColor.Gray,
				X = 0,
				Y = 5
			});

			console.Flush();

			console.ReadKey(true);
		}
	}
}
