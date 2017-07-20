using System;
using TaskManager.Entity;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace TaskManager.Repository
{
    public class TasksRepository:BaseRepository<Task>
    {

        public TasksRepository(string filePath)
            :base(filePath)
        {

        }

        public override void ReadItem(StreamReader sr, Task item)
        {
            item.Id = Convert.ToInt32(sr.ReadLine());
            item.Title = sr.ReadLine();
            item.Description = sr.ReadLine();
            item.TimeDone = int.Parse(sr.ReadLine());
            item.Creator = sr.ReadLine();
            item.UserTask = sr.ReadLine();
            item.DateCreated = DateTime.ParseExact(sr.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            item.LastModified = DateTime.ParseExact(sr.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            item.IsDone = bool.Parse(sr.ReadLine());
        }

        public override void WriteItem(StreamWriter sw, Task item)
        {
            sw.WriteLine(item.Id);
            sw.WriteLine(item.Title);
            sw.WriteLine(item.Description);
            sw.WriteLine(item.TimeDone);
            sw.WriteLine(item.Creator);
            sw.WriteLine(item.UserTask);
            sw.WriteLine(item.DateCreated.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            sw.WriteLine(item.LastModified.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            sw.WriteLine(item.IsDone);
        }
    }
}
