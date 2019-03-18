using System;
using System.Collections.Generic;
using System.Text;

namespace Console.Abstractions
{
    public static class Helpers
    {
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

		public static BufferedPointConsole CacheEnMasse(Console main, out TelemetryConsole mainTelemetry, out TelemetryConsole frontTelemetry)
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
