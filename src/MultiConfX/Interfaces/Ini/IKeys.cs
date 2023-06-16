namespace Hopex.MultiConfX.Interfaces.Ini
{
    /// <summary>
    /// Includes keys processing methods.
    /// </summary>
    public interface IKeys
    {
        /// <summary>
        /// Returns a list of keys for the specified section.
        /// </summary>
        /// <param name="section">The name of the file section.</param>
        /// <returns>List of keys of the specified section.</returns>
        System.Collections.Generic.List<string> GetKeys(string section);

        /// <summary>
        /// Changes the name of the section key.
        /// </summary>
        /// <param name="section">The name of the file section.</param>
        /// <param name="key">The original name of the section key.</param>
        /// <param name="newKey">The new name of the section key.</param>
        /// <returns>An object for processing modified data.</returns>
        MultiConfX.Ini.IniData EditKey(string section, string key, string newKey);

        /// <summary>
        /// Deleting the key of the specified section from the file.
        /// </summary>
        /// <param name="section">The name of the file section.</param>
        /// <param name="key">The name of the section key.</param>
        /// <returns>An object for processing modified data.</returns>
        MultiConfX.Ini.IniData RemoveKey(string section, string key);

        /// <summary>
        /// Deleting the keys with they values from the file.
        /// </summary>
        /// <param name="section">The name of the file section.</param>
        /// <param name="keys">Key names.</param>
        /// <returns>An object for processing modified data.</returns>
        MultiConfX.Ini.IniData RemoveKeys(string section, params string[] keys);
    }
}
