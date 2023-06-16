namespace Hopex.MultiConfX.Interfaces.Ini
{
    /// <summary>
    /// Includes sections processing methods.
    /// </summary>
    public interface ISections
    {
        /// <summary>
        /// Returns a list of file sections.
        /// </summary>
        /// <returns>List of file sections.</returns>
        System.Collections.Generic.List<string> GetSections();

        /// <summary>
        /// Changes the name of the file section.
        /// </summary>
        /// <param name="section">The original name of the file section.</param>
        /// <param name="newSection">The new name of the file section.</param>
        /// <returns>An object for processing modified data.</returns>
        MultiConfX.Ini.IniData EditSection(string section, string newSection);

        /// <summary>
        /// Deleting the specified section from the file, including child keys.
        /// </summary>
        /// <param name="section">The name of the file section.</param>
        /// <returns>An object for processing modified data.</returns>
        MultiConfX.Ini.IniData RemoveSection(string section);

        /// <summary>
        /// Deleting the sections with they keys and values from the file.
        /// </summary>
        /// <param name="sections">Sections names.</param>
        /// <returns>An object for processing modified data.</returns>
        MultiConfX.Ini.IniData RemoveSections(params string[] sections);
    }
}
