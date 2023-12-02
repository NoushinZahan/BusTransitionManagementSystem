using BTMS.BlazorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTMS.BlazorApp.Shared.InputModels
{
    [ComplexType]
    public class SeatFareInputModel
    {
        [Required, DataType(DataType.Currency)]
        public decimal? SeatFare { get; set; }
       
       
        [Required, EnumDataType(typeof(FareType))]
        public FareType FareType { get; set; }
        public bool IsActive { get; set; }
    }
}
