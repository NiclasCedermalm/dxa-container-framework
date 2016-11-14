using Sdl.Web.Common.Models;
using Sdl.Web.Mvc.Controllers;
using SDL.DXA.Container.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDL.DXA.Container.Framework.Controllers
{
    public class ContainerController : BaseController
    {
        public ActionResult Container(EntityModel entity, int containerSize = 0)
        {
            SetupViewData(entity, containerSize);

            AbstractContainerModel container = (AbstractContainerModel) entity;

            /*
            request.setAttribute("container", container);
        request.setAttribute(REGION_MODEL, container.getRegion());
        request.setAttribute("webRequestContext", this.webRequestContext);
            */

            return View(entity.MvcData.ViewName, entity);
        }
    }
}