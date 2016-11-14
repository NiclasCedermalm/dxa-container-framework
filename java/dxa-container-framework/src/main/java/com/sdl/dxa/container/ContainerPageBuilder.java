package com.sdl.dxa.container;

import com.sdl.dxa.container.model.AbstractContainerModel;
import com.sdl.dxa.container.model.ContainerRegionModel;
import com.sdl.webapp.common.api.content.ContentProvider;
import com.sdl.webapp.common.api.content.ContentProviderException;
import com.sdl.webapp.common.api.localization.Localization;
import com.sdl.webapp.common.api.model.EntityModel;
import com.sdl.webapp.common.api.model.PageModel;
import com.sdl.webapp.common.api.model.RegionModel;
import com.sdl.webapp.common.exceptions.DxaException;
import com.sdl.webapp.tridion.mapping.PageBuilder;
import lombok.extern.slf4j.Slf4j;
import org.dd4t.contentmodel.ComponentPresentation;
import org.springframework.core.Ordered;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Container Page Builder
 *
 * @author nic
 */
@Component
@Slf4j
public class ContainerPageBuilder implements PageBuilder {


    @Override
    public PageModel createPage(org.dd4t.contentmodel.Page genericPage, PageModel pageModel, Localization localization, ContentProvider contentProvider) throws ContentProviderException {

        // TODO: If a container is interrupted with a CP with another template should the container be considered as closed then?

        List<AbstractContainerModel> containers = new ArrayList<>();

        // Get absolute order of the different container items
        //
        Map<String, List<Integer>> regionAbsoluteOrder = new HashMap<>();
        List<Integer> containerAbsoluteOrder = new ArrayList<>();
        int index = 0;
        for (ComponentPresentation cp : genericPage.getComponentPresentations() ) {
            if ( cp.getComponentTemplate().getMetadata().containsKey("regionName") ) {
                String regionName = (String) cp.getComponentTemplate().getMetadata().get("regionName").getValues().get(0);
                List<Integer> orderList = regionAbsoluteOrder.get(regionName);
                if ( orderList == null ) {
                    orderList = new ArrayList<>();
                    regionAbsoluteOrder.put(regionName, orderList);
                }
                orderList.add(index);
            }
            else if ( cp.getComponentTemplate().getMetadata().containsKey("action") ) {
                String controllerAction = (String) cp.getComponentTemplate().getMetadata().get("action").getValues().get(0);
                if ( controllerAction.equals("Container") ) {
                    containerAbsoluteOrder.add(index);
                }
            }
            index++;
        }

        // Go through the entity list and extract all containers
        //
        index = 1;
        for (RegionModel region : pageModel.getRegions() ) {
            for (EntityModel entity : region.getEntities() ) {
                if ( entity instanceof AbstractContainerModel) {
                    AbstractContainerModel container = (AbstractContainerModel) entity;

                    // Override region name using MVC route values
                    //
                    String containerRegionName = container.getMvcData().getRouteValues().get("containerRegion");
                    if ( containerRegionName != null ) {
                        container.setName(containerRegionName);
                    }
                    try {
                        container.setRegion(new ContainerRegionModel(container));
                    }
                    catch ( DxaException e ) {
                        throw new ContentProviderException("Could not create empty region for container: " + container.getName());
                    }
                    container.setIndex(index);
                    index++;
                    containers.add(container);
                }
            }
        }

        // Move all container items to the containers (to avoid the container items to be rendered outside the container)
        //
        for (AbstractContainerModel container : containers ) {
            if ( containerAbsoluteOrder.isEmpty() )  {
                break;
            }
            int containerIndex = containerAbsoluteOrder.get(0);
            containerAbsoluteOrder.remove(0);
            RegionModel regionWithContainerItems = pageModel.getRegions().get(container.getName());
            if ( regionWithContainerItems == null ) {
                // No container items found on the page -> continue
                //
                continue;
            }
            List<Integer> orderList = regionAbsoluteOrder.get(container.getName());
            if ( orderList != null && !orderList.isEmpty() ) {
                index = orderList.get(0);
                if ( containerIndex+1 == index ) {
                    int noOfItems = 0;
                    for (EntityModel entity : regionWithContainerItems.getEntities()) {
                        container.getRegion().addEntity(entity);
                        noOfItems++;
                        orderList.remove(0);
                        if (orderList.isEmpty()) {
                            break;
                        }
                        int nextIndex = orderList.get(0);
                        if (nextIndex != index + 1) {
                            break;
                        } else {
                            index = nextIndex;
                        }
                    }
                    for (int i = 0; i < noOfItems; i++) {
                        regionWithContainerItems.getEntities().remove(0);
                    }
                }
            }
        }

        return pageModel;
    }

    @Override
    public int getOrder() {
        return Ordered.LOWEST_PRECEDENCE;
    }
}
