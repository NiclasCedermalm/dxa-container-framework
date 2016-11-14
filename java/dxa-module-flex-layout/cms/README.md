Install CMS modules and content types
=========================================

There are a number of CMS packages that can be installed in SDL Web 8.
The following packages are provided:

* FlexLayout-Module_v1.0.1.zip - FlexLayout CMS module with needed schemas and templates
* FlexLayout-ContentTypes_v1.0.1.zip - XPM Content types for various container components 

Instructions
-------------

Either SDL Web Content Porter 8 can be used to import the above packages, or the provided PowerShell script can be used.

#### Instructions for using the PowerShell script:

Before running the import script the needed DLLs needs to be copied. See [Import/Export DLLs](./ImportExport/README.md) for further information.

The CMS import script is generic and is used for all packages. The syntax for calling the script:

```
.\cms-import.ps1  -cmsUrl [CMS url] -moduleZip [Module ZIP filename]
```

To setup CMS data the packages needs to be imported in the following order:

1. Setup modules: `.\cms-import.ps1 -moduleZip FlexLayout-Module-v1.0.1.zip`
2. Setup content types: `.\cms-import.ps1 -moduleZip FlexLayout-ContentTypes-v1.0.1.zip`

#### Setup the new content types manually

If you run into problems when importing the content types package, you can setup the content types manually by doing the following:

1. Go to the publication ‘110 DXA Site Type’ and go into the folder Building Blocks/Content/_Clonable Content
2. Create 3 components using the schema ‘Column Layout Container’ with the following values:
    - Name=2-Column Layout, Metadata/Number of columns=2
    - Name=3-Column Layout, Metadata/number of columns=3
    - Name=4-Column Layout, Metadata/number of columns=4
3. Create 2 components using the schema ‘Layout Container’ with the following values:
    - Name=Tab Container
    - Name=Accordion Container
4. Create one component using the schema ‘Carousel Container’ with the following value:
    - Name=Carousel Container
5. Create a new folder under Content called ‘FlexLayout’. This one is just used for storing all content types created through XPM. You can pick any folder here if you want.
6. Open up properties for the publication ‘110 DXA Site Type’. Go to the ‘Content Types’-tab.
7. Create new content types as specified below with content title set to ‘Auto generated’ and storage location set to the ‘FlexLayout’ folder:
    - Title=2-Column Layout, Component Template=Column Layout Container
    - Title=3-Column Layout, Component Template=Column Layout Container
    - Title=4-Column Layout, Component Template=Column Layout Container
    - Title=Tab, Component Template=Tab Container
    - Title=Accordion, Component Template=Accordion Container
    - Title=Carousel, Component Template=Carousel Container
8. See attached screenshot of how the content type definition could look like
9. Then go into ‘Settings’ (available in the left fly-in hamburger menu) & select ‘Inline Editing’
10. Go into ‘Content Type Mappings’ and select ‘110 DXA Site Type’
11. Go into each page template (Content, Content Type Without Navigation, Home Page, Section page etc) and select the new content types and press ‘Apply.
12. Now the content types should be available through XPM

    



