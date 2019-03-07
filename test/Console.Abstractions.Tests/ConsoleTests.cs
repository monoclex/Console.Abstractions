using System;

using FluentAssertions;

using Xunit;

namespace Console.Abstractions.Tests
{
	public class ConsoleTests
	{
		public class MockConsole : Console
		{
			public override ConsoleKeyInfo ReadKey(bool intercept)
				=> throw new NotImplementedException();

			public override int Width { get; }
			public override int Height { get; }

			public override string ReadLine()
				=> throw new NotImplementedException();

			public override void Write(string line)
				=> throw new NotImplementedException();

			public override void Clear()
				=> throw new NotImplementedException();

			public override int X { get; set; }
			public override int Y { get; set; }
			public override ConsoleColor Foreground { get; set; }
			public override ConsoleColor Background { get; set; }
		}

		[Fact]
		public void GetPutCharData()
		{
			var mock = new MockConsole
			{
				X = 7,
				Y = 2,
				Background = ConsoleColor.DarkGreen,
				Foreground = ConsoleColor.Gray
			};

			var putCharData = mock.GetStateAsPutCharData();

			putCharData
				.Should()
				.BeEquivalentTo(new PutCharData
				{
					X = 7,
					Y = 2,
					Background = ConsoleColor.DarkGreen,
					Foreground = ConsoleColor.Gray
				});
		}

		[Fact]
		public void SetPutCharData()
		{
			var mock = new MockConsole();

			mock.SetStateAsPutCharData(new PutCharData
			{
				X = 3,
				Y = 9,
				Background = ConsoleColor.DarkMagenta,
				Foreground = ConsoleColor.DarkCyan
			});

			mock.X.Should().Be(3);
			mock.Y.Should().Be(9);
			mock.Background.Should().Be(ConsoleColor.DarkMagenta);
			mock.Foreground.Should().Be(ConsoleColor.DarkCyan);
		}

		public class PutCharMock : Console
		{
			public override ConsoleKeyInfo ReadKey(bool intercept)
				=> throw new NotImplementedException();

			public override string ReadLine()
				=> throw new NotImplementedException();

			public override void Clear()
				=> throw new NotImplementedException();

			public override int Width { get; }
			public override int Height { get; }

			public string Line { get; set; }
			public Action OnWrite;

			public override void Write(string line)
			{
				Line = line;
				OnWrite();
			}

			public override int X { get; set; }
			public override int Y { get; set; }
			public override ConsoleColor Foreground { get; set; }
			public override ConsoleColor Background { get; set; }
		}

		[Fact]
		public void PutChar()
		{
			var mock = new PutCharMock
			{
				X = 2,
				Y = 9,
				Background = ConsoleColor.Blue,
				Foreground = ConsoleColor.DarkGray
			};

			mock.OnWrite = () =>
			{
				mock.X.Should().Be(8);
				mock.Y.Should().Be(5);
				mock.Background.Should().Be(ConsoleColor.DarkMagenta);
				mock.Foreground.Should().Be(ConsoleColor.Cyan);

				mock.Line.Should().Be("E");
			};

			mock.PutChar('E', new PutCharData
			{
				X = 8,
				Y = 5,
				Background = ConsoleColor.DarkMagenta,
				Foreground = ConsoleColor.Cyan
			});

			mock.X.Should().Be(2);
			mock.Y.Should().Be(9);
			mock.Background.Should().Be(ConsoleColor.Blue);
			mock.Foreground.Should().Be(ConsoleColor.DarkGray);
		}
	}
}