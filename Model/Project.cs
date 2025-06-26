namespace StudyTracker.Model
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Admin { get; set; }
    }
}
