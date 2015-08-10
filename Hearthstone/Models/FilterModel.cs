using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hearthstone.Models
{
    public class FilterModel
    {
        public List<string> Class { get; set; }
        public List<string> Quality { get; set; }
        public List<string> Type { get; set; }
        public List<string> Set { get; set; }
        public List<string> Attack { get; set; }

        public List<string> Health { get; set; }
        public List<string> Cost { get; set; }
        public List<string> Race { get; set; }
    }
}