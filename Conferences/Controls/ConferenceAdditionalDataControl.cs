using SitefinityWebApp.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SitefinityWebApp.Conferences.Controls
{
    public class ConferenceAdditionalDataControl: Control, IDataItemContainer
    {
        public ConferenceAdditionalDataControl(ConferenceModel model)
        {
            this.Model = model;
        }

        // The Model the control uses
        public ConferenceModel Model { get; set; }

        public object DataItem
        {
            get
            {
                return this.Model;
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