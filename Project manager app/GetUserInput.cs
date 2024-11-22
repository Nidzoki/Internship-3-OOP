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
            Console.Write(" Project name: ");
            var projectName = Console.ReadLine().Trim();

            if (projectName.Length == 0)
                return null;

            Console.Write("\n\n Project description: ");
            var projectDescription = Console.ReadLine().Trim();

            try
            {
                Console.Write("\n\n Start date in dd-mm-yyyy format: ");
                var startDate = DateTime.Parse(Console.ReadLine());

                Console.Write("\n\n End date in dd-mm-yyyy format: ");
                var endDate = DateTime.Parse(Console.ReadLine());

                if (startDate > endDate)
                    return null;

                return new Project(projectName, projectDescription, startDate, endDate);
            }
            catch
            {
                return null;
            }
        }

        public static Project GetProjectToDelete(Dictionary<Project, List<Task>> projects)
        {
            Console.Clear();
            Console.WriteLine("\n DELETE PROJECT\n\n ");
            Printer.PrintProjects(projects);
            Console.Write("\n Input the project name or x to exit: ");

            var projectName = Console.ReadLine().Trim();

            if(projectName == "x" || !projects.Keys.Select(x => x.Name).Contains(projectName)) return null;

            Console.WriteLine("\n Are you sure you want to delete project named {0} (y/n)?", projectName);
            Console.Write(" Your input: ");

            if (Console.ReadLine() == "y")
                return projects.Keys.ToList().Find(x => x.Name == projectName);
            return null;
        }
    
        public static ProjectStatus? GetProjectStatus()
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
                    return null;
            }
        }
    
        public static Project GetProjectToManage(Dictionary<Project, List<Task>> projects)
        {
            Console.Clear();
            Console.WriteLine("\n MANAGE SPECIFIC PROJECT\n\n ");
            Printer.PrintProjects(projects);
            Console.Write("\n Input the project name or x to exit: ");

            var projectName = Console.ReadLine().Trim();

            if (projectName == "x" || !projects.Keys.Select(x => x.Name).Contains(projectName)) return null;

            Console.WriteLine("\n Are you sure you want to manage project {0} (y/n)?", projectName);
            Console.Write(" Your input: ");

            if (Console.ReadLine() == "y")
                return projects.Keys.ToList().Find(x => x.Name == projectName);
            return null;
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
                {
                    Console.WriteLine("Task name cannot be empty. Please enter a valid name.");
                }
            } while (string.IsNullOrEmpty(name));

            Console.Write("\n\n Task description: ");
            var description = Console.ReadLine().Trim();

            var priority = GetUserInput.GetTaskPriority();

            DateTime date;
            do
            {
                Console.Write("\n\n Task deadline (format: dd-MM-yyyy): ");
                var dateInput = Console.ReadLine().Trim();

                if (!DateTime.TryParse(dateInput, out _))
                {
                    Console.WriteLine("Invalid date format. Please enter the date in the format dd-MM-yyyy.");
                }
            } while (!DateTime.TryParse(Console.ReadLine().Trim(), out date));

            int duration;
            do
            {
                Console.Write("\n\n Task duration (in minutes): ");
                var durationInput = Console.ReadLine().Trim();

                if (!int.TryParse(durationInput, out _))
                {
                    Console.WriteLine("Invalid duration format. Please enter the duration as an integer.");
                }
            } while (!int.TryParse(Console.ReadLine().Trim(), out duration));

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
            {
                Printer.PrintTask(task);
            }
            Console.Write(" Enter task name to delete it: ");
            var name = Console.ReadLine().Trim();

            if (name == string.Empty || !tasks.Select(x => x.Name).Contains(name))
                return null;

            Console.Clear();
            Console.WriteLine("\n DELETE TASK\n\n Are you sure you want to delete task named {0}? (y/n)", name);
            if (Console.ReadLine().Trim() != "y")
            {
                return null;
            }
            return tasks.Find(x => x.Name == name);
        }

        public static Task GetTaskToManage(Dictionary<Project, List<Task>> projects)
        {
            Console.Clear();
            Console.WriteLine("\n MANAGE TASK\n\n Tasks: \n");

            foreach (var project in projects) 
            {
                foreach (var task in project.Value)
                {
                    Printer.PrintTask(task);
                }
            }

            Console.Write("\n Input the task name or x to exit: ");

            var taskName = Console.ReadLine().Trim();

            if (taskName == "x" || !projects.Values
                .SelectMany(x => x).Select(x => x.Name)
                .ToList().Contains(taskName)) return null;

            Console.WriteLine("\n Are you sure you want to manage task {0} (y/n)?", taskName);
            Console.Write(" Your input: ");

            if (Console.ReadLine() == "y")
            {
                foreach(var taskList in projects.Values)
                {
                    var task = taskList.FirstOrDefault(x => x.Name == taskName);
                    if (task != null)
                        return task;
                }
            }
                
            return null;
        }

        public static TaskStatus? GetTaskStatus()
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
                    return null;
            }
        }
    }
}
