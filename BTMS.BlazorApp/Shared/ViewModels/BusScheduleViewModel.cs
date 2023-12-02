using BTMS.BlazorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTMS.BlazorApp.Shared.ViewModels
{
    public class BusScheduleViewModel
    {
        public int BusScheduleId { get; set; }
        
        public DateTime? Date { get; set; }
        [Required, Column(TypeName = "time")]
        public TimeSpan? Time { get; set; }

        public string RouteName { get; set; } = default!;
        public BusType BusType { get; set; }
        public string BusName { get; set; } = default!;

    }
}
