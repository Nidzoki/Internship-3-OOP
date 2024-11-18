using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_manager_app
{
    public class Program
    {
        static void Main()
        {
            var projectsDictionary = new Dictionary<Project, List<Task>>();
            var appInterface = new AppInterface();
            var quit = false;

            while (!quit)
            {
                if(appInterface.MainMenu(ref projectsDictionary) == "Exit")
                    quit = true;
            }

            Console.Clear();
            Console.WriteLine("\n Exiting project manager app...\n\n Press any key to continue...");
            Console.ReadKey();
        }
    }
}
