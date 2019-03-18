using System;
using System.Collections.Generic;
using System.Text;

namespace Console.Abstractions
{
    public struct PutCharData : IEquatable<PutCharData>
    {
	    public int X;

		public int Y;

		public ConsoleColor Background;

        public ConsoleColor Foreground;

		public bool Equals(PutCharData other)
			=> X == other.X && Y == other.Y && Background == other.Background && Foreground == other.Foreground;

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;

			return obj is PutCharData other && Equals(other);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = X;
				hashCode = (hashCode * 397) ^ Y;
				hashCode = (hashCode * 397) ^ (int) Background;
				hashCode = (hashCode * 397) ^ (int) Foreground;

				return hashCode;
			}
		}
	}
}
