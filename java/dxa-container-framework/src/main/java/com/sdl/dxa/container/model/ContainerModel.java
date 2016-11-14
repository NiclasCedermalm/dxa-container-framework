package com.sdl.dxa.container.model;

import com.sdl.webapp.common.api.model.EntityModel;
import com.sdl.webapp.common.api.model.RegionModel;

/**
 * Container Model
 *
 * @author nic
 */
public interface ContainerModel extends EntityModel {

    /**
     * @return name of the container (same as the region name)
     */
    String getName();

    /**
     * @return get the underlying DXA region with all container items
     */
    RegionModel getRegion();

    /**
     * Get edit markup for the inline editor.
     * @return HTML markup
     */
    String getEditMarkup();

    /**
     * Get container metadata. Used for complex metadata (modeled by embedded schemas).
     * @return  metadata
     */
    ContainerMetadata getMetadata();

    /**
     * Get the container index (i.e. the order on the page)
     * @return index
     */
    int getIndex();
}
