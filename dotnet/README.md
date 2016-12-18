DXA Container Framework for .NET
==================================

This repository contains of the following .NET modules:
* SDL.DXA.Container.Framework - The framework itself to build container and container items
* SDL.DXA.Modules.FlexLayout - A set of example container implementations based on the DXA white label design. Can easily be modified for other designs.


Setup
-------------
1. If you do not have a DXA.NET setup (for SDL Web 8) you can easily do this by following the instructions given here: [Installing the web application (.NET)](http://docs.sdl.com/LiveContent/content/en-US/SDL%20DXA-v6/GUID-8633F5AE-8472-4D53-AD38-A7A33DD1F5A3)
2. Clone this repository: `git clone https://github.com/NiclasCedermalm/dxa-container-framework`
3. Either open up the solution 'dotnet/SDL.DXA.Container.Framework.sln' or add the VS projects under dotnet to your Visual Studio solution
4. Set the environment variable %DXA_SITE_DIR% to point to your DXA Site path (in visual studio or in your IIS instance)
5. Restart Visual studio and rebuild the solution. Verify so Container and FlexLayout Areas and DLLs are copied to your site folder
6. Add the container page builder to the Web.config of your DXA webapp:
```
<modelBuilderPipeline>
    ...
	 <add type="SDL.DXA.Container.Framework.Mapping.ContainerPageBuilder, SDL.DXA.Container.Framework" />
</modelBuilderPipeline>
```
7. Now is your DXA instance ready for creation of flexible layouts
