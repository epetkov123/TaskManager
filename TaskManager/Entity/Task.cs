using System;

namespace TaskManager.Entity
{
    public class Task:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int TimeDone { get; set; }
        public string Creator { get; set; }
        public string UserTask { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDone { get; set; }
    }
}
