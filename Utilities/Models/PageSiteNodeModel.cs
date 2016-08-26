namespace SitefinityWebApp.Utilities.Models
{
    public class PageSiteNodeModel
    {
        public PageSiteNodeModel()
        {
        }

        public PageSiteNodeModel(PageNodeModel relatedPage, ImageModel relatedImage,
            string additionalInfo)
            : base()
        {
            this.RelatedPage = relatedPage;
            this.RelatedImage = relatedImage;
            this.AdditionalInfo = additionalInfo;
        }

        public ImageModel RelatedImage { get; set; }

        public PageNodeModel RelatedPage { get; set; }

        public string AdditionalInfo { get; set; }
    }
}