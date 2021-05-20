using System;

namespace ThirdPartyEventEditor.Models
{
    public class ThirdPartyEvent
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public string PosterImage { get; set; }
    }
}