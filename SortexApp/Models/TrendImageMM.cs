using System;
using System.Collections.Generic;
using System.Text;

namespace SortexApp.Models
{
    public class TrendImageMM
    {
        public int Id { get; set; }

        public int TrendId { get; set; }
        public Trend Trend { get; set; }

        public int TrendImageId { get; set; }
        public TrendImage TrendImage { get; set; }
    }
}
