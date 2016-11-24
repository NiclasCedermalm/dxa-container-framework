package com.sdl.dxa.modules.flexlayout;

import com.sdl.dxa.modules.core.model.entity.Teaser;
import com.sdl.dxa.modules.flexlayout.model.CarouselContainer;
import com.sdl.dxa.modules.flexlayout.model.ColumnLayoutContainer;
import com.sdl.dxa.modules.flexlayout.model.TitledEntityContainer;
import com.sdl.webapp.common.api.mapping.views.AbstractInitializer;
import com.sdl.webapp.common.api.mapping.views.RegisteredViewModel;
import com.sdl.webapp.common.api.mapping.views.RegisteredViewModels;
import org.springframework.stereotype.Component;

/**
 * Flex Layout Module
 *
 * @author nic
 */
@Component
@RegisteredViewModels({

        // Container Views
        //
        @RegisteredViewModel(viewName = "ColumnLayoutContainer", modelClass = ColumnLayoutContainer.class),
        @RegisteredViewModel(viewName = "TabContainer", modelClass = TitledEntityContainer.class),
        @RegisteredViewModel(viewName = "AccordionContainer", modelClass =  TitledEntityContainer.class),
        @RegisteredViewModel(viewName = "CarouselContainer", modelClass = CarouselContainer.class),

        // TODO: Divide this into two modules -> generic + example views

        // Entity Views
        //
        @RegisteredViewModel(viewName = "CarouselTeaser", modelClass = Teaser.class)
})
public class FlexLayoutModule extends AbstractInitializer {

    @Override
    protected String getAreaName() {
        return "FlexLayout";
    }

}
