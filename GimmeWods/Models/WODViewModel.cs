using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GimmeWods.Models
{
 

    public class RandomWod
    {
        public string WODName { get; set; }
        public int WODID { get; set; }
        public bool IsOutsite { get; set; }
        public string WodType { get; set; }
        public List<string> SelectedEquipment { get; set; }
        public List<string> SelectedMovement { get; set; }
        public List<WodExercise> ExerciseList { get; set; }
    }

    public class WodExercise
    {
        public int ExerciseID { get; set; }
        public string Exercise { get; set; }
    }
}