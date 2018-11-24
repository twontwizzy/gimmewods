using CombineWebService.Models;
using CombineWebService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CombineWebService.Controllers
{
    public class CombineController : ApiController
    {
        // GET: api/Combine
        public List<Combine> Get()
        {
            return CombineService.GetCombines();
        }

        // GET: api/Combine/5
        public Combine Get(int id)
        {
            return CombineService.GetCombines().Where(x => x.CombineID == id).FirstOrDefault();
        }

        // POST: api/Combine
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Combine/5
        public void Put(CombineTest val)
        {
            CombineService.UpdateParticipantTest(val.CombineTestID, val.Result);
        }

        // DELETE: api/Combine/5
        public void Delete(int id)
        {
        }

        [Route("api/Combine/Participants/{combineID:int}")]
        [HttpGet]
        public List<Participants> GetCombineParticipants(int combineId)
        {
            return CombineService.GetCombineParticipants(combineId);
        }

        [Route("api/Combine/Test/{combineID:int}/{participantID:int}")]
        [HttpGet]
        public List<CombineTest> GetParticipantTest(int combineId, int participantId)
        {
            return CombineService.GetParticipantTest(combineId, participantId);
        }
    }
}
