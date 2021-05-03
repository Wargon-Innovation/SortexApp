using System;
using System.Collections.Generic;
using System.Text;

namespace SortexApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; }
    }
}
