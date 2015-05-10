using System.Web.Http;
using System.Web.Mvc;
using Yangaroo.Core.Models;

namespace Yangaroo.Controllers
{
    public class CupcakeController : Controller
    {
        //
        // GET: /Cupcake/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RouteCupCakes()
        {
            return View();
        }

        public ActionResult RouteCupCake(string cupCake)
        {
            return View(cupCake);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Insert([FromUri]int id)
        {
            return RedirectToAction("Index");
        }
    }
}
