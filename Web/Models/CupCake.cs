using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yangaroo.Core.Models
{
    public class CupCake
    {
        [Required(ErrorMessage = "Please enter a date")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a price")]
        public decimal? Price { get; set; }

        public string Ingredients { get; set; }

        public IEnumerable<string> IngredientsOutput { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateTimeCreated { get; set;}

        [DataType(DataType.Date)]
        public DateTime? DateTimeLastModified { get; set; }

        public int? Id { get; set; }
    }
}
