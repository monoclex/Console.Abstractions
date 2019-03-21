using System;
using System.Collections.Generic;
using System.Text;

using FluentAssertions;

using Xunit;

namespace Console.Abstractions.Tests
{

	public class PropertyCacheConsoleTests
	{
		[Fact]
		public void PropertiesInitiallyCached()
		{
			var mock = new MockConsole(20, 21, 41, 4, ConsoleColor.DarkGray, ConsoleColor.Blue);

			var testing = new PropertyCacheConsole(mock);

			testing.Width.Should().Be(mock.Width);
			testing.Height.Should().Be(mock.Height);
			testing.X.Should().Be(mock.X);
			testing.Y.Should().Be(mock.Y);
			testing.Foreground.Should().Be(mock.Foreground);
			testing.Background.Should().Be(mock.Background);
		}
	}
}
