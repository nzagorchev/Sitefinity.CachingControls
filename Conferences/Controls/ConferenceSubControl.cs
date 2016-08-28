using SitefinityWebApp.Utilities.Cache;
using SitefinityWebApp.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Linq;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Libraries.Model;

namespace SitefinityWebApp.Conferences.Controls
{
    public class ConferenceSubControl : SimpleView
    {
        #region Properties
        /// <summary>
        /// The PageNodeId the control will bind to
        /// </summary>
        public string ConferenceId { get; set; }

        /// <summary>
        /// The ProviderName of the dynamic item provider
        /// </summary>
        public string ProviderName { get; set; }

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
                    return ConferenceSubControl.layoutTemplatePath;
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

            if (!string.IsNullOrEmpty(this.ConferenceId))
            {
                ConferenceModel pageNodeModel = this.GetConferenceModelById(new Guid(this.ConferenceId), this.ProviderName);
                // Construct the control
                ConferenceAdditionalDataControl item = new ConferenceAdditionalDataControl(pageNodeModel);

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

        public ConferenceModel GetConferenceModelById(Guid id, string provider)
        {
            if (id == Guid.Empty)
            {
                return null;
            }
            // Construct the cache key
            string cacheKey = CacheUtilities.BuildCacheKey(ConferenceSubControl.cacheKey, id.ToString());
            var model = (ConferenceModel)CacheUtilities.CacheManagerGlobal[cacheKey];
            if (model == null)
            {
                lock (this.nodeDataLock)
                {
                    model = (ConferenceModel)CacheUtilities.CacheManagerGlobal[cacheKey];
                    if (model == null)
                    {
                        // Get the conference item
                        DynamicModuleManager manager = DynamicModuleManager.GetManager(provider);
                        Type latestHotTopicsType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Conferences.Conference");

                        DynamicContent conference = manager.GetDataItem(latestHotTopicsType, id);
                        // Build the Model
                        model = this.PopulateModel(conference);

                        // Add the model in the cache
                        CacheUtilities.CacheManagerGlobal.Add(
                                cacheKey,
                                model,
                                CacheItemPriority.Normal,
                                null,
                            // Add cache dependency for automatic invalidation
                                new DataItemCacheDependency(typeof(DynamicContent), id),
                            // Configure sliding time
                                new SlidingTime(TimeSpan.FromMinutes(20)));
                    }
                }
            }

            return model;
        }

        protected virtual ConferenceModel PopulateModel(DynamicContent conference)
        {
            var images = conference.GetRelatedItems<Image>("Images");
            var imagesModels = new List<ImageModel>();

            foreach (var img in images)
            {
                var model = new ImageModel();            
                model.Title = img.Title;
                model.AlternativeText = img.AlternativeText;
                model.ThumbnailUrl = img.ResolveThumbnailUrl();
                imagesModels.Add(model);
            }

            var sessions = conference.GetRelatedItems<DynamicContent>("Sessions");
            var sessionsModels = new List<SessionModel>();

            foreach (var session in sessions)
            {
                var model = new SessionModel();
                model.Title = session.GetValue<string>("Title");
                model.Duration = session.GetValue<int>("Duration");
                sessionsModels.Add(model);
            }

            var conferenceModel = new ConferenceModel();
            conferenceModel.Images = imagesModels;
            conferenceModel.Sessions = sessionsModels;

            return conferenceModel;
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/Conferences/Controls/ConferenceSubControl.ascx";

        protected static readonly string cacheKey = "|cad|id|";

        private object nodeDataLock = new object();
        #endregion
    }
}