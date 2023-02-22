using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace source.Models
{
    public class OrderTour
    {
        [Key]
        public string id { set; get; } = Guid.NewGuid().ToString();

        public string name  {set;get;} = "";

        [Required(ErrorMessage = "Vui long nhap day du thong tin")]
        public string phone  {set;get;} = "";
        public string email  {set;get;} = "";
        public string adultCount  {set;get;} = "";
        public string childrenCount {set;get;} = "";
        public string? message {set;get;}

        public bool IsConfirm {set;get;} = false;

        public Tour Tour {set;get;} = default!;
        public DateTime? createdAt { set; get; }







    }




}