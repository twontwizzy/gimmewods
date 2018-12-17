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
            model.CombineList = Services.CombineService.GetCombines();
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

            model.ParticipantList = Services.CombineService.GetParticipants();

            model.TestList = Services.CombineService.GetTests();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCombine(Models.Combine model)
        {
            

            model.CombineID = Services.CombineService.InsertCombine(model.CombineName, model.CombineDate);

            for(int i = 0; i < model.TestList.Count(); i++)
            {
                if (model.TestList[i].IncludeInCombine == true && model.TestList[i].Attempts > 0)
                {
                    Services.CombineService.InsertCombineTests(model.CombineID, model.TestList[i].CombineTestID, model.TestList[i].IncludeInCombine, model.TestList[i].Attempts, model.TestList[i].InclueRepCount);
                }
            }

            for (int i = 0; i < model.TestList.Count(); i++)
            {

                for (int p = 0; p < model.ParticipantList.Count(); p++)
                {
                    if (model.TestList[i].IncludeInCombine == true)
                    {
                        if (model.ParticipantList[p].Checked == true && model.TestList[i].Attempts > 0)
                        {
                            for (int a = 1; a <= model.TestList[i].Attempts; a++)
                            {
                                Services.CombineService.InsertCombineParticipants(model.CombineID, model.ParticipantList[p].ParticipantID, model.TestList[i].CombineTestID, a);
                            }
                        }
                    }
                }
            }

            model.TestList = Services.CombineService.GetTests();
            model.ParticipantList = Services.CombineService.GetParticipants();

            return View(model);
        }

        public ActionResult EditCombine(int id)
        {
            Models.Combine model = new Models.Combine();

            model = Services.CombineService.GetCombines().Where(m => m.CombineID == id).SingleOrDefault();

            model.ParticipantList = Services.CombineService.GetParticipants(id);

            model.TestList = Services.CombineService.GetTests(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditCombine(Models.Combine model)
        {
           

            Services.CombineService.UpdateCombine(model.CombineName, model.CombineDate, model.CombineID);

            //Delete Tests
            Services.CombineService.DeleteCombineTests(model.CombineID);
            //Insert Tests
            for (int i = 0; i < model.TestList.Count(); i++)
            {
                if (model.TestList[i].IncludeInCombine == true && model.TestList[i].Attempts > 0)
                {
                    Services.CombineService.InsertCombineTests(model.CombineID, model.TestList[i].CombineTestID, model.TestList[i].IncludeInCombine, model.TestList[i].Attempts, model.TestList[i].InclueRepCount);
                }
            }

            //Delte Participants
            Services.CombineService.DeleteParticipantTests(model.CombineID);
            //Insert Participants
            for (int i = 0; i < model.TestList.Count(); i++)
            {

                for (int p = 0; p < model.ParticipantList.Count(); p++)
                {
                    if (model.TestList[i].IncludeInCombine == true)
                    {
                        if (model.ParticipantList[p].Checked == true && model.TestList[i].Attempts > 0)
                        {
                            for (int a = 1; a <= model.TestList[i].Attempts; a++)
                            {
                                Services.CombineService.InsertCombineParticipants(model.CombineID, model.ParticipantList[p].ParticipantID, model.TestList[i].CombineTestID, a);
                            }
                        }
                    }
                }
            }

            model.TestList = Services.CombineService.GetTests(model.CombineID);
            model.ParticipantList = Services.CombineService.GetParticipants(model.CombineID);

            return View(model);
        }
    }
}