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
                    if (filter.Class != null)
                    {
                        var ary = filter.Class.ToArray();
                        Cards = Cards.Where(c => ary.Contains(c.playerClass));
                    }
                    //Quality
                    if (filter.Quality != null)
                    {
                        var ary = filter.Quality.ToArray();
                        Cards = Cards.Where(c => ary.Contains(c.rarity));
                    }
                    //Type
                    if (filter.Type != null)
                    {
                        var ary = filter.Type.ToArray();
                        Cards = Cards.Where(c => ary.Contains(c.type));
                    }
                     //Set
                    if (filter.Set != null)
                    {
                        var ary = filter.Set.ToArray();
                        Cards = Cards.Where(c => ary.Contains(c.set));
                    }
                     //Attack
                    if (filter.Attack != null)
                    {
                        var ary = filter.Attack.ToArray();
                        Cards = Cards.Where(c => ary.Contains(c.attack.ToString()));
                    }
                     //Health
                    if (filter.Health != null)
                    {
                        var ary = filter.Health.ToArray();
                        Cards = Cards.Where(c => ary.Contains(c.health.ToString()));
                    }
                     //Cost
                    if (filter.Cost != null)
                    {
                        var ary = filter.Cost.ToArray();
                        Cards = Cards.Where(c => ary.Contains(c.cost.ToString()));
                    }
                     //Race
                    if (filter.Race != null)
                    {
                        var ary = filter.Race.ToArray();
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