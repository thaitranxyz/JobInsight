namespace JobInsight.Model
{
    public class Application
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
        public string Note { get; set; }
        public DateTime DateApplied { get; set; } = DateTime.Now;
        public string Result { get; set; }
    }

    public enum Result
    {
        NotYet, Success, Failed, Ghosted
    }

    public enum Status
    {
        OnGoing, Pending, Done
    }


}
