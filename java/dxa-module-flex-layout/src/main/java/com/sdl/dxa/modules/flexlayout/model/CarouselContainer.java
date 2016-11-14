package com.sdl.dxa.modules.flexlayout.model;

import com.sdl.webapp.common.api.mapping.semantic.annotations.SemanticEntity;
import com.sdl.webapp.common.api.mapping.semantic.annotations.SemanticProperty;
import com.sdl.webapp.common.exceptions.DxaException;

import static com.sdl.webapp.common.api.mapping.semantic.config.SemanticVocabulary.SDL_CORE;

/**
 * Carousel Container
 *
 * @author nic
 */
@SemanticEntity(entityName = "CarouselContainer",  vocabulary = SDL_CORE, prefix = "c")
public class CarouselContainer extends LayoutContainer {

    @SemanticProperty("c:interval")
    private int interval = 5000; // Default: 5000 ms

    public CarouselContainer() throws DxaException {
    }

    public int getInterval() {
        return interval;
    }
}
