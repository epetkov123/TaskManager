using System;

namespace TaskManager.Entity
{
    public class Comment:BaseEntity
    {
        public int TaskId { get; set; }
        public string CommentDescription { get; set; }
        public string Creator { get; set; }
    }
}
