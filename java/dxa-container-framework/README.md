DXA Container Framework for Java
===================================


The DXA Container Framework is framework for DXA.Java that provides support for container components.
The container is a mixture of a region and a component. The container is basically a component that can be drag & dropped onto a page.
This creates an inner XPM region where other components can be dropped.
Examples of typical containers:

* Image container with overlays
* Column layout
* Tab
* Accordion
* Carousel

The framework comes with the following:
* Page model builder which restructure the page model with container models
* Generic models for containers and container items

For examples on a container implementation, please refer to the [Flex-Layout DXA Module](../dxa-module-flex-layout/README.md).

Setup
------

Compile the code and install it in your local Maven repository. Then add it as a dependency in your DXA webapp:

```
<dependencies>
    ...
    
    <dependency>
        <groupId>com.sdl.dxa.container</groupId>
        <artifactId>dxa-container-framework</artifactId>
        <version>1.1.0</version>
    </dependency>
    
</dependencies>
```