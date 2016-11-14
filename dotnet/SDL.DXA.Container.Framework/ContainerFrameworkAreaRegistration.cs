using Sdl.Web.Mvc.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDL.DXA.Container.Framework
{
    public class ContainerFrameworkAreaRegistration : BaseAreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Container";
            }
        }
    }
}