using Sdl.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL.DXA.Container.Framework.Models
{
    /// <summary>
    /// Container Model 
    /// </summary>
    public interface IContainerModel
    {
        /// <summary>
        /// Name of the container (same as the region name)
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The underlying DXA region with all container items
        /// </summary>
        RegionModel Region { get; }

        /// <summary>
        /// Edit markup for the inline editor of the container (optional).
        /// </summary>
        string EditMarkup { get; }

        /// <summary>
        /// Container metadata. Used for complex metadata (modeled by embedded schemas).
        /// </summary>
        ContainerMetadata Metadata { get; }

        /// <summary>
        /// The container index (i.e. the order on the page)
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Number of container items in the container
        /// </summary>
        int Count { get; }

    }
}
