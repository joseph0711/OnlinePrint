using System.Web.Mvc;

namespace OnlinePrint.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult notfound()
        {
            return View();
        } 
    }
}