DXA Container Framework
============================


The DXA Container Framework is framework that provides support for container components in SDL Web & DXA.
The container is a mixture of a region and a component. The container is basically a component that can be drag & dropped onto a page. This creates an inner XPM region where other components can be dropped.
Examples of typical containers:

* Image container with overlays
* Column layouts (e.g. 3-column layout)
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

Comparing traditional regions with containers:
* Region are a course-grained area on a page, such as main area, side area, header, footer etc. They are managed by the page template/view.
* Containers are more fine-grained areas on a page which can be managed by editors such as adding new layouts on the page, add components to the layouts and move components between layouts.

The framework requires a CME extension to be fully functional in SDL Web Experience Manager. DXA generates drop zones with hints on what container they represents. As Experience Manager drops new components at end end of the page (for previously empty drop zones) something is needed to rearrange the order of the page object so it becomes a well-formed container page.

The container framework comes with the following:

* CME extension
* CMS import scripts for the example module 'FlexLayout'
* Modules for DXA.Java:
  - dxa-container-framework - The container framework for DXA.Java
  - dxa-module-flex-layout - DXA.Java module with example container implementations using vanilla DXA views
* Modules for DXA.NET:
  - SDL.DXA.Container.Framework - The container framework for DXA.Net
  - SDL.DXA.Modules.FlexLayout - DXA.NET module with example container implementations using vanilla DXA views

CMS Setup
-----------

The DXA Container Framework requires you install an extension in SDL Web. It make sure all drag & dropped components in XPM ends up in the correct place.
Either compile the C# code in the 'cms/container-gravity-extension' directory or download the pre-compiled DDL here:
[container-gravity-extension-v1.1.0.dll](https://github.com/NiclasCedermalm/dxa-container-framework/raw/master/cms/container-gravity-extension/compiled/container-gravity-extension-v1.1.0.dll)

Upload the DLL to your SDL Web server and place it somewhere local on the server.
Then add the following in your %SDLWEB_HOME%\config\Tridion.ContentManager.config in <extensions> tag:

```
<add assemblyFileName="[PATH TO DLL]\container-gravity-extension-v1.1.0.dll"/>
```

After that restart the services 'SDL Web Content Manager Service Host' and 'SDL Web Transport Distributor Service'

DXA.Java Setup
---------------

To setup the FlexLayout module for DXA.Java, please refer to [Flex Layout DXA Module](./java/dxa-module-flex-layout/README.md).


DXA.NET Setup
---------------

To setup the FlexLayout module for DXA.NET, please refer to [DXA Container Framework for .NET](./dotnet/README.md).

Branching model
----------------

We intend to follow Gitflow (http://nvie.com/posts/a-successful-git-branching-model/) with the following main branches:

 - master - Stable
 - develop - Unstable
 - release/x.y - Release version x.y

Please submit your pull requests on develop. In the near future we intend to push our changes to develop and master from our internal repositories, so you can follow our development process.


License
---------

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and limitations under the License.
