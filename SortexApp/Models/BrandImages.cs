using System;
using System.Collections.Generic;
using System.Text;

namespace SortexApp.Models
{
    public class BrandImages
    {
        public int Id { get; set; }
        public string Img { get; set; }
        public int brandId { get; set; }
        public Brand Brand { get; set; }
    }
}
