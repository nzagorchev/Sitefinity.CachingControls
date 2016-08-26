using SitefinityWebApp.Navigation.Models;
using System;
using System.Linq;
using System.Web.UI;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.Navigation.Controls
{
    public class NavigationSubControl : SimpleView
    {
        #region Properties
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

            string cacheKey = this.BuildCacheKey(id.ToString());
            var model = (PageSiteNodeModel)this.CacheManager[cacheKey];
            if (model == null)
            {
                lock (this.nodeDataLock)
                {
                    model = (PageSiteNodeModel)this.CacheManager[cacheKey];
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

                        this.CacheManager.Add(
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
            var model = new ImageModel();
            var image = item.GetRelatedItems<Image>(fieldName).FirstOrDefault();
            if (image != null)
            {
                model.Title = image.Title;
                model.AlternativeText = image.AlternativeText;
                model.ThumbnailUrl = image.ResolveThumbnailUrl();
            }

            return model;
        }

        protected virtual PageNodeModel GetRelatedPageModel(object item, string fieldName)
        {
            var model = new PageNodeModel();
            var relatedPage = item.GetRelatedItems<PageNode>(fieldName).FirstOrDefault();
            if (relatedPage != null)
            {
                model.Title = relatedPage.Title;
                model.OpenNewWindow = relatedPage.OpenNewWindow;
                model.Url = Telerik.Sitefinity.RelatedData.RelatedDataExtensions.GetDefaultUrl(relatedPage);
            }

            return model;
        }

        private string BuildCacheKey(string key)
        {
            return string.Concat(NavigationSubControl.cacheKey, key.ToUpperInvariant());
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/Navigation/Controls/NavigationSubControl.ascx";

        protected static readonly string cacheKey = "|pnad|id|";

        private object nodeDataLock = new object();

        private ICacheManager CacheManager
        {
            get
            {
                return SystemManager.GetCacheManager(CacheManagerInstance.Global);
            }
        }
        #endregion
    }
}