using SitefinityWebApp.Utilities.Cache;
using SitefinityWebApp.Utilities.Models;
using System;
using System.Web.UI;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.Navigation.Controls
{
    public class NavigationSubControl : SimpleView
    {
        #region Properties
        /// <summary>
        /// The PageNodeId the control will bind to
        /// </summary>
        public string PageNodeId { get; set; }
        
        /// <summary>
        /// Obsolete. Use LayoutTemplatePath instead.
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the layout template's relative or virtual path.
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                if (string.IsNullOrEmpty(base.LayoutTemplatePath))
                    return NavigationSubControl.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }
        #endregion

        #region Control References
        #endregion

        #region Methods
        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            if (!string.IsNullOrEmpty(this.PageNodeId))
            {
                PageSiteNodeModel pageNodeModel = this.GetPageSiteNodeModelByKey(new Guid(this.PageNodeId));
                PageNodeAdditionalDataControl item = new PageNodeAdditionalDataControl(pageNodeModel);

                // Bind the item in the container
                this.LayoutTemplate.InstantiateIn(item);
                this.Controls.Add(item);
                item.DataBind();
            }
        }

        protected override void InitializeControls(GenericContainer container)
        {
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.Write(string.Empty);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write(string.Empty);
        }

        public PageSiteNodeModel GetPageSiteNodeModelByKey(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            string cacheKey = CacheUtilities.BuildCacheKey(NavigationSubControl.cacheKey, id.ToString());
            var model = (PageSiteNodeModel)CacheUtilities.CacheManagerGlobal[cacheKey];
            if (model == null)
            {
                lock (this.nodeDataLock)
                {
                    model = (PageSiteNodeModel)CacheUtilities.CacheManagerGlobal[cacheKey];
                    if (model == null)
                    {
                        PageManager manager = PageManager.GetManager();
                        var node = manager.GetPageNode(id);

                        var redirectPage1 = this.GetRelatedPageModel(node, "RedirectPage1");

                        var thumbnailImage1 = this.GetRelatedImageModel(node, "ThumbnailImage1");

                        string additionalText1 = node.GetValue<Lstring>("AdditionalText1");

                        string redirectButton1 = node.GetValue<Lstring>("RedirectButton1");

                        model = new PageSiteNodeModel(redirectPage1,thumbnailImage1,
                            additionalText1,redirectButton1);

                        CacheUtilities.CacheManagerGlobal.Add(
                                cacheKey,
                                model,
                                CacheItemPriority.Normal,
                                null,
                                new DataItemCacheDependency(typeof(PageNode), id),
                                new SlidingTime(TimeSpan.FromMinutes(20)));
                    }
                }
            }

            return model;
        }

        protected virtual ImageModel GetRelatedImageModel(object item, string fieldName)
        {
            return ModelUtilities.GetRelatedImageModel(item, fieldName);
        }

        protected virtual PageNodeModel GetRelatedPageModel(object item, string fieldName)
        {
            return ModelUtilities.GetRelatedPageModel(item, fieldName);
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/Navigation/Controls/NavigationSubControl.ascx";

        protected static readonly string cacheKey = "|pnad|id|";

        private object nodeDataLock = new object();
        #endregion
    }
}