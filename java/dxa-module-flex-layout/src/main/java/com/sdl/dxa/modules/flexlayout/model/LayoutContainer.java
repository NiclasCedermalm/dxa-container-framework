package com.sdl.dxa.modules.flexlayout.model;

import com.sdl.dxa.container.model.AbstractContainerModel;
import com.sdl.dxa.container.model.ContainerMetadata;
import com.sdl.webapp.common.api.mapping.semantic.annotations.SemanticEntity;
import com.sdl.webapp.common.exceptions.DxaException;

import static com.sdl.webapp.common.api.mapping.semantic.config.SemanticVocabulary.SDL_CORE;

/**
 * General Layout Container
 *
 * @author nic
 */
@SemanticEntity(entityName = "LayoutContainer",  vocabulary = SDL_CORE, prefix = "c")
public class LayoutContainer extends AbstractContainerModel {

    public LayoutContainer() throws DxaException {
        super("Layout");  // Default region name for the container
    }

    @Override
    protected String getEditUrl() {
        return null;
    }

    @Override
    public ContainerMetadata getMetadata() {
        return null;
    }

    @Override
    public void setMetadata(ContainerMetadata metadata) {}
}
