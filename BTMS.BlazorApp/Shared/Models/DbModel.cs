using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTMS.BlazorApp.Shared.Models
{
    public enum BusType { Ac = 1, NonAc, Sleeper, DoubleDecker, Luxury }
    public enum SeatStatus { Available = 1, Booked, Selected }
    public enum PaymentStatus { Pending = 1, Completed, Failed, Cancelled, Refunded }
    public enum FareType { Regular=1, PickSeason, Special }
    public class Bus
    {
        public int BusId { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = default!;
        [Required, StringLength(50)]
        public string Number { get; set; } = default!;
        [Required, EnumDataType(typeof(BusType))]
        public BusType BusType { get; set; }
        [Required]
        public  int? Capacity { get; set; }
        [Required, StringLength(120)]
        public string Picture { get; set; }=default!;
        //navigation
        public virtual ICollection<BusSchedule> Schedules { get; set;}= new List<BusSchedule>();

        
    }
    public class BusRoute
    {
        public int BusRouteId { get; set; }


        [Required, StringLength(100)]
        public string From { get; set; } = default!;//From

        [Required, StringLength(80)]
        public string To { get; set; } = default!;//To
        [Required]
        public int? ApproximateDistance {get;set;}
        [Required]
        public int? ApproximateJourneyHour { get; set; }

        //
        public virtual ICollection<Fare> Fares { get; set; } = new List<Fare>();
        public virtual ICollection<BusSchedule> Schedules { get; set; }= new List<BusSchedule>();
    }
    public class Fare
    {
        public int FareId { get; set; }
        [Required, ForeignKey("BusRoute")]        
        public int BusRouteId { get; set; }
        [Required, Column(TypeName ="money")]
        public decimal SeatFare { get; set; }
        [Required, EnumDataType(typeof(BusType))]
        public BusType BusType { get;set; }
        [Required, EnumDataType(typeof(FareType))]
        public FareType FareType { get; set; }
        public bool IsActive { get; set; }
        //navigation
        public virtual BusRoute? BusRoute { get; set; }
        
       

    }
    public class BusSchedule
    {
        public int BusScheduleId { get; set; }
        [Required, Column(TypeName ="date")]
        public DateTime? Date { get; set; }
        [Required, Column(TypeName ="time")]
        public TimeSpan? Time { get; set; }
        [Required, ForeignKey("Bus")]
        public int BusRouteId { get; set; }
        [Required, ForeignKey("BusRoute")]
        public int BusId { get; set; }
        //navigation
        public virtual Bus? Bus { get; set; }
        public virtual BusRoute? BusRoute { get; set; }
    }

    public class BusDbContext : DbContext
    {
        public BusDbContext(DbContextOptions<BusDbContext> options) : base(options) { }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusRoute> BusRoutes { get; set; }
        public DbSet<Fare> Fares { get; set; }
        public DbSet<BusSchedule> Schedules { get; set; }
    }

}
