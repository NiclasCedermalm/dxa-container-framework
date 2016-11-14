package com.sdl.dxa.modules.flexlayout.model;

import com.sdl.webapp.common.api.mapping.semantic.annotations.SemanticEntity;
import com.sdl.webapp.common.api.mapping.semantic.annotations.SemanticProperty;
import com.sdl.webapp.common.exceptions.DxaException;

import static com.sdl.webapp.common.api.mapping.semantic.config.SemanticVocabulary.SDL_CORE;

/**
 * Column Layout Container
 *
 * @author nic
 */
@SemanticEntity(entityName = "ColumnLayoutContainer",  vocabulary = SDL_CORE, prefix = "c")
public class ColumnLayoutContainer extends LayoutContainer {

    @SemanticProperty("c:columns")
    private int columns;

    public ColumnLayoutContainer() throws DxaException {
        super();
    }

    public int getColumns() {
        return columns;
    }
}
