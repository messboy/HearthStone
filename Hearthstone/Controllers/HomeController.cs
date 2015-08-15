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
        private string _dataPath;
        private CardDao _db;

        public HomeController() {
            _db = new CardDao();
            _dataPath = "~/App_Data/AllSets.zhTW-2.8.0.9554.json";
        }
       

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Card()
        {
            return View();
        }

        //public ActionResult CardData()
        //{
        //    //存取json檔
        //    var path = HttpContext.Server.MapPath(_dataPath);
        //    List<Cards> data = _db.SetData(path).Where(c => c.collectible == true).Where(c => c.type != "Hero").ToList(); ;
        //    //var data = db.GetCards();
        //    ViewBag.data = data;
        //    return View();
        //}

        // [HttpPost]
        //public ActionResult CardData(FilterModel m)
        //{
        //    var data = _db.GetCards(m);
        //    ViewBag.data = data;
        //    return View();
        //}


        [HttpPost]
        public PartialViewResult GetCardDataList(FilterModel m)
        {
            var path = HttpContext.Server.MapPath(_dataPath);
            var data = _db.GetCardsByJson(m, path);
            ViewBag.data = data;

            return PartialView("_CardDataList", data);
        }  

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [NonAction]
        public ActionResult Export()
        {
            var path = HttpContext.Server.MapPath(_dataPath);
            List<Cards> cardList = _db.SetData(path);

            //儲存
            _db.AddCards(cardList);

            return View();
        }


    }
}