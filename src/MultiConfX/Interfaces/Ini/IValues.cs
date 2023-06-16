namespace Hopex.MultiConfX.Interfaces.Ini
{
    /// <summary>
    /// Includes values processing methods.
    /// </summary>
    public interface IValues
    {
        /// <summary>
        /// Returns the value of the specified key and its section.
        /// </summary>
        /// <param name="section">The name of the file section.</param>
        /// <param name="key">The name of the section key.</param>
        /// <returns>The value of the specified key and its section.</returns>
        string GetValue(string section, string key);

        /// <summary>
        /// Changes the key value of the specified section.
        /// </summary>
        /// <param name="section">The name of the file section.</param>
        /// <param name="key">The name of the section key.</param>
        /// <param name="value">The new value of the parameter of the specified section.</param>
        /// <returns>An object for processing modified data.</returns>
        MultiConfX.Ini.IniData EditValue(string section, string key, string value);
    }
}
