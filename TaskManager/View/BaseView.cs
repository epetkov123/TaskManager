using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;
using TaskManager.Repository;
using TaskManager.Tools;

namespace TaskManager.View
{
    public abstract class BaseView<T> where T : BaseEntity, new()
    {
        public abstract BaseRepository<T> CreateRepo();

        public abstract void RenderToConsole(T item);

        public abstract T ReadFromConsole(T item);

        virtual public List<T> GetFilteredItemsList()
        {
            return CreateRepo().GetAll();
        }

        private BaseManagementEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{typeof(T).Name} management:");
                Console.WriteLine($"[G]et all {typeof(T).Name}s");
                Console.WriteLine($"[V]iew {typeof(T).Name}");
                Console.WriteLine($"[A]dd {typeof(T).Name}");
                Console.WriteLine($"[E]dit {typeof(T).Name}");
                Console.WriteLine($"[D]elete {typeof(T).Name}");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return BaseManagementEnum.Select;
                        }
                    case "V":
                        {
                            return BaseManagementEnum.View;
                        }
                    case "A":
                        {
                            return BaseManagementEnum.Insert;
                        }
                    case "E":
                        {
                            return BaseManagementEnum.Update;
                        }
                    case "D":
                        {
                            return BaseManagementEnum.Delete;
                        }
                    case "X":
                        {
                            return BaseManagementEnum.Exit;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid choice.");
                            Console.ReadKey(true);
                            break;
                        }
                }
            }
        }

        public void Show()
        {
            while (true)
            {
                BaseManagementEnum choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case BaseManagementEnum.Select:
                            {
                                GetAll();
                                break;
                            }
                        case BaseManagementEnum.View:
                            {
                                View();
                                break;
                            }
                        case BaseManagementEnum.Insert:
                            {
                                Add();
                                break;
                            }
                        case BaseManagementEnum.Update:
                            {
                                Update();
                                break;
                            }
                        case BaseManagementEnum.Delete:
                            {
                                Delete();
                                break;
                            }
                        case BaseManagementEnum.Exit:
                            {
                                return;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }
        }

        public void GetAll()
        {
            Console.Clear();

            List<T> items = GetFilteredItemsList();

            foreach (T item in items)
            {
                RenderToConsole(item);
            }

            Console.ReadKey(true);
        }
        
        public void Add()
        {
            Console.Clear();

            T item = new T();

            Console.WriteLine($"Add new {typeof(T).Name}:");

            ReadFromConsole(item);

            BaseRepository<T> baseRepository = CreateRepo();
            baseRepository.Save(item);

            Console.WriteLine($"{typeof(T).Name} saved successfully.");
            Console.ReadKey(true);
        }

        public virtual void RenderTask(int baseId)
        {

        }

        public virtual void View()
        {
            Console.Clear();

            Console.Write("ID: ");
            int baseId = Convert.ToInt32(Console.ReadLine());

            BaseRepository<T> baseRepository = CreateRepo();
            T item = baseRepository.GetById(baseId);

            if (item == null)
            {
                Console.Clear();
                Console.WriteLine($"{typeof(T).Name} not found.");
                Console.ReadKey(true);
                return;
            }

            RenderToConsole(item);
            RenderTask(baseId);

            Console.ReadKey(true);
        }

        public void Update()
        {
            Console.Clear();

            Console.Write("ID: ");
            int baseId = Convert.ToInt32(Console.ReadLine());

            BaseRepository<T> baseRepository = CreateRepo();
            T item = baseRepository.GetById(baseId);

            if (item == null)
            {
                Console.Clear();
                Console.WriteLine($"{typeof(T).Name} not found.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("Editing:");
            ReadFromConsole(item);

            baseRepository.Save(item);

            Console.WriteLine($"{typeof(T).Name} saved successfully.");
            Console.ReadKey(true);
        }

        public void Delete()
        {
            BaseRepository<T> baseRepository = CreateRepo();

            Console.Clear();

            Console.WriteLine($"Delete {typeof(T).Name}: ");
            Console.Write($"{typeof(T).Name} Id: ");
            int baseId = Convert.ToInt32(Console.ReadLine());

            T item = baseRepository.GetById(baseId);
            if (item == null)
            {
                Console.WriteLine($"{typeof(T).Name} not found!");
            }
            else
            {
                baseRepository.Delete(item);
                Console.WriteLine($"{typeof(T).Name} deleted successfully.");
            }
            Console.ReadKey(true);
        }

    }
}
