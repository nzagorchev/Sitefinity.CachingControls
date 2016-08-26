namespace SitefinityWebApp.Utilities.Models
{
    public class PageSiteNodeModel
    {
        public PageSiteNodeModel()
        {
        }

        public PageSiteNodeModel(PageNodeModel redirectPage1, ImageModel thumbnailImage1,
            string additionalText1, string redirectButton1)
            : base()
        {
            this.RedirectPage1 = redirectPage1;
            this.ThumbnailImage1 = thumbnailImage1;
            this.AdditionalText1 = additionalText1;
            this.RedirectButton1 = redirectButton1;
        }

        public ImageModel ThumbnailImage1 { get; set; }

        public PageNodeModel RedirectPage1 { get; set; }

        public string AdditionalText1 { get; set; }

        public string RedirectButton1 { get; set; }
    }
}