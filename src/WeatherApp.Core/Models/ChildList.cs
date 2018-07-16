using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WeatherApp.Core.Models
{
    class ChildList
    {
        public IList<ListItem> ChildItems { get; set; }
    }
}