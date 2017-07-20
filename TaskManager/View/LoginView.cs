using System;
using TaskManager.Service;

namespace TaskManager.View
{
    public class LoginView
    {
        public void Show()
        {
            while(true)
            {
                Console.Clear();

                Console.Write("Username: ");
                var username = Console.ReadLine();

                Console.Write("Password: ");
                var password = Console.ReadLine();

                AuthenticationService.AuthenticateUser(username, password);

                if (AuthenticationService.LoggedUser != null)
                {
                    Console.WriteLine("Welcome " + AuthenticationService.LoggedUser.Username);
                    Console.ReadKey(true);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid username or password");
                    Console.ReadKey(true);
                }
            }
        }
    }
}
