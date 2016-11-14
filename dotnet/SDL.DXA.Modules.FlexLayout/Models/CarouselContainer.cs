using Sdl.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDL.DXA.Modules.FlexLayout.Models
{
    /// <summary>
    /// Carousel Container
    /// </summary>
    [SemanticEntity(EntityName = "CarouselContainer", Vocab = CoreVocabulary, Prefix = "c")]
    public class CarouselContainer : LayoutContainer
    {
        const int DEFAULT_INTERVAL = 5000; // Default 5000 ms 
        private int _interval = DEFAULT_INTERVAL;

        [SemanticProperty("c:interval")]
        public int Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                _interval = value;
            }
        }
    }
}