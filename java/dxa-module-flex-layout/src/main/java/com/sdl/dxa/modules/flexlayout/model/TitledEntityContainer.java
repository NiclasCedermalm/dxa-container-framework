package com.sdl.dxa.modules.flexlayout.model;

import com.sdl.webapp.common.api.model.EntityModel;
import com.sdl.webapp.common.exceptions.DxaException;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

/**
 * Titled Content Container
 * General representation of a layout container with a navigation bar. The navigation bar has a title coming from entity.
 * Typical examples are tabs and accordions.
 *
 * @author nic
 */
public class TitledEntityContainer extends LayoutContainer {

    private List<TitledEntity> titledEntities = null;

    public TitledEntityContainer() throws DxaException {

    }

    /**
     * Get list of titled entities
     * @return list
     * @throws DxaException
     */
    public Collection<TitledEntity> getTitledEntities() throws DxaException {
        if ( titledEntities == null ) {
            titledEntities = new ArrayList<>();
            for ( EntityModel entity : this.getRegion().getEntities() ) {
                titledEntities.add(new TitledEntity(entity));
            }
        }
        return titledEntities;
    }
}
