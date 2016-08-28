using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Utilities.Models
{
    public class ConferenceModel
    {
        public List<ImageModel> Images { get; set; }

        public List<SessionModel> Sessions { get; set; }
    }
}