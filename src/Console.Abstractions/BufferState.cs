using System;

namespace Console.Abstractions
{
	public struct BufferState : IEquatable<BufferState>
	{
		public char Character;

		public PutCharData PutCharData;

		public bool Equals(BufferState other)
			=> Character == other.Character && PutCharData.Equals(other.PutCharData);

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;

			return obj is BufferState other && Equals(other);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Character.GetHashCode() * 397) ^ PutCharData.GetHashCode();
			}
		}
	}
}