using System;
using System.IO;

namespace SmartHotel220.Clients.Core.Extensions
{
    /// <summary>
    /// Расширение для UriBuilder
    /// </summary>
    internal static class UriBuilderExtensions
    {
        internal static void AppendToPath(this UriBuilder builder, string pathToAdd)
        {
            var completePath = Path.Combine(builder.Path, pathToAdd);
            builder.Path = completePath;
        } 
    }
}
