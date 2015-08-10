using Hearthstone.Dal;
using Hearthstone.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hearthstone.Controllers
{
    public class HomeController : Controller
    {
        CardDao db = new CardDao();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            
            var data = db.GetCards();
            return View(data);
        }

        [HttpPost]
        public ActionResult About(FilterModel m)
        {
            var data = db.GetCards();
            return View(data);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Export()
        {
            string path = HttpContext.Server.MapPath("~/App_Data/AllSets.zhTW-2.8.0.9554.json");

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
                        mechanics = item.mechanics != null ? string.Join(",", item.mechanics): null,
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

            //儲存
            db.AddCards(cardList);

            return View();
        }
    }
}