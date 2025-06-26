namespace StudyTracker.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Project ProjectId { get; set; }
    }
}
