using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CombineWebService.Models
{
    public class Combine
    {
        public int CombineID { get; set; }
        public string CombineName { get; set; }
        public DateTime CombineDate { get; set; }
        public List<Participants> ParticipantList { get; set; }
        public List<CombineTest> TestList { get; set; }
        public List<Combine> CombineList { get; set; }
        public List<string> SelectedTests { get; set; }
        public List<string> SelectedParticipants { get; set; }
    }

    public class Participants
    {
        public int ParticipantID { get; set; }
        public string ParticipantFirstName { get; set; }
        public string ParticipantsLastName { get; set; }
        public string CombineName { get; set; }
        public List<Participants> ParticipantList { get; set; }
    }

    public class CombineTest
    {
        public int CombineTestID { get; set; }
        public string Test { get; set; }
        public string Participant { get; set; }
        public List<CombineTest> TestList { get; set; }
    }

    public class CombineResult
    {
        public int ParticipantTestID { get; set; }
        public int ParticipantID { get; set; }
        public int CombineTestID { get; set; }
        public string Result { get; set; }
        public string Measurement { get; set; }
        public int Attempt { get; set; }
        public bool IncludeRepCount { get; set; }
        public string NumberOfReps { get; set; }
        public string PageTitle { get; set; }
        public List<CombineTest> TestList { get; set; }
    }
}