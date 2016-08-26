using SitefinityWebApp.Utilities.Models;
using System.Linq;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;

namespace SitefinityWebApp.Utilities
{
    public static class ModelUtilities
    {
        public static ImageModel GetRelatedImageModel(object item, string fieldName)
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

        public static PageNodeModel GetRelatedPageModel(object item, string fieldName)
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
    }
}