using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Core.Models
{
    class ParentList
    {
        public string ParentTitle { get; set; }
        public IList<ChildList> ParentItems { get; set; }
    }
}