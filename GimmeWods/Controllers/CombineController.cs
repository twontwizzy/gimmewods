using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GimmeWods.Controllers
{
    public class CombineController : Controller
    {
        // GET: Combine
        public ActionResult Index()
        {
            Models.Combine model = new Models.Combine();
            return View(model);
        }

        public ActionResult CombineParticipants()
        {
            Models.Participants model = new Models.Participants();
            model.ParticipantList = Services.CombineService.GetParticipants();

            return View(model);
        }

        [HttpPost]
        public ActionResult CombineParticipants(Models.Participants model)
        {
            Services.CombineService.InsertParticipant(model.ParticipantFirstName, model.ParticipantsLastName);

            model = new Models.Participants();
            model.ParticipantList = Services.CombineService.GetParticipants();

            return View(model);
        }

        public ActionResult CombineTest()
        {
            Models.CombineTest model = new Models.CombineTest();
            model.TestList = Services.CombineService.GetTests();

            return View(model);
        }

        [HttpPost]
        public ActionResult CombineTest(Models.CombineTest model)
        {
            Services.CombineService.InsertTests(model.Test, model.Measurement);

            model = new Models.CombineTest();
            model.TestList = Services.CombineService.GetTests();

            return View(model);
        }

        public ActionResult CreateCombine()
        {
            Models.Combine model = new Models.Combine();

            ViewBag.TestSelectList = new SelectList(Services.CombineService.GetTests(), "CombineTestID", "Test");
            ViewBag.ParticipantsSelectList = new SelectList(Services.CombineService.GetParticipants().Select(m => new SelectListItem
            {
                Text = m.ParticipantFirstName + " " + m.ParticipantsLastName,
                Value = m.ParticipantID.ToString()
            }), "Value", "Text");

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCombine(Models.Combine model)
        {
            

            ViewBag.TestSelectList = new SelectList(Services.CombineService.GetTests(), "CombineTestID", "Test");
            ViewBag.ParticipantsSelectList = new SelectList(Services.CombineService.GetParticipants().Select(m => new SelectListItem
            {
                Text = m.ParticipantFirstName + " " + m.ParticipantsLastName,
                Value = m.ParticipantID.ToString()
            }), "Value", "Text");

            model.CombineID = Services.CombineService.InsertCombine(model.CombineName, model.CombineDate);

            for(int i = 0; i < model.SelectedTests.Count(); i++)
            {
                Services.CombineService.InsertCombineTests(model.CombineID, int.Parse(model.SelectedTests[i]));
            }

            for (int i = 0; i < model.SelectedTests.Count(); i++)
            {

                for (int p = 0; p < model.SelectedParticipants.Count(); p++)
                {
                    Services.CombineService.InsertCombineParticipants(model.CombineID, int.Parse(model.SelectedParticipants[p]), int.Parse(model.SelectedTests[i]));
                }
            }

            return View(model);
        }
    }
}