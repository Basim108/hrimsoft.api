using System.Collections.Generic;

namespace Hrimsoft.Api
{
    /// <summary> Swagger Configuration </summary>
    public interface ISwaggerConfig
    {
        /// <summary> Current api version </summary>
        string Version { get; }

        /// <summary> Route to this api </summary>
        string Route { get; }

        /// <summary> Title of the api </summary>
        string Title { get; }

        /// <summary> Description of this api scope </summary>
        string ScopeDescription { get; }

        /// <summary> Description of the api </summary>
        string Description { get; }

        /// <summary>
        /// List of documentation file names (xml) for each projects, that is required to include in Swagger
        /// </summary>
        ICollection<string> XmlDocFiles { get; }
    }
}