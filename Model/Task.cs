using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyTracker.Model
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        [Column(TypeName = "date")]
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public ProjectUser ProjectUserId { get; set; }
        public Project ProjectId { get; set; }
    }
}
