using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPlayground.Controllers
{
    public class CascadingDropdownController : Controller
    {
        //
        // GET: /CascadingDropdown/

        public ActionResult Index()
        {
            ViewBag.Countries = GetCountryList();

            return View();
        }

        private List<SelectListItem> GetCountryList(string country = "Select")
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select", Value = "Select" });
            li.Add(new SelectListItem { Text = "India", Value = "India"});
            li.Add(new SelectListItem { Text = "Australia", Value = "Australia" });
            li.Add(new SelectListItem { Text = "United States of America", Value = "USA" });
            li.Add(new SelectListItem { Text = "Australia", Value = "Australia" });
            li.Add(new SelectListItem { Text = "South Africa", Value = "South Africa" });
            // Set wanted default selected:
            li.Where(i => i.Value == country).ToList().ForEach(x => x.Selected = true);
            return li;
        }


        public JsonResult States(string Country)
        {
            List<string> StatesList = new List<string>();
            switch (Country)
            {
                case "India":
                    StatesList.Add("New Delhi");
                    StatesList.Add("Mumbai");
                    StatesList.Add("Kolkata");
                    StatesList.Add("Chennai");
                    break;
                case "Australia":
                    StatesList.Add("Canberra");
                    StatesList.Add("Melbourne");
                    StatesList.Add("Perth");
                    StatesList.Add("Sydney");
                    break;
                case "USA":
                    StatesList.Add("California");
                    StatesList.Add("Florida");
                    StatesList.Add("New York");
                    StatesList.Add("Washignton");
                    break;
                case "South Africa":
                    StatesList.Add("Cape Town");
                    StatesList.Add("Centurion");
                    StatesList.Add("Durban");
                    StatesList.Add("Jahannesburg");
                    break;
            }
            return Json(StatesList);
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string country = fc["country"];
            string state = fc["state"];

            //rebinding after post
            ViewBag.Countries = GetCountryList(country);
            ViewBag.country = country;
            ViewBag.state = state;

            return View(new { country, state });
        }

    }
}
