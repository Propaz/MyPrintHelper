using System;
using System.IO;
using System.Reflection;

namespace PrinterHelper.Core
{
    internal static class BuildVersion
    {
        /// <summary>
        /// Retrieves the linker timestamp from the assembly's PE header.
        /// </summary>
        /// <param name="assembly">The assembly to get the timestamp from.</param>
        /// <returns>The build date and time in UTC.</returns>
        private static DateTime GetLinkerTimestampUtc(Assembly assembly)
        {
            var location = assembly.Location;
            var buffer = new byte[2048];
            using (var stream = new FileStream(location, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                stream.Read(buffer, 0, 2048);
            }

            var offset = BitConverter.ToInt32(buffer, 60); // PE header offset
            var secondsSince1970 = BitConverter.ToInt32(buffer, offset + 8);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return epoch.AddSeconds(secondsSince1970);
        }

        /// <summary>
        /// Gets the build date and time in local time.
        /// </summary>
        public static DateTime GetBuildDate(Assembly assembly)
        {
            var utcTime = GetLinkerTimestampUtc(assembly);
            return utcTime.ToLocalTime();
        }
    }
}