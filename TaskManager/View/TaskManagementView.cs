using System;
using TaskManager.Tools;
using TaskManager.Repository;
using TaskManager.Entity;
using System.Collections.Generic;
using System.Globalization;
using TaskManager.Service;

namespace TaskManager.View
{
    public class TaskManagementView
    {
        public void Show()
        {
            while (true)
            {
                TaskManagementEnum choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case TaskManagementEnum.Select:
                            {
                                GetAll();
                                break;
                            }
                        case TaskManagementEnum.View:
                            {
                                View();
                                break;
                            }
                        case TaskManagementEnum.Insert:
                            {
                                Add();
                                break;
                            }
                        case TaskManagementEnum.Update:
                            {
                                Update();
                                break;
                            }
                        case TaskManagementEnum.Delete:
                            {
                                Delete();
                                break;
                            }
                        case TaskManagementEnum.CommentManagement:
                            {
                                CommentManagementView view = new CommentManagementView();
                                view.Show();
                                break;
                            }
                        case TaskManagementEnum.TimeManagement:
                            {
                                TimeManagementView view = new TimeManagementView();
                                view.Show();
                                break;
                            }
                        case TaskManagementEnum.Exit:
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

        private TaskManagementEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Tasks management:");
                Console.WriteLine("[G]et all Tasks");
                Console.WriteLine("[V]iew Task");
                Console.WriteLine("[A]dd Task");
                Console.WriteLine("[E]dit Task");
                Console.WriteLine("[D]elete Task");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return TaskManagementEnum.Select;
                        }
                    case "V":
                        {
                            return TaskManagementEnum.View;
                        }
                    case "A":
                        {
                            return TaskManagementEnum.Insert;
                        }
                    case "E":
                        {
                            return TaskManagementEnum.Update;
                        }
                    case "D":
                        {
                            return TaskManagementEnum.Delete;
                        }
                    case "X":
                        {
                            return TaskManagementEnum.Exit;
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

            TasksRepository tasksRepository = new TasksRepository("tasks.txt");
            List<Task> tasks = tasksRepository.GetAll();

            foreach (Task task in tasks)
            {
                Console.WriteLine("ID: " + task.Id);
                Console.WriteLine("Title: " + task.Title);
                Console.WriteLine("Description: " + task.Description);
                Console.WriteLine("To be done in: " + task.TimeDone);
                Console.WriteLine("Created by: " + task.Creator);
                Console.WriteLine("To be done by: " + task.UserTask);
                Console.WriteLine($"Created on : {task.DateCreated.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}");
                Console.WriteLine($"Last modified : {task.LastModified.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}");
                Console.WriteLine("Is it done: " + task.IsDone);

                Console.WriteLine("########################################");
            }

            Console.ReadKey(true);
        }

        private void View()
        {
            Console.Clear();

            TasksRepository tasksRepository = new TasksRepository("tasks.txt");
            List<Task> tasks = tasksRepository.GetAll();

            foreach (Task eachTask in tasks)
            {
                Console.WriteLine("ID: " + eachTask.Id);
                Console.WriteLine("Title: " + eachTask.Title);

                Console.WriteLine("########################################");
            }

            Console.Write("Task ID: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            Task task = tasksRepository.GetById(taskId);
            if (task == null)
            {
                Console.Clear();
                Console.WriteLine("Task not found.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("ID: " + task.Id);
            Console.WriteLine("Title: " + task.Title);
            Console.WriteLine("Description: " + task.Description);
            Console.WriteLine("To be done in: " + task.TimeDone);
            Console.WriteLine("Created by: " + task.Creator);
            Console.WriteLine("To be done by: " + task.UserTask);
            Console.WriteLine($"Created on : {task.DateCreated.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Last modified : {task.LastModified.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}");
            Console.WriteLine("Is it done: " + task.IsDone);

            while (true)
            {
                Console.WriteLine("[C]omment Management");
                Console.WriteLine("[T]ime Management");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "C":
                        {
                            CommentManagementView view = new CommentManagementView();
                            view.Show();
                            break;
                        }
                    case "T":
                        {
                            TimeManagementView view = new TimeManagementView();
                            view.Show();
                            break;
                        }
                    case "X":
                        {
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid choice.");
                            Console.ReadKey(true);
                            break;
                        }
                }

                Console.ReadKey(true);
            }
        }

        private void Add()
        {
            Console.Clear();

            Task task = new Task();

            Console.WriteLine("Add new task: ");

            Console.Write("Title: ");
            task.Title = Console.ReadLine();

            Console.Write("Description: ");
            task.Description = Console.ReadLine();

            Console.Write("To be done in: ");
            task.TimeDone = int.Parse(Console.ReadLine());

            task.Creator = AuthenticationService.LoggedUser.Username;

            Console.Write("To be done by: ");
            task.UserTask = Console.ReadLine();

            task.DateCreated = DateTime.Now.Date;
            task.LastModified = DateTime.Now.Date;

            Console.Write("Is it done: ");
            task.IsDone = false;

            UsersRepository usersRepository = new UsersRepository("users.txt");
            var usersData = usersRepository.GetAll();
            var checkCreator = false;
            var checkUserTask = false;

            foreach(var userData in usersData)
            {
                if(task.Creator == userData.Username || task.Creator == userData.FirstName || task.Creator == userData.LastName)
                {
                    checkCreator = true;
                }

                if (task.UserTask == userData.Username || task.UserTask == userData.FirstName || task.UserTask == userData.LastName)
                {
                    checkUserTask = true;
                }
            }

            if (checkCreator && checkUserTask)
            {
                TasksRepository tasksRepository = new TasksRepository("tasks.txt");
                tasksRepository.Save(task);

                Console.WriteLine("Tasks saved successfully.");
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("Task cannot be saved because users do not exist.");
                Console.ReadKey(true);
            }
        }

        private void Update()
        {
            Console.Clear();

            Console.Write("Task ID: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            TasksRepository tasksRepository = new TasksRepository("tasks.txt");
            Task task = tasksRepository.GetById(taskId);

            if (task == null)
            {
                Console.Clear();
                Console.WriteLine("Task not found.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("Editing task (" + task.Title + ")");
            Console.WriteLine("ID: " + task.Id);

            Console.WriteLine("Task title: " + task.Title);
            //Console.Write("New task title: ");
            //string title = Console.ReadLine();

            Console.WriteLine("Task description: " + task.Description);
            //Console.Write("New task description: ");
            //string description = Console.ReadLine();

            /*
            Console.WriteLine("Task time to be done: " + task.TimeDone);
            Console.Write("New task time to be done: ");
            var timeDone = Console.ReadLine();

            Console.WriteLine("Task creator: " + task.Creator);
            Console.Write("New task creator: ");
            task.Creator = AuthenticationService.LoggedUser.Username;
            
            Console.WriteLine("Assigned user: " + task.UserTask);
            Console.Write("New assigned user: ");
            string userTask = Console.ReadLine();
            */

            task.LastModified = DateTime.Now.Date;

            Console.WriteLine("Task is done?: " + task.IsDone);
            Console.Write("New task status: ");
            var isDone = Console.ReadLine();

            if (!string.IsNullOrEmpty(isDone))
                task.IsDone = bool.Parse(isDone);

            tasksRepository.Save(task);

            Console.WriteLine("Task saved successfully.");
            Console.ReadKey(true);
        }

        private void Delete()
        {
            TasksRepository tasksRepository = new TasksRepository("tasks.txt");

            Console.Clear();

            Console.WriteLine("Delete task:");
            Console.Write("Task Id: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            Task task = tasksRepository.GetById(taskId);
            if (task == null)
            {
                Console.WriteLine("Task not found!");
            }
            else
            {
                tasksRepository.Delete(task);
                Console.WriteLine("Task deleted successfully.");
            }
            Console.ReadKey(true);
        }
    }
}
