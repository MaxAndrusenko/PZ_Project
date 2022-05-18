using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AutoDealer_Service.Controllers
{
    public class CarController : Controller
    {
        private readonly CarService carService;

        public CarController()
        {
            carService = new CarService();
        }
        // GET: Car
        public async Task<ActionResult> Index()
        {
            var cars = await carService.GetCars();

            return View(cars);
        }

        public ActionResult AddCar()
        {
            return View();
        }

        public ActionResult EditCar()
        {
            return View();
        }

        public ActionResult DeleteCar()
        {
            return RedirectToAction("Index");
        }
    }
}