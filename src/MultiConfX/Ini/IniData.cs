using System.Collections.Generic;
using System.Linq;

using Hopex.MultiConfX.Interfaces;
using Hopex.MultiConfX.Interfaces.Ini;

using Newtonsoft.Json;

namespace Hopex.MultiConfX.Ini
{
    /// <summary>
    /// An object for data processing.
    /// </summary>
    public class IniData : IJsonable, ISections, IKeys, IItems
    {
        /// <summary>
        /// Sections of the file.
        /// </summary>
        internal SortedDictionary<string, SortedDictionary<string, string>> Sections
        {
            get;
            private set;
        } = new SortedDictionary<string, SortedDictionary<string, string>>();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public List<string> GetSections() => Sections.Select(x => x.Key).ToList();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="section"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public List<string> GetKeys(string section) => Sections[section].Select(key => key.Key).ToList();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="section"><inheritdoc/></param>
        /// <param name="key"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string GetValue(string section, string key) => Sections[section][LineFormatter.KeyForIni(key: key)];

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="section"><inheritdoc/></param>
        /// <param name="newSection"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IniData EditSection(string section, string newSection)
        {
            if (
                Sections.ContainsKey(key: section) &&
                !Sections.ContainsKey(key: newSection)
            )
            {
                Sections.Add(
                    key: newSection, 
                    value: Sections[section]
                );
                Sections.Remove(key: section);
            }

            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="section"><inheritdoc/></param>
        /// <param name="key"><inheritdoc/></param>
        /// <param name="newKey"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IniData EditKey(string section, string key, string newKey)
        {
            var @keyFormatted = LineFormatter.KeyForIni(key: key);
            var @newKeyFormatted = LineFormatter.KeyForIni(key: newKey);

            if (
               Sections[section].ContainsKey(key: keyFormatted) &&
               !Sections[section].ContainsKey(key: newKeyFormatted)
            )
            {
                Sections[section].Add(
                    key: newKeyFormatted, 
                    value: Sections[section][keyFormatted]
                );
                Sections[section].Remove(key: keyFormatted);
            }

            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="section"><inheritdoc/></param>
        /// <param name="key"><inheritdoc/></param>
        /// <param name="value"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IniData EditValue(string section, string key, string value)
        {
            var @keyFormatted = LineFormatter.KeyForIni(key);

            if (
               Sections.ContainsKey(key: section) &&
               Sections[section].ContainsKey(key: keyFormatted) &&
               !Sections[section][keyFormatted].Equals(value: value)
            )
                Sections[section][keyFormatted] = value;

            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="section"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IniData RemoveSection(string section)
        {
            if (Sections.ContainsKey(section))
                Sections.Remove(section);

            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="sections"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IniData RemoveSections(params string[] sections)
        {
            sections.ToList().ForEach(action: section =>
            {
                RemoveSection(section);
            });

            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="section"><inheritdoc/></param>
        /// <param name="key"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IniData RemoveKey(string section, string key)
        {
            var @keyFormatted = LineFormatter.KeyForIni(key);

            if (
                Sections.ContainsKey(section) &&
                Sections[section].ContainsKey(keyFormatted)
            )
                Sections[section].Remove(keyFormatted);

            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="section"><inheritdoc/></param>
        /// <param name="keys"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IniData RemoveKeys(string section, params string[] keys)
        {
            keys.ToList().ForEach(action: key =>
            {
                RemoveKey(section, key);
            });

            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="section"><inheritdoc/></param>
        /// <param name="key"><inheritdoc/></param>
        /// <param name="value"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IniData AddItem(string section, string key, string value = null)
        {
            var @keyFormatted = LineFormatter.KeyForIni(key);
            
            if (!Sections.ContainsKey(key: section))
                Sections.Add(
                    key: section,
                    value: new SortedDictionary<string, string>()
                );

            if (!Sections[section].ContainsKey(key: keyFormatted))
                Sections[section].Add(
                    key: keyFormatted,
                    value: string.Empty
                );

            Sections[section][keyFormatted] = value ?? string.Empty;

            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="isIndented"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string ToJson(bool isIndented)
        {
            return JsonConvert.SerializeObject(
                value: Sections,
                formatting: isIndented
                    ? Formatting.Indented
                    : Formatting.None
            );
        }
    }
}
