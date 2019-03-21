using System;
using System.Collections.Generic;
using System.Text;

using FluentAssertions;

namespace Console.Abstractions.Tests
{
	public class ConsoleTestHelpers
	{
		public static void EnsurePassthroughSize(MockConsole mock, Console test)
		{
			EnsureEqualSizes();

			mock.SizeWidth = 100;
			mock.SizeHeight = 200;

			EnsureEqualSizes();

			void EnsureEqualSizes()
			{
				test.Width.Should().Be(mock.Width, "Width of the console should be passed through.");
				test.Width.Should().Be(mock.Height, "Height of the console should be passed through.");
			}
		}
	}
}
