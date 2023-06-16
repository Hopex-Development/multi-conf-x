namespace Hopex.MultiConfX.Interfaces.Env
{
    /// <summary>
    /// Includes items processing methods.
    /// </summary>
    public interface IItems
    {
        /// <summary>
        /// Adding a key, and its value. If there is no key, he will be created, and the value,
        /// if it already exists, will be overwritten.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="value">Value of specific key.</param>
        /// <returns>An object for processing modified data.</returns>
        MultiConfX.Env.EnvData AddItem(string key, string value = null);
    }
}
