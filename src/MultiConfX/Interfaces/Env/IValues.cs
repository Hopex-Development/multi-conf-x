namespace Hopex.MultiConfX.Interfaces.Env
{
    /// <summary>
    /// Includes values processing methods.
    /// </summary>
    public interface IValues
    {
        /// <summary>
        /// Returns the value of the specified key.
        /// </summary>
        /// <param name="key">The name of the key.</param>
        /// <returns>The value of the specified key.</returns>
        string GetValue(string key);

        /// <summary>
        /// Changes the value of the key.
        /// </summary>
        /// <param name="key">The name of the key.</param>
        /// <param name="value">New key value.</param>
        /// <returns>An object for processing modified data.</returns>
        MultiConfX.Env.EnvData EditValue(string key, string value);
    }
}
