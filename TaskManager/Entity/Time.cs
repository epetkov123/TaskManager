using System;

namespace TaskManager.Entity
{
    public class Time:BaseEntity
    {
        public int TimeTaken { get; set; }
        public int TaskId { get; set; }
        public string Creator { get; set; }
        public string UserReported { get; set; }
        public DateTime DateTaken { get; set; }
    }
}
