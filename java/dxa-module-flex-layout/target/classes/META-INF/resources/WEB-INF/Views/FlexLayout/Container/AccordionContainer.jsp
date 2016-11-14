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
            <c:set var="accordionId" value="${container.id}"/>
            <div class="panel-group responsive-accordion" id="${accordionId}">
                <c:forEach var="titledEntity" items="${container.titledEntities}" varStatus="status">
                    <c:set var="collapseId" value="${container.id}_${status.index}"/>
                    <div class="panel panel-default">
                        <div class="panel-heading" data-toggle="collapse" data-target="#${collapseId}"
                             data-parent="#${accordionId}">
                            <xpm:entity entity="${titledEntity.entity}"/>
                            <h4 class="panel-title" ${markup.property(titledEntity.entity, "headline")}>${titledEntity.title}</h4>
                        </div>
                        <div id="${collapseId}" class="panel-collapse collapse ${status.index == 0 ? 'in' : ''}">
                            <div class="panel-body">
                                <dxa:entity entity="${titledEntity.entity}"/>
                            </div>
                        </div>
                    </div>
                </c:forEach>
            </div>
        </c:if>
        <c:if test="${container.region.entities.size() == 0}">
            <%-- Show a visual XPM drop zone --%>
            <div class="panel-group responsive-accordion" id="${container.id}">
                <div class="panel panel-default">
                    <div class="panel-heading" data-toggle="collapse" data-target="#${container.id}_0"
                         data-parent="#${container.id}" style="border-style: dashed; border-color: lightgrey; border-width: 2px;">
                        <h4 class="panel-title" style="color: lightgrey;">Accordion</h4>
                    </div>
                    <div class="panel-heading" data-toggle="collapse" data-target="#${container.id}_1"
                         data-parent="#${container.id}" style="border-style: dashed; border-color: lightgrey; border-width: 2px;">
                        <h4 class="panel-title" style="color: lightgrey;">&nbsp;</h4>
                    </div>
                </div>
            </div>
        </c:if>
    </div>
</article>
