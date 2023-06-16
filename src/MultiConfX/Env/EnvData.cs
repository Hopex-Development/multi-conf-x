using System.Collections.Generic;
using System.Linq;

using Hopex.MultiConfX.Interfaces;
using Hopex.MultiConfX.Interfaces.Env;

using Newtonsoft.Json;

namespace Hopex.MultiConfX.Env
{
    /// <summary>
    /// An object for data processing.
    /// </summary>
    public class EnvData : IJsonable, IKeys, IValues, IItems
    {
        /// <summary>
        /// File keys.
        /// </summary>
        internal SortedDictionary<string, string> Keys
        {
            get;
            private set;
        } = new SortedDictionary<string, string>();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public List<string> GetKeys() => Keys.Select(selector: key => key.Key).ToList();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="key"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string GetValue(string key) => Keys[LineFormatter.KeyForEnv(key: key)];

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="key"><inheritdoc/></param>
        /// <param name="newKey"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public EnvData EditKey(string key, string newKey)
        {
            var @keyFormatted = LineFormatter.KeyForEnv(key: key);
            var @newKeyFormatted = LineFormatter.KeyForEnv(key: newKey);

            if (
                Keys.ContainsKey(key: keyFormatted) && 
                !Keys.ContainsKey(key: newKeyFormatted)
            )
            {
                Keys.Add(
                    key: newKeyFormatted,
                    value: Keys[keyFormatted]
                );
                Keys.Remove(key: keyFormatted);
            }

            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="key"><inheritdoc/></param>
        /// <param name="value"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public EnvData EditValue(string key, string value)
        {
            var @keyFormatted = LineFormatter.KeyForEnv(key: key);

            if (Keys.ContainsKey(key: keyFormatted) && !Keys[keyFormatted].Equals(value: value))
                Keys[keyFormatted] = value;

            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="key"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public EnvData RemoveKey(string key)
        {
            var @keyFormatted = LineFormatter.KeyForEnv(key: key);

            if (Keys.ContainsKey(key: keyFormatted))
                Keys.Remove(key: keyFormatted);

            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="keys"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public EnvData RemoveKeys(params string[] keys)
        {
            keys.ToList().ForEach(action: key =>
            {
                RemoveKey(key);
            });

            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="key"><inheritdoc/></param>
        /// <param name="value"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public EnvData AddItem(string key, string value = null)
        {
            var @keyFormatted = LineFormatter.KeyForEnv(key: key);

            if (!Keys.ContainsKey(key: keyFormatted))
                Keys.Add(
                    key: keyFormatted,
                    value: string.Empty
                );

            Keys[keyFormatted] = value ?? string.Empty;

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
                value: Keys,
                formatting: isIndented
                    ? Formatting.Indented
                    : Formatting.None
            );
        }
    }
}
