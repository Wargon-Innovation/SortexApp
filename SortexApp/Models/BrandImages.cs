using System;
using System.Collections.Generic;
using System.Text;

namespace SortexApp.Models
{
    public class BrandImages
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int brandId { get; set; }
        public string ImageDescription { get; set; }
        public Brand Brand { get; set; }
    }
}
