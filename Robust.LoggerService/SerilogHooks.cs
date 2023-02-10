using Serilog.Sinks.File.Archive;
using System.IO.Compression;

namespace Robust.LoggerService
{
    public class SerilogHooks
    {
        public static ArchiveHooks MyArchiveHooks => new ArchiveHooks(CompressionLevel.NoCompression, "D:\\WebApis\\Logs\\LoggingWithSerilog\\Archive\\{UtcDate:yyyy}\\{UtcDate:MM}");
    }
}
