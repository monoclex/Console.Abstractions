using System;
using System.Linq;

using Console.Abstractions;

namespace Example1
{
	class Program
	{
		static void Main(string[] args)
		{
			var sysConsole = new SystemConsole();
			var sysTelemetry = new TelemetryConsole(sysConsole);
			var cache = new PropertyApplyCacheConsole(new PropertyCacheConsole(sysTelemetry));
			var frontTelemetry = new TelemetryConsole(cache);
			Console.Abstractions.Console console = frontTelemetry;

			var msg = Enumerable.Repeat("Hello, World!", 20)
				.Aggregate((a, b) => $"[{a}, {b}]");

			bool toggle = false;
			for(var i = 0; i < 101; i++)
			{
				toggle = !toggle;

				console.Write(msg, new PutCharData
				{
					Background = ConsoleColor.DarkGreen,
					Foreground = ConsoleColor.Green,
					X = 0,
					Y = 0
				});
			}

			console.Clear();
			console.X = 0;
			console.Y = 0;

			sysConsole.Write("System " + sysTelemetry + "\r\n");
			sysConsole.Write("Front  " + frontTelemetry + "\r\n");

			console.ReadKey(true);
		}
	}
}
