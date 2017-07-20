using System;
using System.Collections.Generic;
using System.Globalization;
using TaskManager.Entity;
using TaskManager.Repository;
using TaskManager.Tools;
using TaskManager.Service;

namespace TaskManager.View
{
    public class TimeManagementView
    {
        public void Show()
        {
            while (true)
            {
                TimeManagementEnum choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case TimeManagementEnum.Select:
                            {
                                GetAll();
                                break;
                            }
                        case TimeManagementEnum.View:
                            {
                                View();
                                break;
                            }
                        case TimeManagementEnum.Insert:
                            {
                                Add();
                                break;
                            }
                        case TimeManagementEnum.Update:
                            {
                                Update();
                                break;
                            }
                        case TimeManagementEnum.Delete:
                            {
                                Delete();
                                break;
                            }
                        case TimeManagementEnum.Exit:
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

        private TimeManagementEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Times management:");
                Console.WriteLine("[G]et all Times");
                Console.WriteLine("[V]iew Time");
                Console.WriteLine("[A]dd Time");
                Console.WriteLine("[E]dit Time");
                Console.WriteLine("[D]elete Time");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return TimeManagementEnum.Select;
                        }
                    case "V":
                        {
                            return TimeManagementEnum.View;
                        }
                    case "A":
                        {
                            return TimeManagementEnum.Insert;
                        }
                    case "E":
                        {
                            return TimeManagementEnum.Update;
                        }
                    case "D":
                        {
                            return TimeManagementEnum.Delete;
                        }
                    case "X":
                        {
                            return TimeManagementEnum.Exit;
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

        private void GetAll()
        {
            Console.Clear();

            TimesRepository timesRepository = new TimesRepository("Times.txt");
            List<Time> times = timesRepository.GetAll();
            Console.WriteLine("List all time logs:");

            foreach (Time time in times)
            {
                Console.WriteLine("ID: " + time.Id);
                Console.WriteLine("Task ID: " + time.TaskId);
                Console.WriteLine("Time taken: " + time.TimeTaken);
                Console.WriteLine("User assigned: " + time.UserReported);
                Console.WriteLine("Created by: " + time.Creator);
                Console.WriteLine($"Created on : {time.DateTaken.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}");

                Console.WriteLine("########################################");
            }

            Console.ReadKey(true);
        }

        private void View()
        {
            Console.Clear();

            Console.Write("Time ID: ");
            int timeId = Convert.ToInt32(Console.ReadLine());

            TimesRepository timesRepository = new TimesRepository("times.txt");

            Time time = timesRepository.GetById(timeId);
            if (time == null)
            {
                Console.Clear();
                Console.WriteLine("Time not found.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("ID: " + time.Id);
            Console.WriteLine("Task ID: " + time.TaskId);
            Console.WriteLine("Time taken: " + time.TimeTaken);
            Console.WriteLine("User assigned: " + time.UserReported);
            Console.WriteLine("Created by: " + time.Creator);
            Console.WriteLine($"Created on : {time.DateTaken.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}");

            Console.ReadKey(true);
        }

        private void Add()
        {
            Console.Clear();

            UsersRepository usersRepository = new UsersRepository("users.txt");
            List<User> users = usersRepository.GetAll();

            Time time = new Time();

            Console.WriteLine("Add new Time: ");

            Console.Write("Time: ");
            time.TimeTaken = int.Parse(Console.ReadLine());

            Console.Write("Task ID: ");
            time.TaskId = int.Parse(Console.ReadLine());

            time.Creator = AuthenticationService.LoggedUser.Username;

            Console.Write("User reported: ");
            time.UserReported = Console.ReadLine();

            time.DateTaken = DateTime.Now.Date;

            if (time.TaskId > 0)
            {
                TimesRepository timesRepository = new TimesRepository("times.txt");
                timesRepository.Save(time);
                Console.WriteLine("Time saved successfully.");
            }
            else
            {
                Console.WriteLine("Not valid user or task id.");
            }
            Console.ReadKey(true);

        }

        private void Update()
        {
            Console.Clear();

            Console.Write("Time ID: ");
            int timeId = Convert.ToInt32(Console.ReadLine());

            TimesRepository timesRepository = new TimesRepository("times.txt");
            Time time = timesRepository.GetById(timeId);

            if (time == null)
            {
                Console.Clear();
                Console.WriteLine("Time not found.");
                Console.ReadKey(true);
                return;
            }
            else if (AuthenticationService.LoggedUser.Username == time.Creator)
            {
                Console.WriteLine("Editing time:");
                Console.WriteLine("ID: " + time.Id);
                Console.WriteLine("User assigned: " + time.UserReported);

                Console.WriteLine("Time: " + time.TimeTaken);
                Console.Write("New time: ");
                var timeTaken = Console.ReadLine();

                time.Creator = AuthenticationService.LoggedUser.Username;
                time.DateTaken = DateTime.Now.Date;

                if (!string.IsNullOrEmpty(timeTaken))
                    time.TimeTaken = int.Parse(timeTaken);

                timesRepository.Save(time);
                Console.WriteLine("Time saved successfully.");               
            }
            else
            {
                Console.WriteLine("Not allowed to edit time.");
            }
            Console.ReadKey(true);
        }

        private void Delete()
        {
            TimesRepository timesRepository = new TimesRepository("times.txt");

            Console.Clear();

            Console.WriteLine("Delete TimeLog:");
            Console.Write("Time Id: ");
            int timeId = Convert.ToInt32(Console.ReadLine());

            var user = timesRepository.GetById(timeId);
            Time time = timesRepository.GetById(timeId);

            if (time == null)
            {
                Console.WriteLine("Time not found!");
            }
            else if (AuthenticationService.LoggedUser.Username == user.Creator)
            {
                timesRepository.Delete(time);
                Console.WriteLine("Time deleted successfully.");

            }
            else
            {
                Console.WriteLine("Not allowed to delete time.");
            }

            Console.ReadKey(true);
        }
    }
}