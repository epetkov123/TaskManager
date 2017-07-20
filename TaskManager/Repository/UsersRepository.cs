using System;
using System.Collections.Generic;
using System.IO;
using TaskManager.Entity;

namespace TaskManager.Repository
{
    public class UsersRepository:BaseRepository<User>
    {

        public UsersRepository(string filePath)
            :base(filePath)
        { 

        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    User user = new User();
                    user.Id = int.Parse(sr.ReadLine());
                    user.FirstName = sr.ReadLine();
                    user.LastName = sr.ReadLine();
                    user.Username = sr.ReadLine();
                    user.Password = sr.ReadLine();
                    user.IsAdmin = bool.Parse(sr.ReadLine());

                    if (user.Username == username && user.Password == password)
                    {
                        return user;
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return null;
        }

        public override void ReadItem(StreamReader sr, User item)
        {
            item.Id = Convert.ToInt32(sr.ReadLine());
            item.FirstName = sr.ReadLine();
            item.LastName = sr.ReadLine();
            item.Username = sr.ReadLine();
            item.Password = sr.ReadLine();
            item.IsAdmin = bool.Parse(sr.ReadLine());
        }

        public override void WriteItem(StreamWriter sw, User item)
        {
            sw.WriteLine(item.Id);
            sw.WriteLine(item.FirstName);
            sw.WriteLine(item.LastName);
            sw.WriteLine(item.Username);
            sw.WriteLine(item.Password);
            sw.WriteLine(item.IsAdmin);
        }
    }
}
