using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TaskManager.Entity;

namespace TaskManager.Repository
{
    public class TimesRepository:BaseRepository<Time>
    {
        public TimesRepository(string filePath)
            :base(filePath)
        {
            
        }

        public override void ReadItem(StreamReader sr, Time item)
        {
            item.Id = int.Parse(sr.ReadLine());
            item.TimeTaken = int.Parse(sr.ReadLine());
            item.TaskId = int.Parse(sr.ReadLine());
            item.Creator = sr.ReadLine();
            item.UserReported = sr.ReadLine();
            item.DateTaken = DateTime.ParseExact(sr.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public override void WriteItem(StreamWriter sw, Time item)
        {
            sw.WriteLine(item.Id);
            sw.WriteLine(item.TimeTaken);
            sw.WriteLine(item.TaskId);
            sw.WriteLine(item.Creator);
            sw.WriteLine(item.UserReported);
            sw.WriteLine(item.DateTaken);
        }
    }
}
