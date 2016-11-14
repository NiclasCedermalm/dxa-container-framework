using Sdl.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDL.DXA.Container.Framework.Models
{
    /// <summary>
    /// Abstract container model which container implementations needs implement
    /// </summary>
    [SemanticEntity(EntityName = "Container", Vocab = CoreVocabulary, Prefix = "c")]
    public class AbstractContainerModel : EntityModel, IContainerModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        protected AbstractContainerModel(string name)
        {
            Name = name;
        }

        public string Name
        {
            get; internal set;
        }

        public string EditMarkup
        {
            get; set;
        }

        public int Index
        {
            get; set;
        }

        public ContainerMetadata Metadata
        {
            get; set;
        }

        public RegionModel Region
        {
            get; set;
        }

        public int Count
        {
            get
            {
                if ( Region != null )
                {
                    return Region.Entities.Count;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}