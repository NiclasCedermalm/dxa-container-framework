using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tridion.ContentManager;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.ContentManagement.Fields;

namespace SDL.DXA.Extensions.Container
{
    /// <summary>
    /// Container representation
    /// </summary>
    public class Container
    {
        protected Page page;
        protected Component containerComponent;
        protected ComponentTemplate containerTemplate;
        private string containerName;
        private int index;
        private int pageIndex;
    
        /// <summary>
        /// Get all container components on a page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        static public IList<Container> GetContainers(Page page)
        {
            int containerCount = 0;
            int pageIndex = 0;
            IList<Container> containers = new List<Container>();
            Container currentContainer= null;

            foreach (ComponentPresentation cp in page.ComponentPresentations )
            {             
                if ( IsContainer(cp) )
                {                  
                    currentContainer = new Container(page, cp.Component, cp.ComponentTemplate);
                    currentContainer.Index = ++containerCount;
                    currentContainer.pageIndex = pageIndex;
                    containers.Add(currentContainer);
                }
                else
                {
                    if (currentContainer != null && currentContainer.Owns(cp)) 
                    {
                        ComponentPresentationInfo cpInfo = currentContainer.AddToComponentPresentationList(cp, pageIndex);
                    }
                }
                pageIndex++; 
            }
       
            return containers;
        }

        /// <summary>
        /// Check if provided component presentation is a container component or not.
        /// </summary>
        /// <param name="componentPresentation"></param>
        /// <returns>bool</returns>
        static public bool IsContainer(ComponentPresentation componentPresentation)
        {
            return GetContainerName(componentPresentation.ComponentTemplate) != null;
        }

        /// <summary>
        /// Extract container index from component presentation.
        /// </summary>
        /// <param name="componentPresentation"></param>
        /// <returns></returns>
        static public int ExtractContainerIndex(ComponentPresentationInfo componentPresentation)
        {
            return ExtractContainerIndex(componentPresentation.ComponentPresentation.ComponentTemplate.Id);
        }

        /// <summary>
        /// Extract container index from template TCM-URI (where the index is piggybacked as ID version)
        /// </summary>
        /// <param name="templateUri"></param>
        /// <returns></returns>
        static public int ExtractContainerIndex(TcmUri templateUri)
        {
            String itemId = templateUri.ItemId.ToString();
            int containerIndex = templateUri.Version;
            
            if ( !templateUri.IsVersionless )
            {
                return containerIndex;
            }
            return -1;
        }

        /// <summary>
        /// Remove container index from Template TCM-URI
        /// </summary>
        /// <param name="templateUri"></param>
        /// <returns></returns>
        static public TcmUri RemoveContainerIndex(TcmUri templateUri)
        {
            return templateUri.GetVersionlessUri();
        }

        /// <summary>
        /// Get container name
        /// </summary>
        /// <param name="containerTemplate"></param>
        /// <returns></returns>
        static public string GetContainerName(ComponentTemplate containerTemplate)
        {
            if ( containerTemplate != null && containerTemplate.Metadata != null && containerTemplate.MetadataSchema != null )
            {
                var metadata = new ItemFields(containerTemplate.Metadata, containerTemplate.MetadataSchema);
                if (metadata.Contains("routeValues"))
                {
                    TextField routeValues = (TextField) metadata["routeValues"];
                    if (routeValues.Values.Count() > 0 && routeValues.Value.Contains("containerRegion"))
                    {
                        return routeValues.Value.Replace("containerRegion:", "");
                    }
                }
            }        
            return null;
        }

        /// <summary>
        /// Protecte constructor
        /// </summary>
        /// <param name="page"></param>
        /// <param name="containerComponent"></param>
        /// <param name="containerTemplate"></param>
        protected Container(Page page, Component containerComponent, ComponentTemplate containerTemplate)
        {
            this.page = page;
            this.containerComponent = containerComponent;
            this.containerTemplate = containerTemplate;
            this.ComponentPresentations = new List<ComponentPresentationInfo>();
            this.containerName = GetContainerName(containerTemplate);
        }

        public string Id
        {
            get { return this.containerComponent.Id; }
        }

        public string TemplateId
        {
            get { return this.containerTemplate.Id; }
        }

        public string Title 
        {
            get { return this.containerComponent.Title; }
        }

        public string Name
        {
            get { return this.containerName; }
        }
 
        public int Index
        {
            get { return this.index; }
            internal set { this.index = value; }
        }

        public int PageIndex
        {
            get { return this.pageIndex; }
            set { this.pageIndex = value; }
        }

