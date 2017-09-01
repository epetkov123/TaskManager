using System;
using TaskManager.Tools;
using TaskManager.Repository;
using TaskManager.Entity;
using TaskManager.Service;

namespace TaskManager.View
{
    public class CommentManagementView:BaseView<Comment>
    {

        public override BaseRepository<Comment> CreateRepo()
        {
            return new CommentsRepository("comments.txt");
        }

        public override void RenderToConsole(Comment comment)
        {
            Console.WriteLine("ID: " + comment.Id);
            Console.WriteLine("Comment: " + comment.CommentDescription);
            Console.WriteLine("Task ID: " + comment.TaskId);
            Console.WriteLine("Creator: " + comment.Creator);

            Console.WriteLine("########################################");
        }

        public override Comment ReadFromConsole(Comment comment)
        {
            Console.WriteLine("Add new Comment: ");

            Console.Write("Comment: ");
            comment.CommentDescription = Console.ReadLine();

            Console.Write("Task ID: ");
            comment.TaskId = int.Parse(Console.ReadLine());

            comment.Creator = AuthenticationService.LoggedUser.Username;

            return comment;
        }

        /*
        public void Show()
        {
            while (true)
            {
                CommentManagementEnum choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case CommentManagementEnum.View:
                            {
                                View();
                                break;
                            }
                        case CommentManagementEnum.Insert:
                            {
                                Add();
                                break;
                            }
                        case CommentManagementEnum.Update:
                            {
                                Update();
                                break;
                            }
                        case CommentManagementEnum.Delete:
                            {
                                Delete();
                                break;
                            }
                        case CommentManagementEnum.Exit:
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

        private CommentManagementEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Comments management:");
                Console.WriteLine("[V]iew Comment");
                Console.WriteLine("[A]dd Comment");
                Console.WriteLine("[E]dit Comment");
                Console.WriteLine("[D]elete Comment");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "V":
                        {
                            return CommentManagementEnum.View;
                        }
                    case "A":
                        {
                            return CommentManagementEnum.Insert;
                        }
                    case "E":
                        {
                            return CommentManagementEnum.Update;
                        }
                    case "D":
                        {
                            return CommentManagementEnum.Delete;
                        }
                    case "X":
                        {
                            return CommentManagementEnum.Exit;
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

        private void View()
        {
            Console.Clear();

            Console.Write("Comment ID: ");
            int commentId = Convert.ToInt32(Console.ReadLine());

            CommentsRepository commentsRepository = new CommentsRepository("comments.txt");

            Comment comment = commentsRepository.GetById(commentId);
            if (comment == null)
            {
                Console.Clear();
                Console.WriteLine("Comment not found.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("ID: " + comment.Id);
            Console.WriteLine("Comment: " + comment.CommentDescription);
            Console.WriteLine("Task ID: " + comment.TaskId);
            Console.WriteLine("Creator: " + comment.Creator);

            Console.ReadKey(true);
        }

        private void Add()
        {
            Console.Clear();

            Comment comment = new Comment();

            Console.WriteLine("Add new Comment: ");

            Console.Write("Comment: ");
            comment.CommentDescription = Console.ReadLine();

            Console.Write("Task ID: ");
            comment.TaskId = int.Parse(Console.ReadLine());

            comment.Creator = AuthenticationService.LoggedUser.Username;

            TasksRepository tasksRepository = new TasksRepository("tasks.txt");
            if (tasksRepository.GetById(comment.TaskId) != null)
            {
                CommentsRepository commentsRepository = new CommentsRepository("comments.txt");
                commentsRepository.Save(comment);
                Console.WriteLine("Comment saved successfully.");
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("Task doesn't exist.");
                Console.ReadKey(true);
            }
            
        }

        private void Update()
        {
            Console.Clear();

            Console.Write("Comment ID: ");
            int commentId = Convert.ToInt32(Console.ReadLine());

            CommentsRepository commentsRepository = new CommentsRepository("comments.txt");
            Comment comment = commentsRepository.GetById(commentId);

            if (comment == null)
            {
                Console.Clear();
                Console.WriteLine("Comment not found.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("Editing Comment (" + comment.CommentDescription + ")");
            Console.WriteLine("ID: " + comment.Id);

            Console.WriteLine("Comment: " + comment.CommentDescription);
            Console.Write("New comment: ");
            string title = Console.ReadLine();

            string creator = AuthenticationService.LoggedUser.Username;

            if (!string.IsNullOrEmpty(title))
                comment.CommentDescription = title;

            commentsRepository.Save(comment);

            Console.WriteLine("Comment saved successfully.");
            Console.ReadKey(true);
        }

        private void Delete()
        {
            CommentsRepository commentsRepository = new CommentsRepository("comments.txt");

            Console.Clear();

            Console.WriteLine("Delete Comment:");
            Console.Write("Comment ID: ");
            int commentId = Convert.ToInt32(Console.ReadLine());

            Comment comment = commentsRepository.GetById(commentId);

            if (comment == null)
            {
                Console.WriteLine("Comment not found!");
            }
            else
            {
                commentsRepository.Delete(comment);
                Console.WriteLine("Comment deleted successfully.");
            }
            Console.ReadKey(true);
        }
        */
    }

}
