using System;

namespace TaskManager.View
{
    public class AdminView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Administration View:");
                    Console.WriteLine("[U]ser Management");
                    Console.WriteLine("[T]ask Management");
                    Console.WriteLine("E[x]it");

                    string input = Console.ReadLine();
                    switch (input.ToUpper())
                    {
                        case "U":
                            {
                                UserManagementView view = new UserManagementView();
                                view.Show();
                                break;
                            }
                        case "T":
                            {
                                TaskManagementView view = new TaskManagementView();
                                view.Show();
                                break;
                            }
                        case "X":
                            {
                                return;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid choice");
                                Console.ReadKey(true);
                                break;
                            }
                    }
                }
            }
        }
    }
}
