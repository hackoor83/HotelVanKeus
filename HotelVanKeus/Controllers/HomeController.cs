using HotelVanKeus.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelVanKeus.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {   
            return View("Index");
        }

    }
}
