using Hearthstone.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace Hearthstone.Dal
{
    public class CardDao
    {
        public List<Cards> SetData(string path)
        {

            String jsonString = string.Empty;
            using (StreamReader sr = new StreamReader(path))
            {
                jsonString = sr.ReadToEnd();
            }
            JObject jsonResponse = JObject.Parse(jsonString);


            List<Cards> cardList = new List<Cards>();
            foreach (var x in jsonResponse)
            {
                string set = x.Key;
                List<CardModel> list = JsonConvert.DeserializeObject<List<CardModel>>(x.Value.ToString());

                //塞到card
                foreach (var item in list)
                {
                    var c = new Cards()
                    {
                        guid = Guid.NewGuid().ToString(),
                        id = item.id,
                        name = item.name,
                        local = item.local,
                        type = item.type,
                        faction = item.faction,
                        rarity = item.rarity,
                        cost = item.cost,
                        race = item.race,
                        playerClass = item.playerClass,
                        text = item.text,
                        inPlayText = item.inPlayText,
                        mechanics = item.mechanics != null ? string.Join(",", item.mechanics) : null,
                        flavor = item.flavor,
                        artist = item.artist,
                        attack = item.attack,
                        health = item.health,
                        collectible = item.collectible,
                        elite = item.elite,
                        howToGet = item.howToGet,
                        howToGetGold = item.howToGetGold,
                        img = item.img,
                        imgGold = item.imgGold,
                        set = set
                    };
                    cardList.Add(c);
                }
            }
            return cardList;
        }

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

                    //CardName
                    if (!string.IsNullOrEmpty(filter.Name))
                    {
                        Cards = Cards.Where(c => c.name == filter.Name);
                    }

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

        public List<Cards> GetCardsByJson(FilterModel filter, string DataPath)
        {
            List<Cards> CardList = new List<Cards>();
            try
            {
                    //存取json檔
                    var Cards = SetData(DataPath).Where(c => c.collectible == true).Where(c => c.type != "Hero");

                    //CardName
                    if (!string.IsNullOrEmpty(filter.Name))
                    {
                        Cards = Cards.Where(c => c.name == filter.Name);
                    }

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