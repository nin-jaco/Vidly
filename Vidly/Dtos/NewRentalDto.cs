using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class NewRentalDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
        [Display(Name = "Date Rented")]
        public DateTime DateRented { get; set; }
        [Display(Name = "Date Returned")]
        public DateTime? DateReturned { get; set; }
    }
}