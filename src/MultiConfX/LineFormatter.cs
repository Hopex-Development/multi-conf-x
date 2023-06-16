using System.Linq;

namespace Hopex.MultiConfX
{
    /// <summary>
    /// Formatting keys and values.
    /// </summary>
    public class LineFormatter
    {
        /// <summary>
        /// Formatting key for INI file.
        /// </summary>
        /// <param name="key">The key for formatting.</param>
        /// <returns>Formatted key.</returns>
        public static string KeyForIni(string key) => string.Join(
            separator: string.Empty,
            values: key
                .Split(' ')
                .ToList()
                .Select(x => x = x.Substring(0, 1).ToUpper() + x.Substring(1, x.Length - 1))
            ).Trim();

        /// <summary>
        /// Formatting key for ENV file.
        /// </summary>
        /// <param name="key">The key for formatting.</param>
        /// <returns>Formatted key.</returns>
        public static string KeyForEnv(string key) => key.Replace(" ", "_").Trim('_').ToUpper();

        /// <summary>
        /// Formatting value.
        /// </summary>
        /// <param name="value">The value for formatting.</param>
        /// <returns>Formatted value.</returns>
        public static string Value(string value) => (value.Contains(" ") ? $"\"{value}\"" : value).Trim();
    }
}
