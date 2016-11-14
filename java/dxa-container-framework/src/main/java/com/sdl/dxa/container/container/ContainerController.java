package com.sdl.dxa.container.container;

import com.sdl.dxa.container.model.AbstractContainerModel;
import com.sdl.webapp.common.api.WebRequestContext;
import com.sdl.webapp.common.api.content.ContentProviderException;
import com.sdl.webapp.common.api.model.MvcData;
import com.sdl.webapp.common.controller.BaseController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;

import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletRequest;

import static com.sdl.webapp.common.controller.RequestAttributeNames.REGION_MODEL;

/**
 * Container Controller
 *
 * @author nic
 */
@Controller
@RequestMapping("/system/mvc/Container/ContainerController")
public class ContainerController extends BaseController {

    @Autowired
    private WebRequestContext webRequestContext;

    @RequestMapping(method = RequestMethod.GET, value = "Container/{entityId}")
    public String handleContainer(HttpServletRequest request, @PathVariable String entityId) throws ContentProviderException {

        AbstractContainerModel container = (AbstractContainerModel) this.getEntityFromRequest(request, entityId);

        request.setAttribute("container", container);
        request.setAttribute(REGION_MODEL, container.getRegion());
        request.setAttribute("webRequestContext", this.webRequestContext);

        final MvcData mvcData = container.getMvcData();
        return viewNameResolver.resolveView(mvcData, "Container");
    }

}
