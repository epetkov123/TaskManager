using System;
using TaskManager.Entity;
using TaskManager.Repository;

namespace TaskManager.Service
{
    public static class AuthenticationService
    {
        public static User LoggedUser { get; private set; }

        public static void AuthenticateUser(string username, string password)
        {
            UsersRepository userRepo = new UsersRepository("users.txt");
            LoggedUser = userRepo.GetByUsernameAndPassword(username, password);
        }
    }
}
