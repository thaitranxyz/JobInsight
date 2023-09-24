using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace JobInsight.Model
{
    public class Application
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public Status Status { get; set; }
        public string Url { get; set; }
        public string Note { get; set; }
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }
        public DateTime DateApplied { get; set; } = DateTime.Now;
        public string Result { get; set; }
    }

    public enum Result
    {
        [Display(Name="Not Ready")]
        NotYet, 
        Success, 
        Failed, 
        Ghosted
    }

    public enum Status
    {
        [Display(Name="Not Started")]
        NotStarted,
        [Display(Name="On Going")]
        OnGoing,
        Pending,
        Done
    }


}
