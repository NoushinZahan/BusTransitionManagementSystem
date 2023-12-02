using BTMS.BlazorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTMS.BlazorApp.Shared.Validators;

namespace BTMS.BlazorApp.Shared.InputModels
{
    public class FareInputModel
    {
        [Required, NotZero(ErrorMessage ="Value cannot be 0")]
        public int BusRouteId { get; set; }
       
        [Required, EnumDataType(typeof(BusType))]
        public BusType BusType { get; set; }
        [ValidateComplexType]
        public List<SeatFareInputModel> SeatFares { get; set; }=new List<SeatFareInputModel>();
    }
}
