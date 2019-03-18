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

			var i = 0;
			bool toggle = false;

			for (var ___ = 0; ___ < 200; ___++)
			{
				if (i == 60 || i == 0)
				{
					toggle = !toggle;
				}

				if (toggle)
				{
					i++;
				}
				else
				{
					i--;
				}

				console.Clear(new PutCharData
				{
					Background = ConsoleColor.Black,
					Foreground = ConsoleColor.Gray,
					X = 0,
					Y = 0
				});

				DrawBorder(console, ConsoleColor.DarkGreen, ConsoleColor.Green);

				DrawBorder(new ConsolePiece(console, 15, 5, 10, 10), ConsoleColor.DarkCyan, ConsoleColor.Cyan);

				DrawBorder(new ConsolePiece(console, 5 + i, 5, 5, 5), ConsoleColor.DarkRed, ConsoleColor.Red);

				console.Flush();
			}

			console.ReadKey(true);

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

		static void DrawBorder(IConsole console, ConsoleColor bg, ConsoleColor fg)
		{
			console.Clear(new PutCharData
			{
				X = 0,
				Y = 0,
				Background = bg,
				Foreground = fg
			});

			console.Write("+", new PutCharData
			{
				X = 0,
				Y = 0,
				Background = bg,
				Foreground = fg
			});

			console.Write("+", new PutCharData
			{
				X = console.Width - 1,
				Y = 0,
				Background = bg,
				Foreground = fg
			});

			console.Write(new string('-', console.Width - 2), new PutCharData
			{
				X = 1,
				Y = 0,
				Background = bg,
				Foreground = fg
			});

			for (var y = 0; y < console.Height - 2; y++)
			{
				console.Write("|", new PutCharData
				{
					X = 0,
					Y = y + 1,
					Background = bg,
					Foreground = fg
				});

				console.Write("|", new PutCharData
				{
					X = console.Width - 1,
					Y = y + 1,
					Background = bg,
					Foreground = fg
				});
			}

			console.Write(new string('-', console.Width - 2), new PutCharData
			{
				X = 1,
				Y = console.Height - 1,
				Background = bg,
				Foreground = fg
			});

			console.Write("+", new PutCharData
			{
				X = 0,
				Y = console.Height - 1,
				Background = bg,
				Foreground = fg
			});

			console.Write("+", new PutCharData
			{
				X = console.Width - 1,
				Y = console.Height - 1,
				Background = bg,
				Foreground = fg
			});
		}
	}
}
