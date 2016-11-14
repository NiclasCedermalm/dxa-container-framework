package com.sdl.dxa.container.model;

import com.sdl.webapp.common.api.mapping.semantic.annotations.SemanticEntity;
import com.sdl.webapp.common.api.model.RegionModel;
import com.sdl.webapp.common.api.model.entity.AbstractEntityModel;
import com.sdl.webapp.common.exceptions.DxaException;

import static com.sdl.webapp.common.api.mapping.semantic.config.SemanticVocabulary.SDL_CORE;

/**
 * Abstract Container Model
 *
 * @author nic
 */
@SemanticEntity(entityName = "Container",  vocabulary = SDL_CORE, prefix = "c", public_ = false)
public abstract class AbstractContainerModel extends AbstractEntityModel implements ContainerModel {

    private String name;
    private RegionModel region;
    private int index;

    static final String EDIT_MARKUP = "<a class=\"xpm-button popup-iframe\" href=\"%s\"><i class=\"fa fa-pencil-square\"></i></a>";

    /**
     * Constructor
     * @param name
     * @throws DxaException
     */
    public AbstractContainerModel(String name) throws DxaException {
        this.name = name;
    }

    /**
     * URL to an editor widget which is opened as an iframe layer in XPM
     * @return url
     */
    protected abstract String getEditUrl();

    @Override
    public abstract ContainerMetadata getMetadata();

    /**
     * Set container metadata. Used by edit widgets to manipulate metadata.
     * @param metadata
     */
    public abstract void setMetadata(ContainerMetadata metadata);

    @Override
    public String getName() {
        return name;
    }

    /**
     * Set name of the container (done by the page builder)
     * @param name
     */
    public void setName(String name) {
        this.name = name;
    }

    @Override
    public RegionModel getRegion() {
        return region;
    }

    /**
     * Set region (done by the page builder)
     * @param region
     */
    public void setRegion(RegionModel region) {
        this.region = region;
    }

    @Override
    public String getEditMarkup() {
        return String.format(EDIT_MARKUP, this.getEditUrl());
    }

    @Override
    public int getIndex() {
        return this.index;
    }

    /**
     * Set container index (done by the page builder)
     * @param index
     */
    public void setIndex(int index) {
        this.index = index;
    }
}
