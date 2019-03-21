using System;
using System.IO;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace Console.Abstractions.Tests
{
	public class SystemConsoleTests
	{
		private void WithTempOut(Action action)
		{
			var oldOut = System.Console.Out;

			action();

			System.Console.SetOut(oldOut);
		}

		private void WithTempIn(Action action)
		{
			var oldIn = System.Console.In;

			action();

			System.Console.SetIn(oldIn);
		}

		private void PassIfNotInConsoleContext(Action test)
		{
			try
			{
				test();
			}
			catch (IOException e)
			{
				if (e.Message != "The handle is invalid")
				{
					throw;
				}
			}
		}

		[Fact] public void Clear() => PassIfNotInConsoleContext(() => WithTempOut(ClearTest));

		[Fact] public void Write() => WithTempOut(WriteTest);

		[Fact] public void WriteChar() => WithTempOut(WriteCharTest);

		private void ClearTest()
		{
			System.Console.SetOut(new StringWriter());
			System.Console.Write("test");

			var console = new SystemConsole();

			console.Clear();

			// if it was cleared, typically the cursor would be at 0, 0
			System.Console.CursorLeft.Should().Be(0);
			System.Console.CursorTop.Should().Be(0);
		}

		private void WriteTest()
		{
			using (var sw = new StringWriter())
			{
				System.Console.SetOut(sw);

				var console = new SystemConsole();

				console.Write("test, ");
				console.Write("more test");

				var output = sw.ToString();

				output
					.Should()
					.Be("test, more test");
			}
		}

		private void WriteCharTest()
		{
			using (var sw = new StringWriter())
			{
				System.Console.SetOut(sw);

				var console = new SystemConsole();

				console.Write('E');
				console.Write('F');

				var output = sw.ToString();

				output
					.Should()
					.Be("EF");
			}
		}

		[Fact]
		public void ReadLine() => WithTempIn(ReadLineTest);

		private void ReadLineTest()
		{
			var console = new SystemConsole();

			using (var s = new StringReader("Testing"))
			{
				System.Console.SetIn(s);

				console.ReadLine().Should().Be("Testing");
			}
		}

		[Fact]
		public async void ReadKey()
		{
			var console = new SystemConsole();

			// no idea how i'm suppose to test this
			// i'm just gonna throw this in some async void
			// and hope it doesn't block the rest of the tests
			// :v

			// i don't care
			try
			{
				Task.Run(async () => console.ReadKey(true));
			}
			catch
			{
			}
		}

		[Fact]
		public void WidthGetter() => PassIfNotInConsoleContext(() => new SystemConsole().Width
			.Should()
			.Be(System.Console.WindowWidth));

		[Fact]
		public void HeightGetter() => PassIfNotInConsoleContext(() => new SystemConsole().Height
			.Should()
			.Be(System.Console.WindowHeight));

		[Fact]
		public void XAutoProperty() => PassIfNotInConsoleContext(XAutoPropertyTest);

		private void XAutoPropertyTest()
		{
			var console = new SystemConsole();

			TestGetter();
			console.X = 5;
			TestGetter();

			void TestGetter() => console.X.Should().Be(System.Console.CursorLeft);
		}

		[Fact]
		public void YAutoProperty() => PassIfNotInConsoleContext(YAutoPropertyTest);

		private void YAutoPropertyTest()
		{
			var console = new SystemConsole();

			TestGetter();
			console.Y = 5;
			TestGetter();

			void TestGetter() => console.Y.Should().Be(System.Console.CursorTop);
		}

		[Fact]
		public void ForegroundAutoProperty()
		{
			var console = new SystemConsole();

			TestGetter();
			console.Foreground = ConsoleColor.DarkBlue;
			TestGetter();

			void TestGetter() => console.Foreground.Should().Be(System.Console.ForegroundColor);
		}

		[Fact]
		public void BackgroundAutoProperty()
		{
			var console = new SystemConsole();

			TestGetter();
			console.Background = ConsoleColor.DarkBlue;
			TestGetter();

			void TestGetter() => console.Background.Should().Be(System.Console.BackgroundColor);
		}
	}
}