using System;
using System.Collections.Generic;
using System.Text;

namespace SortexApp.Models
{
    public class BrandView
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Gender { get; set; }
        public string Classification { get; set; }
        public List<Tag> TagList { get; set; } = new List<Tag>();
        public List<BrandImages> brandImages{ get; set; } = new List<BrandImages>();
        
     
    }
}
