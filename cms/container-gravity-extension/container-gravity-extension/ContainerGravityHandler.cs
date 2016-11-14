using System.Collections.Generic;
using Tridion.ContentManager;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.Extensibility;
using Tridion.ContentManager.Extensibility.Events;

namespace SDL.DXA.Extensions.Container
{
    /// <summary>
    /// Container gravity handler. Finds components on the page that is put on a invalid location by XPM and
    /// correct that. The container framework is based on that components comes in a certain order in the page, i.e.
    /// container component expect that its items comes as next-coming components.
    /// The DXA container framework piggybacks an additional identifier (a container index) in the template ID in the XPM region.
    /// This container index is an aid for this extension to find the correct container for components drag&dropped through XPM.
    /// </summary>
    [TcmExtension("DXA-ContainerGravityHandler")]
    public class ContainerGravityHandler : TcmExtension
    {
        /// <summary>
        /// Constructor
        /// </summary>                
        public ContainerGravityHandler()
        {
            EventSystem.Subscribe<Page, SaveEventArgs>(OnPageSave, EventPhases.Initiated);
        }

        /// <summary>
        /// On Page Save.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="args"></param>
        /// <param name="phase"></param>
        public static void OnPageSave(Page page, SaveEventArgs args, EventPhases phase)
        {      
            if ( IsContainerPage(page) )
            {
                IList<Container> containers = Container.GetContainers(page);
                if (containers.Count > 0)
                {
                    var foundComponentInWrongContainer = ProcessComponentPresentationsInWrongContainer(page, containers);
                    if ( !foundComponentInWrongContainer )
                    {
                        var containerMetadata = ContainerMetadata.Load(page);
                        if ( containerMetadata != null && containerMetadata.Count != containers.Count )
                        {
                            ProcessContainersOnWrongLocation(containers, page, containerMetadata);
                        }                       
                    }
                    ProcessComponentTemplates(page.ComponentPresentations);
                    ContainerMetadata.Save(page, containers);
                }
            }
        }

        /// <summary>
        /// Check if page is container page or not.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        protected static bool IsContainerPage(Page page)
        {
            foreach (ComponentPresentation cp in page.ComponentPresentations)
            {
                if (Container.IsContainer(cp))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Process component presentations that are in the wrong container
        /// </summary>
        /// <param name="page"></param>
        /// <param name="containers"></param>
        /// 
        static public bool ProcessComponentPresentationsInWrongContainer(Page page, IList<Container> containers)
        {
            int pageIndex = 0;
            foreach ( var componentPresentation in page.ComponentPresentations )
            {
                int containerIndex = Container.ExtractContainerIndex(componentPresentation.ComponentTemplate.Id);
                if ( containerIndex != -1 )
                {
                    var selectedContainer = containers[containerIndex - 1];
                    var existingCp = selectedContainer.GetComponentPresentation(pageIndex);
                    if ( existingCp != null && existingCp.ComponentPresentation == componentPresentation )
                    {
                        // Component presentation already in correct container. Skipping...
                        //
                        break;
                    }

                    page.ComponentPresentations.RemoveAt(pageIndex);
                
                    // Compensate start page index of containers below current component presentation
                    //
                    foreach ( var container in containers )
                    {
                        if ( container.PageIndex > pageIndex )
                        {
                            container.PageIndex--;
                        }
                    }

                    selectedContainer.Add(componentPresentation); 
                    return true;
                }

                pageIndex++;
            }
            return false;
        }

        /// <summary>
        /// Process containers on wrong location in the page
        /// </summary>
        /// <param name="newContainers"></param>
        /// <param name="page"></param>
        /// <param name="oldContainers"></param>
        static public void ProcessContainersOnWrongLocation(IList<Container> newContainers, Page page, IList<ContainerMetadata> containerMetadataList)
        {
 
            for ( int i=0; i < newContainers.Count; i++ )
            {
                var container = newContainers[i];
                if ( container.ComponentPresentations.Count == 0 && i+1 < newContainers.Count )
                {
                    var nextContainer = newContainers[i + 1];
                    if ( container.PageIndex == nextContainer.PageIndex-1 ) // Found two containers lying beside eachother on the page
                    {
                        // Verify so the container items is not in the wrong container.
                        // If the container has been drag&dropped through XPM it will be attached to the container directly. So this needs to be corrected in that case.
                        //
                        ContainerMetadata containerMetadata = null;
                        foreach ( var metadata in containerMetadataList )
                        {
                            if ( container.Matches(metadata) )
                            {
                                containerMetadata = metadata;
                                break;
                            }
                        }
                        
                        if ( containerMetadata != null && containerMetadata.Count > 0 )
                        {
                            int newPageIndex = container.PageIndex + containerMetadata.Count + 1;
                            var containerCp = page.ComponentPresentations[nextContainer.PageIndex];
                            page.ComponentPresentations.RemoveAt(nextContainer.PageIndex);
                            page.ComponentPresentations.Insert(newPageIndex, containerCp);
                            return;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Process component templates and clean up the component template ID. An additional marker is piggybacked in the ID to indicate the 
        /// container index, which needs to be removed before saving the page.
        /// </summary>
        /// <param name="componentPresentations"></param>
        static public void ProcessComponentTemplates(IList<ComponentPresentation> componentPresentations)
        {
            foreach ( ComponentPresentation cp in componentPresentations )
            {
                // Check if component template really exist -> if not extract the region index from the template ID
                //                       
                if (Container.ExtractContainerIndex(cp.ComponentTemplate.Id) != -1)
                {
                    TcmUri realTemplateUri = Container.RemoveContainerIndex(cp.ComponentTemplate.Id);
                    cp.ComponentTemplate = new ComponentTemplate(realTemplateUri, cp.Session); 
                }
            }
        }
        
    }

}
