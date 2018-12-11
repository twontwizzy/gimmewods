using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GimmeWods.Models
{
    public class Combine
    {
        public int CombineID { get; set; }
        public string CombineName { get; set; }
        [DisplayName("Combine Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
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
        public bool Checked { get; set; }
        public List<Participants> ParticipantList { get; set; }
    }

    public class CombineTest
    {
        public int CombineTestID { get; set; }
        public string Test { get; set; }
        public string Result { get; set; }
        public string Measurement { get; set; }
        public List<CombineTest> TestList { get; set; }
        public bool IncludeInCombine { get; set; }
        public int Attempts { get; set; }
        public bool InclueRepCount { get; set; }
    }


}