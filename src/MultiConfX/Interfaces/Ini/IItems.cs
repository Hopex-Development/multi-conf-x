namespace Hopex.MultiConfX.Interfaces.Ini
{
    /// <summary>
    /// Includes items processing methods.
    /// </summary>
    public interface IItems
    {
        /// <summary>
        /// Adding a section, a key, and its value. If there is no section or key, they will be created, and the value,
        /// if it already exists, will be overwritten.
        /// </summary>
        /// <param name="section">The name of the file section.</param>
        /// <param name="key">The name of the section key.</param>
        /// <param name="value">The value of the parameter of the specified section.</param>
        /// <returns>An object for processing modified data.</returns>
        MultiConfX.Ini.IniData AddItem(string section, string key, string value = null);
    }
}
