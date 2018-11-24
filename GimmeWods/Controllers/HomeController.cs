using GimmeWods.Models;
using GimmeWods.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GimmeWods.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            RandomWod model = new RandomWod();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(RandomWod model)
        {
            string movement = string.Empty;
            if (model.SelectedMovement != null)
                movement = string.Join(",", model.SelectedMovement);

            string equipment = string.Empty;
            if(model.SelectedEquipment != null)
               equipment = string.Join(",", model.SelectedEquipment);

            model = WODService.GetRandomWod(model.IsOutsite, model.WodType, movement, equipment);
            model.ExerciseList = WODService.GetExercises(model.WODID);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}