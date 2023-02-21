using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace source.Models
{

    public class HotelImg
    {
        [Key]
        public string id { set; get; } = default!;
        public string src { get; set; } = default!;
        public string alt { get; set; } = default!;
        public Hotel Hotel { set; get; } = default!;

    }
}