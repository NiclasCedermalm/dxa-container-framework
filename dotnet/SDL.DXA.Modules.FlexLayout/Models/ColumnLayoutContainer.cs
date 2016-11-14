using Sdl.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDL.DXA.Modules.FlexLayout.Models
{
    /// <summary>
    /// Column Layout Container
    /// </summary>
    [SemanticEntity(EntityName = "ColumnLayoutContainer", Vocab = CoreVocabulary, Prefix = "c")]
    public class ColumnLayoutContainer : LayoutContainer
    {
        [SemanticProperty("c:columns")]
        public int Columns { get; set; }
    }
}