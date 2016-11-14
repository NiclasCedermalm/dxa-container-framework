package com.sdl.dxa.container.model;

import com.sdl.webapp.common.api.mapping.semantic.annotations.SemanticEntity;
import com.sdl.webapp.common.api.model.entity.AbstractEntityModel;

import static com.sdl.webapp.common.api.mapping.semantic.config.SemanticVocabulary.SDL_CORE;

/**
 * Container Item
 * Is used as base class for entities that can only reside within a container such as an image overlay.
 * Is not needed for normal DXA entities such as articles, teasers etc
 *
 * @author nic
 */
@SemanticEntity(entityName = "ContainerItem",  vocabulary = SDL_CORE, prefix = "i", public_ = false)
public abstract class ContainerItem extends AbstractEntityModel {

    /**
     * @return  container item metadata (e.g. placement in the container etc)
     */
    public abstract ContainerItemMetadata getMetadata();

    /**
     * Set metadata. Is used by edit widgets to manipulate metadata
     * @param metadata
     */
    public abstract void setMetadata(ContainerItemMetadata metadata);


}
