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
        public string GetDate { get { return "Startdatum: " + Start + "\nSlutdatum: " + End; } }
        public string Description { get; set; }
        public string Contact { get; set; }
        public bool isVisible { get; set; }
    }
}
