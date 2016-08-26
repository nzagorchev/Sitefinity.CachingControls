using SitefinityWebApp.Utilities.Models;
using System.Web.UI;

namespace SitefinityWebApp.Navigation.Controls
{
    public class PageNodeAdditionalDataControl : Control, IDataItemContainer
    {
        public PageNodeAdditionalDataControl(PageSiteNodeModel pageNodeModel)
        {
            this.PageSiteNodeModel = pageNodeModel;
        }

        public PageSiteNodeModel PageSiteNodeModel { get; set; }

        public object DataItem
        {
            get
            {
                return this.PageSiteNodeModel;
            }
        }

        public int DataItemIndex
        {
            get
            {
                return 0;
            }
        }

        public int DisplayIndex
        {
            get
            {
                return 0;
            }
        }
    }
}