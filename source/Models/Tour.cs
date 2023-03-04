using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace source.Models
{
    public class Tour
    {
        [Key]
        public string id { set; get; } = default!;

        public string title { set; get; } = default!;

        public double price { set; get; }

        public string openTime { set; get; } = default!;

        public string location { set; get; } = default!;

        public string time { set; get; } = default!;

        public string mainImg { set; get; } = default!;


        public DateTime? createdAt { set; get; }

        [Column(TypeName = "ntext")]
        public string? desc { set; get; }


        [Column(TypeName = "ntext")]
        public string? info { set; get; }


        [Column(TypeName = "ntext")]
        public string? schedule { set; get; }

        [Column(TypeName = "ntext")]
        public string? departureSchedule { set; get; }

        public CategoryTour categoryTour { set; get; } = default!;

        public List<TourImage> TourImages { set; get; } = default!;

        public List<OrderTour> OrderTours {set;get;} = default!;

    }




}