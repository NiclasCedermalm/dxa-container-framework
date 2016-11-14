package com.sdl.dxa.container.model;

import com.google.common.base.Joiner;
import com.sdl.webapp.common.api.localization.Localization;
import com.sdl.webapp.common.api.model.region.RegionModelImpl;
import com.sdl.webapp.common.api.xpm.ComponentType;
import com.sdl.webapp.common.api.xpm.XpmRegion;
import com.sdl.webapp.common.api.xpm.XpmRegionConfig;
import com.sdl.webapp.common.exceptions.DxaException;
import com.sdl.webapp.common.util.ApplicationContextHolder;

import java.util.ArrayList;
import java.util.List;

/**
 * Container Region Model
 *
 * @author nic
 */
public class ContainerRegionModel extends RegionModelImpl {

    private static final String XPM_REGION_MARKUP = "<!-- Start Region: {title: \"%s\", allowedComponentTypes: [%s], minOccurs: %s} -->";

    private static final String XPM_COMPONENT_TYPE_MARKUP = "{schema: \"%s\", template: \"%s\"}";

    private ContainerModel containerModel;

    /**
     * Constructor
     * @param containerModel
     * @throws DxaException
     */
    public ContainerRegionModel(ContainerModel containerModel) throws DxaException {
        super(containerModel.getName());
        this.containerModel = containerModel;
    }

    @Override
    public String getXpmMarkup(Localization localization) {

        XpmRegionConfig xpmRegionConfig = getXpmRegionConfig();
        XpmRegion xpmRegion = xpmRegionConfig.getXpmRegion(this.getName(), localization);

        if (xpmRegion == null) {
            return "";
        }

        List<String> types = new ArrayList<>();

        for (ComponentType ct : xpmRegion.getComponentTypes()) {
            types.add(String.format(XPM_COMPONENT_TYPE_MARKUP, ct.getSchemaId(), ct.getTemplateId() + "-v" + this.containerModel.getIndex()));
        }

        // TODO: obtain MinOccurs & MaxOccurs from regions.json
        return String.format(
                XPM_REGION_MARKUP,
                this.getName(),
                Joiner.on(", ").join(types),
                0);
    }

    private static XpmRegionConfig getXpmRegionConfig() {
        return ApplicationContextHolder.getContext().getBean(XpmRegionConfig.class);
    }
}
