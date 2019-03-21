using System;
using System.Collections.Generic;
using System.Text;

using FluentAssertions;

using Xunit;

namespace Console.Abstractions.Tests
{
	public class TelemetryConsoleTests
	{
		[Fact]
		public void DoesntCacheSize()
		{
			var mock = MockConsole.Create();
			var telemetry = new TelemetryConsole(mock);

			ConsoleTestHelpers.EnsurePassthroughSize(mock, telemetry);
		}

		[Fact]
		public void WriteCalls()
		{
			var telemetry = new TelemetryConsole(MockConsole.Create());

			for (var i = 0; i < 200; i++)
			{
				telemetry.Write(' ');
			}

			telemetry.WriteCalls.Should().Be(200);

			for (var i = 0; i < 200; i++)
			{
				telemetry.Write("E");
			}

			telemetry.WriteCalls.Should().Be(400);
		}

		public class GetterAndSetterAccessorTests
		{
			private void TestTelemetry<T>(Func<Telemetry<T>> getTelemetry, Action makeGet, Action makeSet)
			{
				for (var i = 0; i < 200; i++)
				{
					makeGet();
				}

				getTelemetry().GetterCalls
					.Should().Be(200);

				for (var i = 0; i < 200; i++)
				{
					makeSet();
				}

				getTelemetry().SetterCalls
					.Should().Be(200);
			}

			[Fact]
			public void X()
			{
				var telemetry = new TelemetryConsole(MockConsole.Create());

				TestTelemetry(() => telemetry.XTelemetry, () => {
					var _ = telemetry.X;
				}, () => telemetry.X = 5);
			}

			[Fact]
			public void Y()
			{
				var telemetry = new TelemetryConsole(MockConsole.Create());

				TestTelemetry(() => telemetry.YTelemetry, () => {
					var _ = telemetry.Y;
				}, () => telemetry.Y = 5);
			}

			[Fact]
			public void Foreground()
			{
				var telemetry = new TelemetryConsole(MockConsole.Create());

				TestTelemetry(() => telemetry.ForegroundTelemetry, () => {
					var _ = telemetry.Foreground;
				}, () => telemetry.Foreground = ConsoleColor.Green);
			}

			[Fact]
			public void Background()
			{
				var telemetry = new TelemetryConsole(MockConsole.Create());

				TestTelemetry(() => telemetry.BackgroundTelemetry, () => {
					var _ = telemetry.Background;
				}, () => telemetry.Background = ConsoleColor.Green);
			}
		}
	}
}
