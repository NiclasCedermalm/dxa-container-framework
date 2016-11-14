<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@ page import="com.sdl.webapp.common.api.ScreenWidth" %>
<%@ page import="com.sdl.webapp.common.api.model.EntityModel" %>
<%@ page import="java.util.Iterator" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="dxa" uri="http://www.sdl.com/tridion-dxa" %>
<%@ taglib prefix="xpm" uri="http://www.sdl.com/tridion-xpm" %>

<jsp:useBean id="container" type="com.sdl.dxa.modules.flexlayout.model.ColumnLayoutContainer" scope="request"/>
<jsp:useBean id="markup" type="com.sdl.webapp.common.markup.Markup" scope="request"/>
<jsp:useBean id="screenWidth" type="com.sdl.webapp.common.api.ScreenWidth" scope="request"/>

<div style="padding-top: 4px;">
    <%
        final int cols = screenWidth == ScreenWidth.SMALL ? 2 : container.getColumns();
        final int rows = (int) Math.ceil(container.getRegion().getEntities().size() / (double) cols);
        final Iterator<EntityModel> iterator = container.getRegion().getEntities().iterator();
        int containerSize = 12 / cols;
        for (int row = 0; row < rows; row++) {
    %>
    <div class="row">
        <xpm:region region="${container.region}"/>
        <%
            for (int col = 0; col < cols && iterator.hasNext(); col++) {
                final EntityModel entity = iterator.next();
        %>
        <div class="col-sm-6 <c:if test="${container.columns gt 2}">col-md-<%=containerSize%></c:if>">
            <dxa:entity containerSize="<%=containerSize%>" entity="<%=entity%>"/>
        </div>
        <%
            }
        %>
    </div>
    <%
        }
    %>
    <c:if test="${container.region.entities.size() == 0}">
        <%-- Show a visual XPM drop zone --%>
        <div class="row">
            <xpm:region region="${container.region}"/>
            <c:forEach begin="1" end="${container.columns}">
                <div class="col-sm-6 <c:if test="${container.columns gt 2}">col-md-<%=containerSize%></c:if>">
                    <div style="height: 300px; border-style: dashed; border-color: lightgrey; border-width: 2px;"></div>
                </div>
            </c:forEach>
        </div>
    </c:if>
</div>
