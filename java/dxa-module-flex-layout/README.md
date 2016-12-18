Flex Layout DXA Module
========================

This DXA module provides examples of flexible layouts, which can be managed through SDL Web Experience Manager.
It is based on the [DXA Container Framework](../dxa-container-framework/README.md). The module contains layout containers for the following:

* Tabs
* Accordions
* Carousels
* Column layouts

The views are based on the standard DXA white label design. 

Usage
------

Run the [CMS Import scripts](../../cms/flex-layout/install/README.md). They will install the needed schemas, templates and XPM content types.
Republish the Settings page to update DXA with the new schemas and templates.

Install the DXA Container Framework + Flex Layout DXA module in your local Maven repository by doing the following in the root of this repository:

```
mvn install
```

Add the DXA module as Maven dependency to your web application POM file:

```
    <dependencies> 
        ...
        <dependency>
            <groupId>com.sdl.dxa.container</groupId>
            <artifactId>dxa-module-flex-layout</artifactId>
            <version>1.1.0</version>
        </dependency>
        
    </dependencies>

```

Setup a DXA from scratch with the Flex Layout module
------------------------------------------------------

If you want to setup a clean DXA with the FlexLayout module and the DXA Container Framework from scratch, you can follow the below steps:

1. Create a new DXA webapp by doing the following:
    - mvn archetype:generate
    - select com.sdl.dxa:dxa-webapp-archetype v1.6.0
2. Install the DXA Container Framework modules:
    - Clone the code repository: `git clone https://github.com/NiclasCedermalm/dxa-container-framework`
    - go to dxa-container-framework/java
    - Install the modules into the local Maven repository: `mvn install`
3. Update DXA webapp with dependency to the Flex Layout module by adding the following to the webapp's POM file:

```
    <dependency>
        <groupId>com.sdl.dxa.container</groupId>
        <artifactId>dxa-module-flex-layout</artifactId>
        <version>1.1.0</version>
    </dependency>     

```  
        
4. Update the src/main/resources/cd_client_conf.xml to point to your discovery service
5. Package the webapp by doing: `mvn package` 
6. Install the webapp into your application server/servlet container. Make sure it runs as the root webapp.       
7. Now you have a DXA instance ready for creation of flexible layouts           
    
    

