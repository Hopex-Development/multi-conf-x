namespace Hopex.MultiConfX.Interfaces.Env
{
    /// <summary>
    /// Includes keys processing methods.
    /// </summary>
    public interface IKeys
    {
        /// <summary>
        /// Returns a list of keys.
        /// </summary>
        /// <returns>List of keys.</returns>
        System.Collections.Generic.List<string> GetKeys();

        /// <summary>
        /// Changes the name of the key.
        /// </summary>
        /// <param name="key">The original name of the key.</param>
        /// <param name="newKey">The new name of the key.</param>
        /// <returns>An object for processing modified data.</returns>
        MultiConfX.Env.EnvData EditKey(string key, string newKey);

        /// <summary>
        /// Deleting the key with him value from the file.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <returns>An object for processing modified data.</returns>
        MultiConfX.Env.EnvData RemoveKey(string key);

        /// <summary>
        /// Deleting the keys with they values from the file.
        /// </summary>
        /// <param name="keys">Key names.</param>
        /// <returns>An object for processing modified data.</returns>
        MultiConfX.Env.EnvData RemoveKeys(params string[] keys);
    }
}
