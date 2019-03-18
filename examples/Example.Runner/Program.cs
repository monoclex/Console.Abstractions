using Console.Abstractions;

using System;

using Example.BouncingBox;
using Example.MovingText;

namespace Example.Runner
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var sysConsole = new SystemConsole();
			var console = Helpers.CacheEnMasse(sysConsole, out var sysTelemetry, out var frontTelemetry);

			// CHANGE THIS
			int exampleId = 1;

			Core.Example example;

			switch (exampleId)
			{
				case 0:
					example = new MovingTextExample();
					break;

				case 1:
					example = new BouncingBoxExample();
					break;

				default: throw new Exception();
			}

			// wait for a key press before continueing
			sysConsole.ReadKey(true);

			// run the example
			example.Run(console, () => console.Flush()); // console);

			// wait for a key press before displaying statistics
			console.ReadKey(true);

			// display statistics about the drawing
			// using sysConsole to not interfere with the telemetry
			sysConsole.Clear();
			sysConsole.X = 0;
			sysConsole.Y = 0;
			sysConsole.Background = ConsoleColor.Black;
			sysConsole.Foreground = ConsoleColor.Gray;

			sysConsole.Write("System WriteCalls: " + sysTelemetry.WriteCalls + "\r\n");
			sysConsole.Write("Front-end write calls:" + frontTelemetry.WriteCalls + "\r\n");
			sysConsole.Write("System Telemetry: " + sysTelemetry + "\r\n");
			sysConsole.Write("Front-end Telemetry: " + frontTelemetry + "\r\n");

			sysConsole.ReadKey(true);
		}
	}
}