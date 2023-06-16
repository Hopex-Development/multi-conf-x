using System.IO;

using static System.Collections.Specialized.BitVector32;

namespace Hopex.MultiConfX.Env
{
    /// <summary>
    /// ENV file handler.
    /// </summary>
    public class Env
    {
        private string _path;

        private int _capacity = 1024;

        private string _commentDelimiter = "#";

        /// <summary>
        /// Data downloaded from the file.
        /// </summary>
        /// <returns>An object for processing uploaded data.</returns>
        public EnvData Data { get; internal set; } = new EnvData();

        /// <summary>
        /// ENV file handler.
        /// </summary>
        public Env() { }

        /// <summary>
        /// ENV file handler.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public Env(string path) => SetPath(path: path);

        /// <summary>
        /// ENV file handler.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="commentDelimiter">The comment symbol.</param>
        public Env(string path, string commentDelimiter)
            => SetPath(path: path).SetCommentDelimiter(commentDelimiter: commentDelimiter);

        /// <summary>
        /// ENV file handler.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="capacity">Suggested initial file size. By default, 1024.</param>
        public Env(string path, int capacity)
            => SetPath(path: path).SetCapacity(capacity: capacity);

        /// <summary>
        /// ENV file handler.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="commentDelimiter">The comment symbol.</param>
        /// <param name="capacity">Suggested initial file size. By default, 1024.</param>
        public Env(string path, string commentDelimiter, int capacity)
            => SetPath(path: path)
                .SetCommentDelimiter(commentDelimiter: commentDelimiter)
                .SetCapacity(capacity: capacity);

        /// <summary>
        /// Loading data from a file.
        /// </summary>
        /// <returns>An object for processing uploaded data.</returns>
        public EnvData Load()
        {
            if (File.Exists(path: GetPath()))
            {
                Data.Keys.Clear();

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

                        if (configLine.Contains(value: "="))
                        {
                            var index = configLine.IndexOf(value: '=');

                            string key = configLine.Substring(
                                startIndex: 0,
                                length: index
                            );

                            string value = configLine.Substring(startIndex: index + 1).Trim();
                            if (value.StartsWith(value: "\"") && value.EndsWith(value: "\""))
                                value = value.Substring(
                                    startIndex: 1,
                                    length: value.Length - 2
                                );

                            Data.Keys[LineFormatter.KeyForEnv(key: key)] = value;
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
            if (File.Exists(GetPath()))
            {
                using (StreamWriter streamWriter = new StreamWriter(GetPath()))
                {
                    Data.GetKeys().ForEach(action: key =>
                    {
                        var @keyFormatted = LineFormatter.KeyForEnv(key: key);
                        var @valueFormatted = LineFormatter.Value(value: Data.Keys[keyFormatted]);

                        streamWriter.WriteLine(
                            value: $"{keyFormatted}={valueFormatted}"
                        );
                    });
                }

                return true;
            }

            return false;
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
        /// Setting the path to the file..
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public Env SetPath(string path)
        {
            if (_path != path)
                _path = path.EndsWith(value: ".env")
                    ? path
                    : $"{path}.env";

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
        public Env SetCapacity(int capacity)
        {
            if (_capacity != capacity)
                _capacity = capacity <= 0 ? 1024 : capacity;

            return this;
        }

        /// <summary>
        /// Returns the suggested initial file size.
        /// </summary>
        /// <returns>The set initial file size.</returns>
        public string GetCommentDelimiter()
        {
            return _commentDelimiter;
        }

        /// <summary>
        /// Setting the comment separator. The default character is "#".
        /// </summary>
        /// <param name="commentDelimiter">Comment separator.</param>
        public Env SetCommentDelimiter(string commentDelimiter)
        {
            if (_commentDelimiter != commentDelimiter)
                _commentDelimiter = commentDelimiter;

            return this;
        }
    }
}
