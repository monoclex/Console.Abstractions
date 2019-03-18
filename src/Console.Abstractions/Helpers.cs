namespace Console.Abstractions
{
	/// <summary>
	/// Helps create consoles.
	/// </summary>
	public static class Helpers
	{
		/// <summary>
		/// Creates a <see cref="BufferedPointConsole"/> that utilizes
		/// the multiple built-in console caching techniques properly
		/// to minimize the amount of calls made to the original console.
		/// </summary>
		/// <param name="main">The console to cache en masse.</param>
		/// <returns>A <see cref="BufferedPointConsole"/> with layers of caching.</returns>
		public static BufferedPointConsole CacheEnMasse(Console main)
		{
			// this will coagulate multiple writes into a single write
			// that'll perform less calls
			var coagulator = new WriteCoagulatorConsole(main);

			// we need to wrap it in a flushable so on the BFP's flush,
			// the coagulator flushes too
			var writeCoagulation = new FlushableWrapper(coagulator, coagulator);

			// this will cache multiple getter & setter calls into
			// as few calls as possible
			var propCache = new PropertyApplyCacheConsole(new PropertyCacheConsole(writeCoagulation));

			// flushable wrapper for same reason as writeCoagulation
			var propWrapper = new FlushableWrapper(propCache, writeCoagulation);

			// create the BPC
			var bufferedPointConsole = new BufferedPointConsole(propWrapper);

			return bufferedPointConsole;
		}

		/// <summary>
		/// Creates a <see cref="BufferedPointConsole"/> that utilizes
		/// the multiple built-in console caching techniques properly
		/// to minimize the amount of calls made to the original console,
		/// and features two telemetry consoles so you can view the total
		/// amount of traffic going from the <see cref="BufferedPointConsole"/>
		/// all the way to the <paramref name="main"/> console/
		/// </summary>
		/// <param name="main">The console to cache en masse.</param>
		/// <param name="mainTelemetry">Telemetry console used for viewing the total calls to the <paramref name="main"/> console.</param>
		/// <param name="frontTelemetry">Front-end telemetry console used for viewing the total traffic coming from the <see cref="BufferedPointConsole"/>.</param>
		/// <returns>A <see cref="BufferedPointConsole"/> with layers of caching.</returns>
		public static BufferedPointConsole CacheEnMasse
			(Console main, out TelemetryConsole mainTelemetry, out TelemetryConsole frontTelemetry)
		{
			// same as CacheEnMasse but with telemetry

			mainTelemetry = new TelemetryConsole(main);

			var coagulator = new WriteCoagulatorConsole(mainTelemetry);

			var writeCoagulation = new FlushableWrapper(coagulator, coagulator);

			var propCache = new PropertyApplyCacheConsole(new PropertyCacheConsole(writeCoagulation));

			var propWrapper = new FlushableWrapper(propCache, writeCoagulation);

			frontTelemetry = new TelemetryConsole(propWrapper);

			var frontWrapper = new FlushableWrapper(frontTelemetry, propWrapper);

			var bufferedPointConsole = new BufferedPointConsole(frontWrapper);

			return bufferedPointConsole;
		}
	}
}