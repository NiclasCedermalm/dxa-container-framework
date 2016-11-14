DXA Container Framework
============================


The DXA Container Framework is framework that provides support for container components in SDL Web & DXA.
The container is a mixture of a region and a component. The container is basically a component that can be drag&dropped onto a page. 
This creates an inner XPM region where other components can be dropped. 
Examples of typical containers:

* Image container with overlays
* Column layout
* Tab
* Accordion
* Carousel

To make containers to work smoothly with Experience Manager and Update Preview the containers need to be ordinary SDL Web components.
The container items are as well ordinary components added directly on the page. What container the items belongs to determines of the order of 
the page and region specified in the component template.

Example of a page with containers:

- 2 Column Layout Container
- Teaser #1.1
- Teaser #1.2
- Carousel Container
- Teaser #2.1
- Teaser #2.2

The framework requires a CME extension to be fully functional in Experience Manager. DXA generates drop zones with hints on what
container they represents. As Experience Manager drops new components at end end of the page (for previously empty drop zones) something is
needed to rearrange the order of the page object so it becomes a well-formed container page.

Setup
------

The DXA Container Framework requires you install an extension in SDL Web. It make sure all drag&dropped components in XPM ends up in the correct place.
Either compile the C# code in the src/cms/container-gravity-extension directory or download the pre-compiled DDL here: 
[container-gravity-extension-v1.0.1.dll](https://github.com/NiclasCedermalm/modules4dxa-java/raw/master/dxa-container-framework/src/cms/container-gravity-extension/compiled/container-gravity-extension-v1.0.1.dll)

Upload the DLL to your SDL Web server and place it somewhere local on the server.
Then add the following in your %SDLWEB_HOME%\config\Tridion.ContentManager.config in <extensions> tag:

```
<add assemblyFileName="[PATH TO DLL]\container-gravity-extension.dll"/>
```

After that restart the services 'SDL Web Content Manager Service Host' and 'SDL Web Transport Distributor Service'