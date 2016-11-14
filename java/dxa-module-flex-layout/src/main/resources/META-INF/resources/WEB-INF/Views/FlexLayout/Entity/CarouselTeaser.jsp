<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="dxa" uri="http://www.sdl.com/tridion-dxa" %>
<%@ taglib prefix="xpm" uri="http://www.sdl.com/tridion-xpm" %>
<jsp:useBean id="entity" type="com.sdl.webapp.common.api.model.entity.Teaser" scope="request"/>
<jsp:useBean id="markup" type="com.sdl.webapp.common.markup.Markup" scope="request"/>
<jsp:useBean id="screenWidth" type="com.sdl.webapp.common.api.ScreenWidth" scope="request"/>

<c:choose>
    <c:when test="${screenWidth == 'EXTRA_SMALL'}">
        <c:set var="imageAspect" value="2.0"/>
    </c:when>
    <c:when test="${screenWidth == 'SMALL'}">
        <c:set var="imageAspect" value="2.5"/>
    </c:when>
    <c:otherwise>
        <c:set var="imageAspect" value="3.3"/>
    </c:otherwise>
</c:choose>
<div ${markup.entity(entity)}>
    <c:choose>
        <c:when test="${not empty entity.media}">
            <span ${markup.property(entity, "media")}>
                <dxa:media media="${entity.media}" widthFactor="100%" aspect="${imageAspect}"/>
            </span>
        </c:when>
        <c:otherwise>
            <img src="data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7" alt=""
                 width="100%">
        </c:otherwise>
    </c:choose>
    <c:if test="${not empty entity.headline or not empty entity.text}">
        <div class="overlay overlay-tl ribbon">
            <c:if test="${not empty entity.headline}">
                <h2 ${markup.property(entity, "headline")}>${entity.headline}</h2>
            </c:if>
            <c:if test="${not empty entity.text}">
                <div ${markup.property(entity, "text")}>
                    <dxa:richtext content="${entity.text}"/>
                </div>
            </c:if>
        </div>
    </c:if>
    <c:if test="${not empty entity.link.linkText}">
        <div class="carousel-caption">
            <p>
                <a href="${entity.link.url}" title="${entity.link.alternateText}"
                   class="btn btn-primary" ${markup.property(entity, "link")}>
                        ${entity.link.linkText}
                </a>
            </p>
        </div>
    </c:if>
</div>
