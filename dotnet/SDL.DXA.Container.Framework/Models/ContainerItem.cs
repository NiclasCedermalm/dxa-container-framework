using Sdl.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDL.DXA.Container.Framework.Models
{
    /// <summary>
    /// Container Item
    /// Is used as base class for entities that can only reside within a container such as an image overlay.
    /// Is not needed for normal DXA entities such as articles, teasers etc
    /// </summary>
    public abstract class ContainerItem : EntityModel
    {
        /// <summary>
        ///Ccontainer item metadata (e.g. placement in the container etc)
        /// </summary>
        /// <returns></returns>
        public abstract ContainerItemMetadata getMetadata();

        /// <summary>
        /// Set metadata. Is used by edit widgets to manipulate metadata
        /// </summary>
        /// <param name="metadata"></param>
        public abstract void setMetadata(ContainerItemMetadata metadata);
    }
}