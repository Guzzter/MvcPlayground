using MvcPlayground.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPlayground.Controllers
{
    public class AutocompleteTextBoxController : Controller
    {
        //
        // GET: /BasicAutocompleteTextBox/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AutoCompleteWithEditorTemplate()
        {

            return View(new Person());
        }

        public ActionResult Autocomplete(string term)
        {
            var items = new[] { "Apple", "Pear", "Banana", "Pineapple", "Peach" };

            var filteredItems = items.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
    }
}
