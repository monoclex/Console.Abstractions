using System;

namespace Console.Abstractions
{
	/// <summary>
	/// Information about where to place items on a console.
	/// </summary>
	public struct PutCharData : IEquatable<PutCharData>
	{
		/// <summary>
		/// The X-position, zero-indexed starting from the left-hand side of the screen.
		/// </summary>
		public int X;

		/// <summary>
		/// The Y-position, zero-indexed starting from the top of the screen.
		/// </summary>
		public int Y;

		/// <summary>
		/// The background color of this character.
		/// </summary>
		public ConsoleColor Background;

		/// <summary>
		/// The foreground color of this character.
		/// </summary>
		public ConsoleColor Foreground;

		// these equality methods were auto generated,
		// leaving them untested

		public bool Equals(PutCharData other)
			=> X == other.X && Y == other.Y && Background == other.Background && Foreground == other.Foreground;

		public override bool Equals(object obj)
			=>
				// only because people on a discord server held a gun to my head
				// if (ReferenceEquals(null, obj)) return false;

				obj is PutCharData other && Equals(other);

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = X;
				hashCode = (hashCode * 397) ^ Y;
				hashCode = (hashCode * 397) ^ (int)Background;
				hashCode = (hashCode * 397) ^ (int)Foreground;

				return hashCode;
			}
		}
	}
}