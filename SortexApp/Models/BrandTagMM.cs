using System;
using System.Collections.Generic;
using System.Text;

namespace SortexApp.Models
{
   public class BrandTagMM
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
