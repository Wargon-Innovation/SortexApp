using System;
using System.Collections.Generic;
using System.Text;

namespace SortexApp.Models
{
    public class Tag
    {
        public int Id { get; set; }

        private string _value;

        public string Value
        {
            get { return _value.ToLower(); }
            set { _value = value; }
        }

    }
}
