using System;
using System.Security.Cryptography.X509Certificates;

using Console.Abstractions;

namespace Example.Core
{
	/// <summary>
	/// Some kind of example.
	/// </summary>
	public abstract class Example
	{
		public abstract void Run(IConsole console, Action flush);
	}
}
