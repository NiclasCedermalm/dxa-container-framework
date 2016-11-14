<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="dxa" uri="http://www.sdl.com/tridion-dxa" %>
<%@ taglib prefix="xpm" uri="http://www.sdl.com/tridion-xpm" %>
<jsp:useBean id="container" type="com.sdl.dxa.modules.flexlayout.model.TitledEntityContainer" scope="request"/>
<jsp:useBean id="markup" type="com.sdl.webapp.common.markup.Markup" scope="request"/>

<article class="rich-text">
    <div class="content">
        <xpm:region region="${container.region}"/>
        <c:if test="${container.region.entities.size() > 0}">
            <c:set var="panelId" value="${container.id}"/>
            <div class="panel panel-default">
                <div class="panel-body tab-container">
                        <%-- Tab dropdown --%>
                    <div class="dropdown visible-xs">
                        <select class="tab-group form-control" data-toggle="tab">
                            <c:forEach var="titledEntity" items="${container.titledEntities}" varStatus="status">
                                <c:set var="ident" value="tab${panelId}_${status.index}"/>
                                <option value="#${ident}" data-toggle="tab">${titledEntity.title}</option>
                            </c:forEach>
                        </select>
                    </div>
                    <%-- Tab tips --%>
                    <ul class="tab-group nav nav-tabs hidden-xs">
                        <c:forEach var="titledEntity" items="${container.titledEntities}" varStatus="status">
                            <li class="${status.index == 0 ? 'active' : ''}">
                                <xpm:entity entity="${titledEntity.entity}"/>
                                <c:set var="ident" value="tab${panelId}_${status.index}"/>
                                <a href="#${ident}"
                                   data-toggle="tab" ${markup.property(titledEntity.entity, titledEntity.fieldName)}>${titledEntity.title}</a>
                            </li>
                        </c:forEach>
                    </ul>
                        <%-- Tab panes --%>
                    <div class="tab-content">
                        <c:forEach var="entity" items="${container.region.entities}" varStatus="status">
                            <c:set var="ident" value="tab${panelId}_${status.index}"/>
                            <div class="tab-pane ${status.index == 0 ? 'active' : ''}" id="${ident}">
                                <dxa:entity entity="${entity}"/>
                            </div>
                        </c:forEach>
                    </div>
                </div>
            </div>
        </c:if>
        <c:if test="${container.region.entities.size() == 0}">
            <%-- Show a visual XPM drop zone --%>
            <ul class="tab-group nav nav-tabs hidden-xs" style="border-bottom: none; margin-top: 4px;">
                <li class="active">
                    <a href="#${container.id}_0"
                       data-toggle="tab" style="border-style: dashed; border-color: lightgrey; border-width: 2px; color: lightgrey;">Tab</a>
                </li>
            </ul>
            <div class="tab-content" style="height: 150px;border-style: dashed; border-color: lightgrey; border-width: 2px; margin-top: -12px;">
                <div class="tab-pane active" id="${container.id}_0">
                </div>
            </div>
        </c:if>
    </div>
</article>
