using Hearthstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Hearthstone.Dal
{
    public class CardDao
    {
        public List<Cards> GetCards()
        {
            List<Cards> CardList = new List<Cards>();
            try
            {
                using (HearthStoneEntities db = new HearthStoneEntities())
                {
                    CardList = db.Cards.Where(c => c.collectible == true).Where(c => c.type != "Hero").ToList();
                    //設定圖片路徑 - 外連
                    CardList.ForEach(c => c.img = string.Format("{0}{1}.png", ConstantModel.ImgUrl, c.id));
                }
            }
            catch (Exception e)
            {
            }
            return CardList;
        }

        public List<Cards> GetCards(FilterModel filter)
        {
            List<Cards> CardList = new List<Cards>();
            try
            {
                using (HearthStoneEntities db = new HearthStoneEntities())
                {
                    var Cards = db.Cards.Where(c => c.collectible == true).Where(c => c.type != "Hero");

                    //職業
                    if (!string.IsNullOrEmpty(filter.Class))
                    {
                        var ary = filter.Class.Split(',');
                        Cards = Cards.Where(c => ary.Contains(c.playerClass));
                    }
                    //Quality
                    if (!string.IsNullOrEmpty(filter.Quality))
                    {
                        var ary = filter.Quality.Split(',');
                        Cards = Cards.Where(c => ary.Contains(c.rarity));
                    }
                    //Type
                    if (!string.IsNullOrEmpty(filter.Type))
                    {
                        var ary = filter.Type.Split(',');
                        Cards = Cards.Where(c => ary.Contains(c.type));
                    }
                     //Set
                    if (!string.IsNullOrEmpty(filter.Set))
                    {
                        var ary = filter.Set.Split(',');
                        Cards = Cards.Where(c => ary.Contains(c.set));
                    }
                     //Attack
                    if (!string.IsNullOrEmpty(filter.Attack))
                    {
                        var ary = filter.Attack.Split(',');
                        Cards = Cards.Where(c => ary.Contains(c.attack.ToString()));
                    }
                     //Health
                    if (!string.IsNullOrEmpty(filter.Health))
                    {
                        var ary = filter.Health.Split(',');
                        Cards = Cards.Where(c => ary.Contains(c.health.ToString()));
                    }
                     //Cost
                    if (!string.IsNullOrEmpty(filter.Cost))
                    {
                        var ary = filter.Cost.Split(',');
                        Cards = Cards.Where(c => ary.Contains(c.cost.ToString()));
                    }
                     //Race
                    if (!string.IsNullOrEmpty(filter.Race))
                    {
                        var ary = filter.Race.Split(',');
                        Cards = Cards.Where(c => ary.Contains(c.race));
                    }

                    CardList = Cards.ToList();
                    //設定圖片路徑 - 外連
                    CardList.ForEach(c => c.img = string.Format("{0}{1}.png", ConstantModel.ImgUrl, c.id));
                }
            }
            catch (Exception e)
            {
            }
            return CardList;
        }

        public Boolean AddCards(List<Cards> list)
        {
            using (HearthStoneEntities db = new HearthStoneEntities())
            {

                try
                {
                    db.Cards.AddRange(list);
                    db.SaveChanges();


                }
                catch (Exception e)
                {
                    return false;
                }

            }
            return true;
        } // AddCards()
    }
}