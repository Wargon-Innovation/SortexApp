using System;
using System.Collections.Generic;
using System.Text;

namespace SortexApp.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Manufacture { get; set; }
        public string Gender { get; set; }
        public string Classification { get; set; }
        public List<BrandTagMM> BrandTag { get; set; }

    }
}