        /// <summary>
        /// Check if a component presentation is own by this container
        /// </summary>
        /// <param name="componentPresentation"></param>
        /// <returns></returns>
        public bool Owns(ComponentPresentation componentPresentation)
        {
            if (componentPresentation.ComponentTemplate.Metadata != null && componentPresentation.ComponentTemplate.MetadataSchema != null)
            {
                var metadata = new ItemFields(componentPresentation.ComponentTemplate.Metadata, componentPresentation.ComponentTemplate.MetadataSchema);
                if (metadata.Contains("regionName"))
                {
                    TextField regionName = (TextField)metadata["regionName"];
                    if (regionName.Values.Count() > 0 )
                    {
                        return regionName.Value.Equals(this.containerName);
                    }
                }
            }
            return false;
        }

        public bool Matches(ContainerMetadata containerMetadata)
        {
            return containerMetadata.Id == this.containerComponent.Id &&
                   containerMetadata.TemplateId == this.containerTemplate.Id;
        }

        public IList<ComponentPresentationInfo> ComponentPresentations { get; private set; }

        public ComponentPresentationInfo GetComponentPresentation(int pageIndex)
        {
            foreach ( var cp in this.ComponentPresentations )
            {
                if ( cp.PageIndex == pageIndex )
                {
                    return cp;
                }
            }
            return null;
        }

        private ComponentPresentationInfo AddToComponentPresentationList(ComponentPresentation componentPresentation, int pageIndex)
        {
            ComponentPresentationInfo cpInfo = new ComponentPresentationInfo(componentPresentation, this, pageIndex);
            this.ComponentPresentations.Add(cpInfo);
            return cpInfo;
        }

        /// <summary>
        /// Add component presentation the container
        /// </summary>
        /// <param name="componentPresentation"></param>
        public void Add(ComponentPresentation componentPresentation)
        {
            this.AddToComponentPresentationList(componentPresentation, pageIndex + this.ComponentPresentations.Count() + 1);
            page.ComponentPresentations.Insert(pageIndex + this.ComponentPresentations.Count(), componentPresentation);
            
        }

    }


    /// <summary>
    /// Component Presentation Information
    /// </summary>
    public class ComponentPresentationInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="componentPresentation"></param>
        /// <param name="owner"></param>
        /// <param name="pageIndex"></param>
        public ComponentPresentationInfo(ComponentPresentation componentPresentation, Container owner, int pageIndex)
        {
            ComponentPresentation = componentPresentation;
            ContainerIndex = Container.ExtractContainerIndex(componentPresentation.ComponentTemplate.Id);
            Owner = owner;
            PageIndex = pageIndex;
        }

        public ComponentPresentation ComponentPresentation { get; private set; }
   
        public int ContainerIndex { get; private set; }
        public int PageIndex { get; private set; }
        public Container Owner { get; private set; }
    }

    /// <summary>
    /// Container metadata stored on page appdata
    /// </summary>
    public class ContainerMetadata
    {
        public string Id
        {
            get; internal set;
        }

        public string TemplateId
        {
            get; internal set;
        }

        public int Count
        {
            get; internal set;
        }

        public static IList<ContainerMetadata> Load(Page page)
        {
            var metadataList = new List<ContainerMetadata>();

            var appData = page.LoadApplicationData("ContainerMetadata");
            if (appData != null)
            {

                // Metadata format: [ID]#[Template ID]=[Count],...
                //
                var metadataStr = Encoding.Default.GetString(appData.Data);
                Log.Info("Read metadata: " + metadataStr);

                string[] tokens = metadataStr.Split( new string[] { "#", "=", ";" }, StringSplitOptions.RemoveEmptyEntries);
                int index = 0;
                while (index < tokens.Length)
                {
                    ContainerMetadata metadata = new ContainerMetadata();
                    metadata.Id = tokens[index++];
                    metadata.TemplateId = tokens[index++];
                    metadata.Count = Int32.Parse(tokens[index++]);
                    metadataList.Add(metadata);
                }
            }
            return metadataList;
        }

        public static void Save(Page page, IList<Container> containers)
        {
            var metadata = new StringBuilder();
            foreach ( var container in containers )
            {
                metadata.Append(container.Id);
                metadata.Append("#");
                metadata.Append(container.TemplateId);
                metadata.Append("=");
                metadata.Append(container.ComponentPresentations.Count);
                metadata.Append(";");
            }
            Log.Info("Saving metadata: " + metadata);
            ApplicationData appData = new ApplicationData("ContainerMetadata");
            appData.Data = Encoding.Default.GetBytes(metadata.ToString());
            page.SaveApplicationData(appData);
        }

    }
}
