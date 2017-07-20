using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;

namespace TaskManager.Repository
{
    public abstract class BaseRepository<T> where T : BaseEntity, new()
    {
        protected readonly string filePath;

        public BaseRepository(string filePath)
        {
            this.filePath = filePath;
        }
        public abstract void ReadItem(StreamReader sr, T item);
        public abstract void WriteItem(StreamWriter sw, T item);

        private int GetNextId()
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            int id = 1;
            try
            {
                while (!sr.EndOfStream)
                {
                    T item = new T();
                    ReadItem(sr, item);

                    if (id <= item.Id)
                    {
                        id = item.Id + 1;
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return id;
        }

        private void Insert(T item)
        {
            item.Id = GetNextId();

            FileStream fs = new FileStream(filePath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                WriteItem(sw, item);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void Update(T item)
        {
            string tempFilePath = "temp." + filePath;

            FileStream ifs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ifs);

            FileStream ofs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ofs);

            try
            {
                while (!sr.EndOfStream)
                {

                    T current = new T();
                    ReadItem(sr, item);

                    if (current.Id != item.Id)
                    {
                        WriteItem(sw, current);
                    }
                    else
                    {
                        WriteItem(sw, item);
                    }


                }
            }
            finally
            {
                sw.Close();
                ofs.Close();
                sr.Close();
                ifs.Close();
            }

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public T GetById(int id)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    T item = new T();
                    ReadItem(sr, item);
                    if (item.Id == id)
                    {
                        return item;
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return default(T);
        }

        public List<T> GetAll()
        {
            List<T> result = new List<T>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    T item = new T();
                    ReadItem(sr, item);
                    result.Add(item);
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return result;
        }

        public void Delete(T item)
        {
            string tempFilePath = "temp." + filePath;

            FileStream ifs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ifs);

            FileStream ofs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ofs);

            try
            {
                while (!sr.EndOfStream)
                {
                    T current = new T();
                    ReadItem(sr, current);

                    if (current.Id != item.Id)
                    {
                        WriteItem(sw, current);
                    }
                }
            }
            finally
            {
                sw.Close();
                ofs.Close();
                sr.Close();
                ifs.Close();
            }

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }
        public void Save(T item)
        {
            if (item.Id > 0)
            {
                Update(item);
            }
            else
            {
                Insert(item);
            }
        }
    }
}