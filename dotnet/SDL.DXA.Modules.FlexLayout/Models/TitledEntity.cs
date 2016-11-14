using Sdl.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDL.DXA.Modules.FlexLayout.Models
{
    /// <summary>
    /// Titled Entity. Is an wrapper around DXA entity model to extract
    /// an title of it to be used in a titled entity layout container(such as tabs, accordions etc).
    /// </summary>
    public class TitledEntity
    {
        // TODO: Have the field names configurable
        static string[] TITLE_FIELD_NAMES = {
                    "Headline",
                    "Heading",
                    "Title"
        };

        public TitledEntity(EntityModel entity)
        {
            this.Entity = entity;
            this.ExtractTitle(entity);
        }

        public string Title
        {
            get; private set;
        }

        public string FieldName
        {
            get; private set;
        }

        public EntityModel Entity
        {
            get; private set;
        }

        protected void ExtractTitle(EntityModel entity)
        {
            var properties = entity.GetType().GetProperties();
            foreach ( var fieldName in TITLE_FIELD_NAMES )
            {
                var property = entity.GetType().GetProperty(fieldName);
                if ( property != null )
                {
                    this.Title = property.GetValue(entity).ToString();
                    this.FieldName = fieldName;
                    return;
                }
            }
        }
    }
}