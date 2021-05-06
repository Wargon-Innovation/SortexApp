using System;
using System.Collections.Generic;
using System.Text;

namespace SortexApp.Models
{
    public class Trend
    {
        public int Id { get; set; }
        public string Season { get; set; }
        public string Description { get; set; }
        public List<TrendImageMM> TrendImages { get; set; }
    }
}
