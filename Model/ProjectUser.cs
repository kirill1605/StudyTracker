namespace StudyTracker.Model
{
    public class ProjectUser
    {
        public int Id { get; set; }
        public Role? RoleId { get; set; }
        public User UserId { get; set; }
        public Project ProjectId { get; set; }
    }
}
