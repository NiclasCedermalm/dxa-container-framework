package com.sdl.dxa.modules.flexlayout.model;

import com.sdl.webapp.common.api.model.EntityModel;
import com.sdl.webapp.common.exceptions.DxaException;

import java.beans.BeanInfo;
import java.beans.Introspector;
import java.beans.PropertyDescriptor;

/**
 * Titled Entity. Is an wrapper around DXA entity model to extract
 * an title of it to be used in a titled entity layout container (such as tabs, accordions etc).
 *
 * @author nic
 */
public class TitledEntity {

    private EntityModel entity;
    private String title;
    private String fieldName;

    static final String[] TITLE_FIELD_NAMES = {
            "headline",
            "heading",
            "title"
    };

    public TitledEntity(EntityModel entity) throws DxaException {
        this.entity = entity;
        extractTitle(entity);
    }

    public String getTitle() {
        return this.title;
    }

    public String getFieldName() {
        return fieldName;
    }

    public EntityModel getEntity() {
        return entity;
    }

    private void extractTitle(EntityModel entity) throws DxaException {
        try {
            BeanInfo beanInfo = Introspector.getBeanInfo(entity.getClass());
            for ( String fieldName : TITLE_FIELD_NAMES ) {
                for (PropertyDescriptor property : beanInfo.getPropertyDescriptors() ) {
                    if ( property.getName().equals(fieldName) && property.getPropertyType().isAssignableFrom(String.class) ) {
                        this.title = (String) property.getReadMethod().invoke(entity);
                        this.fieldName = fieldName;
                        return;
                    }
                }
            }
        }
        catch ( Exception e ) {
            throw new DxaException("Could not extract title from entity: " + entity.getId(), e);
        }
    }
}
