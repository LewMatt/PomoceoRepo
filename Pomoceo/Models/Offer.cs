using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pomoceo.Models
{
    public class Offer
    {
        [Key]
        public int OfferID { get; set; }
        public int CategoryID { get; set; }
        public string UserID { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string City { get; set; }

        
    }
    public enum OfferType
    {
        oferowanie,
        potrzeba
    }
}