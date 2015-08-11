using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hearthstone.Models
{
    public class FilterModel
    {
        public string Class { get; set; }
        public string Quality { get; set; }
        public string Type { get; set; }
        public string Set { get; set; }
        public string Attack { get; set; }

        public string Health { get; set; }
        public string Cost { get; set; }
        public string Race { get; set; }

        //TODO
        public string Name { get; set; }

        public string CardState { get; set; }   //當下是清單還是圖片
    }
}