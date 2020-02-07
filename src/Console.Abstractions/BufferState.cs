using System;

namespace Console.Abstractions
{
	/// <summary>
	/// A state of some item in the buffer.
	/// </summary>
	public struct BufferState : IEquatable<BufferState>
	{
		/// <summary>
		/// The character it is.
		/// </summary>
		public char Character;

		/// <summary>
		/// Info used for writing the char
		/// </summary>
		public PutCharData PutCharData;

		// these equality methods were auto generated,
		// leaving them untested

		/// <inheritdoc/>
		public bool Equals(BufferState other)
			=> Character == other.Character && PutCharData.Equals(other.PutCharData);

		/// <inheritdoc/>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;

			return obj is BufferState other && Equals(other);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			unchecked
			{
				return (Character.GetHashCode() * 397) ^ PutCharData.GetHashCode();
			}
		}
	}
}