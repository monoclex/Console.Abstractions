using System;

using Moq;

using Xunit;

namespace Console.Abstractions.Tests
{
	public class ConsoleExtensionsTests
	{
		[Fact]
		public void WriteLine()
		{
			var consoleMock = new Mock<Console>();

			consoleMock.Setup(c => c.Write("test")).Verifiable();
			consoleMock.Setup(c => c.Write(Environment.NewLine)).Verifiable();

			var console = consoleMock.Object;

			console.WriteLine("test");

			consoleMock.Verify(c => c.Write("test"), Times.Once);
			consoleMock.Verify(c => c.Write(Environment.NewLine), Times.Once);
		}

		[Fact]
		public void SetPosition()
		{
			var consoleMock = new Mock<Console>();

			consoleMock.SetupSet<int>(c => c.X = 9).Verifiable();
			consoleMock.SetupSet<int>(c => c.Y = 2).Verifiable();

			var console = consoleMock.Object;

			console.SetPosition(9, 2);

			consoleMock.VerifySet(c => c.X = 9, Times.Once);
			consoleMock.VerifySet(c => c.Y = 2, Times.Once);
		}
	}
}