using System;
using TaskManager.Tools;
using TaskManager.Entity;
using TaskManager.Repository;
using System.Collections.Generic;

namespace TaskManager.View
{
    public class UserManagementView:BaseView<User>
    {
        public override void RenderToConsole(User user)
        {
            Console.WriteLine("ID: " + user.Id);
            Console.WriteLine("Username: " + user.Username);
            Console.WriteLine("Password: " + user.Password);
            Console.WriteLine("First Name: " + user.FirstName);
            Console.WriteLine("Last Name: " + user.LastName);
            Console.WriteLine("Is Admin: " + user.IsAdmin);

            Console.WriteLine("########################################");
        }

        public override User ReadFromConsole(User user)
        {
            Console.Write("Username: ");
            user.Username = Console.ReadLine();

            Console.Write("Password: ");
            user.Password = Console.ReadLine();

            Console.Write("First Name: ");
            user.FirstName = Console.ReadLine();

            Console.Write("Last Name: ");
            user.LastName = Console.ReadLine();

            Console.Write("Is Admin (True/False): ");
            user.IsAdmin = Convert.ToBoolean(Console.ReadLine());

            return user;
        }

        public override BaseRepository<User> CreateRepo()
        {
            return new UsersRepository("users.txt");
        }

    }
}
