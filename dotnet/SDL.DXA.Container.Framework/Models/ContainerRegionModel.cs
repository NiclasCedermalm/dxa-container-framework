using Sdl.Web.Common.Configuration;
using Sdl.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDL.DXA.Container.Framework.Models
{
    /// <summary>
    /// Container Region Model
    /// </summary>
    public class ContainerRegionModel : RegionModel
    {
        private IContainerModel containerModel;

        private const string XpmRegionMarkup = "<!-- Start Region: {{title: \"{0}\", allowedComponentTypes: [{1}], minOccurs: {2}}} -->";
        private const string XpmComponentTypeMarkup = "{{schema: \"{0}\", template: \"{1}-v{2}\"}}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="containerModel"></param>
        public ContainerRegionModel(IContainerModel containerModel) : base(containerModel.Name)
        {
            this.containerModel = containerModel;
        }

        /// <summary>
        /// Get container region XPM markup with the container index piggy-backed on the template ID.
        /// This to make sure the new container items that drag&dropped into the region ends up in the correct container.
        /// </summary>
        /// <param name="localization"></param>
        /// <returns></returns>
        public override string GetXpmMarkup(Localization localization)
        {
            XpmRegion xpmRegion = localization.GetXpmRegionConfiguration(Name);

            if ( xpmRegion == null )
            {
                return string.Empty;
            }

            return string.Format(
                XpmRegionMarkup,
                Name,
                string.Join(", ", xpmRegion.ComponentTypes.Select(ct => string.Format(XpmComponentTypeMarkup, ct.Schema, ct.Template, this.containerModel.Index))),
                0);
        }
    }
}