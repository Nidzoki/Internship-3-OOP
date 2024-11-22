using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_manager_app
{
    public static class GetUserInput
    {
        public static Project GetNewProjectData()
        {
            Console.Clear();
            Console.WriteLine("\n CREATE NEW PROJECT\n\n");

            string projectName;
            do
            {
                Console.Write(" Project name: ");
                projectName = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(projectName))
                    Console.WriteLine("Project name cannot be empty. Please enter a valid name.");
            } while (string.IsNullOrEmpty(projectName));

            Console.Write("\n\n Project description: ");
            var projectDescription = Console.ReadLine().Trim();

            DateTime startDate = DateTime.Now;
            var askForStartDate = true;
            while (askForStartDate)
            {
                Console.Write("\n\n Start date in dd-MM-yyyy format: ");
                var dateInput = Console.ReadLine().Trim();

                if (DateTime.TryParse(dateInput, out startDate))
                    askForStartDate = false;
                else
                    Console.WriteLine("Invalid date format. Please enter the date in the format dd-MM-yyyy.");
            }

            DateTime endDate = DateTime.Now;
            var askForEndDate = true;
            while (askForEndDate)
            {
                Console.Write("\n\n End date in dd-MM-yyyy format: ");
                var dateInput = Console.ReadLine().Trim();

                if (DateTime.TryParse(dateInput, out endDate))
                {
                    if (endDate >= startDate)
                        askForEndDate = false;
                    else
                        Console.WriteLine("End date cannot be before the start date. Please enter a valid end date.");
                }
                else
                    Console.WriteLine("Invalid date format. Please enter the date in the format dd-MM-yyyy.");
            }
            return new Project(projectName, projectDescription, startDate, endDate);
        }

        public static Project GetProjectToDelete(Dictionary<Project, List<Task>> projects)
        {
            Console.Clear();
            Console.WriteLine("\n DELETE PROJECT\n\n ");
            Printer.PrintProjects(projects);

            Project selectedProject = null;

            var askForName = true;
            while (askForName)
            {
                Console.Write("\n Input the project name or x to exit: ");
                var projectName = Console.ReadLine().Trim();

                if (projectName.ToLower() == "x")
                    return null;

                selectedProject = projects.Keys.FirstOrDefault(x => x.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase));

                if (selectedProject != null)
                    askForName = false;

                Console.WriteLine("Project name not found. Please try again.");
            }

            while (true)
            {
                Console.WriteLine($"\n Are you sure you want to delete the project named {selectedProject.Name} (y/n)?");
                Console.Write(" Your input: ");
                var confirmation = Console.ReadLine().Trim().ToLower();

                if (confirmation == "y")
                    return selectedProject;
                else if (confirmation == "n")
                    return null;

                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }

        public static ProjectStatus GetProjectStatus()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n CHOOSE PROJECT STATUS\n\n 1. Active\n 2. Standby\n 3. Finished");
                Console.Write(" Your input: ");

                switch (Console.ReadLine().Trim())
                {
                    case "1":
                        return ProjectStatus.Active;
                    case "2":
                        return ProjectStatus.Standby;
                    case "3":
                        return ProjectStatus.Finished;
                    default:
                        Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");
                        Console.WriteLine("Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static Project GetProjectToManage(Dictionary<Project, List<Task>> projects)
        {
            Console.Clear();
            Console.WriteLine("\n MANAGE SPECIFIC PROJECT\n\n ");
            Printer.PrintProjects(projects);

            Project selectedProject = null;
            var askForName = true;
            while (askForName)
            {
                Console.Write("\n Input the project name or x to exit: ");
                var projectName = Console.ReadLine().Trim();

                if (projectName.ToLower() == "x")
                    return null;

                selectedProject = projects.Keys.FirstOrDefault(x => x.Name == projectName);

                if (selectedProject != null)
                    askForName = false;
                else
                    Console.WriteLine("Project name not found. Please try again.");
            }

            while (true)
            {
                Console.WriteLine($"\n Are you sure you want to manage project {selectedProject.Name} (y/n)?");
                Console.Write(" Your input: ");
                var confirmation = Console.ReadLine().Trim().ToLower();

                if (confirmation == "y")
                {
                    return selectedProject;
                }
                else if (confirmation == "n")
                {
                    return null;
                }

                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }

        public static Task GetTaskData(string parentProject)
        {
            Console.Clear();
            Console.WriteLine("\n CREATE NEW TASK\n\n");

            string name;
            do
            {
                Console.Write(" Task name: ");
                name = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(name))
                    Console.WriteLine("Task name cannot be empty. Please enter a valid name.");

            } while (string.IsNullOrEmpty(name));

            Console.Write("\n\n Task description: ");
            var description = Console.ReadLine().Trim();

            var priority = GetUserInput.GetTaskPriority();

            DateTime date = DateTime.Now;
            var askForDate = true;
            while (askForDate)
            {
                Console.Write("\n\n Task deadline (format: dd-MM-yyyy): ");
                var dateInput = Console.ReadLine().Trim();

                if (DateTime.TryParse(dateInput, out date))
                    askForDate = false;
                else
                    Console.WriteLine("Invalid date format. Please enter the date in the format dd-MM-yyyy.");
            }

            int duration = 0;
            var askForDuration = true;
            while (askForDuration)
            {
                Console.Write("\n\n Task duration (in minutes): ");
                var durationInput = Console.ReadLine().Trim();

                if (int.TryParse(durationInput, out duration))
                    askForDuration = false;
                else
                    Console.WriteLine("Invalid duration format. Please enter the duration as an integer.");
            }
            return new Task(name, parentProject, date, duration, description, priority);
        }

        private static PriorityLevel GetTaskPriority()
        {
            Console.Clear();
            Console.WriteLine("\n CHOOSE TASK PRIORITY\n\n 1. High\n 2. Medium\n 3. Low (default)\n\n");
            Console.Write(" Your input: ");

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    return PriorityLevel.High;
                case "2":
                    return PriorityLevel.Medium;
                case "3":
                    return PriorityLevel.Low;
                default:
                    return PriorityLevel.Low;
            }
        }

        public static Task GetTaskToRemove(List<Task> tasks)
        {
            Console.Clear();
            Console.WriteLine("\n DELETE TASK\n\n");

            Console.WriteLine(" List of tasks in this project:\n");
            foreach (var task in tasks)
                Printer.PrintTask(task);

            string taskName = string.Empty;
            Task selectedTask = null;

            var askForName = true;
            while (askForName)
            {
                Console.Write(" Enter task name to delete it or x to exit: ");
                taskName = Console.ReadLine().Trim();

                if (taskName.ToLower() == "x")
                    return null;

                selectedTask = tasks.FirstOrDefault(t => t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase));

                if (selectedTask != null)
                    askForName = false;

                Console.WriteLine("Task name not found. Please try again.");
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n DELETE TASK\n\n Are you sure you want to delete task named {0}? (y/n)", taskName);
                Console.Write(" Your input: ");
                var confirmation = Console.ReadLine().Trim().ToLower();

                if (confirmation == "y")
                    return selectedTask;
                else if (confirmation == "n")
                    return null;

                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }

        public static Task GetTaskToManage(Dictionary<Project, List<Task>> projects)
        {
            Console.Clear();
            Console.WriteLine("\n MANAGE TASK\n\n Tasks: \n");

            var taskListsThatHaveTasks = projects.Values.Where(x => x.Count > 0).Count();

            if (taskListsThatHaveTasks == 0)
            {
                Console.WriteLine("\n There is no tasks to manage.\n\n Press any key to continue...");
                Console.ReadKey();
                return null;
            }

            foreach (var project in projects)
            {
                foreach (var task in project.Value)
                    Printer.PrintTask(task);
            }

            string taskName;
            Task selectedTask = null;
            var askForTaskName = true;
            while (askForTaskName)
            {
                Console.Write("\n Input the task name or x to exit: ");
                taskName = Console.ReadLine().Trim();

                if (taskName.ToLower() == "x")
                    return null;

                selectedTask = projects.Values
                    .SelectMany(taskList => taskList)
                    .FirstOrDefault(task => task.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase));

                if (selectedTask != null)
                    askForTaskName = false;

                Console.WriteLine("Task name not found. Please try again.");
            }

            while (true)
            {
                Console.WriteLine($"\n Are you sure you want to manage task {selectedTask.Name} (y/n)?");
                Console.Write(" Your input: ");
                var confirmation = Console.ReadLine().Trim().ToLower();

                if (confirmation == "y")
                    return selectedTask;
                else if (confirmation == "n")
                    return null;

                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }

        public static TaskStatus GetTaskStatus()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n CHOOSE TASK STATUS\n\n 1. Active\n 2. Postponed\n 3. Finished");
                Console.Write(" Your input: ");

                switch (Console.ReadLine().Trim())
                {
                    case "1":
                        return TaskStatus.Active;
                    case "2":
                        return TaskStatus.Postponed;
                    case "3":
                        return TaskStatus.Finished;
                    default:
                        Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");
                        Console.WriteLine("Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
