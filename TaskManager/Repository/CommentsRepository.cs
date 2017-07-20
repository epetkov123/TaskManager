using System;
using System.IO;
using TaskManager.Entity;

namespace TaskManager.Repository
{
    public class CommentsRepository:BaseRepository<Comment>
    {
        

        public CommentsRepository(string filePath)
            :base(filePath)
        {
            
        }

        public override void ReadItem(StreamReader sr, Comment item)
        {
            item.Id = Convert.ToInt32(sr.ReadLine());
            item.TaskId = int.Parse(sr.ReadLine());
            item.CommentDescription = sr.ReadLine();
            item.Creator = sr.ReadLine();
        }

        public override void WriteItem(StreamWriter sw, Comment item)
        {
            sw.WriteLine(item.Id);
            sw.WriteLine(item.TaskId);
            sw.WriteLine(item.CommentDescription);
            sw.WriteLine(item.Creator);
        }
    }
}
