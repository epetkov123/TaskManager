using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Service;
using TaskManager.Tools;

namespace TaskManager.View
{
    public class CommentsTimesView
    {
        public void Show(int taskId)
        {
            while (true)
            {
                CustomManagementEnum choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case CustomManagementEnum.Comment:
                            {
                                CommentManagementView view = new CommentManagementView();
                                view.Show();
                                break;
                            }
                        case CustomManagementEnum.TimeLog:
                            {
                                TimeManagementView view = new TimeManagementView();
                                view.Show();
                                break;
                            }
                        case CustomManagementEnum.Update:
                            {
                                TaskManagementView view = new TaskManagementView();
                                view.ChangeStatus(taskId);
                                break;
                            }
                        case CustomManagementEnum.Exit:
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

        public CustomManagementEnum RenderMenu()
        {
            while (true)
            {
                //Console.Clear();
                Console.WriteLine("[C]omment Management");
                Console.WriteLine("[T]ime Management");
                Console.WriteLine("[U]pdate task status");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "C":
                        {
                            return CustomManagementEnum.Comment;
                        }
                    case "T":
                        {
                            return CustomManagementEnum.TimeLog;
                        }
                    case "U":
                        {
                            return CustomManagementEnum.Update;
                        }
                    case "X":
                        {
                            return CustomManagementEnum.Exit;
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
    }
}
