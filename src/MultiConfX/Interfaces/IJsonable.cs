using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hopex.MultiConfX.Interfaces
{
    /// <summary>
    /// Includes JSON serializing processing methods.
    /// </summary>
    public interface IJsonable
    {
        /// <summary>
        /// Serializes the specified object to a JSON string using formatting.
        /// </summary>
        /// <param name="isIndented">Causes child objects to be indented according to the
        /// Newtonsoft.Json.JsonTextWriter.Indentation and Newtonsoft.Json.JsonTextWriter.IndentChar settings.
        /// </param>
        /// <returns>A JSON string representation of the object.</returns>
        string ToJson(bool isIndented);
    }
}
