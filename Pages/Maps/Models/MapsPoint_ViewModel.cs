using EviCRM.Server.Core.EntityFramework;

namespace EviCRM.Server.Pages.Maps.Models
{
    public class MapsPoint_ViewModel
    {

        public float Lat { get; set; }

        public float Lng { get; set; }

        public float Alt { get; set; }

        public string Name { get; set; }

        public string user_login { get; set; }

        public DateTime addedDateTime { get; set; }

        public schema_alexandra_locations sal {get;set;}
       
        public schema_maps_points smp { get; set; }
        public MapPopupData mpd { get; set; } 
    }
}
