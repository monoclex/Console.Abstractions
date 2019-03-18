namespace Console.Abstractions
{
	/// <summary>
	/// Something that needs to be flushed.
	/// </summary>
	public interface IFlushable
	{
		/// <summary>
		/// Flush it.
		/// </summary>
		void Flush();
	}
}