using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hearthstone.Models
{
    public class CardModel
    {
        public string guid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string local { get; set; }
        public string type { get; set; }
        public string faction { get; set; }
        public string rarity { get; set; }
        public Nullable<int> cost { get; set; }
        public string race { get; set; }
        public string playerClass { get; set; }
        public string text { get; set; }
        public string inPlayText { get; set; }
        public string[] mechanics { get; set; }
        public string flavor { get; set; }
        public string artist { get; set; }
        public Nullable<int> attack { get; set; }
        public Nullable<int> health { get; set; }
        public Nullable<bool> collectible { get; set; }
        public string elite { get; set; }
        public string howToGet { get; set; }
        public string howToGetGold { get; set; }
        public string img { get; set; }
        public string imgGold { get; set; }
        public string set { get; set; }
    }
}