using Sdl.Web.Common.Models;
using Sdl.Web.Mvc.Configuration;
using SDL.DXA.Modules.FlexLayout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDL.DXA.Modules.FlexLayout
{
    public class FlexLayoutAreaRegistration : BaseAreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FlexLayout";
            }
        }

        protected override void RegisterAllViewModels()
        {
            // Container Views
            //
            RegisterViewModel("ColumnLayoutContainer", typeof(ColumnLayoutContainer), "Container");
            RegisterViewModel("CarouselContainer", typeof(CarouselContainer), "Container");
            RegisterViewModel("TabContainer", typeof(TitledEntityContainer), "Container");
            RegisterViewModel("AccordionContainer", typeof(TitledEntityContainer), "Container");

            // Entity Views
            //
            RegisterViewModel("CarouselTeaser", typeof(Teaser));
        }
    }
}