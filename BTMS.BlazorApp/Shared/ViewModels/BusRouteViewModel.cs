using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTMS.BlazorApp.Shared.ViewModels
{
    public class BusRouteViewModel
    {
        public int BusRouteId { get; set; }


        [Required, StringLength(100)]
        public string RouteName { get; set; } = default!;
        [Required]
        public int? ApproximateDistance { get; set; }
        [Required]
        public int? ApproximateJourneyHour { get; set; }
    }
}
