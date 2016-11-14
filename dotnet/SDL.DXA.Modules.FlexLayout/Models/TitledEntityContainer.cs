using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDL.DXA.Modules.FlexLayout.Models
{
    /// <summary>
    /// Titled Content Container
    /// General representation of a layout container with a navigation bar.The navigation bar has a title coming from entity.
    /// Typical examples are tabs and accordions.
    /// </summary>
    public class TitledEntityContainer : LayoutContainer
    {
        private List<TitledEntity> titledEntities = null;

        /// <summary>
        /// Get list of titled entities
        /// </summary>
        public IList<TitledEntity> TitledEntities
        {
            get
            {
                if ( titledEntities == null )
                {
                    titledEntities = new List<TitledEntity>();
                    foreach ( var entity in Region.Entities )
                    {
                        titledEntities.Add(new TitledEntity(entity));
                    }
                }
                return titledEntities;
            }
        }
    }
}