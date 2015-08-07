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