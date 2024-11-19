using System;
using System.Collections.Generic;
using System.Linq;
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
            " 8. Exit");
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
    }
}
