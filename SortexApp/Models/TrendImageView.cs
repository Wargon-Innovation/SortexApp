using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SortexApp.Models
{
    public class TrendImageView 
    {
        
            public int Id { get; set; }
            public string Season { get; set; }
            public string Description { get; set; }

            public List<TrendImage> TrendImages { get; set; } = new List<TrendImage>();

            public int NumberOfImages { get; set; }
            public bool IsVisible { get; set; }
    }
}
