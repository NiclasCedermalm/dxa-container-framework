<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="dxa" uri="http://www.sdl.com/tridion-dxa" %>
<%@ taglib prefix="xpm" uri="http://www.sdl.com/tridion-xpm" %>
<jsp:useBean id="container" type="com.sdl.dxa.modules.flexlayout.model.CarouselContainer" scope="request"/>
<jsp:useBean id="markup" type="com.sdl.webapp.common.markup.Markup" scope="request"/>

<div>
    <c:set var="carouselId" value="carousel-${container.id}"/>
    <c:if test="${container.region.entities.size() > 0 }">
        <div id="${carouselId}" class="carousel slide" data-ride="carousel" data-interval="${container.interval}">
            <xpm:region region="${container.region}"/>
            <ol class="carousel-indicators">
                <c:forEach var="indicator" varStatus="status" items="${container.region.entities}">
                    <c:choose>
                        <c:when test="${status.index == 0}">
                            <li data-target="#${carouselId}" data-slide-to="${status.index}" class="active"><xpm:entity entity="${indicator}"/></li>
                        </c:when>
                        <c:otherwise>
                            <li data-target="#${carouselId}" data-slide-to="${status.index}"><xpm:entity entity="${indicator}"/></li>
                        </c:otherwise>
                    </c:choose>
                </c:forEach>
            </ol>
            <div class="carousel-inner">
                <c:forEach var="entity" varStatus="status" items="${container.region.entities}">
                    <c:choose>
                        <c:when test="${status.index == 0}">
                            <div class="item active">
                                <dxa:entity entity="${entity}"/>
                            </div>
                        </c:when>
                        <c:otherwise>
                            <div class="item">
                                <dxa:entity entity="${entity}"/>
                            </div>
                        </c:otherwise>
                    </c:choose>
                </c:forEach>
            </div>
            <a class="left carousel-control" href="#${carouselId}" data-slide="prev">
                <i class='fa fa-chevron-left carousel-icon-left'></i>
            </a>
            <a class="right carousel-control" href="#${carouselId}" data-slide="next">
                <i class='fa fa-chevron-right carousel-icon-right'></i>
            </a>
        </div>
    </c:if>
    <c:if test="${container.region.entities.size() == 0}">
        <%-- Show a visual XPM drop zone --%>
        <div id="${carouselId}" class="carousel slide" data-ride="carousel" style="border-width: 2px; border-color: lightgrey; height: 300px; border-style: dashed;background: none;">
            <xpm:region region="${container.region}"/>
            <ol class="carousel-indicators"></ol>
            <div class="carousel-inner"></div>
            <a class="left carousel-control" href="#${carouselId}" data-slide="prev">
                <i class='fa fa-chevron-left carousel-icon-left'></i>
            </a>
            <a class="right carousel-control" href="#${carouselId}" data-slide="next">
                <i class='fa fa-chevron-right carousel-icon-right'></i>
            </a>
        </div>
    </c:if>
</div>
