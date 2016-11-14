using Sdl.Web.Tridion.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DD4T.ContentModel;
using Sdl.Web.Common.Configuration;
using Sdl.Web.Common.Models;
using SDL.DXA.Container.Framework.Models;
using Sdl.Web.Common;
using Sdl.Web.Common.Logging;

namespace SDL.DXA.Container.Framework.Mapping
{
    /// <summary>
    /// Container Page Builder
    /// </summary>
    public class ContainerPageBuilder : IModelBuilder
    {
        public void BuildPageModel(ref PageModel pageModel, DD4T.ContentModel.IPage page, IEnumerable<DD4T.ContentModel.IPage> includes, Localization localization)
        {
            Log.Info("Container Framework page builder triggered...");

            var containers = new List<AbstractContainerModel>();

            // Get absolute order of the different container items
            //
            var regionAbsoluteOrder = new Dictionary<string,IList<int>>();
            var containerAbsoluteOrder = new List<int>();
            int index = 0;
            foreach ( var  cp in page.ComponentPresentations )
            {
                if (cp.ComponentTemplate.MetadataFields.ContainsKey("regionName"))
                {
                    string regionName = (string) cp.ComponentTemplate.MetadataFields["regionName"].Values[0];
                    Log.Info("Container item region name: " + regionName);
                    IList<int> orderList = null;
                    regionAbsoluteOrder.TryGetValue(regionName, out orderList);
                    if (orderList == null)
                    {
                        orderList = new List<int>();
                        regionAbsoluteOrder.Add(regionName, orderList);
                    }
                    orderList.Add(index);
                }
                else if (cp.ComponentTemplate.MetadataFields.ContainsKey("action"))
                {
                    string controllerAction = (string) cp.ComponentTemplate.MetadataFields["action"].Values[0];
                    if (controllerAction.Equals("Container"))
                    {
                        containerAbsoluteOrder.Add(index);
                    }
                }
                index++;
            }

            // Go through the entity list and extract all containers
            //
            index = 1;
            foreach (var region in pageModel.Regions )
            {
                // TODO: Remove container region from the top level? Otherwise XPM drop zones are generated for them as well?

                foreach (var entity in region.Entities)
                {
                    if (entity is AbstractContainerModel )
                    {
                        Log.Info("Found a container model...");
                        AbstractContainerModel container = (AbstractContainerModel) entity;

                        // Override region name using MVC route values
                        //
                        string containerRegionName = null;
                        container.MvcData.RouteValues.TryGetValue("containerRegion", out containerRegionName);
                        if (containerRegionName != null)
                        {
                            container.Name = containerRegionName;
                        }
                        // TODO: if no container region name -> should we skip this container then???
                        try
                        {
                            Log.Info("Creating a container region...");
                            container.Region = new ContainerRegionModel(container);
                        }
                        catch (DxaException e)
                        {
                            throw new DxaException("Could not create empty region for container: " + container.Name, e);
                        }
                        container.Index = index;
                        index++;
                        containers.Add(container);
                    }
                }
            }

            // Move all container items to the containers (to avoid the container items to be rendered outside the container)
            //
            foreach (var container in containers)
            {
                if (containerAbsoluteOrder.Count == 0 )
                {
                    break;
                }
                int containerIndex = containerAbsoluteOrder[0];
                containerAbsoluteOrder.RemoveAt(0);
                RegionModel regionWithContainerItems = null;
                pageModel.Regions.TryGetValue(container.Name, out regionWithContainerItems);
                if (regionWithContainerItems == null)
                {
                    // No container items found on the page -> continue
                    //
                    continue;
                }
                IList<int> orderList = null;
                regionAbsoluteOrder.TryGetValue(container.Name, out orderList);
                if (orderList != null && orderList.Count > 0 )
                {
                    index = orderList[0];
                    if (containerIndex + 1 == index)
                    {
                        int noOfItems = 0;
                        foreach (var entity in regionWithContainerItems.Entities)
                        {
                            container.Region.Entities.Add(entity);
                            noOfItems++;
                            orderList.RemoveAt(0);
                            if (orderList.Count == 0)
                            {
                                break;
                            }
                            int nextIndex = orderList[0];
                            if (nextIndex != index + 1)
                            {
                                break;
                            }
                            else
                            {
                                index = nextIndex;
                            }
                        }
                        for (int i = 0; i < noOfItems; i++)
                        {
                            regionWithContainerItems.Entities.RemoveAt(0);
                        }
                    }
                }
            }

        }

        public void BuildEntityModel(ref EntityModel entityModel, IComponentPresentation cp, Localization localization)
        {
        }

        public void BuildEntityModel(ref EntityModel entityModel, IComponent component, Type baseModelType, Localization localization)
        {
        }

    }
}