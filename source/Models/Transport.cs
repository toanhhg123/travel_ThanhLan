using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace source.Models
{
    public class Transport
    {
        [Key]
        public string id { set; get; } = Guid.NewGuid().ToString();

        public string title { set; get; } = "";

        public double price { set; get; } = 0;

        public string openTime { set; get; } = "";

        public string location { set; get; } = "";

        public string time { set; get; } = "";

        public string mainImg { set; get; } = "";


        public DateTime? createdAt { set; get; }

        [Column(TypeName = "ntext")]
        public string? desc { set; get; }


        [Column(TypeName = "ntext")]
        public string? info { set; get; }


        [Column(TypeName = "ntext")]
        public string? schedule { set; get; }

        [Column(TypeName = "ntext")]
        public string? departureSchedule { set; get; }


        public List<TransportImage> TransportImages { set; get; } = default!;


    }




}