using System.Collections.Generic;
using System.IO;

namespace Hopex.MultiConfX.Ini
{
    /// <summary>
    /// The INI file handler.
    /// </summary>
    public class Ini
    {
        private string _path;

        private int _capacity = 1024;

        private string _commentDelimiter = "#";

        /// <summary>
        /// Data downloaded from the file.
        /// </summary>
        /// <returns>An object for processing uploaded data.</returns>
        public IniData Data { get; internal set; } = new IniData();

        /// <summary>
        /// The INI file handler.
        /// </summary>
        public Ini() { }

        /// <summary>
        /// The INI file handler.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public Ini(string path) => SetPath(path: path);

        /// <summary>
        /// The INI file handler.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="commentDelimiter">The comment symbol.</param>
        public Ini(string path, string commentDelimiter)
            => SetPath(path: path).SetCommentDelimiter(commentDelimiter: commentDelimiter);

        /// <summary>
        /// The INI file handler.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="capacity">Suggested initial file size. By default, 1024.</param>
        public Ini(string path, int capacity)
            => SetPath(path: path).SetCapacity(capacity: capacity);

        /// <summary>
        /// The INI file handler.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="commentDelimiter">The comment symbol.</param>
        /// <param name="capacity">Suggested initial file size. By default, 1024.</param>
        public Ini(string path, string commentDelimiter, int capacity)
            => SetPath(path: path)
                .SetCommentDelimiter(commentDelimiter: commentDelimiter)
                .SetCapacity(capacity: capacity);

        /// <summary>
        /// Loading data from a file.
        /// </summary>
        /// <returns>An object for processing uploaded data.</returns>
        public IniData Load()
        {
            if (File.Exists(GetPath()))
            {
                Data.Sections.Clear();

                using (StreamReader streamReader = new StreamReader(path: GetPath()))
                {
                    string
                        configLine = string.Empty,
                        configSection = string.Empty;

                    while ((configLine = streamReader.ReadLine()) != null)
                    {
                        configLine = configLine.Trim();

                        if (
                            configLine.Length == 0 ||
                            (
                                !string.IsNullOrEmpty(value: GetCommentDelimiter()) &&
                                configLine.StartsWith(value: GetCommentDelimiter())
                            )
                        )
                            continue;

                        if (configLine.StartsWith(value: "[") && configLine.Contains(value: "]"))
                        {
                            configSection = configLine.Substring(
                                startIndex: 1,
                                length: configLine.IndexOf(']') - 1
                            ).Trim();

                            if (!Data.Sections.ContainsKey(key: configSection))
                            {
                                Data.Sections.Add(
                                    key: configSection,
                                    value: new SortedDictionary<string, string>()
                                );
                            }

                            continue;
                        }

                        if (configLine.Contains("="))
                        {
                            var index = configLine.IndexOf(value: '=');

                            string parameterOfSection = configLine.Substring(
                                startIndex: 0,
                                length: index
                            ).Trim();

                            string value = configLine.Substring(startIndex: index + 1).Trim();
                            if (value.StartsWith(value: "\"") && value.EndsWith(value: "\""))
                                value = value.Substring(
                                    startIndex: 1, 
                                    length: value.Length - 2
                                );

                            Data.Sections[configSection][LineFormatter.KeyForIni(key: parameterOfSection)] = value;
                        }
                    }
                }
            }

            return Data;
        }

        /// <summary>
        /// Saving modified data to a file.
        /// </summary>
        /// <returns><see langword="true"/>, if the data was written to the file without errors.</returns>
        public bool Save()
        {
            if (!File.Exists(path: GetPath()))
                File.Create(path: GetPath()).Dispose();

            using (StreamWriter streamWriter = new StreamWriter(
                path: GetPath(), 
                append: false
            ))
            {
                Data.GetSections().ForEach(action: section =>
                {
                    streamWriter.WriteLine(value: $"[{section.Trim()}]");
                    Data.GetKeys(section).ForEach(action: key =>
                    {
                        var @keyFormatted = LineFormatter.KeyForIni(key: key);
                        var @valueFormatted = LineFormatter.Value(value: Data.Sections[section][key]);

                        streamWriter.WriteLine(value: $"{keyFormatted}={valueFormatted}");
                    });
                });
            }

            return true;
        }

        /// <summary>
        /// Returns the path to the file.
        /// </summary>
        /// <returns>The set path to the file.</returns>
        public string GetPath()
        {
            return _path;
        }

        /// <summary>
        /// Setting the path to the file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public Ini SetPath(string path)
        {
            if (_path != path)
                _path = path.EndsWith(value: ".ini") ? path : $"{path}.ini";

            return this;
        }

        /// <summary>
        /// Returns the suggested initial file size.
        /// </summary>
        /// <returns>The set initial file size.</returns>
        public int GetCapacity()
        {
            return _capacity;
        }

        /// <summary>
        /// Setting the initial file size.
        /// </summary>
        /// <param name="capacity">File size.</param>
        public Ini SetCapacity(int capacity)
        {
            if (_capacity != capacity)
                _capacity = capacity <= 0 ? 1024 : capacity;
            
            return this;
        }

        /// <summary>
        /// Returns the comment separator.
        /// </summary>
        /// <returns>The installed comment separator.</returns>
        public string GetCommentDelimiter()
        {
            return _commentDelimiter;
        }

        /// <summary>
        /// Setting the comment separator. The default character is "#".
        /// </summary>
        /// <param name="commentDelimiter">Comment separator.</param>
        public Ini SetCommentDelimiter(string commentDelimiter)
        {
            if (_commentDelimiter != commentDelimiter)
                _commentDelimiter = commentDelimiter;

            return this;
        }
    }
}
