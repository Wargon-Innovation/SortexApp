using System;
using System.Collections.Generic;
using System.Text;

namespace SortexApp.Models
{
    public class TrendImageView
    {
        
            public int Id { get; set; }
            public string Season { get; set; }
            public string Description { get; set; }

            public List<String> TrendImages = new List<string>();

            public int NumberOfImages { get; set; }
            public bool IsVisible { get; set; }
    }
}
