using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
