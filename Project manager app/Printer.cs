using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Project_manager_app
{
    public static class Printer
    {
        public static void PrintMainMenu(bool error)
        {
            Console.Clear();
            Console.WriteLine("\n MAIN MENU\n\n" +
             (error ? " Error: The input is invalid!\n\n" : "") +
            " 1. Display all projects with their associated tasks\n" +
            " 2. Add a new project\n" +
            " 3. Delete a project\n" +
            " 4. Show all tasks with a deadline in the next 7 days\n" +
            " 5. Display projects filtered by status\n" +
            " 6. Manage individual projects\n" +
            " 7. Manage individual tasks\n" +
            " 8. Bonus functionality\n" +
            " 9. Exit");
            Console.Write("\n Your input: ");
        }

        public static void PrintProjectsAndTasks(Dictionary<Project, List<Task>> projects)
        {
            foreach (var project in projects)
            {
                Console.WriteLine($" Project name: {project.Key.Name}" +
                    $"\n Description: {project.Key.Description}" +
                    $"\n Start date: {project.Key.StartDate:dd-mm-yyyy}" +
                    $"\n End date: {project.Key.EndDate:dd-mm-yyyy}" +
                    $"\n Status: {project.Key.Status}" +
                    $"\n Tasks:");

                if (project.Value.Count == 0){
                    Console.WriteLine("\t This project has no tasks\n");
                    continue;
                }

                foreach (var task in project.Value)
                {
                    Console.WriteLine($"\n\tTask name: {task.Name}" +
                        $"\n\tDescription: {task.Description}" +
                        $"\n\tDeadline: {task.Deadline}" +
                        $"\n\tStatus: {task.Status}" +
                        $"\n\tExpected duration time: {task.DurationInMinutes}" +
                        $"\n\tParent project: {task.ParentProject}\n");
                }
            }
        }

        public static void PrintProjects(Dictionary<Project, List<Task>> projects)
        {
            foreach (var project in projects)
            {
                Console.WriteLine($" Project name: {project.Key.Name}" +
                    $"\n Description: {project.Key.Description}" +
                    $"\n Start date: {project.Key.StartDate:dd-mm-yyyy}" +
                    $"\n End date: {project.Key.EndDate:dd-mm-yyyy}" +
                    $"\n Status: {project.Key.Status}");
            }
        }

        public static void PrintTask(Task task)
        {
            Console.WriteLine($" Task: {task.Name} - Description: {Printer.ShortDescription(task.Description)} - Priority: {task.Priority} - Deadline: {task.Deadline} - Duration(min): {task.DurationInMinutes} - Parent project: {task.ParentProject}\n");
        }

        public static string ShortDescription(string description)
        {
            return description.Length > 29 ? description.Substring(0, 30) + "..." : description;
        }

        public static void PrintProject(KeyValuePair<Project, List<Task>> project)
        {
            Console.WriteLine($" Project name: {project.Key.Name}" +
                    $"\n Description: {project.Key.Description}" +
                    $"\n Start date: {project.Key.StartDate:dd-mm-yyyy}" +
                    $"\n End date: {project.Key.EndDate:dd-mm-yyyy}" +
                    $"\n Status: {project.Key.Status}");
        }

        public static void PrintProjectManagementOptions() 
        {
            Console.Clear();
            Console.WriteLine("\n MANAGE SPECIFIC PROJECT\n\n" 
                + " 1. Display all tasks within a selected project\n" 
                + " 2. Show details of the selected project\n" 
                + " 3. Edit the status of the project\n" 
                + " 4. Add a task within the project\n"
                + " 5. Delete a task from the project\n"
                + " 6. Show the total expected time needed for all active tasks in the project\n\n");

            Console.Write(" Your input: ");
        }

        public static void PrintTasksInsideProjects(Dictionary<Project, List<Task>> projects, Project project)
        {
            Console.Clear();
            Console.WriteLine("\n PRINT TASKS INSIDE PROJECT\n");
            if (projects[project].Count == 0)
                Console.WriteLine(" There is no tasks to display.\n\n Press any key to continue...");
            else
            {
                Console.WriteLine(" Tasks in {1}:\n", project.Name);
                foreach (var task in projects[project])
                {
                    Console.WriteLine($"\tTask name: {task.Name}");
                }
                Console.WriteLine("\n Press any key to continue...");
            }
            Console.ReadKey();
        }

        public static void PrintTaskManagementOptions()
        {
            Console.Clear();
            Console.WriteLine("\n MANAGE SPPECIFIC TASK \n\n 1. Display task details\n 2. Edit task status\n");
        }

        public static void PrintBonusMenu()
        {
            Console.Clear();
            Console.WriteLine("\n BONUS SUBMENU\n\n 1. Display tasks sorted by duration\n 2. Sort tasks by priority\n");
        }
    }
}
