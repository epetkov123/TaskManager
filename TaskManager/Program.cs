using System;
using TaskManager.Service;
using TaskManager.View;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginView loginview = new LoginView();
            loginview.Show();

            if (AuthenticationService.LoggedUser.IsAdmin)
            {
                AdminView adminview = new AdminView();
                adminview.Show();
            }
            else
            {
                TaskManagementView taskView = new TaskManagementView();
                taskView.Show();
            }
        }
    }
}
